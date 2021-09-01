using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BlockTrialOutput : MonoBehaviour
{

    public TrialFeeder trialFeeder;

    private bool starting = true;

    StreamWriter writer = null;
    
    
    void Awake() {
        if (starting) {
            writer = new StreamWriter("Subject_X_TrialOrder.json");
            starting = false;
        }
        // happens start of each trial
        
    }
    
    void Start()
    {
        // place random block order as first line
        writer.WriteLine(trialFeeder.RandomBlockOrder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseWriter() {
        writer.Close();
    }
}
