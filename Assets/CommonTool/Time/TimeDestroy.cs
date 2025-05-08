using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    public float time = 1;
    void Start()
    {
        Invoke("Destroy", time);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }

}
