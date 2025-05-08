using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuetGlowSharper : MonoSingleton<DuetGlowSharper>
{
    public string GlowShut;
    /// <summary>
    /// ��ʱ�䣨�룩
    /// </summary>
    public float BroodMuch;
    public float SmelterMuch;     // ��ǰʣ��ʱ��

    public bool BeHaiti;

    public bool SoHaiti    {
        get {
            return BeHaiti;
        }
        set {
            BeHaiti = value;
            OnGamePause();
        }
    }

    /// <summary>
    /// ��ʼ����չʾ
    /// </summary>
    public bool SoWaxyBlendKeel;
    protected override void Awake()
    {
        base.Awake();
        BroodMuch = 60f;
    }


    public void OnTimeUpdate()
    {
        KeyValuesUpdate key = new KeyValuesUpdate("TimeUpdate", SmelterMuch);
        MessageCenter.SendMessage("TimeUpdate", key);
    }

    public void OnGamePause()
    {
        KeyValuesUpdate key = new KeyValuesUpdate("PauseUpdate", BeHaiti);
        MessageCenter.SendMessage("PauseUpdate", key);
    }

    // ��ѡ�����õ���ʱ
    public void LargeDrown()
    {
        SmelterMuch = BroodMuch;
        SoHaiti = false;
        StopAllCoroutines(); // ֹͣ��ǰЭ��
        StartCoroutine(LoathsomeCountry()); // ��������
    }

    public void OnTimeClose()
    {
        SmelterMuch = BroodMuch;
        SoHaiti = false;
        StopAllCoroutines(); // ֹͣ��ǰЭ��
    }

    IEnumerator LoathsomeCountry()
    {
        while (SmelterMuch > 0 )
        {
            if (!SoHaiti)
            {
                OnTimeUpdate();
                // �ȴ� 1 �루�� Time.timeScale Ӱ�죩
                yield return new WaitForSeconds(1f);
                // ����ʱ��
                SmelterMuch -= 1f;

            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
        Debug.Log("Time's up!");
        // ���������ﴥ���¼���������Ϸ������
        if (SmelterMuch <= 0)
        {
            if (GlowShut == "One")
            {
                PryDuetGlowSharper.GetInstance().OnCheckGame();
            }
            else if (GlowShut == "Two")
            {
                UteDuetGlowSharper.GetInstance().OnCheckGame();
            }
            else if (GlowShut == "Three")
            {
                FlareDuetGlowSharper.GetInstance().OnCheckGame();
            }
        }
    }

    //item 4x4  150   5x5  150  6x6  125
    //Rect 4x4  650*650   4x5 650*800  5x5 850*800
    //     5*6  700*810   6x6 810*810
    /// <summary>
    /// ���ð��Ӵ�С
    /// </summary>
    public void DieSoundGarb(int xcount, int ycount, out int x, out int y)
    {
        int size = MadDuetJazzGarb(xcount, ycount);
        x = xcount * size + 50;
        y = ycount * size + 50;
    }

    /// <summary>
    /// ��ȡ������cell��СFlopX
    /// </summary>
    /// <returns></returns>
    public int MadDuetJazzGarb(int x, int y)
    {
        return (x > 5 || y > 5) ? 125 : 150;
    }
}
