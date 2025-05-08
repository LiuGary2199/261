using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuetBony : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("front")]    public GameObject Verse;   // �������
[UnityEngine.Serialization.FormerlySerializedAs("back")]    public GameObject Waxy;    // �������
[UnityEngine.Serialization.FormerlySerializedAs("flipDuration")]    public float ModeSignaler= 0.5f; // ����ʱ��
[UnityEngine.Serialization.FormerlySerializedAs("ItemButton")]    public Button BonyUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("BackImageArray")]    public Sprite[] OmitAliveBiter;
[UnityEngine.Serialization.FormerlySerializedAs("backImage")]    public Image WaxyAlive;
[UnityEngine.Serialization.FormerlySerializedAs("isFlipped")]
    public bool BeExclaim= false;   // ��ǰ״̬
    private bool _BeDevote; //����״̬
[UnityEngine.Serialization.FormerlySerializedAs("_blockIndex")]    public int _BelleIndex;
[UnityEngine.Serialization.FormerlySerializedAs("_spriteIndex")]
    //�ڶ���Ϸ
    public int _AnswerBrood;
    void Start()
    {
        // �󶨰�ť����¼�
        BonyUnreal.onClick.AddListener(LordOoze);
        //back.SetActive(false); // ��ʼ���ر���
        DuetBoil();
    }
    private void DuetBoil()
    {
        Verse.SetActive(true);
        Waxy.SetActive(false);
        BeExclaim = false;
        this.transform.localRotation = new Quaternion(0, 0, 0, 0);
        this.transform.DOKill();
    }

    /// <summary>
    /// ���ر���
    /// </summary>
    public void ClubTheOmit()
    {
        // ��һ������ Y ����ת�� 90 �ȣ��������棩
        transform.DORotate(new Vector3(0, 270, 0), ModeSignaler / 2)
           .SetEase(Ease.OutQuad).OnComplete(() =>
           {
               Verse.SetActive(true);
               Waxy.SetActive(false);
               BeExclaim = false;
               transform.DORotate(new Vector3(0, 360, 0), ModeSignaler / 2)
                .OnComplete(() =>
                {
                    DuetGlowSharper.GetInstance().SoWaxyBlendKeel = false;
                });
           });
    }

    #region ��Ϸһ
    public void LordOoze()
    {
        if (DOTween.IsTweening(transform)) return; // ��ֹ�ظ����
        if (DuetGlowSharper.GetInstance().SoWaxyBlendKeel) return;
        if (DuetGlowSharper.GetInstance().GlowShut == "Two")
        {
            if (DuetGlowSharper.GetInstance().SoWaxyBlendKeel) return;
            UteDuetGlowSharper.GetInstance().OtherUteBlendJules(this);
        }
        MusicMgr.GetInstance().PlayToneUp();
        // ��һ������ Y ����ת�� 90 �ȣ��������棩
        transform.DORotate(new Vector3(0, 90, 0), ModeSignaler / 2)
           .SetEase(Ease.OutQuad).OnComplete(() =>
           {
               ClubMadSteam();
           });
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void ClubMadSteam()
    {
        Verse.SetActive(false);
        Waxy.SetActive(true);
        BeExclaim = true;
        // �ڶ�����������ת�� 180 �ȣ���ʾ���棩
        transform.DORotate(new Vector3(0, 180, 0), ModeSignaler / 2)
            .OnComplete(() => {

                if (DuetGlowSharper.GetInstance().GlowShut == "One")
                {
                    if (DuetGlowSharper.GetInstance().SoWaxyBlendKeel) return;
                    //�����Ƿ�Ϊ�ԵĿ�
                    OtherBlendDevote();
                }
                /*else if (DuetGlowSharper.GetInstance().GameType == "Two")
                {
                    if (DuetGlowSharper.GetInstance().IsOpenBlockShow) return;
                    UteDuetGlowSharper.GetInstance().CheckTwoBlockEqual(this);
                }*/
            });
    }

    

    public void KeelWeeBlendPryWholly()
    {
        if (!_BeDevote) return;
        ClubMadSteam();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(GreetClubMadOmit());
        }
    }

    IEnumerator GreetClubMadOmit()
    {
        yield return new WaitForSeconds(1f);
        ClubTheOmit();
        DuetGlowSharper.GetInstance().SoWaxyBlendKeel = false;
        //DuetGlowSharper.GetInstance().ResetTimer();
    }

    

    /// <summary>
    /// ��Ϸһ
    /// </summary>
    /// <param name="isTrue"></param>
    /// <param name="index"></param>
    public void DieOmitBubble(bool isTrue, int index)
    {
        WaxyAlive.gameObject.SetActive(false);
        _BeDevote = isTrue;
        _BelleIndex = index;
        DuetBoil();
        Waxy.GetComponent<Image>().sprite = isTrue ? OmitAliveBiter[0] : OmitAliveBiter[1];
    }

    private void OtherBlendDevote()
    {
        if (_BeDevote)
        {
            PryDuetGlowSharper.GetInstance().NutWeeBlendAxWeeBulb(_BelleIndex);
            PryDuetGlowSharper.GetInstance().DeepenHoney(10);
        }
        else
        {
            PryDuetGlowSharper.GetInstance().GarIf();
        }
    }
    #endregion

    #region ��Ϸ��
    /// <summary>
    /// ��Ϸ��
    /// </summary>
    /// <param name="index"></param>
    public void DieUteDuetOmitBubble(int index,int blockIndex)
    {
        //Debug.Log("------------      " + index);
        WaxyAlive.gameObject.SetActive(true);
        _AnswerBrood = index;
        _BelleIndex = blockIndex;
        WaxyAlive.sprite = Resources.Load<Sprite>("Art/Tex/NewGame_TwoFlop/When_M2_" + index);
        DuetBoil();
    }

    /// <summary>
    /// �ڶ�����Ϸչʾ
    /// </summary>
    public void KeelWeeBlendUteWholly()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(UteGlowGreetClubMadOmit());
        }
       
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void UteGlowClubMadSteam()
    {
        Verse.SetActive(false);
        Waxy.SetActive(true);
        BeExclaim = true;
        // �ڶ�����������ת�� 180 �ȣ���ʾ���棩
        transform.DORotate(new Vector3(0, 180, 0), ModeSignaler / 2)
            .OnComplete(() => {
                
            });
    }

    /// <summary>
    /// �ڶ���Ϸ�رձ���
    /// </summary>
    public void UteGlowClubMadOmit()
    {
        StartCoroutine(IE_UteGlowClubMadOmit());
    }

    IEnumerator UteGlowGreetClubMadOmit()
    {
        yield return new WaitForSeconds(0.5f);
        UteGlowClubMadSteam();
        yield return new WaitForSeconds(1.5f);
        ClubTheOmit();
        
    }

    IEnumerator IE_UteGlowClubMadOmit()
    {
        yield return new WaitForSeconds(0.5f);
        ClubTheOmit();
    }
    #endregion

}
