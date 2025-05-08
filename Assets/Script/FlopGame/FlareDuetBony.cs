using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlareDuetBony : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
[UnityEngine.Serialization.FormerlySerializedAs("FrontObj")]    public GameObject SteamToo;
[UnityEngine.Serialization.FormerlySerializedAs("StartObj")]    public GameObject VirusToo;
[UnityEngine.Serialization.FormerlySerializedAs("EndObj")]    public GameObject SheToo;
[UnityEngine.Serialization.FormerlySerializedAs("CircleObj")]    public GameObject WorkerToo;
[UnityEngine.Serialization.FormerlySerializedAs("LineObj")]    public GameObject PoreToo;
[UnityEngine.Serialization.FormerlySerializedAs("SelectBtn")]    public Button SurveyLys;
[UnityEngine.Serialization.FormerlySerializedAs("BlockPos")]    public Vector2Int BlendHut;
    private int _Jury;
    private int _SayShut;
    /// <summary>
    /// �Ƿ����洢
    /// </summary>
    private bool _BeInitial;

    private void Awake()
    {
       
    }
    public void OnBlockInit()
    {
        _SayShut = 1;
        _BeInitial = false;
        SteamToo.SetActive(_Jury == 1);
        VirusToo.SetActive(_Jury == 2);
        WorkerToo.SetActive(_Jury == 0);
        SheToo.SetActive(false);
        PoreToo.SetActive(false);
    }

    public void FlareDuetBonyBoil(int type,Vector2Int pos)
    {
        _Jury = type;
        _SayShut = type;
        BlendHut = pos;
        SteamToo.SetActive(type == 1);
        VirusToo.SetActive(type == 2);
        WorkerToo.SetActive(type == 0);
        SheToo.SetActive(false);
        PoreToo.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("����    " + _BeInitial);
        if ( _BeInitial || _SayShut == 2)
        {
            FlareDuetGlowSharper.GetInstance().SoSurveyDoll = true;
            FlareDuetGlowSharper.GetInstance().SurveyNutAxBulb(this);
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("̧��");
        FlareDuetGlowSharper.GetInstance().SoSurveyDoll = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (FlareDuetGlowSharper.GetInstance().SoSurveyDoll && _Jury != 1)
        {
            //Debug.Log("��������");
            FlareDuetGlowSharper.GetInstance().SurveyNutAxBulb(this);
            
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (FlareDuetGlowSharper.GetInstance().SoSurveyDoll && _Jury != 1)
        {
            //Debug.Log("�����뿪");
            //OnSetBlockState(false);
        }
    }

    

    /// <summary>
    /// ���÷���״̬
    /// </summary>
    /// <param name="isHead">ture Ϊͷ����false Ϊ����</param>
    public void OnSetBlockState(bool isHead,bool isStorage)
    {
        _SayShut = isHead ? 2 : 1;
        _BeInitial = isStorage;
        VirusToo.SetActive(isHead);
        SheToo.SetActive(!isHead);
    }

    public void OnSetLinePosition(int Rotation)
    {
        PoreToo.SetActive(Rotation != 1);
        PoreToo.transform.localRotation = new Quaternion(0,0,Rotation,0);
    }
    
}
