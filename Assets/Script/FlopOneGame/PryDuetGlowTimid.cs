using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PryDuetGlowTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("List_Hp")]    public List<GameObject> Bulb_If;
[UnityEngine.Serialization.FormerlySerializedAs("SpriteArray")]    public Sprite[] BubbleBiter;
[UnityEngine.Serialization.FormerlySerializedAs("ScoreText")]
    public Text HoneyPart;
[UnityEngine.Serialization.FormerlySerializedAs("LevelText")]    public Text HordePart;
[UnityEngine.Serialization.FormerlySerializedAs("TimeText")]    public Text MuchPart;
[UnityEngine.Serialization.FormerlySerializedAs("TimeImage")]    public Image MuchAlive;
[UnityEngine.Serialization.FormerlySerializedAs("PauseBtn")]    public Button HaitiLys;
[UnityEngine.Serialization.FormerlySerializedAs("RankBtn")]    public Button LimbLys;
[UnityEngine.Serialization.FormerlySerializedAs("HomeBtn")]    public Button WageLys;
[UnityEngine.Serialization.FormerlySerializedAs("MaskObj")]    public GameObject IbexToo;
[UnityEngine.Serialization.FormerlySerializedAs("OneFlopCtrl")]    public PryDuetGlowMere PryDuetMere;

    protected override void Awake()
    {
        base.Awake();
        MessageCenter.AddMsgListener("HpUpdate", OnHpUpdate);
        MessageCenter.AddMsgListener("GameUpdate", OnGameUpdate);
        MessageCenter.AddMsgListener("TimeUpdate", OnTimeUpdate);
        MessageCenter.AddMsgListener("ScoreUpdate", OnScoreUpdate);
        MessageCenter.AddMsgListener("GameOver", OnGameOver);
        MessageCenter.AddMsgListener("PauseUpdate", OnPauseUpdate);

        LimbLys.onClick.AddListener(()=> {
            UIManager.GetInstance().ShowUIForms("DuetGlowLimbTimid");
        });

        HaitiLys.onClick.AddListener((UnityEngine.Events.UnityAction)(()=> {
            DuetGlowSharper.GetInstance().SoHaiti = !DuetGlowSharper.GetInstance().SoHaiti;
            HaitiLys.GetComponent<Image>().sprite = DuetGlowSharper.GetInstance().SoHaiti ? BubbleBiter[1]: BubbleBiter[0];
        }));

        WageLys.onClick.AddListener(()=> {
            DuetGlowSharper.GetInstance().OnTimeClose();
            OnTimeInit();
            CloseUIForm("PryDuetGlowTimid");
        });

        //TimeImageFillAmountAni();
        MuchAlive.fillAmount = 1;
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

    private void OnScoreUpdate(KeyValuesUpdate kv)
    {
        int score = (int)kv.Values;
        HoneyPart.text = score.ToString();
    }

    private void Update()
    {
        
    }
    private void OnGameOver(KeyValuesUpdate kv)
    {
        Action action = (Action)kv.Values;
        PryDuetMere.SheGlowSoundCup(action);
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

    private void OnGameUpdate(KeyValuesUpdate kv)
    {
        OnTimeInit();
        HordePart.text = PryDuetGlowSharper.GetInstance().PitDuetStudio.ToString();
        PryDuetMere.OnGameUpdate();
        MuchAliveWaryFilterCup();
    }

    private void OnHpUpdate(KeyValuesUpdate kv)
    {
        int hp = (int)kv.Values;
        for (int i = 0; i < Bulb_If.Count; i++)
        {
            Bulb_If[i].SetActive(i <= (hp - 1));
        }
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

           }); // ����ʱ�����ص�
    }
}
