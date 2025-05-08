using UnityEngine;
using UnityEditor;
using OfficeOpenXml;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ExcelReader : EditorWindow
{
    private string[] excelFiles;
    private string selectedExcelFile;
    private string excelPath = "Assets/Excel"; // Excel �ļ�����Ŀ¼
    private string saveScriptPath = "Assets/Script/Data"; // �������ɽű���Ŀ¼
    private string saveAssetPath = "Assets/Resources/Data"; // �����Զ���Asset��Ŀ¼


    [MenuItem("Tools/��ȡ Excel")]
    public static void OpenWindow()
    {
        ExcelReader window = GetWindow<ExcelReader>();
        window.titleContent = new GUIContent("Excel ��ȡ��");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("ѡ�� Excel �ļ�:");
        // ��ȡ "Assets/���" �ļ��������е� Excel �ļ�
        excelFiles = Directory.GetFiles(excelPath, "*.xlsx");
        // �����ļ����� "~$" ��ͷ���ļ�
        excelFiles = excelFiles.Where(filePath => !Path.GetFileName(filePath).StartsWith("~$")).ToArray();
        // �԰�ť��ʽ��ʾÿ���ļ���
        foreach (string filePath in excelFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (GUILayout.Button(fileName))
                selectedExcelFile = filePath;
        }
        //ѡ����ĳ���ļ� ��ʾ��������İ�ť
        if (!string.IsNullOrEmpty(selectedExcelFile))
        {
            GUILayout.Space(10);
            string fileName = Path.GetFileNameWithoutExtension(selectedExcelFile);
            GUILayout.Label($"�� {fileName} �����в���:");
            if (GUILayout.Button("�������������"))
                CreatClass();
            if (GUILayout.Button("��ȡ�������"))
                ReadData();
        }
    }

    void CreatClass() //��������Ӧ��������  xxxInfo.cs  xxxInfoList.cs
    {
        // �洢�������
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
        List<string> headers = new List<string>();
        List<string> types = new List<string>();
        List<string> descriptions = new List<string>();
        using (ExcelPackage package = new ExcelPackage(new FileInfo(selectedExcelFile)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(); // ���������ڵ�һ����������
            int rowCount = worksheet.Dimension.Rows;
            //ȥ���հ����Ҳ����� 
            int RealColumnCount = worksheet.Dimension.Columns; //ʵ����������
            int columnCount = 0; //��Ч����
            for (int col = 1; col <= RealColumnCount; col++)
            {
                if (worksheet.Cells[1, col].Value != null)
                    columnCount++;
                else
                    break;
            }
            Debug.Log($"<color=yellow><b>���ݶ�ȡ��Χ(����ͷ) ��1-{rowCount}  ��1-{columnCount}</b></color>");
            // ��һ�в�������
            for (int col = 1; col <= columnCount; col++)
                headers.Add(worksheet.Cells[1, col].Value.ToString());
            // �ڶ�����������
            for (int col = 1; col <= columnCount; col++)
                types.Add(worksheet.Cells[2, col].Value.ToString());
            // ����������
            for (int col = 1; col <= columnCount; col++)
                descriptions.Add(worksheet.Cells[3, col].Value.ToString());
            // ������
            for (int row = 4; row <= rowCount; row++)
            {
                Dictionary<string, object> dataEntry = new Dictionary<string, object>();
                for (int col = 1; col <= columnCount; col++)
                {
                    string header = headers[col - 1];
                    object value = worksheet.Cells[row, col].Value;
                    dataEntry.Add(header, value);
                }
                if (dataEntry.Count > 0)  // ���ڸ�������Ч����ʱ����ӵ��б���
                    dataList.Add(dataEntry);
            }
        }

        // ���������
        string className = Path.GetFileNameWithoutExtension(selectedExcelFile) + "Info";
        string classCode = "";
        classCode = CreatClassCode(className, headers.ToArray(), types.ToArray(), descriptions.ToArray());
        string scriptFilePath = saveScriptPath + "/" + className + ".cs";
        File.WriteAllText(scriptFilePath, classCode);
        AssetDatabase.Refresh();

        string listClassName = className + "List";
        classCode = CreatListClassCode(className);
        string listScriptFilePath = saveScriptPath + "/" + listClassName + ".cs";
        File.WriteAllText(listScriptFilePath, classCode);
        AssetDatabase.Refresh();

        Debug.Log($"<color=green><b>�����������[{className}]��[{listClassName}]����������{saveScriptPath}</b></color>");
    }
    string CreatClassCode(string className, string[] headers, string[] types, string[] descriptions)
    {
        string classCode = "//�˱��� Excel ��ȡ���Զ����� \nusing UnityEngine;\n"; // ��������ռ�
        classCode += "using System.Collections.Generic;\n\n"; // ��� List �������ڵ������ռ�
        classCode += "[System.Serializable]\n"; // ������л����
        classCode += "public class " + className + "\n{\n";
        for (int i = 0; i < headers.Length; i++)
            classCode += "\t//" + descriptions[i] + "\n\tpublic " + types[i] + " " + headers[i] + ";\n";
        classCode += "}\n";
        return classCode;
    }
    string CreatListClassCode(string className)
    {
        string classCode = "//�˱��� Excel ��ȡ���Զ����� \nusing UnityEngine;\n";
        classCode += "using System.Collections.Generic;\n\n";
        classCode += "[System.Serializable]\n";
        classCode += "public class " + className + "List : ScriptableObject \n{\n";
        classCode += "\tpublic List<" + className + "> Data = new List<" + className + ">();\n";
        classCode += "}\n";
        return classCode;
    }


    void ReadData() //��ȡ������ݲ�����Ϊ�Զ���Asset�ļ� xxx.asset
    {
        // ��ȡ����
        string className = Path.GetFileNameWithoutExtension(selectedExcelFile) + "InfoList";
        // ���� ScriptableObject�Զ���Asset�ļ�
        Type objectType = Type.GetType(className + ", Assembly-CSharp");
        ScriptableObject dataList = CreateInstance(objectType);
        // ��ȡ������ݲ���ֵ�� ScriptableObject
        dataList = ReadExcelData(className, selectedExcelFile, dataList);
        // ���� ScriptableObject
        SaveAsset(Path.GetFileNameWithoutExtension(selectedExcelFile), dataList);
    }
    ScriptableObject ReadExcelData(string className, string excelFilePath, ScriptableObject dataList) //��ȡ�������
    {
        using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            int rowCount = worksheet.Dimension.Rows;
            //ȥ���հ����Ҳ����� 
            int RealColumnCount = worksheet.Dimension.Columns; //ʵ����������
            int columnCount = 0; //��Ч����
            for (int col = 1; col <= RealColumnCount; col++)
            {
                if (worksheet.Cells[1, col].Value != null)
                    columnCount++;
                else
                    break;
            }
            Debug.Log($"<color=yellow><b>���ݶ�ȡ��Χ(������ͷ) ��4-{rowCount}  ��1-{columnCount}</b></color>");
            // ��ȡ�����б��ֶ�
            Type ListdataType = Type.GetType(className + ", Assembly-CSharp");
            FieldInfo dataField = ListdataType.GetField("Data");
            Type dataType = dataField.FieldType.GetGenericArguments()[0];
            // ���� List ʵ��
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(dataType));
            for (int row = 4; row <= rowCount; row++)
            {
                // ��������ʵ��
                var dataEntry = Activator.CreateInstance(dataType);
                // Ϊ����ʵ����ֵ
                for (int col = 1; col <= columnCount; col++)
                {
                    string fieldName = worksheet.Cells[1, col].Value.ToString();
                    FieldInfo field = dataType.GetField(fieldName);
                    if (field != null)
                    {
                        object value = worksheet.Cells[row, col].Value;
                        if (value != null)
                        {
                            //Debug.Log(fieldName + ": " + value.ToString());
                            if (value is string) // ȥ���ַ���ǰ��Ŀո�
                                value = ((string)value).Trim();
                            field.SetValue(dataEntry, Convert.ChangeType(value, field.FieldType));
                        }
                    }
                }
                // ������ʵ����ӵ��б���
                MethodInfo addMethod = list.GetType().GetMethod("Add");
                addMethod.Invoke(list, new object[] { dataEntry });
            }
            // ���б����õ� ScriptableObject ��
            dataField.SetValue(dataList, list);
            return dataList;
        }
    }
    void SaveAsset(string className, ScriptableObject dataList) //�����Զ���Asset�ļ�
    {
        string assetFilePath = saveAssetPath + "/" + className + ".asset";
        AssetDatabase.CreateAsset(dataList, assetFilePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"<color=green><b>�Ѷ�ȡ[{className}]���ݲ���������{saveAssetPath}</b></color>");
    }
}
