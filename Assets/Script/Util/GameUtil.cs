using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil
{
    /// <summary>
    /// 获取multi系数
    /// </summary>
    /// <returns></returns>
    private static double GetMulti(RewardType type, double cumulative, MultiGroup[] multiGroup)
    {
        foreach (MultiGroup item in multiGroup)
        {
            if (item.max > cumulative)
            {
                if (type == RewardType.Cash)
                {
                    float random =UnityEngine.Random.Range((float)NetInfoMgr.instance.InitData.cash_random[0], (float)NetInfoMgr.instance.InitData.cash_random[1]);
                    return item.multi * (1 + random);
                }
                else
                {
                    return item.multi;
                }
            }
        }
        return 1;
    }

    public static double GetGoldMulti()
    {
        return GetMulti(RewardType.Gold, SaveDataManager.GetDouble(CConfig.sv_CumulativeGoldCoin), NetInfoMgr.instance.InitData.gold_group);
    }

    public static double GetCashMulti()
    {
        return GetMulti(RewardType.Cash, SaveDataManager.GetDouble(CConfig.sv_CumulativeToken), NetInfoMgr.instance.InitData.cash_group);
    }
    public static double GetAmazonMulti()
    {
        return GetMulti(RewardType.Amazon, SaveDataManager.GetDouble(CConfig.sv_CumulativeAmazon), NetInfoMgr.instance.InitData.amazon_group);
    }

    //不带随机数的获取multi系数
    private static double GetMulti_NoRandom(double cumulative, MultiGroup[] multiGroup)
    {
        foreach (MultiGroup item in multiGroup)
        {
            if (item.max > cumulative)
                return item.multi;
        }
        return 1;
    }
    public static double GetGoldMulti_NoRandom()
    {
        return GetMulti_NoRandom(SaveDataManager.GetDouble(CConfig.sv_CumulativeGoldCoin), NetInfoMgr.instance.InitData.gold_group);
    }
    public static double GetCashMulti_NoRandom()
    {
        return GetMulti_NoRandom(SaveDataManager.GetDouble(CConfig.sv_CumulativeToken), NetInfoMgr.instance.InitData.cash_group);
    }
    public static double GetAmazonMulti_NoRandom()
    {
        return GetMulti_NoRandom(SaveDataManager.GetDouble(CConfig.sv_CumulativeAmazon), NetInfoMgr.instance.InitData.amazon_group);
    }

    /// <summary>
    /// 设置板子容量
    /// </summary>
    /// <returns></returns>
    public static int SetBoardCount(int FlopNumber)
    {
        int index = 0;
        if (FlopNumber < 9)
        {
            index = 0;
        }
        else if (FlopNumber >= 9 && FlopNumber < 10)
        {
            index = 1;
        }
        else if (FlopNumber >= 10 && FlopNumber < 13)
        {
            index = 2;
        }
        else if (FlopNumber >= 13 && FlopNumber < 16)
        {
            index = 3;
        }
        else if (FlopNumber >= 16)
        {
            index = 4;
        }
        return index;
    }

    /// <summary>
    /// 在矩阵中选择成功块随机数
    /// </summary>
    /// <param name="M"></param>
    /// <param name="N"></param>
    /// <returns></returns>
    public static List<int> SelectRandomNumbers(int flopNumber,int all)
    {
        int N = flopNumber;
        int M = all;
        if (M < N)
        {
            throw new ArgumentException("总数M < 抽取数N");
        }

        List<int> allNumbers = new List<int>();
        for (int i = 0; i < M; i++)
        {
            allNumbers.Add(i); // 假设数字从 0 到 M-1
        }

        List<int> selectedNumbers = new List<int>();
        System.Random random = new System.Random();

        for (int i = 0; i < N; i++)
        {
            int randomIndex = random.Next(allNumbers.Count);
            selectedNumbers.Add(allNumbers[randomIndex]);
            allNumbers.RemoveAt(randomIndex); // 确保不会重复选择
        }

        return selectedNumbers;
    }

    /// <summary>
    /// Fisher-Yates 洗牌算法
    /// </summary>
    /// <param name="list"></param>
    public static void Shuffle(List<int> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}


/// <summary>
/// 奖励类型
/// </summary>
public enum RewardType { Gold, Cash, Amazon,Null }
