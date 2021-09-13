using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BlockTrialOutput : MonoBehaviour
{

    public TrialFeeder trialFeeder;

    private int subjectID = -1;

    StreamWriter writer = null;
    
    
    void Awake() {
        // happens start of each trial
        
    }
    
    void Start()
    {
        // place random block order as first line
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        subjectID = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        writer = new StreamWriter("DataOutput/Subject_" + subjectID + "_TrialOrder.json");
        writer.Write("[");
        for (int i = 0; i < trialFeeder.RandomBlockOrder.Count; i++) {
            writer.Write("" + trialFeeder.RandomBlockOrder[i]);
            if (i != trialFeeder.RandomBlockOrder.Count-1) writer.Write(",");
        }
        writer.WriteLine("]");
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteString(string s) {
        File.AppendAllText("DataOutput/Subject_" + subjectID + "_TrialOrder.json", s);
    }
}
