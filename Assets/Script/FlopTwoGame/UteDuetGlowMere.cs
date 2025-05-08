using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UteDuetGlowMere : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("GridLayout")]    public GridLayoutGroup WifeSlight;
[UnityEngine.Serialization.FormerlySerializedAs("DuetBony")]
    public GameObject DuetBony;
[UnityEngine.Serialization.FormerlySerializedAs("Board")]    //���
    public GameObject Sound;
    //�����
    private ObjectPool _BoreSeed;
    //����itemlist
    private List<GameObject> _Hand_DuetBony;

    void Awake()
    {

        MessageCenter.AddMsgListener("CheckGameUpdate", OnCheckGameUpdate);
        _Hand_DuetBony = new();
        DuetSeedBoil();
    }

    private void OnCheckGameUpdate(KeyValuesUpdate kv)
    {
        bool isGame = (bool)kv.Values;
        var data = UteDuetGlowSharper.GetInstance().Bulb_OtherUteGlowWee;
        if (isGame)
        {
            StartCoroutine(GreetSystemJulesBlend(data));
        }
        else
        {
            for (int i = 0; i < data.Count; i++)
            {
                data[i].UteGlowClubMadOmit();
            }
        }
        StartCoroutine(GreetOtherDuetGlow());
    }

    IEnumerator GreetSystemJulesBlend(List<DuetBony> data)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < data.Count; i++)
        {
            for (int j = 0; j < _Hand_DuetBony.Count; j++)
            {
                if (data[i]._BelleIndex == _Hand_DuetBony[j].GetComponent<DuetBony>()._BelleIndex)
                {
                    _BoreSeed.Recycle(_Hand_DuetBony[j].gameObject);
                    _Hand_DuetBony.Remove(_Hand_DuetBony[j]);
                }
            }
        }
    }

    IEnumerator GreetOtherDuetGlow()
    {
        yield return new WaitForSeconds(1f);
        DuetGlowSharper.GetInstance().SoWaxyBlendKeel = false;
        UteDuetGlowSharper.GetInstance().Bulb_OtherUteGlowWee.Clear();
        UteDuetGlowSharper.GetInstance().OtherDuetGlow(_Hand_DuetBony);
    }


    public void OnGameUpdate()
    {
        WifeSlight.enabled = true;
        SubjectApe();
        OneMemoryFlopGameInit();
    }

    /// <summary>
    /// ����س�ʼ��
    /// </summary>
    private void DuetSeedBoil()
    {
        _BoreSeed = new ObjectPool();
        _BoreSeed.Init("TwoFlopPool", Sound.transform);
        _BoreSeed.Prefab = DuetBony;
    }

    public void OneMemoryFlopGameInit()
    {
        Sound.transform.localPosition = new Vector3(1200, 0, 0);
        Vector2Int board= UteDuetGlowSharper.GetInstance().PitSurveySound;
        int cell = DuetGlowSharper.GetInstance().MadDuetJazzGarb(board.x,board.y);
        WifeSlight.constraint = GridLayoutGroup.Constraint.Flexible;
        WifeSlight.spacing = new Vector2(10, 10);
        WifeSlight.cellSize = new Vector2(cell, cell);
        //���ð��Ӵ�С
        DuetGlowSharper.GetInstance().DieSoundGarb(board.x, board.y, out int x, out int y);
        Sound.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);

        var data = UteDuetGlowSharper.GetInstance().DieDuetSoundBony();

        for (int i = 0; i < data.Count; i++)
        {
            GameObject obj = _BoreSeed.Get();
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<DuetBony>().DieUteDuetOmitBubble(data[i].BubbleBrood,i);
            _Hand_DuetBony.Add(obj);
        }

        VirusGlowSoundCup();
    }

    /// <summary>
    /// �������з���
    /// </summary>
    public void SubjectApe()
    {
        for (int i = 0; i < _Hand_DuetBony.Count; i++)
        {
            _BoreSeed.Recycle(_Hand_DuetBony[i]);
        }
        _Hand_DuetBony.Clear();
    }

    /// <summary>
    /// ��ʼʱ����嶯��
    /// </summary>
    public void VirusGlowSoundCup()
    {
        //end��-160   start��1200
        //Debug.Log("------------------��������");

        DuetGlowSharper.GetInstance().SoHaiti = true;
        Sound.transform.DOLocalMoveX(-160, 1f)
            .SetEase(Ease.Linear) // ���ö�������Ϊ����
           .OnComplete(() => {
               WifeSlight.enabled = false;
               for (int i = 0; i < _Hand_DuetBony.Count; i++)
               {
                   _Hand_DuetBony[i].GetComponent<DuetBony>().KeelWeeBlendUteWholly();
               }
               DuetGlowSharper.GetInstance().LargeDrown();
               ADManager.Instance.NoThanksAddCount(1);
           }); // ����ʱ�����ص�
    }

    public void SheGlowSoundCup(Action action)
    {
        //Debug.Log("------------------�볡����");
        Sound.transform.DOLocalMoveX(-1700, 1f)
            .SetEase(Ease.Linear) // ���ö�������Ϊ����
           .OnComplete(() => {
               action?.Invoke();
               DuetGlowSharper.GetInstance().SoHaiti = true;
           }); // ����ʱ�����ص�
    }
}
