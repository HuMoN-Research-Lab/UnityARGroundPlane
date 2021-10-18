using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class TimerStart : MonoBehaviour
{

    public TrialFeeder main;
    
    bool beenHit = false;
    float lastHitTime = -1f;
    void OnTriggerEnter(Collider other) {
        if (!beenHit) {
            if (other == null || other.gameObject.name.Contains("TDW") || other.gameObject.GetComponentInParent<RTObjectMarkers>()) {
                beenHit = true;
                lastHitTime = Time.time;
                //Debug.Log("TimerStart");
                gameObject.GetComponent<AudioSource>().Play();
                main.StartTiming();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (main == null) {
            main = GameObject.Find("ReadTiles").GetComponent<TrialFeeder>();
        }

        if (Time.time - lastHitTime > 5) {
            beenHit = false;
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            OnTriggerEnter(null);
        }
    }
}
