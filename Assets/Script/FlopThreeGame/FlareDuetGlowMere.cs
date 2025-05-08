using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlareDuetGlowMere : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("GridLayout")]    public GridLayoutGroup WifeSlight;
[UnityEngine.Serialization.FormerlySerializedAs("DuetBony")]    public GameObject DuetBony;
[UnityEngine.Serialization.FormerlySerializedAs("Board")]    //���
    public GameObject Sound;
    //�����
    private ObjectPool _BoreSeed;
    //����itemlist
    private List<GameObject> _Hand_DuetBony;

    private void Awake()
    {
        _Hand_DuetBony = new();
        DuetSeedBoil();

        
    }

    /// <summary>
    /// ����س�ʼ��
    /// </summary>
    private void DuetSeedBoil()
    {
        _BoreSeed = new ObjectPool();
        _BoreSeed.Init("FlopPool", Sound.transform);
        _BoreSeed.Prefab = DuetBony;
    }

    public void OnGameUpdate()
    {
        SubjectApe();
        OneMemoryFlopGameInit();
    }

    public void OneMemoryFlopGameInit()
    {
        Sound.transform.localPosition = new Vector3(1200, 0, 0);
        Vector2Int board= FlareDuetGlowSharper.GetInstance().PitSurveySound;
        int cell = DuetGlowSharper.GetInstance().MadDuetJazzGarb(board.x, board.y);

        var levelData = FlareDuetGlowSharper.GetInstance().PitHordeIdle; 
        WifeSlight.constraint = GridLayoutGroup.Constraint.Flexible;
        WifeSlight.spacing = new Vector2(10, 10);
        WifeSlight.cellSize = new Vector2(cell, cell);
        //���ð��Ӵ�С
        DuetGlowSharper.GetInstance().DieSoundGarb(board.x, board.y, out int x, out int y);
        Sound.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);

        for (int i = 0; i < board.x ; i++)
        {
            for (int j = 0; j < board.y; j++)
            {
                GameObject obj = _BoreSeed.Get();
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<FlareDuetBony>().FlareDuetBonyBoil(levelData.board[i][j],new Vector2Int(i,j));
                _Hand_DuetBony.Add(obj);
            }
        }

        VirusGlowSoundCup();
    }

    /// <summary>
    /// �������з���
    /// </summary>
    public void SubjectApe()
    {
        for (int i = 0; i < _Hand_DuetBony.Count; i++)
        {
            _BoreSeed.Recycle(_Hand_DuetBony[i]);
        }
        _Hand_DuetBony.Clear();
    }

    /// <summary>
    /// ��ʼʱ����嶯��
    /// </summary>
    public void VirusGlowSoundCup()
    {
        //end��-160   start��1200
        //Debug.Log("------------------��������");

        DuetGlowSharper.GetInstance().SoHaiti = true;
        Sound.transform.DOLocalMoveX(-160, 1f)
            .SetEase(Ease.Linear) // ���ö�������Ϊ����
           .OnComplete(() => {
               
               DuetGlowSharper.GetInstance().LargeDrown();
               ADManager.Instance.NoThanksAddCount(1);
           }); // ����ʱ�����ص�
    }

    public void SheGlowSoundCup(Action action)
    {
        //Debug.Log("------------------�볡����");
        Sound.transform.DOLocalMoveX(-1700, 1f)
            .SetEase(Ease.Linear) // ���ö�������Ϊ����
           .OnComplete(() => {
               action?.Invoke();
               DuetGlowSharper.GetInstance().SoHaiti = true;
           }); // ����ʱ�����ص�
    }
}
