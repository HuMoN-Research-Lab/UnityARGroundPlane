using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class ReloadSceneOnHit : MonoBehaviour
{
    public TrialFeeder main;

    bool beenHit = false;

    float lastHitTime = -1f;

    void OnTriggerEnter(Collider other) {
        if (!beenHit) {
            if (other == null || other.gameObject.name.Contains("TDW") || other.gameObject.GetComponentInParent<RTObjectMarkers>()) {
                beenHit = true;
                lastHitTime = Time.time;
                gameObject.GetComponent<AudioSource>().Play();
                main.StopTiming();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void Update() {
        if (main == null) {
            main = GameObject.Find("ReadTiles").GetComponent<TrialFeeder>();
        }

        if (Time.time - lastHitTime > 10) {
            beenHit = false;
        }
        // testing
        if (Input.GetKeyDown(KeyCode.F)) OnTriggerEnter(null);
    }
}
