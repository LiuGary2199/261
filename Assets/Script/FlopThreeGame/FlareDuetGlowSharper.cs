using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FlareDuetGlowSharper : MonoSingleton<FlareDuetGlowSharper>
{
    public int PitHorde;

    public int PitHordeWeeStudio;
    /// <summary>
    /// ������Ϸ�ؿ�����
    /// </summary>
    public ThreeLevelDatas Bulb_FlareHorde;

    public List<Vector2Int> Bulb_HordeSound;

    public Vector2Int PitSurveySound;

    public ThreeLevelInfo PitHordeIdle;

    public List<FlareDuetBony> Bulb_SurveyBlend;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public bool SoSurveyDoll;


    protected override void Awake()
    {
        base.Awake();
        PitHorde = 0;
        Bulb_SurveyBlend = new();
        Bulb_HordeSound = new List<Vector2Int>() { new Vector2Int(5, 5), new Vector2Int(6, 6) };
        SoupFlareGlowHordeOnly();
        FlareGlowHordeBoil();
    }
    #region �¼�
    public void OnGameUpdate()
    {
        KeyValuesUpdate key = new KeyValuesUpdate("ThreeGameUpdate", null);
        MessageCenter.SendMessage("ThreeGameUpdate", key);
    }

    #endregion

    /// <summary>
    /// ��ȡ�ؿ�json
    /// </summary>
    public void SoupFlareGlowHordeOnly()
    {
        Bulb_SurveyBlend = new();
        string readData;
        TextAsset json = Resources.Load<TextAsset>("LocationJson/ThreeGame");
        //string filePath = Application.dataPath + "/Art/GameJson/ThreeGame.json";
        //using (StreamReader sr = File.OpenText(filePath))
        //{
        //    readData = sr.ReadToEnd();
        //    sr.Close();
        //}
        Bulb_FlareHorde = JsonMapper.ToObject<ThreeLevelDatas>(json.text);
    }

    /// <summary>
    /// ��������Ϸ�ؿ���Ϣ
    /// </summary>
    public void FlareGlowHordeBoil()
    {
        SoSurveyDoll = false;
        PitHordeWeeStudio = 1;
        Bulb_SurveyBlend.Clear();
        if (PitHorde < Bulb_FlareHorde.List_Level.Count)
        {
            PitHordeIdle = Bulb_FlareHorde.List_Level[PitHorde];
        }
        else
        {
            int Write= Random.Range(1, Bulb_FlareHorde.List_Level.Count);
            PitHordeIdle = Bulb_FlareHorde.List_Level[Write];
        }
        PitSurveySound = PitHordeIdle.num == 1 ? Bulb_HordeSound[0] : Bulb_HordeSound[1];
        foreach (var item in PitHordeIdle.board)
        {
            for (int i = 0; i < item.Count; i++)
            {
                if (item[i] == 0)
                {
                    PitHordeWeeStudio++;
                }
            }
        }
        Debug.Log("-------------------- ��ǰ�ؿ��Ļ�ʤ����  " + PitHordeWeeStudio);
    }

    public void SurveyNutAxBulb(FlareDuetBony item)
    {
        //Debug.Log("---------------------------ѡ��   " + item.BlockPos  + " ------    "  + IsCheckItemToList(item));
        if (SoOtherBonyAxBulb(item))
            return;
        if (!Bulb_SurveyBlend.Contains(item))
        {
            item.OnSetBlockState(true,true);
            for (int i = 0; i < Bulb_SurveyBlend.Count; i++)
            {
                Bulb_SurveyBlend[i].OnSetBlockState(false,true);
                /*if (i < List_SelectBlock.Count - 1)
                {
                    int rotationZ = GetPointToItemPosition(List_SelectBlock[i+1].BlockPos, List_SelectBlock[i].BlockPos);
                    List_SelectBlock[i].OnSetLinePosition(rotationZ);
                }*/
            }
            Bulb_SurveyBlend.Add(item);
            MusicMgr.GetInstance().PlayToneUp();
        }
        else
        {
            int Write= Bulb_SurveyBlend.IndexOf(item);
            for (int i = 0; i < Bulb_SurveyBlend.Count; i++)
            {
                if (i < Write)
                {
                    Bulb_SurveyBlend[i].OnSetBlockState(false, true);
                }
                else if (i == Write)
                { Bulb_SurveyBlend[i].OnSetBlockState(true, true); }
                else
                { 
                    Bulb_SurveyBlend[i].OnBlockInit(); 
                }
            }
            Bulb_SurveyBlend.RemoveRange(Write + 1, Bulb_SurveyBlend.Count - (Write+1));
        }
        //Debug.Log("----------------------   �ռ�������   ��  " + List_SelectBlock.Count);
        OnCheckGame();
    }

    /// <summary>
    /// ���item�Ƿ��ܼ��뵽list��
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private bool SoOtherBonyAxBulb(FlareDuetBony item)
    {
        if (Bulb_SurveyBlend.Count == 0)
        {
            return false;
        }

        if (Bulb_SurveyBlend.Contains(item))
        {
            return false;
        }

        if (Bulb_SurveyBlend.Count > 0 && Bulb_SurveyBlend[Bulb_SurveyBlend.Count - 1].BlendHut == item.BlendHut)
        {
            return true;
        }

        return !SoShearEmission(item.BlendHut,Bulb_SurveyBlend[Bulb_SurveyBlend.Count - 1].BlendHut);
    }
    // �жϵ� m �Ƿ��ڵ� N ��ǰ������
    public bool SoShearEmission(Vector2 m, Vector2 N)
    {
        // ���� m �Ƿ��ھ��� (0, 0) �� (5, 5) ��Χ��
        if (m.x >= 0 && m.x <= PitSurveySound.x && m.y >= 0 && m.y <= PitSurveySound.y)
        {
            // ��� m �Ƿ��� N ��ǰ������
            return (m.x == N.x && Mathf.Abs(m.y - N.y) == 1) || (m.y == N.y && Mathf.Abs(m.x - N.x) == 1);
        }
        return false;
    }

    /// <summary>
    /// �жϵ�m �ڵ�n��ǰ������
    /// </summary>

    public int MadShearAxBonyHeighten(Vector2 m, Vector2 n)
    {
        if (m.y == n.y && m.x > n.x)
        {
            Debug.Log("----------   ���m �� " + m + "  ----   �ڵ�n  : " + n + "   �Ϸ�");
            return 90;
        }
        else if (m.y == n.y && m.x < n.x) 
        {
            Debug.Log("----------   ���m �� " + m + "  ----   �ڵ�n  : " + n + "   �·�");
            return 270;
        }
        else if (m.x == n.x && m.y > n.y)
        {
            Debug.Log("----------   ���m �� " + m + "  ----   �ڵ�n  : " + n + "   �ҷ�");
            return 0;
        }
        else if (m.x == n.x && m.y < n.y)
        {
            Debug.Log("----------   ���m �� " + m + "  ----   �ڵ�n  : " + n + "   ��");
            return 180;
        }
        return 1;
    }

    public void OnCheckGame()
    {
        if (PitHordeWeeStudio == Bulb_SurveyBlend.Count && DuetGlowSharper.GetInstance().SmelterMuch > 0)
        {
            PitHorde++;
            GlowChipDeepen();
        }

        if (DuetGlowSharper.GetInstance().SmelterMuch <= 0)
        {
            PitHorde = 0;
            GlowChipDeepen();
        }
    }

    private void GlowChipDeepen()
    {
        
        FlareGlowHordeBoil();
        OnGameUpdate();
    }

    
}

public class ThreeLevelDatas
{
    public List<ThreeLevelInfo> List_Level;
}

public class ThreeLevelInfo
{
    public int level;
    public int num;
    public List<List<int>> board;
}
