using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;
using UXF;

public class ReloadSceneOnHit : MonoBehaviour
{

    public Session UXF_Session = null;
    public TrialBegin tb = null;
    void OnTriggerEnter(Collider other) {
        if (other == null || other.gameObject.name.Contains("TEST") || other.gameObject.GetComponentInParent<RTObjectMarkers>())
     //       Reporter.print("Test?");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            UXF_Session.CurrentTrial.End();
            tb.trialInProgress = false;
    }

    void Update() {
        // testing
        if (Input.GetKeyDown(KeyCode.F)) OnTriggerEnter(null);
    }
}
