using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PryDuetGlowSharper : MonoSingleton<PryDuetGlowSharper>
{
    public int DuetX;
    public int DuetY;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    public int PitDuetStudio;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    public int PitHoneyStudio;
    
    /// <summary>
    /// ���Ӵ�С
    /// </summary>
    public List<Vector2Int> Bulb_SoundEagle;
    /// <summary>
    /// ��ǰ��������
    /// </summary>
    public Vector2Int PitSoundSpurge;
    /// <summary>
    /// Ѫ��
    /// </summary>
    public int PitIf;
    

    
    
    /// <summary>
    /// ��Ϸѡ�жԵ�Index
    /// </summary>
    public List<int> Bulb_GlowSurveyWeeBrood;
    /// <summary>
    /// ��Ϸ����ĳɹ���
    /// </summary>
    public List<int> Bulb_GlowPlaintWeeBlend;

    public List<int> Bulb_ConsultHoney;

    protected override void Awake()
    {
        base.Awake();
        
        Bulb_ConsultHoney = new();
        Bulb_GlowSurveyWeeBrood = new();
        Bulb_GlowPlaintWeeBlend = new();
        Bulb_SoundEagle = new List<Vector2Int>() { new Vector2Int(4,4), new Vector2Int(4, 5), new Vector2Int(5,5),
        new Vector2Int(5,6),new Vector2Int(6,6)};

        PitDuetStudio = 1;
        SoupConsultHoney();
        DeepenHoney(0);
        GlowChipBoil();
    }

   

    #region �¼�
    public void OnGameUpdate()
    {
        DuetGlowSharper.GetInstance().SoWaxyBlendKeel = true;
        KeyValuesUpdate key = new KeyValuesUpdate("GameUpdate", null);
        MessageCenter.SendMessage("GameUpdate", key);
    }

    public void OnHpUpdate()
    {
        KeyValuesUpdate key = new KeyValuesUpdate("HpUpdate", PitIf);
        MessageCenter.SendMessage("HpUpdate", key);
    }

    
    public void OnScoreUpdate()
    {
        KeyValuesUpdate key = new KeyValuesUpdate("ScoreUpdate", PitHoneyStudio);
        MessageCenter.SendMessage("ScoreUpdate", key);
    }

    public void OnGameOver(Action action)
    {
        DuetGlowSharper.GetInstance().SoHaiti = true;
        KeyValuesUpdate key = new KeyValuesUpdate("GameOver", action);
        MessageCenter.SendMessage("GameOver", key);
    }

   
    #endregion

    #region �ⲿ����
    /// <summary>
    /// ��Ѫ
    /// </summary>
    public void GarIf()
    {
        PitIf--;
        OnHpUpdate();
        OnCheckGame();
    }

    /// <summary>
    /// ��鵱ǰ���Ƿ�Ϊ�ɹ���
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool OtherBroodSoWeeBlend(int index)
    {
        for (int i = 0; i < Bulb_GlowPlaintWeeBlend.Count; i++)
        {
            if (index == Bulb_GlowPlaintWeeBlend[i])
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// ���ӳɹ���index
    /// </summary>
    /// <param name="index"></param>
    public void NutWeeBlendAxWeeBulb(int index)
    {
        Bulb_GlowSurveyWeeBrood.Add(index);
        OnCheckGame();
    }

    


    

    /// <summary>
    /// ���·���
    /// </summary>
    public void DeepenHoney(int score)
    {
        if (score == 0)
        {
            FadeConsultHoney();
            PitHoneyStudio = 0;
        }
        else
        {
            PitHoneyStudio += score;
        }
        OnScoreUpdate();
    }

    /// <summary>
    /// ��Ϸʧ��
    /// </summary>
    public void GlowRail()
    {
        PitDuetStudio = 1;
        DeepenHoney(0);
        GlowBoil();
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void GlowLarge()
    {
        GlowBoil();
    }
    #endregion

    #region ˽�з���
    private void GlowChipBoil()
    {
        Bulb_GlowSurveyWeeBrood.Clear();
        Bulb_GlowPlaintWeeBlend.Clear();
        PitIf = 3;
        DuetGlowSharper.GetInstance().SoHaiti = true;
        DuetGlowSharper.GetInstance().SoWaxyBlendKeel = true;
        int Write= GameUtil.SetBoardCount(PitDuetStudio);
        PitSoundSpurge = Bulb_SoundEagle[Write];
        Bulb_GlowPlaintWeeBlend = GameUtil.SelectRandomNumbers(PitDuetStudio, PitSoundSpurge.x * PitSoundSpurge.y);
        OnHpUpdate();
    }


   
    /// <summary>
    /// ��Ϸ�ɹ�
    /// </summary>
    private void MadGlowDistort()
    {
        PitDuetStudio++;
        GlowBoil();
    }

    /// <summary>
    /// ��Ϸʧ��
    /// </summary>
    private void MadGlowRank()
    {
        UIManager.GetInstance().ShowUIForms("DuetGlowRailTimid");
        //OnGameOver();
       
        OnScoreUpdate();
    }

   
    /// <summary>
    /// ��Ϸ���ݳ�ʼ����������
    /// </summary>
    private void GlowBoil()
    {
        OnGameOver(()=> {
            GlowChipBoil();
            OnGameUpdate();
        });
    }

    /// <summary>
    /// �����Ϸ�ɹ�
    /// </summary>
    public void OnCheckGame()
    {
        if (PitIf <= 0)
        {
            MadGlowRank();
        }
        if (DuetGlowSharper.GetInstance().SmelterMuch <= 0)
        {
            MadGlowRank();
        }
        if (Bulb_GlowSurveyWeeBrood.Count == Bulb_GlowPlaintWeeBlend.Count)
        {
            MadGlowDistort();
        }
    }

    

    

    public void SoupConsultHoney()
    {
        string history = SaveDataManager.GetString("HistoryScore");
        string[] strArray = history.Split(',');
        if (strArray.Length < 5)
        {
            Bulb_ConsultHoney = new List<int>() { 0, 0, 0, 0, 0 };
        }
        else
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                Bulb_ConsultHoney.Add(int.Parse(strArray[i]));
            }
        }
        Bulb_ConsultHoney.Sort((a, b) => b.CompareTo(a));
    }

    public void FadeConsultHoney()
    {
        if (Bulb_ConsultHoney.Count <= 0) return;
        Bulb_ConsultHoney.Sort((a,b) => b.CompareTo(a));
        
        if (PitHoneyStudio > Bulb_ConsultHoney[Bulb_ConsultHoney.Count - 1])
        {
            Bulb_ConsultHoney.Remove(Bulb_ConsultHoney.Count - 1);
            Bulb_ConsultHoney.Add(PitHoneyStudio);
        }
        Bulb_ConsultHoney.Sort((a, b) => b.CompareTo(a));
        string str = string.Join(",", Bulb_ConsultHoney);
        SaveDataManager.SetString("HistoryScore",str);
    }

    #endregion


}
