using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour
{

    [Tooltip("The gameobject/prefab to spawn.")]
    [SerializeField]
    private GameObject TargetPrefab;

    [Tooltip("Width of targets in meters.")]
    [SerializeField]
    private float TargetWidth = 0.1f;

    public Material ObstacleMat, TargetMat;

    private List<FloorObjectInfo> floorObjects;

    // Start is called before the first frame update
    void Awake() {
        // grab first JSON file from DataOutput
        // test w/ C:\Users\Matthis Lab\Documents\GitHub\UnityARGroundPlane\DataOutput\1628604831_Trial_5.json
        // TODO: Grab iteratively
        StreamReader reader = new StreamReader("DataOutput/1628604831_Trial_5.json");
        floorObjects = new List<FloorObjectInfo>();
        // serialize; ignore first item, loop through rest and place until line read is '[' - make this a function to call for scene switching
        ParseJSONFile(reader);
    }

    // Update is called once per frame
    public FloorObjectInfo CreateFromJSON(string jString) {
        //return JsonUtility.FromJson<FloorObjectInfo>(jString);
        Debug.Log(jString);
        string[] brokenLine = jString.Split(',');
        Debug.Log(brokenLine);
        GameObject targetInstance = Instantiate(TargetPrefab, Vector3.zero, Quaternion.identity);
        return targetInstance.GetComponent<FloorObjectInfo>();
    }

    private void ParseJSONFile(StreamReader sr) {
        sr.ReadLine(); // always '[' to start
        sr.ReadLine(); // always timestamp
        string line;
        while ((line = sr.ReadLine()) != "]") {
            //remove comma
            line = line.Remove(line.Length-1);
            //call 'Create...' and add to list
            floorObjects.Add(CreateFromJSON(line));
        }

    }
}
