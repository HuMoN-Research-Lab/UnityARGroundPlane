using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPropsOnLoad : MonoBehaviour
{
    private static DontDestroyPropsOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
