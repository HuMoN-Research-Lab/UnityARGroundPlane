using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySceneLoaderOnLoad : MonoBehaviour
{
    private static DontDestroySceneLoaderOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
