﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawnArea : MonoBehaviour
{
    [Tooltip("Drag your floor object here. Make its material 'std' from the Materials folder.")]
    public Renderer Floor;
    public Color FloorColor;

    [Header("Set object's y-scale to the desired target height in meters.")]
    [Range (5, 200)]
    [Tooltip("Number of targets to spawn.")]
    [SerializeField]
    private int NumTargetsToSpawn = 25;

    [Tooltip("The gameobject/prefab to spawn.")]
    [SerializeField]
    private GameObject TargetPrefab;

    [Tooltip("Width of targets in meters.")]
    [SerializeField]
    private float TargetWidth = 0.1f;

    [Tooltip("Joint names we are looking for the target (self) to collide with. Default: joint name from backflip demo.")]
    [SerializeField]
    private List<string> HitJoints;

    [Space]

    [Header("Colors chosen at random, audio corresponds in list order.")]
    [Tooltip("The list of possible colors, increase size to increase color possibilities.")]
    [SerializeField]
    private List<Color> Colors;

    [Tooltip("List of sounds that correspond to targets based on order, wrapping with modulo.")]
    [SerializeField]
    private List<AudioClip> Sounds;

    // Option to randomize all the colors in the list, located under the gear at the top right of the component inspector
    [ContextMenu("Randomize Colors")]
    private void RandomizeColors() {
        for (int i = 0; i < Colors.Count; i++)
            Colors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    // Awake() is called when an object is instantiated and activated for the first time, so it will happen on a scene loading transition.
    // This is different to Start() which only happens if this script is active on application start, and only happens once.
    void Awake()
    {
        // Set floot color to customized choice
        Floor.material.color = FloorColor;
        
        // Scale the object we will be spawning such that it's height matches the height of our box
        Vector3 prefabScale = TargetPrefab.transform.localScale;
        TargetPrefab.transform.localScale = new Vector3(TargetWidth, transform.localScale.y*.5f, TargetWidth);

        for (int i = 0; i < NumTargetsToSpawn; i++) {
            // Pick a random location within our box
            Vector3 randomPositionWithin = new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            randomPositionWithin = transform.TransformPoint(randomPositionWithin * 0.5f);
            // Create our target
            GameObject targetInstance = Instantiate(TargetPrefab, randomPositionWithin, Quaternion.identity);
            DetectMarker targetScript = targetInstance.GetComponent<DetectMarker>();

            // Set color, sound, and targeted markers
            int randomIndex = Random.Range(0, Colors.Count);
            targetScript.SetColor(Colors[randomIndex]);
            targetScript.SetAudioFeedback(Sounds[randomIndex%Colors.Count]);
            targetScript.targetJoints = HitJoints;

            // Organize underneath self in hierarchy
            targetInstance.transform.SetParent(transform);
        }
    }
}
