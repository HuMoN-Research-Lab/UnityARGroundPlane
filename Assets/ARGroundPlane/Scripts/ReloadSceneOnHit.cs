﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class ReloadSceneOnHit : MonoBehaviour
{
    public TrialFeeder main;

    bool beenHit = false;

    void OnTriggerEnter(Collider other) {
        if (!beenHit) {
            if (other == null || other.gameObject.name.Contains("TDW") || other.gameObject.GetComponentInParent<RTObjectMarkers>()) {
                beenHit = true;
                main.StopTiming();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        beenHit = false;
    }

    void Update() {
        if (main == null) {
            main = GameObject.Find("ReadTiles").GetComponent<TrialFeeder>();
        }
        // testing
        if (Input.GetKeyDown(KeyCode.F)) OnTriggerEnter(null);
    }
}
