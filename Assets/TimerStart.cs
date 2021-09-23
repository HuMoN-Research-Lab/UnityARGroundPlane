using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class TimerStart : MonoBehaviour
{

    public TrialFeeder main;
    
    bool beenHit = false;
    void OnTriggerEnter(Collider other) {
        if (!beenHit) {
            if (other == null || other.gameObject.name.Contains("TDW") || other.gameObject.GetComponentInParent<RTObjectMarkers>()) {
                beenHit = true;
                main.StartTiming();
            }
        }

        beenHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (main == null) {
            main = GameObject.Find("ReadTiles").GetComponent<TrialFeeder>();
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            OnTriggerEnter(null);
        }
    }
}
