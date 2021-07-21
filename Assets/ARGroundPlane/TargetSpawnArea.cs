using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnArea : MonoBehaviour
{
    [Tooltip("Drag your floor object here. Make its material 'std' from the Materials folder.")]
    public Renderer Floor;
    public Color FloorColor;

    [Header("Set object's y-scale to the desired target height in meters.")]

    [Range (1, 20)]
    [Tooltip("Number of Targets")]
    [SerializeField]
    private int NumberOfTargets= 0;
    private Color TargetColor = Color.black;

    [Range (1, 20)]
    [Tooltip("This specifies the number of Obstacles, the rest will be targets.")]
    [SerializeField]
    private int NumberOfObstacles = 0;
    private Color ObstacleColor = Color.red;

    [Tooltip("The gameobject/prefab to spawn.")]
    [SerializeField]
    private GameObject TargetPrefab;

    [Tooltip("Width of targets in meters.")]
    [SerializeField]
    private float TargetWidth = 0.1f;

    [Tooltip("Minimum distance between targets and obstacles")]
    [SerializeField]
    private float MinDistBetweenObjects = 0.1f;

    [Tooltip("Joint names we are looking for the target (self) to collide with. Default: joint name from backflip demo.")]
    [SerializeField]
    private List<string> HitJoints;

    [Space]

    public Material ObstacleMat, TargetMat;

    [Space]

    public bool RandomRotation = false;


    // [Header("Colors chosen at random, audio corresponds in list order.")]
    // [Tooltip("The list of possible colors, increase size to increase color possibilities.")]
    // [SerializeField]
    // private List<Color> Colors;

    [Tooltip("List of sounds that correspond to targets based on order, wrapping with modulo.")]
    [SerializeField]
    private List<AudioClip> Sounds;

    // Option to randomize all the colors in the list, located under the gear at the top right of the component inspector
    // [ContextMenu("Randomize Colors")]
    // private void RandomizeColors() {
    //     for (int i = 0; i < Colors.Count; i++)
    //         Colors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    // }

    // These two functions can let the unity editor act as a target layout maker with a simple run of this Block level scene
    // Option to save out current target locations/colors
    [ContextMenu("Save Current Targets")]
    private void SaveCurrentTargets() {
        // run through all children, save color/loc
    }


    // Awake() is called when an object is instantiated and activated for the first time, so it will happen on a scene loading transition.
    // This is different to Start() which only happens if this script is active on application start, and only happens once.
    void Awake()
    {
        // Set floot color to customized choice
        Floor.material.color = FloorColor;
        List<Transform> allChildren = new List<Transform>();
        // Scale the object we will be spawning such that it's height matches the height of our box
        Vector3 prefabScale = TargetPrefab.transform.localScale;
        TargetPrefab.transform.localScale = new Vector3(TargetWidth, transform.localScale.y*.5f, TargetWidth);
        int obsCount = NumberOfObstacles;

        for (int i = 0; i < NumberOfTargets; i++) {
            bool crowded = false;
            // Pick a random location within our box
            Vector3 randomPositionWithin = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            randomPositionWithin = transform.TransformPoint(randomPositionWithin * 0.5f);
            allChildren = new List<Transform>(GetComponentsInChildren<Transform>());
            // List is slower than array^
            foreach (Transform child in allChildren) {
                if (Vector3.Distance(child.position, randomPositionWithin) > MinDistBetweenObjects) // current MinDist is 0.13
                    continue;
                else
                {
                crowded = true;
                break;
                }
            }
            
            if (crowded == true){
                i--;
                continue;
            }

            // Create our target
            GameObject targetInstance = Instantiate(TargetPrefab, randomPositionWithin, Quaternion.identity);
            DetectMarker targetScript = targetInstance.GetComponent<DetectMarker>();

            targetScript.SetMaterial(TargetMat);
            targetScript.SetAudioFeedback(Sounds[0]);
            targetScript.targetJoints = HitJoints;

            // Organize underneath self in hierarchy
            targetInstance.transform.SetParent(transform);

            // Update data for JSON output
            targetInstance.GetComponent<FloorObjectInfo>().FillInfo("target");
            targetInstance.name = targetInstance.name + "" + i;
        }

        for (int i = 0; i < NumberOfObstacles; i++) {
            bool crowded = false;
            // Pick a random location within our box
            Vector3 randomPositionWithin = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            randomPositionWithin = transform.TransformPoint(randomPositionWithin * 0.5f);
            allChildren = new List<Transform>(GetComponentsInChildren<Transform>());
            // List is slower than array^
            foreach (Transform child in allChildren) {
                if (Vector3.Distance(child.position, randomPositionWithin) > MinDistBetweenObjects) // current MinDist is 0.13
                    continue;
                else
                {
                crowded = true;
                break;
                }
            }
            
            if (crowded == true){
                i--;
                continue;
            }

            // Create our target
            float randYRot = Random.Range(0, 360);
            GameObject targetInstance = Instantiate(TargetPrefab, randomPositionWithin, Quaternion.identity);
            Vector3 v = targetInstance.transform.rotation.eulerAngles;
            //targetInstance.gameObject.transform.rotation.eulerAngles = new Vector3(v.x, randYRot, v.z);
            DetectMarker targetScript = targetInstance.GetComponent<DetectMarker>();

            targetScript.SetMaterial(ObstacleMat);
            targetScript.SetAudioFeedback(Sounds[2]);
            targetScript.targetJoints = HitJoints;

            // Organize underneath self in hierarchy
            targetInstance.transform.SetParent(transform);

            // Update data for JSON output
            // targetInstance.GetComponent<FloorObjectInfo>().FillInfo("obstacle");
            // targetInstance.name = targetInstance.name + "" + i;
        }

    }

    
}
