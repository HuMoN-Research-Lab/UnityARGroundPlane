using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitByJoint : MonoBehaviour
{
    [Tooltip("Distance from floor to relevant joint when grounded in meters. ie, distance from marker over foot to floor. Defualt: height for SR_LToeTip to barely hit.")]
    public float heightFromFloor = 0.013f;
    [Tooltip("Joint name we are looking for the target (self) to collide with. Default: joint name from backflip demo.")]
    public string targetJoint = "SR_LToeTip";


    private AudioSource audioFeedbackSource;

    void Awake() {
        audioFeedbackSource = gameObject.GetComponent<AudioSource>();
        // object prefab starts at 0 y scale, make it our desired height off the floor
        gameObject.transform.localScale += new Vector3(0, heightFromFloor * 2f, 0);
    }

    void OnTriggerEnter(Collider other) {
        // grab time of event asap
        string timeString = System.DateTime.Now.ToString("hh.mm.ss.ffffff");
        // disregard when not the name we're looking for
        if (!string.Equals(other.gameObject.name, targetJoint)) return;
        // play sound as soon as verified
        audioFeedbackSource.Play();
        // calculate distance from center of target object to center of colliding joint
        Vector3 selfPos = this.transform.position;
        Vector3 hitPos = other.transform.position;
        float dist3D = Vector3.Distance(selfPos, hitPos);
        // calculate distance from center of target object to center of colliding join on the x,z plane
        Vector2 self2D = new Vector2(selfPos.x, selfPos.z);
        Vector2 hit2D = new Vector2(hitPos.x, hitPos.z);
        float dist2D = Vector2.Distance(self2D, hit2D);

        // TODO: log externally?
        Debug.Log(timeString + ":\nXZ Plane Distance: " + dist2D + "m\n3D Distance: " + dist3D + "m");
    }

    // TODO: Add easy moving of target/ random placement on floor
}
