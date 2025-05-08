using UnityEngine;
using System;
using System.Collections;

public class TimeManager : MonoSingleton<TimeManager>
{
    private const string LastCheckDayKey = "LastCheckDay";
    private const string LastOnlineKey = "LastOnline";

    //获取Unix时间戳（以秒为单位）
    public long GetUnixTimeStamp()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    //获取离线时间（以秒为单位）
    public int GetOfflineTime()
    {
        if (PlayerPrefs.HasKey(LastOnlineKey))
        {
            long lastOnline = long.Parse(PlayerPrefs.GetString(LastOnlineKey));
            return (int)(GetUnixTimeStamp() - lastOnline);
        }
        else
            return 0;
    }

    //秒转时分秒
    public string SecondToTime(int Second, int Type = 0)
    {
        int hour = Second / 3600;
        int minute = (Second % 3600) / 60;
        int second2 = Second % 60;
        if (Type == 1)
            return string.Format("{0:D2}:{1:D2}", minute, second2);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second2);
    }

    //判断是否过了新的一天
    public bool HasNewDayPassed()
    {
        DateTime now = DateTime.Now.Date;
        if (PlayerPrefs.HasKey(LastCheckDayKey))
        {
            DateTime lastCheckDay = DateTime.Parse(PlayerPrefs.GetString(LastCheckDayKey));
            if (lastCheckDay != now)
            {
                UpdateLastCheckDay();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            UpdateLastCheckDay();
            return true;
        }
    }
    //判断是否到了新的一个月
    public bool HasNewMonthPassed()
    {
        DateTime now = DateTime.Now.Date;
        if (PlayerPrefs.HasKey(LastCheckDayKey))
        {
            DateTime lastCheckDay = DateTime.Parse(PlayerPrefs.GetString(LastCheckDayKey));
            if (lastCheckDay.Month != now.Month)
            {
                UpdateLastCheckDay();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            UpdateLastCheckDay();
            return true;
        }
    }

    //更新最后检查日期
    private void UpdateLastCheckDay()
    {
        PlayerPrefs.SetString(LastCheckDayKey, DateTime.Now.Date.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();
    }

    //更新最后在线时间
    void OnApplicationQuit()
    {
        UpdateLastOnlineTime();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            UpdateLastOnlineTime();
    }
    private void UpdateLastOnlineTime()
    {
        PlayerPrefs.SetString(LastOnlineKey, GetUnixTimeStamp().ToString());
        PlayerPrefs.Save();
    }

    //  延时
    public Coroutine Delay(float delay, Action action)
    {
        return StartCoroutine(DelayIE(delay, action));
    }
    IEnumerator DelayIE(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
    //  延时-不受TimeScale影响
    public Coroutine Delay_RealTime(float delay, Action action)
    {
        return StartCoroutine(DelayRealTimeIE(delay, action));
    }
    IEnumerator DelayRealTimeIE(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }
    // 终止延时
    public void StopDelay(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}