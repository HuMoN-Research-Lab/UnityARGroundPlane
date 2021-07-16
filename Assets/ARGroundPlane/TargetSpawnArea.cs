using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnArea : MonoBehaviour
{
    [Tooltip("Drag your floor object here. Make its material 'std' from the Materials folder.")]
    public Renderer Floor;
    public Color FloorColor;

    [Header("Set object's y-scale to the desired target height in meters.")]

    [Range (5, 200)]
    [Tooltip("Total Number of Objects")]
    [SerializeField]
    private int TotalNumberOfObjects = 25;
    private Color TargetColor = Color.black;

    [Range (5, 200)]
    [Tooltip("This specifies the number of obstacles, the rest will be targets.")]
    [SerializeField]
    private int NumberOfObstacles = 25;
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
    // // Option to load saved target config
    // private void LoadTargetConfig(string path) {
    //     // load target config file from path on disk
    // }
    // public string DebugConfigPath = "";
    // [ContextMenu("Load Target Configuration")]
    // private void DebugConfigLoad() {
    //     LoadTargetConfig(DebugConfigPath);
    // }


    // Awake() is called when an object is instantiated and activated for the first time, so it will happen on a scene loading transition.
    // This is different to Start() which only happens if this script is active on application start, and only happens once.
    void Awake()
    {
        // Set floot color to customized choice
        Floor.material.color = FloorColor;
        Transform[] allChildren = null;
        // Scale the object we will be spawning such that it's height matches the height of our box
        Vector3 prefabScale = TargetPrefab.transform.localScale;
        TargetPrefab.transform.localScale = new Vector3(TargetWidth, transform.localScale.y*.5f, TargetWidth);
        int obsCount = NumberOfObstacles;

        for (int i = 0; i < TotalNumberOfObjects; i++) {
            bool crowded = false;
            // Pick a random location within our box
            Vector3 randomPositionWithin = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            randomPositionWithin = transform.TransformPoint(randomPositionWithin * 0.5f);
            allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren) {
                if (Vector3.Distance(child.position,randomPositionWithin) > MinDistBetweenObjects) // MinDist needs to be .25
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

            // Set color, sound, and targeted markers
            //int randomIndex = Random.Range(0, Colors.Count);
            // bool isObstacle = (Random.value < .5 ? true : false);
            // if (isObstacle) {
            //     if (obsCount <= 0) {
            //         isObstacle = false;
            //     }
            //     obsCount--;
            // }
            // targetScript.SetColor((isObstacle ? ObstacleColor : TargetColor));
            targetScript.SetMaterial(TargetMat); //(isObstacle ? ObstacleMat : TargetMat));
            // targetScript.GetComponentInChildren<DetectMarker>().SetColor((isObstacle ? ObstacleColor : TargetColor));
            targetScript.SetAudioFeedback(Sounds[0]); //(isObstacle ? Sounds[2] : Sounds[0]));
            targetScript.targetJoints = HitJoints;

            // Organize underneath self in hierarchy
            targetInstance.transform.SetParent(transform);

            // Update data for JSON output
            targetInstance.GetComponent<FloorObjectInfo>().FillInfo();
            targetInstance.name = targetInstance.name + "" + i;
        }

        //Transform[] allChildren = GetComponentsInChildren<Transform>();
        for (int i = 0; i < NumberOfObstacles; i++) {
            //grab object from list; in order
            GameObject go = allChildren[i].gameObject;
            DetectMarker d = go.GetComponent<DetectMarker>();
            Debug.Log(d);
            Debug.Log(ObstacleMat);
            d.SetMaterial(ObstacleMat);
            d.SetAudioFeedback(Sounds[2]);
        }
    }

    
}
