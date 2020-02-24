using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCAVEOnLoad : MonoBehaviour
{
    private static DontDestroyCAVEOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
