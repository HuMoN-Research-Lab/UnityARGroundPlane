# Unity AR Groundplane

Software to run augmented reality experiment trials in the Human Motion Lab in 453 Richards, Northeastern University. Using a Qualisys motion capture system to track user's head location and the UniCAVE plugin to correct ground projections to a test subject's perspective. Eye tracking is also to be incorporated at a later date.
This system will be used to study foot hold targetting during locomotion and other walking based trial-tasks, eventually including a software sweet to setup testing scenes and actually run structured experiments.

---

## Current Hardware
- 28 Camera Qualysis System (w/3 optional rgb cameras, 31 total) running at 300hz
	- TODO: Flesh out camera models here in a list
- Single Computer running an RTX 2060
- Single 3440x1440 PC monitor
- 2 Optoma projectors running at 120 fps at 720p

*Hardware may change/expand as more projectors are added and more pcs are required.*

## Current Software
- Unity 2018.3.2f1 *(for compatibility with QTM plugin)*
- QTM
- QTM Unity plugin Package 7
- UniCAVE 2019
- *No longer using github unity plugin - memory-leak-style error where large number of git instances are started, causing Unity to hault*

## Current Start-Up
- With QTM running, track and label at least a "Head" 6DOF object and make sure you are broadcasting 6DOF in real time
- In Unity, give this object label to the "Head" object's tracking script, located in the CAVE system
- Optionally, add an unlabeled marker number to "Wand" for basic interactivity
- Build the project with the demo_target_cave scene
- Run the program, setting the computer monitor as the target display (the UniCAVE plugin will handle setting up the other screens)

---

### Work to be Done Soon (In No Particular Order)
- Edge blending of multiple projectors
- Exploring Unity UI compatibility with UniCAVE screen handling
- Changes to shadow culling settings in Unity
- Devising a more accurate system to offset QTM coordinates to Unity coordinates
- Flexible foot interactions
- Body stats tracking/COM calculations
	- Defining a clear format for data output
- Latency reduction (current whole system latency at 105-110 ms)

### Work to be Done a Little Later
- Experiment/Trials structure (collaborative with another lab on campus)
- Eye tracking
- Real-time decision making to handle redundant projectors and limit visible real-world shadows for user