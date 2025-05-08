using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//登录服务器返回数据
public class RootData
{
    public int code { get; set; }
    public string msg { get; set; }
    public ServerData data { get; set; }
}
//用户登录信息
public class ServerUserData
{
    public int code { get; set; }
    public string msg { get; set; }
    public int data { get; set; }
}
//服务器的数据
public class ServerData
{
    public string init { get; set; }
    public string init_br { get; set; }
    public string init_jp { get; set; }
    public string init_us { get; set; }
    public string init_ru { get; set; }
    public string init_id { get; set; }

    public string soho_shop { get; set; }
    public string soho_shop_br { get; set; }
    public string soho_shop_jp { get; set; }
    public string soho_shop_us { get; set; }
    public string soho_shop_ru { get; set; }
    public string soho_shop_id { get; set; }

    public string version { get; set; }
    public string game_data { get; set; }

    public string apple_pie { get; set; }
    public string inter_b2f_count { get; set; }
    public string inter_freq { get; set; }
    public string relax_interval { get; set; }
    public string trial_MaxNum { get; set; }
    public string nextlevel_interval { get; set; }
    public string adjust_init_rate_act { get; set; }
    public string adjust_init_act_position { get; set; }
    public string adjust_init_adrevenue { get; set; }
}
public class Init
{
    public List<SlotItem> slot_group { get; set; }

    public double[] cash_random { get; set; }
    public MultiGroup[] cash_group { get; set; }
    public MultiGroup[] gold_group { get; set; }
    public MultiGroup[] amazon_group { get; set; }
}

public class SlotItem
{
    public int multi { get; set; }
    public int weight { get; set; }
}

public class MultiGroup
{
    public int max { get; set; }
    public int multi { get; set; }
}

public class AchievementData //成就数据
{
    public int ID;
    public string Name;
    public string Description;
    public int NeedValue;
    public int AdReward;
    public int BaseReward;
    public int CollectState; //0未领取 1已领取基础 2已领取广告 3基础和广告都领取
}

public class BaseRewardData
{
    public string type { get; set; }
    public int weight { get; set; }
    public double reward_num { get; set; }
}
public class RewardData
{
    public RewardType Type;  //道具类型
    public double Num;  //奖励数量
}

public class GameData //游戏数据
{
    public int spinmax; //自动恢复最大次数
    public int spin_cd; //自动恢复冷却时间
    public int bonus_min; //Bonus最小触发次数 如果概率触发的方式始终没触发 到达这个次数必定触发
    public int bubble_cd; //飞行气泡出现时间
    public List<BaseRewardData> bubbledatalist; //飞行气泡奖励
    public int pigMax; //金猪最大数量
    public int[] pigmul; //金猪倍率
    public List<BaseRewardData> pigdatalist; //金猪奖励
    public List<BaseRewardData> wheeldatalist; //转盘签到奖励
    public List<BaseRewardData> puzzledatalist; //拼图奖励
    public List<BaseRewardData> amazontasklist; //成就战令奖励
    public List<BaseRewardData> cashdatalist; //转出3个绿票的奖励
    public int[] jackpotweight; //12选3玩法 Mmmg触发权重
    public int TotalValue10; //10局游戏发放的奖金总值
    public double ReplaceWildRate; //Wild出现概率
    public int MaxWildCount; //最大Wild数量
    public double BonusRate; //Bonus出现概率
    public int MaxBonusCount; //最大Bonus数量
    public double CashRate; //绿票出现概率
    public int MaxCashCount; //最大绿票数量
    public double TwelveToThree_Rate; //十二选三 多福多财玩法 触发概率
    public int TwelveToThree_WildCount; //十二选三 多福多财玩法 必触发收集数量
    public double PuzzleRate; //拼图出现概率
    public double FsBonusRate; //freespin bonus出现概率
    public double RsBonusRate; //Respin bonus2出现概率
    public double RsBonusAddRate; //Respin bonus3变为+1概率
    public double RsBonusChangeRate; //Respin bonus2升级为bonus3概率
    public double RsGrandRate; //Respin 必定15个全中概率
}
