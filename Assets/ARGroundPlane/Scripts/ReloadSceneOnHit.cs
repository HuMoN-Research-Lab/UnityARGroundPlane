using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class ReloadSceneOnHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other == null || other.gameObject.GetComponentInParent<RTObjectMarkers>())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update() {
        // testing
        if (Input.GetKeyDown(KeyCode.F)) OnTriggerEnter(null);
    }
}
