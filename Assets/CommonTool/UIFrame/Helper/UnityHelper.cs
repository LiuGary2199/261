/*
 *
 *
 * Unity帮助脚本
 * 功能：提供程序用户一些常用的功能方法实现，快速开发
 *
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnityHelper : MonoBehaviour
{
    /// <summary>
    /// 查找子节点对象
    /// 内部使用递归
    /// </summary>
    /// <param name="goParent">父对象</param>
    /// <param name="childName">查找子对象的名称</param>
    /// <returns></returns>
    public static Transform FindTheChildNode(GameObject goParent, string childName)
    {
        //查找结果
        Transform searchTrans = null;
        searchTrans = goParent.transform.Find(childName);
        if (searchTrans == null)
        {
            foreach (Transform trans in goParent.transform)
            {
                searchTrans = FindTheChildNode(trans.gameObject, childName);
                if (searchTrans != null)
                {
                    return searchTrans;
                }
            }
        }
        return searchTrans;
    }

    /// <summary>
    /// 获取子节点对象脚本
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="goParent">父对象</param>
    /// <param name="childName">子对象名称</param>
    /// <returns></returns>
    public static T GetTheChildNodeComponentScripts<T>(GameObject goParent, string childName) where T : Component
    {
        //查找特定子节点
        Transform searchTranformNode = null;
        searchTranformNode = FindTheChildNode(goParent, childName);
        if (searchTranformNode != null)
        {
            return searchTranformNode.gameObject.GetComponent<T>();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 给子节点添加脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="goParent">父对象</param>
    /// <param name="childName">子对象名称</param>
    /// <returns></returns>
    public static T AddChildNodeComponent<T>(GameObject goParent, string childName) where T : Component
    {
        Transform searchTransform = null;
        searchTransform = FindTheChildNode(goParent, childName);
        if (searchTransform != null)
        {
            T[] componentScriptsArray = searchTransform.GetComponents<T>();
            for (int i = 0; i < componentScriptsArray.Length; i++)
            {
                if (componentScriptsArray[i] != null)
                {
                    Destroy(componentScriptsArray[i]);
                }
            }
            return searchTransform.gameObject.AddComponent<T>();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 给子节点添加父对象
    /// </summary>
    /// <param name="parents">父对象</param>
    /// <param name="child"></param>
    public static void AddChildNodeToParentNode(Transform parents, Transform child)
    {
        child.SetParent(parents, false);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }

    /// <summary>
    /// 更高辨识度的log
    /// </summary>
    /// <param name="log">log内容</param>
    /// <param name="color">颜色 0黄 1青 2绿 3蓝 10红</param>
    public static void Print(object log, int color = 0)
    {
        if (color == 0)
            print("<color=yellow><b>+++++   " + log + "</b></color>");
        else if (color == 1)
            print("<color=cyan><b>+++++   " + log + "</b></color>");
        else if (color == 2)
            print("<color=green><b>+++++   " + log + "</b></color>");
        else if (color == 3)
            print("<color=blue><b>+++++   " + log + "</b></color>");
        else if (color == 10)
            print("<color=red><b>+++++   " + log + "</b></color>");
    }

    /// <summary>
    /// 翻译
    /// </summary>
    public static string Translation(string key)
    {
        string str = I2.Loc.LocalizationManager.GetTranslation(key);
        if (string.IsNullOrEmpty(str))
            return key;
        return str;
    }

    /// <summary>
    ///  生成不重复随机数组
    /// </summary>
    public static int[] UniqueRandomInts(int min, int max, int count)
    {
        // 检查是否有足够的数字可以生成。
        if (max - min + 1 < count)
        {
            Debug.LogError($"所求数组长度大于给定区间： {min}到{max}之间的数不够{count}个");
            return null;
        }
        List<int> numbers = Enumerable.Range(min, max - min + 1).ToList();
        System.Random rng = new System.Random();
        // 随机选择元素并从列表中移除
        int[] result = new int[count];
        for (int i = 0; i < count; i++)
        {
            // 获得随机索引和对应的值
            int randomIndex = rng.Next(numbers.Count);
            int value = numbers[randomIndex];
            // 保存随机值并删除已选择的值
            result[i] = value;
            numbers.RemoveAt(randomIndex);
        }
        return result;
    }

    private static Material GrayMat;

    /// <summary>
    /// UI置灰
    /// </summary>
    public static void SetUIGray(Image image, bool isGray)
    {
        if (GrayMat == null)
            GrayMat = Resources.Load<Material>("Art/GrayMat");
        image.material = isGray ? GrayMat : null;
    }

    /// <summary>
    ///  打点枚举转字符串
    /// </summary>
    public static string EnumToString(Enum enumValue)
    {
        return ((int)(object)enumValue).ToString();
    }
}