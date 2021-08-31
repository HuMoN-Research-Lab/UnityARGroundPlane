using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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

    private Dictionary<int, string> condDict;
    private List<int> RandomBlockOrder = null;

    private List<int> BlockNumberList = null;

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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        dir = new DirectoryInfo("DataInput");
        //files = dir.GetFiles("*.json");
        Debug.Log("Block Counter: " + BlockCounter + "\nCondition: " + condDict[RandomBlockOrder[BlockCounter]]);
        files = dir.GetFiles("*" + condDict[RandomBlockOrder[BlockCounter]] + ".json");

        if (TrialNumber < files.Length) {
           spawnTiles.StartUp("DataInput/" + files[TrialNumber].Name);
            TrialNumber += 1;
        } else if (TrialNumber == files.Length){
            BlockCounter += 1;
            TrialNumber = 0;
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
}
