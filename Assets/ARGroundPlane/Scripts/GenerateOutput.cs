using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using QualisysRealTime.Unity;

public class GenerateOutput : MonoBehaviour
{
    [SerializeField]
    private List<RTObject> Trackers;

    [SerializeField]
    private GameObject TargetSpawn;

    private static int TrialCount;
    private XmlWriterSettings XmlSettings;

    private XmlWriter DocWriter;

    private string SessionFolder;

    void Start() {
    //     // add scene exit functionality
    //     SceneManager.sceneUnloaded += EndOfTrial;

    //     // specify output folder
    //     SessionFolder = System.DateTime.Now.ToString("hh.mm.ss.ffffff");
         TrialCount = 0;
        SceneManager.sceneLoaded += FindTargetSpawner;
    //     XmlSettings = new XmlWriterSettings();
    //     XmlSettings.Indent = true;
    //     XmlSettings.IndentChars = "\t";
    //     XmlSettings.ConformanceLevel = ConformanceLevel.Document;
    //     XmlSettings.CheckCharacters = true;
    }

    void FindTargetSpawner(Scene scene, LoadSceneMode mode) {
        TargetSpawn = GameObject.Find("RandomTargetSpawner");
    }

    void Awake() {
        // add scene exit functionality
        SceneManager.sceneUnloaded += EndOfTrial;

        // specify output folder
        SessionFolder = "Subject1";
        //TrialCount = 1;

        XmlSettings = new XmlWriterSettings();
        XmlSettings.Indent = true;
        XmlSettings.IndentChars = "\t";
        XmlSettings.ConformanceLevel = ConformanceLevel.Document;
        XmlSettings.CheckCharacters = true;

        Debug.Log("Awake");
        //TargetSpawn = GameObject.Find("RandomTargetSpawner");

        // open file
        DocWriter = XmlWriter.Create("Trial_" + TrialCount++ + ".xml", XmlSettings);
        // write header
        DocWriter.WriteStartDocument();
        DocWriter.WriteStartElement("Trial");
        // write targets
        DocWriter.WriteStartElement("Targets");
        DetectMarker[] targets = TargetSpawn.GetComponentsInChildren<DetectMarker>();
        foreach(DetectMarker t in targets) {
            DocWriter.WriteStartElement("Target");
            DocWriter.WriteElementString("Position", t.transform.position.ToString());
            DocWriter.WriteEndElement();
        }
        DocWriter.WriteEndElement();
        // open frames list
        DocWriter.WriteStartElement("Frames");

    }

    void Update() {
        // open new xml object for this Frame, id it, open a bodies list object
        if (DocWriter.WriteState == WriteState.Closed || DocWriter.WriteState == WriteState.Error) Awake();
        DocWriter.WriteStartElement("Frame");
        DocWriter.WriteAttributeString("time-passed", Time.time.ToString());
        DocWriter.WriteAttributeString("QTM-Time", RTClient.GetInstance().GetTimeStamp().ToString());
        DocWriter.WriteStartElement("Bodies");

        foreach(RTObject rto in Trackers) {
            // Stuff for every object
            DocWriter.WriteStartElement("Body");
            DocWriter.WriteAttributeString("name", rto.name);
            DocWriter.WriteElementString("Position", rto.GetComponentInChildren<Transform>().position.ToString());
            DocWriter.WriteElementString("Rotation", rto.GetComponentInChildren<Transform>().rotation.eulerAngles.ToString());
            
            // Specific RT data for different types
            // switch (rto) {
            //     case QualisysRealTime.Unity.RTObjectMarkers rtMarkers:
            //         // go through all children of rtMarkers
            //         //DocWriter.WriteStartElement("Markers");
            //         foreach (GameObject m in rtMarkers.GetMarkerGOs()) {
            //             //  DocWriter.WriteStartElement("Marker");
            //             //  DocWriter.WriteAttributeString("name", m.name);
            //             //  DocWriter.WriteElementString("Position", m.transform.position.ToString());
            //             //  DocWriter.WriteEndElement();
            //          }
            //         // close <Markers>
            //         //DocWriter.WriteEndElement();
            //         break;
            //     case QualisysRealTime.Unity.RTObject rtObject:
            //         // we only have the end object pos/rot, not each marker
            //         // already taken care of, this space is for further special output if needed
            //         break;
            //     default:
            //         // "Customized output functionality has not been specified for object type " + typeof(rto) + " such as " + rto.name
            //         break;
            // }

            // Close "Body"
            DocWriter.WriteEndElement();
        }
        // Close "Bodies"
        DocWriter.WriteEndElement();

        // Add hit targets here
        DocWriter.WriteStartElement("ActiveTargets");
        DocWriter.WriteEndElement();

        // Close "Frame"
        DocWriter.WriteEndElement();
    }

    void OnTargetCollision(DetectMarker target) {
        // incorporate target collisions between frames? ASAP? 
    }

    void EndOfTrial<Scene>(Scene s) {
        // close "Frames"
        DocWriter.WriteEndElement();
        // close document
        DocWriter.WriteEndDocument();
        // flush output to file
        try {
            DocWriter.Flush();
            // TODO: Catch?
        } finally {
            if (DocWriter != null)
                DocWriter.Close();
        }
    }
}
