using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class ReloadSceneOnHit : MonoBehaviour
{
    public TrialFeeder main;

    public AudioSource TrialEndSound;

    bool beenHit = false;

    float lastHitTime = -1f;

    void OnTriggerEnter(Collider other) {
        if (!beenHit) {
            if (other == null || other.gameObject.name.Contains("TDW") || other.gameObject.GetComponentInParent<RTObjectMarkers>()) {
                beenHit = true;
                lastHitTime = Time.time;
                TrialEndSound.Play();
                main.StopTiming();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void Update() {
        if (main == null) {
            main = GameObject.Find("ReadTiles").GetComponent<TrialFeeder>();
        }

        if (Time.time - lastHitTime > 5) {
            beenHit = false;
        }
        // testing
        if (Input.GetKeyDown(KeyCode.F)) OnTriggerEnter(null);
    }
}
