using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBlockEndOnLoad : MonoBehaviour
{
    private static DontDestroyBlockEndOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
