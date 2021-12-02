using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyRotatePointOnLoad : MonoBehaviour
{
    private static DontDestroyRotatePointOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}

