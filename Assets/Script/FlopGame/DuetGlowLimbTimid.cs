using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuetGlowLimbTimid : BaseUIForms
{
[UnityEngine.Serialization.FormerlySerializedAs("CloseButton")]    public Button TitleUnreal;
[UnityEngine.Serialization.FormerlySerializedAs("List_ScoreText")]    public List<Text> Bulb_HoneyPart;
    protected override void Awake()
    {
        base.Awake();
        TitleUnreal.onClick.AddListener(()=> {
            CloseUIForm("DuetGlowLimbTimid");
        });
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        List<int> tempList = PryDuetGlowSharper.GetInstance().Bulb_ConsultHoney;
        Debug.Log("-----------------   " + tempList.Count  + "    -------    "+ Bulb_HoneyPart.Count);
        for (int i = 0; i < tempList.Count; i++)
        {
            //List_ScoreText[i].gameObject.SetActive(tempList[i] != 0);
            Bulb_HoneyPart[i].text = tempList[i].ToString();
        }
    }
}
