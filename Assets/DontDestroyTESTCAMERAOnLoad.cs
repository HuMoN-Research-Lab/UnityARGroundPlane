using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTESTCAMERAOnLoad : MonoBehaviour
{
    private static DontDestroyTESTCAMERAOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
