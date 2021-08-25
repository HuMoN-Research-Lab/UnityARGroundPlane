using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyReadTilesOnLoad : MonoBehaviour
{
    private static DontDestroyReadTilesOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
