using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObjectInfo : MonoBehaviour
{

    public Vector3 position;
    public string type = null;

    public void FillInfo()
    {
        position = transform.position;
        Renderer r = GetComponent<Renderer>();
        //Debug.Log(r.material.name);
        type = r.material.name.Contains("obsMat") ? "OBSTACLE" : "TARGET";
    }
}
