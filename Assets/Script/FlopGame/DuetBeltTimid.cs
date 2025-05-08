using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuetBeltTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("PlayButton")]    public Button SomeUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("SettingButton")]    public Button CorrectUnreal;
    protected override void Awake()
    {
        base.Awake();

        SomeUnreal.onClick.AddListener(()=> {
            UIManager.GetInstance().ShowUIForms(nameof(DuetGlowBeltTimid));
        });

        CorrectUnreal.onClick.AddListener(()=> {
            UIManager.GetInstance().ShowUIForms(nameof(DuetCorrectTimid));
        });
    }
}
