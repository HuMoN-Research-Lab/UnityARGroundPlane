using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCanvasOnLoad : MonoBehaviour
{
    private static DontDestroyCanvasOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
