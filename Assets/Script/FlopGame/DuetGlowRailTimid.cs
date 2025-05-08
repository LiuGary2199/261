using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuetGlowRailTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("ScoreText")]
    public Text HoneyPart;
[UnityEngine.Serialization.FormerlySerializedAs("HighScoreText")]    public Text MateHoneyPart;
[UnityEngine.Serialization.FormerlySerializedAs("ContinueBtn")]    public Button OmissionLys;
[UnityEngine.Serialization.FormerlySerializedAs("RestartBtn")]    public Button RefereeLys;
    protected override void Awake()
    {
        base.Awake();
        MessageCenter.AddMsgListener("ScoreUpdate", OnScoreUpdate);

        OmissionLys.onClick.AddListener(()=> {
            ADManager.Instance.playRewardVideo((success) =>
            {
                if (success)
                {
                    PryDuetGlowSharper.GetInstance().GlowLarge();
                    CloseUIForm("DuetGlowRailTimid");
                }
            }, "11");
            
        });

        RefereeLys.onClick.AddListener(()=> {
            PryDuetGlowSharper.GetInstance().GlowRail();
            CloseUIForm("DuetGlowRailTimid");
        });
    }

    private void OnScoreUpdate(KeyValuesUpdate kv)
    {
        DieGlowRailHoney(PryDuetGlowSharper.GetInstance().PitHoneyStudio,0);
    }

    public void DieGlowRailHoney(int score,int highScore)
    {
        HoneyPart.text = score.ToString();
        MateHoneyPart.text = highScore.ToString();
    }
}
