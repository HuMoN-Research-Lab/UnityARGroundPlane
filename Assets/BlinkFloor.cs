using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BlinkFloor : MonoBehaviour
{

    int blinkingCount = 0;
    bool toBlack = true;

    public float lerpRate = .1f;

    float currentLerp = 0;

    private int subjectID;

    Renderer r;

    // Start is called before the first frame update
    void Awake()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        subjectID = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        //TODO: Grab name from UI?
        StreamWriter writer = new StreamWriter("DataOutput/" + subjectID + "_QualisysPupilCalibrationTimestamps.json");
        writer.Close();

        r = gameObject.GetComponent<MeshRenderer>();
        r.material.color = Color.white;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (blinkingCount < 6) {
            currentLerp += lerpRate;
            if (toBlack) {
                if (currentLerp > 1) {
                    //reset
                    toBlack = false;
                    currentLerp = 0;
                    blinkingCount++;

                    System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                    int epoch = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                    File.AppendAllText("DataOutput/" + subjectID + "_QualisysPupilCalibrationTimestamps.json", epoch + "\n");
                } else {
                    r.material.color = Color.Lerp(Color.white, Color.black, currentLerp);
                }
            } else {
                if (currentLerp > 1) {
                    //reset
                    toBlack = true;
                    currentLerp = 0;
                    blinkingCount++;
                    System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                    int epoch = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                    File.AppendAllText("DataOutput/" + subjectID + "_QualisysPupilCalibrationTimestamps.json", epoch + "\n");
                } else {
                    r.material.color = Color.Lerp(Color.black, Color.white, currentLerp);
                }
            }
        }
    }
}
