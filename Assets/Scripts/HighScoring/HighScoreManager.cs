using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _listEntryPrefab;
    [SerializeField] private GameObject _listContainer;
    
    private string saveFile;
    private List<HighScoreData> _highScores;
    
    void Awake()
    {
        saveFile = Application.persistentDataPath + "/highscores.json";
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
        else
        {
            HSDL.highScoreDataList = new List<HighScoreData>();
        }
        
        return HSDL;
    }
    
    public void RecordNewScore(string name, int score)
    {
        Debug.Log("recording score: " + name + " - " + score);
        
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

    void ClearRecords()
    {
        HighScoreDataList HSDL = new HighScoreDataList();
        string newJSON = JsonUtility.ToJson(HSDL);
        File.WriteAllText(saveFile, newJSON);
    }

    void ClearContainer()
    {
        foreach (Transform child in _listContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
    
    HighScoreDataList SortByScore(HighScoreDataList unsortedHSDL)
    {
        List<HighScoreData> sorted = unsortedHSDL.highScoreDataList.OrderByDescending(order => order.score).ToList();
        HighScoreDataList sortedHSDL = new HighScoreDataList();
        sortedHSDL.highScoreDataList = sorted;
        return sortedHSDL;
    }

    public void PopulateList(HighScoreDataList HSDL)
    {
        HighScoreDataList sorted = SortByScore(HSDL);
        foreach (HighScoreData hsd in sorted.highScoreDataList)
        {
            TMP_Text t = Instantiate(_listEntryPrefab, _listContainer.transform);
            t.text = hsd.score.ToString() + " (" + hsd.name + ", " + hsd.date + ")";
        }
    }
    
}
