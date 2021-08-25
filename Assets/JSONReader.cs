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
        // test w/ C:\Users\Matthis Lab\Documents\GitHub\UnityARGroundPlane\DataOutput\1629814433_Trial_1.json
        // TODO: Grab iteratively
        StreamReader reader = new StreamReader("DataOutput/1629814433_Trial_1.json");
        floorObjects = new List<FloorObjectInfo>();
        // serialize; ignore first item, loop through rest and place until line read is '[' - make this a function to call for scene switching
        ParseJSONFile(reader);
    }

    // Update is called once per frame
    public FloorObjectInfo CreateFromJSON(string jString) {
        //{"position":{"x":-1.2431681156158448,"y":0.0010050020646303893,"z":-0.4258761405944824},"yRotation":0.0,"type":"obstacle"}
        char[] separators = new char[] { '{', '}', ':', ',', '"' };
        string[] brokenLine = jString.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
        //Debug.Log(brokenLine);
        Vector3 tempPosition = new Vector3();
        float tempYRot = 0.0f;
        string tempType = "";
        for (int i = 0; i < brokenLine.Length; i++) {
            switch(brokenLine[i]) {
                case "position":
                    //nothing
                    break;
                case "x":
                    tempPosition.x = float.Parse(brokenLine[i+1]);
                    break;
                case "y":
                    tempPosition.y = float.Parse(brokenLine[i+1]);
                    break;
                case "z":
                    tempPosition.z = float.Parse(brokenLine[i+1]);
                    break;
                case ",":
                    break;
                case "yRotation":
                    tempYRot = float.Parse(brokenLine[i+1]);
                    break;
                case "type":
                    tempType = brokenLine[i+1];
                    break;
                default:
                    break;
            }
        }

        GameObject targetInstance = Instantiate(TargetPrefab, Vector3.zero, Quaternion.identity);
        FloorObjectInfo foi = targetInstance.GetComponent<FloorObjectInfo>();
        DetectMarker dm = targetInstance.GetComponent<DetectMarker>();
        //Debug.Log(tempPosition);

        foi.position = tempPosition;
        foi.yRotation = tempYRot;
        foi.ApplyInfo();
        
        foi.type = tempType;
        if (tempType.Equals("target")) { 
            dm.SetMaterial(TargetMat);
            //dm.SetAudioFeedback(Sounds[])
        } else {
            dm.SetMaterial(ObstacleMat);
            //dm.SetAudioFeedback(Sounds[])
        }

        //dm.targetJoints = HitJoints;

        targetInstance.transform.SetParent(transform);
        
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
