using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTESTCAMREFOnLoad : MonoBehaviour
{
    private static DontDestroyTESTCAMREFOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
