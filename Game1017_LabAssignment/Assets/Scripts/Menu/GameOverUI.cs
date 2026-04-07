using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text leaderboardText;


    private void Start()
    {
        InitializeBestTimes();
        InitializeLeaderBoard();
    }

    private void InitializeBestTimes()
    {
        float bestTime = SaveSystem.Instance.GetHighTime();
        timeText.text = "Your Time: " + FormatTime(bestTime);

    }

    private void InitializeLeaderBoard()
    {
        List<float> leaderBoard = SaveSystem.Instance.GetLeaderBoard();

        leaderboardText.text = "Leaderboard:\n";
        for (int i = 0; i < leaderBoard.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {FormatTime(leaderBoard[i])}\n";
        }
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
