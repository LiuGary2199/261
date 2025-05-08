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
    private string excelPath = "Assets/Excel"; // Excel 文件所在目录
    private string saveScriptPath = "Assets/Script/Data"; // 保存生成脚本的目录
    private string saveAssetPath = "Assets/Resources/Data"; // 保存自定义Asset的目录


    [MenuItem("Tools/读取 Excel")]
    public static void OpenWindow()
    {
        ExcelReader window = GetWindow<ExcelReader>();
        window.titleContent = new GUIContent("Excel 读取器");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("选择 Excel 文件:");
        // 获取 "Assets/表格" 文件夹中所有的 Excel 文件
        excelFiles = Directory.GetFiles(excelPath, "*.xlsx");
        // 过滤文件名以 "~$" 开头的文件
        excelFiles = excelFiles.Where(filePath => !Path.GetFileName(filePath).StartsWith("~$")).ToArray();
        // 以按钮形式显示每个文件名
        foreach (string filePath in excelFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (GUILayout.Button(fileName))
                selectedExcelFile = filePath;
        }
        //选中了某个文件 显示对其操作的按钮
        if (!string.IsNullOrEmpty(selectedExcelFile))
        {
            GUILayout.Space(10);
            string fileName = Path.GetFileNameWithoutExtension(selectedExcelFile);
            GUILayout.Label($"对 {fileName} 表格进行操作:");
            if (GUILayout.Button("生成数据类代码"))
                CreatClass();
            if (GUILayout.Button("读取表格数据"))
                ReadData();
        }
    }

    void CreatClass() //创建表格对应的数据类  xxxInfo.cs  xxxInfoList.cs
    {
        // 存储表格数据
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();
        List<string> headers = new List<string>();
        List<string> types = new List<string>();
        List<string> descriptions = new List<string>();
        using (ExcelPackage package = new ExcelPackage(new FileInfo(selectedExcelFile)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault(); // 假设数据在第一个工作表中
            int rowCount = worksheet.Dimension.Rows;
            //去掉空白列右侧内容 
            int RealColumnCount = worksheet.Dimension.Columns; //实际所有列数
            int columnCount = 0; //有效列数
            for (int col = 1; col <= RealColumnCount; col++)
            {
                if (worksheet.Cells[1, col].Value != null)
                    columnCount++;
                else
                    break;
            }
            Debug.Log($"<color=yellow><b>数据读取范围(含表头) 行1-{rowCount}  列1-{columnCount}</b></color>");
            // 第一行参数名称
            for (int col = 1; col <= columnCount; col++)
                headers.Add(worksheet.Cells[1, col].Value.ToString());
            // 第二行数据类型
            for (int col = 1; col <= columnCount; col++)
                types.Add(worksheet.Cells[2, col].Value.ToString());
            // 第三行描述
            for (int col = 1; col <= columnCount; col++)
                descriptions.Add(worksheet.Cells[3, col].Value.ToString());
            // 数据行
            for (int row = 4; row <= rowCount; row++)
            {
                Dictionary<string, object> dataEntry = new Dictionary<string, object>();
                for (int col = 1; col <= columnCount; col++)
                {
                    string header = headers[col - 1];
                    object value = worksheet.Cells[row, col].Value;
                    dataEntry.Add(header, value);
                }
                if (dataEntry.Count > 0)  // 仅在该行有有效数据时才添加到列表中
                    dataList.Add(dataEntry);
            }
        }

        // 生成类代码
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

        Debug.Log($"<color=green><b>已生成类代码[{className}]和[{listClassName}]并保存至：{saveScriptPath}</b></color>");
    }
    string CreatClassCode(string className, string[] headers, string[] types, string[] descriptions)
    {
        string classCode = "//此表由 Excel 读取器自动生成 \nusing UnityEngine;\n"; // 添加命名空间
        classCode += "using System.Collections.Generic;\n\n"; // 添加 List 类型所在的命名空间
        classCode += "[System.Serializable]\n"; // 添加序列化标记
        classCode += "public class " + className + "\n{\n";
        for (int i = 0; i < headers.Length; i++)
            classCode += "\t//" + descriptions[i] + "\n\tpublic " + types[i] + " " + headers[i] + ";\n";
        classCode += "}\n";
        return classCode;
    }
    string CreatListClassCode(string className)
    {
        string classCode = "//此表由 Excel 读取器自动生成 \nusing UnityEngine;\n";
        classCode += "using System.Collections.Generic;\n\n";
        classCode += "[System.Serializable]\n";
        classCode += "public class " + className + "List : ScriptableObject \n{\n";
        classCode += "\tpublic List<" + className + "> Data = new List<" + className + ">();\n";
        classCode += "}\n";
        return classCode;
    }


    void ReadData() //读取表格数据并保存为自定义Asset文件 xxx.asset
    {
        // 获取类名
        string className = Path.GetFileNameWithoutExtension(selectedExcelFile) + "InfoList";
        // 创建 ScriptableObject自定义Asset文件
        Type objectType = Type.GetType(className + ", Assembly-CSharp");
        ScriptableObject dataList = CreateInstance(objectType);
        // 读取表格数据并赋值给 ScriptableObject
        dataList = ReadExcelData(className, selectedExcelFile, dataList);
        // 保存 ScriptableObject
        SaveAsset(Path.GetFileNameWithoutExtension(selectedExcelFile), dataList);
    }
    ScriptableObject ReadExcelData(string className, string excelFilePath, ScriptableObject dataList) //读取表格数据
    {
        using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFilePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            int rowCount = worksheet.Dimension.Rows;
            //去掉空白列右侧内容 
            int RealColumnCount = worksheet.Dimension.Columns; //实际所有列数
            int columnCount = 0; //有效列数
            for (int col = 1; col <= RealColumnCount; col++)
            {
                if (worksheet.Cells[1, col].Value != null)
                    columnCount++;
                else
                    break;
            }
            Debug.Log($"<color=yellow><b>数据读取范围(不含表头) 行4-{rowCount}  列1-{columnCount}</b></color>");
            // 获取数据列表字段
            Type ListdataType = Type.GetType(className + ", Assembly-CSharp");
            FieldInfo dataField = ListdataType.GetField("Data");
            Type dataType = dataField.FieldType.GetGenericArguments()[0];
            // 创建 List 实例
            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(dataType));
            for (int row = 4; row <= rowCount; row++)
            {
                // 创建数据实例
                var dataEntry = Activator.CreateInstance(dataType);
                // 为数据实例赋值
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
                            if (value is string) // 去掉字符串前后的空格
                                value = ((string)value).Trim();
                            field.SetValue(dataEntry, Convert.ChangeType(value, field.FieldType));
                        }
                    }
                }
                // 将数据实例添加到列表中
                MethodInfo addMethod = list.GetType().GetMethod("Add");
                addMethod.Invoke(list, new object[] { dataEntry });
            }
            // 将列表设置到 ScriptableObject 中
            dataField.SetValue(dataList, list);
            return dataList;
        }
    }
    void SaveAsset(string className, ScriptableObject dataList) //保存自定义Asset文件
    {
        string assetFilePath = saveAssetPath + "/" + className + ".asset";
        AssetDatabase.CreateAsset(dataList, assetFilePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"<color=green><b>已读取[{className}]数据并保存至：{saveAssetPath}</b></color>");
    }
}
