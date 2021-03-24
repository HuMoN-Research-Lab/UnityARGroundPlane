using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class ChildTargetTracker : Tracker
{
    protected override void SetupDescriptorAndHeader()
    {
        measurementDescriptor = "child_list";
        
        customHeader = new string[]
        {
            "child_list"
        };
    }

    protected override UXFDataRow GetCurrentValues()
    {
        var values = new UXFDataRow()
        {
            ("child_list", ChildStringsList())
        };

        return values;
    }

    private List<string> ChildStringsList() {
        List<Transform> transforms = new List<Transform>(GetComponentsInChildren<Transform>());
        List<string> retVals = new List<string>();
        foreach (Transform t in transforms) {
            string mat = t.gameObject.GetComponent<Renderer>().material.name;
            string targetmat = gameObject.GetComponent<TargetSpawnArea>().TargetMat.name;
            retVals.Add(t.position.ToString() + ", " + (mat.Equals(targetmat)?"target":"obstacle"));
        }
        return retVals;
    }
}
