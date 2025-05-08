using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuetCorrectTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("SpriteArray")]    public Sprite[] BubbleBiter;
[UnityEngine.Serialization.FormerlySerializedAs("OpenButton")]    public Button WaxyUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("CloseButton")]    public Button TitleUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("OpenButton1")]    public Button WaxyUnreal1;
[UnityEngine.Serialization.FormerlySerializedAs("ConvexSweep")]    public Sprite[] MakeupFaint;
[UnityEngine.Serialization.FormerlySerializedAs("HomeBtn")]    public Button WageLys;
    protected override void Awake()
    {
        base.Awake();
        WaxyUnreal.onClick.AddListener(()=> {
            MusicMgr.GetInstance().EffectMusicSwitch = !MusicMgr.GetInstance().EffectMusicSwitch;
            WaxyUnreal.GetComponent<Image>().sprite = MusicMgr.GetInstance().EffectMusicSwitch ? BubbleBiter[0] : BubbleBiter[1];
        });

        WaxyUnreal1.onClick.AddListener(() => {

            MusicMgr.GetInstance().BgMusicSwitch = !MusicMgr.GetInstance().BgMusicSwitch;
            WaxyUnreal1.GetComponent<Image>().sprite = MusicMgr.GetInstance().BgMusicSwitch ? MakeupFaint[0] : MakeupFaint[1];
        });
        WageLys.onClick.AddListener(() => {
            CloseUIForm(nameof(DuetCorrectTimid));
            UIManager.GetInstance().ShowUIForms(nameof(DuetGlowBeltTimid));
        });
        TitleUnreal.onClick.AddListener(()=> {
            CloseUIForm(nameof(DuetCorrectTimid));
        });
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        WaxyUnreal.GetComponent<Image>().sprite = MusicMgr.GetInstance().EffectMusicSwitch ? BubbleBiter[0] : BubbleBiter[1];
        WaxyUnreal1.GetComponent<Image>().sprite = MusicMgr.GetInstance().BgMusicSwitch ? MakeupFaint[0] : MakeupFaint[1];
    }
}
