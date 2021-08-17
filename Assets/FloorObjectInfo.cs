using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorObjectInfo : MonoBehaviour
{

    public Vector3 position;

    public double yRotation;
    public string type = null;

    public void FillInfo(string name)
    {
        position = transform.position;
        yRotation = transform.rotation.y;
        Renderer r = GetComponent<Renderer>();
        //Debug.Log(r.material.name);
        type = name;
    }
}
