using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMarker : MonoBehaviour
{
    public List<string> targetJoints;

    private AudioSource audioFeedbackSource;

    public void SetAudioFeedback(AudioClip ac) {
        audioFeedbackSource.clip = ac;
    }

    public void SetColor(Color c) {
        Renderer r = GetComponent<Renderer>();
        r.material.color = c;
    }

    // TODO: Move hitbox above target?

    void Awake() {
        audioFeedbackSource = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        // grab time of event asap
        string timeString = System.DateTime.Now.ToString("hh.mm.ss.ffffff");
        // disregard when not a name we're looking for
        bool validTarget = false;
        foreach(string os in targetJoints) {
            if (string.Equals(other.gameObject.name, os)) {
                validTarget = true;
                break;
            }
        }

        if (!validTarget) return;

        // play sound as soon as verified
        audioFeedbackSource.Play();
        // calculate distance from center of target object to center of colliding joint
        Vector3 selfPos = this.transform.position;
        Vector3 hitPos = other.transform.position;
        float dist3D = Vector3.Distance(selfPos, hitPos);
        // calculate distance from center of target object to center of colliding joint on the x,z plane
        Vector2 self2D = new Vector2(selfPos.x, selfPos.z);
        Vector2 hit2D = new Vector2(hitPos.x, hitPos.z);
        float dist2D = Vector2.Distance(self2D, hit2D);

        // TODO: log externally?
        Debug.Log(timeString + ":\nXZ Plane Distance: " + dist2D + "m\n3D Distance: " + dist3D + "m");
    }
}
