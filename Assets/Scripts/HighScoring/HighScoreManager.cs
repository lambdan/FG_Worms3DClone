using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
        string fileContents;
        HighScoreDataList HSDL = new HighScoreDataList();

        // Read old data if any
        if (File.Exists(saveFile))
        {
            fileContents = File.ReadAllText(saveFile);
            HSDL = JsonUtility.FromJson<HighScoreDataList>(fileContents);
        }

        // Update with new score
        HSDL.highScoreDataList.Add(newScore);

        // Write savefile back to storage
        string newJSON = JsonUtility.ToJson(HSDL);
        File.WriteAllText(saveFile, newJSON);

        Debug.Log("wrote to " + saveFile);
    }

    public string GetRecords()
    {
        string fileContents;
        HighScoreDataList HSDL = new HighScoreDataList();
        
        if (File.Exists(saveFile))
        {
            fileContents = File.ReadAllText(saveFile);
            HSDL = JsonUtility.FromJson<HighScoreDataList>(fileContents);
        }

        if (HSDL.highScoreDataList.Count == 0)
        {
            return "No high scores set";
        }

        string records = "";
        foreach (HighScoreData highScore in HSDL.highScoreDataList)
        {
            records = records + highScore.score.ToString() + ",";
        }

        return records;
    }
    
}
