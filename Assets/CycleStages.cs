using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CycleStages : MonoBehaviour
{
    public Dictionary<string, int> condDict;

    public List<GameObject> stages;

    public Dropdown blockDropdown;

    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        condDict.Add("Free-Walk", 0);
        condDict.Add("visEasy_bioEasy", 1);
        condDict.Add("visEasy_bioMedium", 2);
        condDict.Add("visEasy_bioHard", 3);
        condDict.Add("visHard_bioEasy", 4);
        condDict.Add("visHard_bioMedium", 5);
        condDict.Add("visHard_bioHard", 6); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            stages[currentIndex].SetActive(false);
            currentIndex += 1;
            //currentIndex %= stages.Count;
            if (currentIndex >= stages.Count) {
                //Debug.Log(condDict[blockDropdown.captionText.text]);
                GlobalBlockNum.SelectedBlock = int.Parse(blockDropdown.captionText.text);
                SceneManager.LoadScene("TrialReader", LoadSceneMode.Single);
            }
            stages[currentIndex].SetActive(true);
        }
    }
}

public static class GlobalBlockNum {
    public static int SelectedBlock {get; set;}
}
