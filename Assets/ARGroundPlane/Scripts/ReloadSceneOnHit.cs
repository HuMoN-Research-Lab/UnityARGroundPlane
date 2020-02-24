using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update() {
        // testing
        if (Input.GetKeyDown(KeyCode.F)) OnCollisionEnter(new Collision());
    }
}
