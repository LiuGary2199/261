using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OfficeOpenXml;
using UnityEditor;
using System.IO;

/**
 * 老虎机数据生成器，继承自 Editor 类，用于生成老虎机相关数据并保存到 Excel 文件
 */

public class SlotMachineDataGenerator : Editor
{
    /**
     * 菜单栏菜单项的点击事件处理函数，用于生成老虎机数据
     */

    [MenuItem("Tools/生成老虎机数据")]
    private static void GenerateSlotMachineData()
    {
        int[] numbers = { 1, 2, 3 };  // 定义数字数组

        using (ExcelPackage package = new ExcelPackage())  // 创建 Excel 包
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Slot Combinations");  // 添加工作表

            // 设置表头信息
            worksheet.Cells[1, 1].Value = "Order";
            worksheet.Cells[1, 2].Value = "ItemCount";
            worksheet.Cells[1, 3].Value = "LineCount";

            worksheet.Cells[2, 1].Value = "string";
            worksheet.Cells[2, 2].Value = "int";
            worksheet.Cells[2, 3].Value = "int";

            worksheet.Cells[3, 1].Value = "排列顺序";
            worksheet.Cells[3, 2].Value = "图标数量";
            worksheet.Cells[3, 3].Value = "连线数量";

            int row = 4;  // 数据写入起始行

            // 生成不同长度的组合数据并写入工作表
            GenerateData(numbers, 5, worksheet, row);
            GenerateData(numbers, 4, worksheet, row);
            GenerateData(numbers, 3, worksheet, row);

            string filePath = Application.dataPath + "/Excel/TriggerMachineOrder.xlsx";  // 生成文件保存路径
            package.SaveAs(new FileInfo(filePath));  // 保存 Excel 文件
            Debug.Log("生成成功");  // 输出生成成功的日志
        }
    }

    /**
     * 生成指定数字数组和长度的组合数据，并写入 Excel 工作表
     * @param numbers 数字数组
     * @param repetitions 组合长度
     * @param worksheet Excel 工作表
     * @param row 写入数据的起始行
     */

    private static void GenerateData(int[] numbers, int repetitions, ExcelWorksheet worksheet, int row)
    {
        List<int[]> combinations = GenerateCombinations(numbers, repetitions);  // 生成组合

        foreach (var combo in combinations)  // 遍历组合
        {
            int sum = 0;  // 初始化和
            int product = 1;  // 初始化乘积

            foreach (var num in combo)  // 计算和与乘积
            {
                sum += num;
                product *= num;
            }

            string comboStr = string.Join(",", combo);  // 将组合转换为字符串
            // 根据组合长度添加补充信息
            if (combo.Length == 3)
                comboStr += ",0,0";
            else if (combo.Length == 4)
                comboStr += ",0";

            worksheet.Cells[row, 1].Value = comboStr;  // 写入组合字符串
            worksheet.Cells[row, 2].Value = sum;  // 写入和
            worksheet.Cells[row, 3].Value = product;  // 写入乘积

            row++;  // 行号递增
        }
    }

    /**
     * 生成指定数字数组和长度的所有组合
     * @param numbers 数字数组
     * @param length 组合长度
     * @return 组合列表
     */

    private static List<int[]> GenerateCombinations(int[] numbers, int length)
    {
        List<int[]> results = new List<int[]>();  // 存储组合结果的列表
        GenerateCombinationsRecursive(results, new int[length], numbers, length, 0);  // 递归生成组合
        return results;  // 返回组合列表
    }

    /**
     * 递归函数，用于生成组合的内部实现
     * @param results 组合结果列表
     * @param current 当前正在生成的组合
     * @param numbers 数字数组
     * @param length 组合长度
     * @param position 当前生成的位置
     */

    private static void GenerateCombinationsRecursive(List<int[]> results, int[] current, int[] numbers, int length, int position)
    {
        if (position == length)  // 完成一个组合的生成
        {
            results.Add((int[])current.Clone());  // 将组合添加到结果列表
            return;
        }

        foreach (var number in numbers)  // 遍历数字数组
        {
            current[position] = number;  // 设置当前位置的数字
            GenerateCombinationsRecursive(results, current, numbers, length, position + 1);  // 递归生成下一个位置的数字
        }
    }
}