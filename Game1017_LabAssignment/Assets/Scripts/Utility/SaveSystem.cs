using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;
public class SaveSystem : Singleton<SaveSystem>
{
    private const string HighTimeKey = "HighTime";
    private const string LeaderBoardCountKey = "LeaderBoardCount";
    private const string LeaderBoardEntryKeyPrefix = "LeaderBoardEntry_";

    [SerializeField] private int maxLeaderboardEntries = 5;

    //Getters
    public float GetHighTime()
    {
        return PlayerPrefs.GetFloat(HighTimeKey, 0f);
    }

    public List<float> GetLeaderBoard()
    {
       List<float> times = new List<float>();
       int count = PlayerPrefs.GetInt(LeaderBoardCountKey, 0);

        for (int i = 0; i < count; i++)
        {
           times.Add(PlayerPrefs.GetFloat(LeaderBoardEntryKeyPrefix + i, 0f));

        }
        return times;
    }

    public void SetHighTimes(float runTime) 
    {
      
        PlayerPrefs.SetFloat(HighTimeKey, runTime);


        List<float> times = GetLeaderBoard();
        times.Add(runTime);
        times.Sort((a, b) => b.CompareTo(a)); // Sort in descending order (best times first)

        if (times.Count > maxLeaderboardEntries)
        {
            times.RemoveRange(maxLeaderboardEntries, times.Count - maxLeaderboardEntries); // Keep only the top entries
        }

        PlayerPrefs.SetInt(LeaderBoardCountKey, times.Count);
        for (int i = 0; i < times.Count; i++)
        {
            PlayerPrefs.SetFloat(LeaderBoardEntryKeyPrefix + i, times[i]);
        }
        PlayerPrefs.Save();
    }
}
