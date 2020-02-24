using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitboxSwitcher : MonoBehaviour
{
    public GameObject hbA;
    public GameObject hbB;

    void Awake() {
        hbA.SetActive(true);
        hbB.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SwitchHitboxActive();
    }

    void SwitchHitboxActive() {
        if (hbA != null) hbA.SetActive(!hbA.activeSelf);
        if (hbB != null) hbB.SetActive(!hbB.activeSelf);
    }
}
