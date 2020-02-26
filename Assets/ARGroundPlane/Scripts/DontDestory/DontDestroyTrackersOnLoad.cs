using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTrackersOnLoad : MonoBehaviour
{
    private static DontDestroyTrackersOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
