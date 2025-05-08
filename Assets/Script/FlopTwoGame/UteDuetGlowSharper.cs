using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UteDuetGlowSharper : MonoSingleton<UteDuetGlowSharper>
{
    /// <summary>
    /// ���Ӵ�С
    /// </summary>
    public List<Vector2Int> Bulb_SoundEagle;

    public List<DuetBony> Bulb_OtherUteGlowWee;

    public int PitHorde;

    public Vector2Int PitSurveySound;
    protected override void Awake()
    {
        base.Awake();
        PitHorde = 1;
        Bulb_OtherUteGlowWee = new();
        Bulb_SoundEagle = new List<Vector2Int>() { new Vector2Int(3,2), new Vector2Int(4,2), new Vector2Int(4,3),
        new Vector2Int(4,4),new Vector2Int(5,4),new Vector2Int(6,4)};
        int Write= MadHordeBrood(PitHorde);
        PitSurveySound = Bulb_SoundEagle[Write] ;
        DieDuetSoundBony();
    }


    #region �¼�
    public void OnCheckGameUpdate(bool isCheck)
    {
        KeyValuesUpdate key = new KeyValuesUpdate("CheckGameUpdate", isCheck);
        MessageCenter.SendMessage("CheckGameUpdate", key);
    }

    public void OnGameUpdate()
    {
        DuetGlowSharper.GetInstance().SoWaxyBlendKeel = true;
        KeyValuesUpdate key = new KeyValuesUpdate("TwoGameUpdate", null);
        MessageCenter.SendMessage("TwoGameUpdate", key);
    }
    #endregion


    /// <summary>
    /// 2x3,1  2x4,5  3x4,10  4x4,25  4x5,50  4x6,100
    /// </summary>
    /// <param name="curLevel"></param>
    /// <returns></returns>
    private int MadHordeBrood(int curLevel)
    {
        if (curLevel <= 1) return 0;
        if (curLevel <= 5) return 1;
        if (curLevel <= 10) return 2;
        if (curLevel <= 25) return 3;
        if (curLevel <= 50) return 4;
        return 5; 
    }

    
    /// <summary>
    /// ��������еĿ����ݡ�������� / 2 = ͼƬ����
    /// </summary>
    public List<TwoFlopItem> DieDuetSoundBony()
    {
        var List_SpriteIndex = GameUtil.SelectRandomNumbers((PitSurveySound.x * PitSurveySound.y) / 2, 35);
        var List_TwoFlopItem = new List<TwoFlopItem>();
        List<int> list = new List<int>();
        for (int i = 0; i < List_SpriteIndex.Count; i++)
        {
            list.Add(List_SpriteIndex[i]);
            list.Add(List_SpriteIndex[i]);
        }
        GameUtil.Shuffle(list);
        for (int i = 0; i < list.Count; i++)
        {
            TwoFlopItem item = new TwoFlopItem();
            item.Write = i;
            item.BubbleBrood = list[i];
            List_TwoFlopItem.Add(item);
        }
        return List_TwoFlopItem;
    }

    public void OtherUteBlendJules(DuetBony item)
    {
        if (Bulb_OtherUteGlowWee.Count < 2)
        {
            Bulb_OtherUteGlowWee.Add(item);
        }

        if (Bulb_OtherUteGlowWee.Count >= 2)
        {
            DuetGlowSharper.GetInstance().SoWaxyBlendKeel = true;
            if (Bulb_OtherUteGlowWee[0]._AnswerBrood == Bulb_OtherUteGlowWee[1]._AnswerBrood)
            {
                OnCheckGameUpdate(true);
            }
            else
            {
                OnCheckGameUpdate(false);
            }
        }
    }


    public void OtherDuetGlow(List<GameObject> list)
    {
        //Debug.Log("-------------------CheckFlopGame  " + list.Count);
        if (list.Count <= 0)
        {
            PitHorde++;
            UteDuetGlowBoil();
            //Debug.Log("-------------------CheckFlopGame  Succes  " +CurLevel);
        }

        OnCheckGame();
    }

    public void OnCheckGame()
    {
        if (DuetGlowSharper.GetInstance().SmelterMuch <= 0)
        {
            PitHorde = 1;
            UteDuetGlowBoil();
            Debug.Log("-------------------CheckFlopGame   Fail  " + PitHorde);
        }
    }

    public void UteDuetGlowBoil()
    {
        int Write= MadHordeBrood(PitHorde);
        PitSurveySound = Bulb_SoundEagle[Write];
        OnGameUpdate();
    }
    
}

public class TwoFlopItem
{
    public int Write;
    public int BubbleBrood;
}