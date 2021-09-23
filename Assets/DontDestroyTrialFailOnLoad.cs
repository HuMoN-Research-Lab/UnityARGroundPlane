using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTrialFailOnLoad : MonoBehaviour
{
    private static DontDestroyTrialFailOnLoad selfRef;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gameObject.SetActive(false);
        if (selfRef == null)
            selfRef = this;
        else
            Destroy(gameObject);
    }
}
