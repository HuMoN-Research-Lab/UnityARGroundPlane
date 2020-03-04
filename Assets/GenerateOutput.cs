using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using QualisysRealTime.Unity;

public class GenerateOutput : MonoBehaviour
{
    [SerializeField]
    private List<RTObject> Trackers;

    [SerializeField]
    private TargetSpawnArea TargetSpawn;

    private static int TrialCount;
    private static XmlWriterSettings XmlSettings;

    private XmlWriter DocWriter;

    private string SessionFolder;

    void Start() {
        // specify output folder
        SessionFolder = System.DateTime.Now.ToString("hh.mm.ss.ffffff");
        TrialCount = 1;

        XmlSettings = new XmlWriterSettings();
        XmlSettings.Indent = true;
        XmlSettings.IndentChars = "\t";
        XmlSettings.ConformanceLevel = ConformanceLevel.Document;
        XmlSettings.CheckCharacters = true;
    }

    void Awake() {
        // open file
        DocWriter = XmlWriter.Create("\\" + SessionFolder + "\\Trial_" + TrialCount + ".xml", XmlSettings);
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
        DocWriter.WriteStartElement("Frame");
        DocWriter.WriteAttributeString("time-passed", Time.time.ToString());
        DocWriter.WriteStartElement("Bodies");

        foreach(RTObject rto in Trackers) {
            // Stuff for every object
            DocWriter.WriteStartElement("Body");
            DocWriter.WriteAttributeString("name", rto.name);
            DocWriter.WriteElementString("Position", rto.transform.position.ToString());
            DocWriter.WriteElementString("Rotation", rto.transform.rotation.ToString());
            
            // Specific RT data for different types
            switch (rto) {
                case QualisysRealTime.Unity.RTObjectMarkers rtMarkers:
                    // go through all children of rtMarkers
                    DocWriter.WriteStartElement("Markers");
                    Transform[] markerTransforms = rtMarkers.GetComponentsInChildren<Transform>();
                    foreach (Transform m in markerTransforms) {
                        DocWriter.WriteStartElement("Marker");
                        DocWriter.WriteAttributeString("name", m.name);
                        DocWriter.WriteElementString("Position", m.position.ToString());
                    }
                    // close <Markers>
                    DocWriter.WriteEndElement();
                    break;
                case QualisysRealTime.Unity.RTObject rtObject:
                    // we only have the end object pos/rot, not each marker
                    // already taken care of, this space is for further special output if needed
                    break;
                default:
                    // "Customized output functionality has not been specified for object type " + typeof(rto) + " such as " + rto.name
                    break;
            }

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

    void OnDestroy() {
        // close "Frames"
        DocWriter.WriteEndElement();
        // close "Trial"
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
