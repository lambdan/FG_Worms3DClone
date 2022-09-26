using System.Collections.Generic;

[System.Serializable]
public class HighScoreData
{
    public string date;
    public string name;
    public int score;
}

[System.Serializable]
public class HighScoreDataList
{
    public List<HighScoreData> highScoreDataList;
}