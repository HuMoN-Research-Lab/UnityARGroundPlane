using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UXF;

public class TrialBegin : MonoBehaviour
{
    public Session UXF_Session;
    public GameObject Spawner;

    public bool trialInProgress = false;



    // Update is called once per frame
    void Update()
    {
        if (!trialInProgress && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Key Hit");
            Block block = UXF_Session.CreateBlock();
            Trial trial = block.CreateTrial();
            SceneManager.LoadScene("TrialStructure", LoadSceneMode.Additive);
            //trial.settings.SetValue("ListTargetsAndObstacles", Spawner.GetComponentInChildren<ChildTargetTracker>());
            UXF_Session.BeginNextTrial();
            trialInProgress = true;
        }
    }

    public void AddScene(Scene s) {
         SceneManager.LoadScene(s.name, LoadSceneMode.Additive);
    }
}
