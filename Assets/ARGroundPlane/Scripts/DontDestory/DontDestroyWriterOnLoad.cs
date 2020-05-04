using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyWriterOnLoad : MonoBehaviour
{
    private static DontDestroyWriterOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
