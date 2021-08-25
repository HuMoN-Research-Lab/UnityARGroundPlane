using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
//using QualisysRealTime.Unity; // eventually we'll want this TDW


// https://docs.unity3d.com/Manual/JSONSerialization.html <- use this TDW 4/7
// and this : https://docs.unity3d.com/2019.4/Documentation/ScriptReference/JsonUtility.ToJson.html

// Stephen: "Make target prefabs serializable."
public class GenJSON : MonoBehaviour
{

    private static int TrialCount = 1;

    [SerializeField]
    private GameObject AllTargetSpawners;

    void FindTargetSpawner() 
    {
        AllTargetSpawners = GameObject.Find("SpawnTiles");
    }

    void Update() {
        FindTargetSpawner();
        if (Input.GetKeyDown(KeyCode.K)) SaveTrial();
    }

    void Awake() {
        
    }

    public void SaveTrial()
    {
        //write whole json file from children
        //Open new writer for file for this trial
        StreamWriter writer = null;

        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        string vis_text = GameObject.Find("DropdownVis").GetComponentInChildren<Text>().text;
        string bio_text = GameObject.Find("DropdownBio").GetComponentInChildren<Text>().text;
        try {
            writer = new StreamWriter("DataOutput/" + cur_time + "_Trial_" + TrialCount + "_" + vis_text + "_" + bio_text + ".json");
        } catch {
            //create /DataOutput
            Directory.CreateDirectory("DataOutput");
            writer = new StreamWriter("DataOutput/" + cur_time + "_Trial_" + TrialCount + "_" + vis_text + "_" + bio_text + ".json");
        }
        TrialCount++;
        writer.WriteLine("[");
        writer.WriteLine("\"" + cur_time + "\",");
        //grab all of "FloorObjectInfo" from children
        FloorObjectInfo[] allChildren = AllTargetSpawners.GetComponentsInChildren<FloorObjectInfo>();
        //Debug.Log(allChildren);
        //serialize/write in loop
        for (int i = 0; i < allChildren.Length; i++) {
            writer.WriteLine(JsonUtility.ToJson(allChildren[i]) + (i == allChildren.Length-1?"":","));
        }
        writer.WriteLine("]");
        writer.Close();
    }
}
