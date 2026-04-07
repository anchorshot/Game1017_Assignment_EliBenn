using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject playbutton, resetButton, gameoverButton;
    [SerializeField] private TMP_Text timerText;

    private float timeElapsed;
    private bool timerRunning = false;
    

    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (timerRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimer();
        }
    }

    public void Initialize()
    {
        playbutton.SetActive(true);
        resetButton.SetActive(false);
        gameoverButton.SetActive(false);
        ResetTimer();


    }

    public void OnPlayPressed()
    {
        playbutton.SetActive(false);
        resetButton.SetActive(true);
        gameoverButton.SetActive(true);

        StartTimer();
    }


    public void OnResetPressed()
    {
        Initialize();

    }
    private void UpdateTimer()
    {
        int minutes =  Mathf.FloorToInt(timeElapsed / 60f);
        int seconds =  Mathf.FloorToInt(timeElapsed % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public float GetTimeElapsed()
    {
        return timeElapsed;
    }
    public void StartTimer()
    {
        timerRunning = true;
    }
    public void StopTimer()
    {
        timerRunning = false;
    }
    public void ResetTimer()
    {
        timeElapsed = 0f;
        timerRunning = false;
        UpdateTimer();
    }
    
}
