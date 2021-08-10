using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorObjectInfo : MonoBehaviour
{

    public Vector3 position;
    public string type = null;

    public void FillInfo(string name)
    {
        position = transform.position;
        Renderer r = GetComponent<Renderer>();
        //Debug.Log(r.material.name);
        type = name;
    }
}
