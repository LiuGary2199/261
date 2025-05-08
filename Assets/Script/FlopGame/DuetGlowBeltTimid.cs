using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuetGlowBeltTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("OneGameButton")]    public Button PryGlowUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("TwoGameButton")]    public Button UteGlowUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("ThreeGameButton")]    public Button FlareGlowUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("BackButton")]    public Button OmitUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("SettingButton")]    public Button CorrectUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("BgShadow")]
    public GameObject GoHarbor;
[UnityEngine.Serialization.FormerlySerializedAs("BtnGroup")]    public GameObject LysArena;
[UnityEngine.Serialization.FormerlySerializedAs("m_PageView")]    public PageView m_LaceReef;
    protected override void Awake()
    {
        base.Awake();
        PryGlowUnreal.onClick.AddListener(()=> {
            DuetGlowSharper.GetInstance().GlowShut = "One";

            UIManager.GetInstance().ShowUIForms(nameof(PryDuetGlowTimid));

            PryDuetGlowSharper.GetInstance().OnGameUpdate();
        });

        UteGlowUnreal.onClick.AddListener(() => {
            DuetGlowSharper.GetInstance().GlowShut = "Two";

            UIManager.GetInstance().ShowUIForms(nameof(UteDuetGlowTimid));

            UteDuetGlowSharper.GetInstance().OnGameUpdate();
        });

        FlareGlowUnreal.onClick.AddListener(() => {
            DuetGlowSharper.GetInstance().GlowShut = "Three";

            UIManager.GetInstance().ShowUIForms(nameof(FlareDuetGlowTimid));

            FlareDuetGlowSharper.GetInstance().OnGameUpdate();

        });

        OmitUnreal.onClick.AddListener(()=> {
            CloseUIForm(nameof(DuetGlowBeltTimid));
        });

        CorrectUnreal.onClick.AddListener(()=> {
            UIManager.GetInstance().ShowUIForms(nameof(DuetCorrectTimid));
        });
    }

    public override void Display(object uiFormParams)
    {
        //BackButton.gameObject.SetActive(false);
        //SettingButton.gameObject.SetActive(false);
        //BgShadow.transform.localScale = Vector3.zero;
        //BtnGroup.transform.localScale = Vector3.zero;
        base.Display(uiFormParams);
        DOVirtual.DelayedCall(0.5f, () => //ͣ��
        {
            m_LaceReef.SliderMoveInit();
        });
        //BgShadow.transform.DOScale(1.2f, 0.8f).SetEase(Ease.Linear) // ���ö�������Ϊ����
        //   .OnComplete(() => {
        //           BgShadow.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear) // ���ö�������Ϊ����
        //           .OnComplete(() => {
        //               m_PageView.SliderMoveInit();
        //           });
        //   });

        //BtnGroup.transform.DOScale(1.2f, 0.8f).SetEase(Ease.Linear) // ���ö�������Ϊ����
        //   .OnComplete(() => {
        //       BtnGroup.transform.DOScale(1f, 0.5f).SetEase(Ease.Linear) // ���ö�������Ϊ����
        //       .OnComplete(() => {
        //           BackButton.gameObject.SetActive(true);
        //           SettingButton.gameObject.SetActive(true);
        //       });
        //   });

        FlareDuetGlowSharper.GetInstance().SoupFlareGlowHordeOnly();

    }

}
