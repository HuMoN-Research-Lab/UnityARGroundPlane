using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QualisysRealTime.Unity;

public class SubjectProximityReaction : MonoBehaviour
{
    public GameObject movingSubject;

    public Material black;
    public Material red;

    public double proximity = .5; //meters

    void Update() {
        Vector2 self2D = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 mSub2D = new Vector2(movingSubject.transform.position.x, movingSubject.transform.position.z);
        float dist2D = Vector2.Distance(self2D, mSub2D);

        MeshRenderer mr = GetComponent<MeshRenderer>();

        if(dist2D < proximity) {
            //set red
            mr.material = red;
        } else {
            //set black
            mr.material = black;
        }
    }
    
}
