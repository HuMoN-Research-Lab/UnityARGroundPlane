using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTESTBALLOnLoad : MonoBehaviour
{
    private static DontDestroyTESTBALLOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
