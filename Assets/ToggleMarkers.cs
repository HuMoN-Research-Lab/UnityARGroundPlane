using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QualisysRealTime.Unity;

public class ToggleMarkers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            RTMarkerStream temp = gameObject.GetComponent<RTMarkerStream>();
            temp.visibleMarkers = !temp.visibleMarkers;
        }
    }
}
