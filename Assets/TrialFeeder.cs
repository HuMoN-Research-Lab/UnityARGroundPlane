using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
    public Text ConditionLabel;

    public GameObject SwitchBoxes;

    void Awake() {
        
        // RandomBlockOrder needs to be established once, awake re-establishes it every initialization.
        // TDW/SER /\ 2021-08-25
        condDict = new Dictionary<int, string>();

        // create a dictionary of condition names
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
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        
        //files = dir.GetFiles("*.json");
        Debug.Log("Block Counter: " + BlockCounter + "\nCondition: " + condDict[RandomBlockOrder[BlockCounter]]);

        BlockLabel.text = "[";
        for (int i = 0; i < RandomBlockOrder.Count; i++) {
            BlockLabel.text += ("" + RandomBlockOrder[i]);
            if (i != RandomBlockOrder.Count-1) BlockLabel.text += (",");
        }
        BlockLabel.text += "]";

        TrialLabel.text = "" + (TrialNumber+1);
        ConditionLabel.text = condDict[RandomBlockOrder[BlockCounter]];

        files = dir.GetFiles("*" + condDict[RandomBlockOrder[BlockCounter]] + ".json");

        if (TrialNumber < files.Length) {
            spawnTiles.StartUp("DataInput/" + files[TrialNumber].Name);
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int epoch = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            blockTrialOutput.WriteString("[\"" + files[TrialNumber].Name + "\", " + epoch + "]\n");
            TrialNumber += 1;
        } else if (TrialNumber == files.Length){
            BlockCounter += 1;
            TrialNumber = 0;
            if (BlockCounter >= condDict.Count()) {
                // END of run through, turn off endings?
                System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                int epoch = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                blockTrialOutput.WriteString("[" + epoch + "]");

                SwitchBoxes.SetActive(false);
            }
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTiles == null) {
            spawnTiles = GameObject.Find("SpawnTiles").GetComponent<JSONReader>();
        }
    }

    public bool IsVisHard() {
        return condDict[RandomBlockOrder[BlockCounter]].Contains("visHard");
    }
}
