/**
 * 
 * 左右滑动的页面视图
 * 
 * ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Unity.VisualScripting;

public class PageView : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    //scrollview
    public ScrollRect m_ScrollRect;
    //求出每页的临界角，页索引从0开始
    List<float> posList = new List<float>();
    //是否拖拽结束
    public bool isDrag = false;
    bool stopMove = true;
    //滑动的起始坐标  
    float targetHorizontal = 0;
    float startDragHorizontal;
    float startTime = 0f;
    //滑动速度  
    public float smooting = 1f;
    public float sensitivity = 0.3f;
    //页面改变
    public Action<int> OnPageChange;
    //当前页面下标
    int currentPageIndex = -1;
    int clickindex = 0;
    public List<GameObject> PointList;

    public Button SubBtn;
    public Button AddBtn;
    public Text m_GameInfo;
    public Text m_GameSmallInfo;
    string[] m_Infostr;
    string[] m_InfoSmallstr;
    public Button GameBtn1;
    public Button GameBtn2;
    public Button GameBtn3;

    public List<GameObject> m_SelectBtn;
    void Start()
    {
    }

    public void  SliderMoveInit()
    {
        m_Infostr = new string[3];
        // 为数组元素赋值
        m_Infostr[0] = "FlashMind";
        m_Infostr[1] = "Beanline";
        m_Infostr[2] = "IconicMind";

        m_InfoSmallstr = new string[3];
        // 为数组元素赋值
        m_InfoSmallstr[0] = "Memorize randomly flipped tiles within 2 seconds, then tap the correct ones to score. Test your focus and speed!";
        m_InfoSmallstr[1] = "Draw a single unbroken line to connect all scattered beans—no overlaps allowed! Test your path-planning skills.";
        m_InfoSmallstr[2] = "Flip tiles to find matching icon pairs in this memory challenge! Match all before time runs out.";
        SubBtn.onClick.RemoveAllListeners();
        AddBtn.onClick.RemoveAllListeners();
        GameBtn1.onClick.RemoveAllListeners();
        GameBtn2.onClick.RemoveAllListeners();
        GameBtn3.onClick.RemoveAllListeners();
        SubBtn.onClick.AddListener(() =>
        {
            ClickChangePage(-1);
        });
        AddBtn.onClick.AddListener(() =>
        {
            ClickChangePage(1);
        });

        GameBtn1.onClick.AddListener(() =>
        {
            ClickChangePageBtn(0);
        });
        GameBtn2.onClick.AddListener(() =>
        {
            ClickChangePageBtn(1);
        });
        GameBtn3.onClick.AddListener(() =>
        {
            ClickChangePageBtn(2);
        });

        float horizontalLength = m_ScrollRect.content.rect.width - this.GetComponent<RectTransform>().rect.width;
        posList.Add(0);
        for (int i = 1; i < m_ScrollRect.content.childCount - 1; i++)
        {
            posList.Add(GetComponent<RectTransform>().rect.width * i / horizontalLength);
        }
        posList.Add(1);
        SetPageIndex(0);

        m_ScrollRect.horizontalNormalizedPosition = posList[0];
    }
   private void ClickChangePage(int mathfNumber)
    {
        if ((clickindex + mathfNumber < 0) || (clickindex + mathfNumber > 2))
        {
            Debug.Log("超出索引");
            return;
        }
        else
        {
            startTime = 0f;
            isDrag = false;
            stopMove = false;
            clickindex = clickindex + mathfNumber;
            targetHorizontal = posList[clickindex];
            SetPageIndex(clickindex);
        }
    }

    private void ClickChangePageBtn(int mathfNumber)
    {
        startTime = 0f;
        isDrag = false;
        stopMove = false;
        targetHorizontal = posList[mathfNumber];
        SetPageIndex(mathfNumber);
    }
    void Update()
    {
        if(!isDrag && !stopMove)
        {
            startTime += Time.deltaTime;
            float t = startTime * smooting;
            m_ScrollRect.horizontalNormalizedPosition = Mathf.Lerp(m_ScrollRect.horizontalNormalizedPosition, targetHorizontal, t);
            if (t >= 1)
            {
                stopMove = true;
            }
        }
    }
    /// <summary>
    /// 设置页面的index下标
    /// </summary>
    /// <param name="index"></param>
    void SetPageIndex(int index)
    {
        if (currentPageIndex != index)
        {
            currentPageIndex = index;
            clickindex = index;
            for (int i = 0; i < PointList.Count; i++)
            {
                if (i == index)
                {
                    PointList[i].SetActive(true);
                }
                else
                {
                    PointList[i].SetActive(false);
                }
            }
            for (int i = 0; i < m_SelectBtn.Count; i++)
            {
                if (i == index)
                {
                    m_SelectBtn[i].SetActive(true);
                }
                else
                {
                    m_SelectBtn[i].SetActive(false);
                }
            }
            m_GameInfo.text = m_Infostr[currentPageIndex].ToString();
            m_GameSmallInfo.text = m_InfoSmallstr[currentPageIndex].ToString();

            if (OnPageChange != null)
            {
                OnPageChange(index);
            }
        }
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        startDragHorizontal = m_ScrollRect.horizontalNormalizedPosition;
    }
    /// <summary>
    /// 拖拽结束
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        float posX = m_ScrollRect.horizontalNormalizedPosition;
        posX += ((posX - startDragHorizontal) * sensitivity);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;
        float offset = Mathf.Abs(posList[index] - posX);
        for(int i = 0; i < posList.Count; i++)
        {
            float temp = Mathf.Abs(posList[i] - posX);
            if (temp < offset)
            {
                index = i;
                offset = temp;
            }
        }
        SetPageIndex(index);
        targetHorizontal = posList[index];
        isDrag = false;
        startTime = 0f;
        stopMove = false;
    }
}
