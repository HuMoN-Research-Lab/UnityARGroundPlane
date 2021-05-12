using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using QualisysRealTime.Unity; // eventually we'll want this TDW
using System.Text.Json;

// https://docs.unity3d.com/Manual/JSONSerialization.html <- use this TDW 4/7
// and this : https://docs.unity3d.com/2019.4/Documentation/ScriptReference/JsonUtility.ToJson.html

// Stephen: "Make target prefabs serializable."
public class GenJSON : MonoBehaviour
{

    private static int TrialCount;

    [SerializeField]
    private GameObject TargetSpawn;
    // Start is called before the first frame update
    void Start()
    { 
        // specify output folder
        // SessionFolder = System.DateTime.Now.ToString("hh.mm.ss.ffffff");
        TrialCount = 1;
        SceneManager.sceneLoaded += FindTargetSpawner;

    }
    void FindTargetSpawner(Scene scene, LoadSceneMode mode) 
    {
        TargetSpawn = GameObject.Find("RandomTargetSpawner");
    }


    void Awake()
    {
        //write whole json file from children
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
