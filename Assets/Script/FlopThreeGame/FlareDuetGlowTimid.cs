using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlareDuetGlowTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("SpriteArray")]    public Sprite[] BubbleBiter;
[UnityEngine.Serialization.FormerlySerializedAs("LevelText")]    public Text HordePart;
[UnityEngine.Serialization.FormerlySerializedAs("TimeText")]    public Text MuchPart;
[UnityEngine.Serialization.FormerlySerializedAs("TimeImage")]    public Image MuchAlive;
[UnityEngine.Serialization.FormerlySerializedAs("PauseBtn")]    public Button HaitiLys;
[UnityEngine.Serialization.FormerlySerializedAs("HomeBtn")]    public Button WageLys;
[UnityEngine.Serialization.FormerlySerializedAs("MaskObj")]    public GameObject IbexToo;
[UnityEngine.Serialization.FormerlySerializedAs("ThreeGameCtrl")]    public FlareDuetGlowMere FlareGlowMere;

    protected override void Awake()
    {
        base.Awake();

        MessageCenter.AddMsgListener("TimeUpdate", OnTimeUpdate);
        MessageCenter.AddMsgListener("ThreeGameUpdate", OnThreeGameUpdate);
        MessageCenter.AddMsgListener("PauseUpdate", OnPauseUpdate);
        WageLys.onClick.AddListener(() =>
        {
            DuetGlowSharper.GetInstance().OnTimeClose();
            OnTimeInit();
            CloseUIForm("FlareDuetGlowTimid");
        });

        HaitiLys.onClick.AddListener((UnityEngine.Events.UnityAction)(() => {
            DuetGlowSharper.GetInstance().SoHaiti = !DuetGlowSharper.GetInstance().SoHaiti;
            HaitiLys.GetComponent<Image>().sprite = DuetGlowSharper.GetInstance().SoHaiti ? BubbleBiter[1] : BubbleBiter[0];
        }));
        MuchAlive.fillAmount = 1;
        
    }


    private void Update()
    {
        if (DuetGlowSharper.GetInstance().SoHaiti)
        {
            MuchAlive.DOPause();
        }
        else
        {
            MuchAlive.DOPlay();
        }
    }

    private void OnPauseUpdate(KeyValuesUpdate kv)
    {
        bool ispause = (bool)kv.Values;
        if (ispause)
        {
            MuchAlive.DOPause();
            //PauseBtn.GetComponent<Image>().sprite = SpriteArray[1];
            IbexToo.SetActive(true);
        }
        else
        {

            MuchAlive.DOPlay();
            //PauseBtn.GetComponent<Image>().sprite = SpriteArray[0];
            IbexToo.SetActive(false);
        }
    }
    private void OnThreeGameUpdate(KeyValuesUpdate kv)
    {
        OnTimeInit();
        HordePart.text = (FlareDuetGlowSharper.GetInstance().PitHorde + 1).ToString();
        FlareGlowMere.OnGameUpdate();
        MuchAliveWaryFilterCup();
    }

    
    private void OnTimeUpdate(KeyValuesUpdate kv)
    {
        
        float time = (float)kv.Values;
        if (time > 59)
        {
            MuchAlive.fillAmount = 1;
        }
        MuchPart.text = time.ToString();
    }


    private void OnTimeInit()
    {
        MuchAlive.DOKill();
        MuchAlive.fillAmount = 1;
        //TimeText.text = DuetGlowSharper.GetInstance().TotalTime.ToString();
    }
    /// <summary>
    /// ʱ��ͼƬ����
    /// </summary>
    private void MuchAliveWaryFilterCup()
    {
        MuchAlive.fillAmount = 1;
        MuchAlive.DOFillAmount(0, DuetGlowSharper.GetInstance().BroodMuch - 1)
           .SetEase(Ease.Linear) // ���ö�������Ϊ����
           .OnComplete(() => {
               FlareDuetGlowSharper.GetInstance().OnCheckGame();
           }); // ����ʱ�����ص�
    }
}
