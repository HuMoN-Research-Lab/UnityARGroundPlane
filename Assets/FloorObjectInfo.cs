using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorObjectInfo : MonoBehaviour
{

    public Vector3 position;

    public float yRotation;
    public string type = null;

    public void FillInfo(string name)
    {
        position = transform.position;
        yRotation = transform.rotation.eulerAngles.y;
        Renderer r = GetComponent<Renderer>();
        //Debug.Log(r.material.name);
        type = name;
    }

    public void ApplyInfo(bool isFlipped, GameObject rotatePoint) {
        transform.position = position;
        if (isFlipped) transform.RotateAround(rotatePoint.transform.position, Vector3.up, 180);
        //transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(0f, yRotation + (isFlipped?180:0), 0f);
        //Debug.Log(transform.rotation.eulerAngles);
    }
}
