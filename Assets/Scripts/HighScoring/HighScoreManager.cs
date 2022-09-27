using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private string saveFile;
    private List<HighScoreData> _highScores;

    public bool test = false;
    
    void Awake()
    {
        saveFile = Application.persistentDataPath + "/highscores.json";
    }
    

    void Update()
    {
        if (test)
        {
            RecordNewScore("abc", 123456);
            test = false;
        }
    }
    
    public HighScoreDataList GetRecords()
    {
        string fileContents;
        HighScoreDataList HSDL = new HighScoreDataList();
        
        if (File.Exists(saveFile))
        {
            fileContents = File.ReadAllText(saveFile);
            HSDL = JsonUtility.FromJson<HighScoreDataList>(fileContents);
        }
        
        return HSDL;
    }
    
    public void RecordNewScore(string name, int score)
    {
        HighScoreData _highScoreData = new HighScoreData();
        _highScoreData.date = DateTime.Now.ToLocalTime().ToString();
        _highScoreData.name = name;
        _highScoreData.score = score;

        SaveData(_highScoreData);
    }

    void SaveData(HighScoreData newScore)
    {
        HighScoreDataList HSDL = GetRecords();

        // Update with new score
        HSDL.highScoreDataList.Add(newScore);

        // Write savefile back to storage
        string newJSON = JsonUtility.ToJson(HSDL);
        File.WriteAllText(saveFile, newJSON);

        Debug.Log("wrote to " + saveFile);
    }

    HighScoreDataList SortByScore(HighScoreDataList unsortedHSDL)
    {
        List<HighScoreData> sorted = unsortedHSDL.highScoreDataList.OrderByDescending(order => order.score).ToList();
        HighScoreDataList sortedHSDL = new HighScoreDataList();
        sortedHSDL.highScoreDataList = sorted;
        return sortedHSDL;
    }

    public void PopulateList(HighScoreDataList HSDL, GameObject target)
    {
        HighScoreDataList sorted = SortByScore(HSDL);
        foreach (HighScoreData hsd in sorted.highScoreDataList)
        {
            GameObject tgo = new GameObject();
            tgo.transform.parent = target.transform;
            TMP_Text txt = tgo.AddComponent<TMPro.TextMeshProUGUI>(); // Add text component to it
            txt.text = hsd.score.ToString();
        }
    }
    
}
