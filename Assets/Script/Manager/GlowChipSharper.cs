using zeta_framework;

public class GlowChipSharper : MonoSingleton<GlowChipSharper>
{

    void Start()
    {
        //定时事件打点
        PurgeRenewalTheme();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            RenewalTheme();
        }
    }

    void PurgeRenewalTheme() //定时事件
    {
        RenewalTheme();
        TimeManager.GetInstance().Delay_RealTime(120, () =>
        {
            PurgeRenewalTheme();
        });
    }
    void RenewalTheme() //定时事件
    {
        string cumulativeGold = SaveDataManager.GetDouble(CConfig.sv_CumulativeGoldCoin).ToString();
        string CumulativeToken = SaveDataManager.GetDouble(CConfig.sv_CumulativeToken).ToString();
        string CumulativeAmazon = SaveDataManager.GetDouble(CConfig.sv_CumulativeAmazon).ToString();
        string PlayedSpinCount = SaveDataManager.GetInt("PlayedSpinCount").ToString();
        PostEventScript.GetInstance().SendEvent("2001", cumulativeGold, CumulativeToken, CumulativeAmazon, PlayedSpinCount);
    }

    public void BoilGlowChip()
    {
#if SOHOShop
        // 提现商店初始化
        // 提现商店中的金币、现金和amazon卡均为double类型，参数请根据具体项目自行处理
        SOHOShopManager.instance.InitSOHOShopAction(
            GetCash,
            GetGold,
            GetAmazon,    // amazon
            (subToken) => { AddCash(-subToken); },
            (subGold) => { AddGold(-subGold); },
            (subAmazon) => { AddAmazon(-subAmazon); });
#endif
    }

    // 金币
    public double MadThin()
    {
        return ResourceCtrl.Instance.gold.currentValue;
    }

    public void NutThin(double gold)
    {
        ResourceCtrl.Instance.AddItemValue(ResourceCtrl.Instance.gold, gold);
        if (gold > 0)
        {
            SaveDataManager.SetDouble(CConfig.sv_CumulativeGoldCoin, SaveDataManager.GetDouble(CConfig.sv_CumulativeGoldCoin) + gold);
        }
    }

    // 现金
    public double MadShin()
    {
        return ResourceCtrl.Instance.diamond.currentValue;
    }

    public void NutShin(double token)
    {
        ResourceCtrl.Instance.AddItemValue(ResourceCtrl.Instance.diamond, token);
        if (token > 0)
        {
            SaveDataManager.SetDouble(CConfig.sv_CumulativeToken, SaveDataManager.GetDouble(CConfig.sv_CumulativeToken) + token);
        }
#if SOHOShop
        SOHOShopManager.instance.UpdateCash();
#endif
    }

    //Amazon卡
    public double MadHazard()
    {
        return ResourceCtrl.Instance.amazon.currentValue;
    }

    public void NutHazard(double amazon)
    {
        ResourceCtrl.Instance.AddItemValue(ResourceCtrl.Instance.amazon, amazon);
        if (amazon > 0)
        {
            SaveDataManager.SetDouble(CConfig.sv_CumulativeAmazon, SaveDataManager.GetDouble(CConfig.sv_CumulativeAmazon) + amazon);
        }
    }

}
