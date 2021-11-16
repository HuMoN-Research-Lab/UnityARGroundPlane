using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Timers;

public class TrialFeeder : MonoBehaviour
{
    DirectoryInfo dir;

    FileInfo[] files;

    public int TrialNumber = 0;
    private int[] BlockNumber = {1, 2, 3, 4, 5, 6};   
    private int BlockCounter = 0;
    // every 20 trials, add 1 to block counter
    public JSONReader spawnTiles;
    public BlockTrialOutput blockTrialOutput;

    public Dictionary<int, string> condDict;
    public List<int> RandomBlockOrder = null;

    private List<int> BlockNumberList = null;

    public Text BlockLabel;
    public Text TrialLabel;
    public Text BlockNum;
    public Text ConditionLabel;

    public Text TimerLabel;

    public GameObject SwitchBoxes;

    public GameObject TrialFailBlock;
    public float TrialSeconds = 5f;
    private bool failed = false;
    private float startTime = 0;

    public bool timing = false;

    public int NumFreeWalkTrials = 21;

    void Awake() {
        
        // RandomBlockOrder needs to be established once, awake re-establishes it every initialization.
        // TDW/SER /\ 2021-08-25
        condDict = new Dictionary<int, string>();

        // create a dictionary of condition names
        condDict.Add(0, "Free-Walk");
        condDict.Add(1, "visEasy_bioEasy");
        condDict.Add(2, "visEasy_bioMedium");
        condDict.Add(3, "visEasy_bioHard");
        condDict.Add(4, "visHard_bioEasy");
        condDict.Add(5, "visHard_bioMedium");
        condDict.Add(6, "visHard_bioHard"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        List<int> BlockNumberList = new List<int>(BlockNumber);
        RandomBlockOrder = BlockNumberList.OrderBy( x => Random.value ).ToList();
        RandomBlockOrder.Insert(0, 0);
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += StopTiming;
        //Debug.Log(RandomBlockOrder[0]);

        // create list of X random 10 digit numbers, where X is the number of files in DataInput directory
        // rename every file to start with a random number
        dir = new DirectoryInfo("DataInput");
        FileInfo[] allFiles = dir.GetFiles("*.json");
        foreach (FileInfo fi in allFiles) {
            System.IO.File.Move("DataInput/" + fi.Name, "DataInput/" + Random.Range(1000, 9999) + fi.Name.Substring(4));
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SwitchBoxes.SetActive(true);
        TrialFailBlock.SetActive(false);
        //files = dir.GetFiles("*.json");
        //Debug.Log("Block Counter: " + BlockCounter + "\nCondition: " + condDict[RandomBlockOrder[BlockCounter]]);

        BlockLabel.text = "";
        for (int i = 0; i < RandomBlockOrder.Count; i++) {
            BlockLabel.text += ("" + condDict[RandomBlockOrder[i]]);
            if (i != RandomBlockOrder.Count-1) BlockLabel.text += (",");
        }

        BlockNum.text = "" + RandomBlockOrder[BlockCounter];
        TrialLabel.text = "" + (TrialNumber+1);
        ConditionLabel.text = condDict[RandomBlockOrder[BlockCounter]];


        //handle 0 block
        if (RandomBlockOrder[BlockCounter] != 0)
            files = dir.GetFiles("*" + condDict[RandomBlockOrder[BlockCounter]] + ".json");

        if (RandomBlockOrder[BlockCounter] == 0) {
            if (TrialNumber < NumFreeWalkTrials) {
                TrialReset();
                TrialNumber += 1;
            } else {
                BlockCounter += 1;
                TrialNumber = 0;
                OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            }
        } else if (TrialNumber < files.Length) {
            spawnTiles.StartUp("DataInput/" + files[TrialNumber].Name);
            //blockTrialOutput.WriteString("[\"" + files[TrialNumber].Name + "\", " + epoch + "]\n");
            TrialReset();
            TrialNumber += 1;
        } else if (TrialNumber == files.Length){
            BlockCounter += 1;
            TrialNumber = 0;
            if (BlockCounter >= condDict.Count()) {
                SwitchBoxes.SetActive(false);
                timing = false;
            } else {
                OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timing) TimerLabel.text = "" + (Time.time - startTime);

        if (spawnTiles == null) {
            spawnTiles = GameObject.Find("SpawnTiles").GetComponent<JSONReader>();
        }

        if (TrialFailBlock == null) {
            TrialFailBlock = GameObject.Find("TrialFail");
        }

        if (failed) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                TrialReset();
            }
        } else {
            if (timing && Time.time - startTime > TrialSeconds) FailTrial();
        }
    }

    public bool IsVisHard() {
        return condDict[RandomBlockOrder[BlockCounter]].Contains("visHard");
    }

    void FailTrial() {
        failed = true;
        gameObject.GetComponent<AudioSource>().Play();
        blockTrialOutput.WriteString("null\n");
        TrialFailBlock.SetActive(true);
        SwitchBoxes.SetActive(false);
    }

    void TrialReset() {
        //turn off fail block
        TrialFailBlock.SetActive(false);
        //turn on ending hitboxes
        SwitchBoxes.SetActive(true);
        failed = false;
        timing = false;
        //handle 0 block
        string outputStr = ((RandomBlockOrder[BlockCounter] == 0) ? "Free-Walk" : files[TrialNumber].Name);
        Debug.Log(outputStr);
        blockTrialOutput.WriteString("\"" + outputStr + "\", ");
    }

    public void StartTiming() {
        timing = true;
        startTime = Time.time;

        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        int epoch = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        blockTrialOutput.WriteString(epoch + ", ");
    }

    public void StopTiming() {
        if (timing) {
            timing = false;
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int epoch = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            blockTrialOutput.WriteString(epoch + "\n");
        }
    }
}
