using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//using QualisysRealTime.Unity; // eventually we'll want this TDW


// https://docs.unity3d.com/Manual/JSONSerialization.html <- use this TDW 4/7
// and this : https://docs.unity3d.com/2019.4/Documentation/ScriptReference/JsonUtility.ToJson.html

// Stephen: "Make target prefabs serializable."
public class GenJSON : MonoBehaviour
{

    private static int TrialCount = 0;

    [SerializeField]
    private GameObject TargetSpawn;

    void FindTargetSpawner(Scene scene, LoadSceneMode mode) 
    {
        TargetSpawn = GameObject.Find("RandomTargetSpawner");
    }


    void Awake()
    {
        //write whole json file from children
        //Open new writer for file for this trial
        StreamWriter writer = new StreamWriter("TestDir/" + System.DateTime.Now.ToString("MM_dd_yyyy.hh.mm") + "_Trial_" + ++TrialCount + ".json");
        writer.WriteLine("[");
        writer.WriteLine("\"" + System.DateTime.Now.ToString("hh.mm.ss.ffffff") + "\",");
        //grab all of "FloorObjectInfo" from children
        FloorObjectInfo[] allChildren = TargetSpawn.GetComponentsInChildren<FloorObjectInfo>();
        //serialize/write in loop
        for (int i = 0; i < allChildren.Length; i++) {
            writer.WriteLine(JsonUtility.ToJson(allChildren[i]) + (i == allChildren.Length-1?"":","));
        }
        writer.WriteLine("]");
        writer.Close();
    }
}
