using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OfficeOpenXml;
using UnityEditor;
using System.IO;

/**
 * �ϻ����������������̳��� Editor �࣬���������ϻ���������ݲ����浽 Excel �ļ�
 */

public class SlotMachineDataGenerator : Editor
{
    /**
     * �˵����˵���ĵ���¼������������������ϻ�������
     */

    [MenuItem("Tools/�����ϻ�������")]
    private static void GenerateSlotMachineData()
    {
        int[] numbers = { 1, 2, 3 };  // ������������

        using (ExcelPackage package = new ExcelPackage())  // ���� Excel ��
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Slot Combinations");  // ��ӹ�����

            // ���ñ�ͷ��Ϣ
            worksheet.Cells[1, 1].Value = "Order";
            worksheet.Cells[1, 2].Value = "ItemCount";
            worksheet.Cells[1, 3].Value = "LineCount";

            worksheet.Cells[2, 1].Value = "string";
            worksheet.Cells[2, 2].Value = "int";
            worksheet.Cells[2, 3].Value = "int";

            worksheet.Cells[3, 1].Value = "����˳��";
            worksheet.Cells[3, 2].Value = "ͼ������";
            worksheet.Cells[3, 3].Value = "��������";

            int row = 4;  // ����д����ʼ��

            // ���ɲ�ͬ���ȵ�������ݲ�д�빤����
            GenerateData(numbers, 5, worksheet, row);
            GenerateData(numbers, 4, worksheet, row);
            GenerateData(numbers, 3, worksheet, row);

            string filePath = Application.dataPath + "/Excel/TriggerMachineOrder.xlsx";  // �����ļ�����·��
            package.SaveAs(new FileInfo(filePath));  // ���� Excel �ļ�
            Debug.Log("���ɳɹ�");  // ������ɳɹ�����־
        }
    }

    /**
     * ����ָ����������ͳ��ȵ�������ݣ���д�� Excel ������
     * @param numbers ��������
     * @param repetitions ��ϳ���
     * @param worksheet Excel ������
     * @param row д�����ݵ���ʼ��
     */

    private static void GenerateData(int[] numbers, int repetitions, ExcelWorksheet worksheet, int row)
    {
        List<int[]> combinations = GenerateCombinations(numbers, repetitions);  // �������

        foreach (var combo in combinations)  // �������
        {
            int sum = 0;  // ��ʼ����
            int product = 1;  // ��ʼ���˻�

            foreach (var num in combo)  // �������˻�
            {
                sum += num;
                product *= num;
            }

            string comboStr = string.Join(",", combo);  // �����ת��Ϊ�ַ���
            // ������ϳ�����Ӳ�����Ϣ
            if (combo.Length == 3)
                comboStr += ",0,0";
            else if (combo.Length == 4)
                comboStr += ",0";

            worksheet.Cells[row, 1].Value = comboStr;  // д������ַ���
            worksheet.Cells[row, 2].Value = sum;  // д���
            worksheet.Cells[row, 3].Value = product;  // д��˻�

            row++;  // �кŵ���
        }
    }

    /**
     * ����ָ����������ͳ��ȵ��������
     * @param numbers ��������
     * @param length ��ϳ���
     * @return ����б�
     */

    private static List<int[]> GenerateCombinations(int[] numbers, int length)
    {
        List<int[]> results = new List<int[]>();  // �洢��Ͻ�����б�
        GenerateCombinationsRecursive(results, new int[length], numbers, length, 0);  // �ݹ��������
        return results;  // ��������б�
    }

    /**
     * �ݹ麯��������������ϵ��ڲ�ʵ��
     * @param results ��Ͻ���б�
     * @param current ��ǰ�������ɵ����
     * @param numbers ��������
     * @param length ��ϳ���
     * @param position ��ǰ���ɵ�λ��
     */

    private static void GenerateCombinationsRecursive(List<int[]> results, int[] current, int[] numbers, int length, int position)
    {
        if (position == length)  // ���һ����ϵ�����
        {
            results.Add((int[])current.Clone());  // �������ӵ�����б�
            return;
        }

        foreach (var number in numbers)  // ������������
        {
            current[position] = number;  // ���õ�ǰλ�õ�����
            GenerateCombinationsRecursive(results, current, numbers, length, position + 1);  // �ݹ�������һ��λ�õ�����
        }
    }
}