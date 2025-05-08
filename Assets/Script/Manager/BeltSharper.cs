using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltSharper : MonoBehaviour
{
    private void Awake()
    {
        MusicMgr.GetInstance().PlayBg(MusicType.SceneMusic.BGM);
    }
}