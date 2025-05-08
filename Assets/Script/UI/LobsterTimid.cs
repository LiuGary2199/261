using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobsterTimid : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("sliderImage")]    public Image FollowAlive;
[UnityEngine.Serialization.FormerlySerializedAs("progressText")]    public Text OverturnPart;

    // Start is called before the first frame update
    private void Start()
    {
        FollowAlive.fillAmount = 0;
        OverturnPart.text = "0%";
    }

    // Update is called once per frame
    private void Update()
    {
        if (FollowAlive.fillAmount <= 0.8f || NetInfoMgr.instance.ready)
        {
            FollowAlive.fillAmount += Time.deltaTime * .3f;
            OverturnPart.text = (int)(FollowAlive.fillAmount * 100) + "%";
            if (FollowAlive.fillAmount >= 1)
            {
                Destroy(transform.parent.gameObject);
                UIManager.GetInstance().ShowUIForms(nameof(DuetBeltTimid));
                
            }
        }
    }
}
