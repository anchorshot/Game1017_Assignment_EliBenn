using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    InMenu,
    InGame,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStates CurrentGameState { get; private set; }

    private SoundManager soundManager;
    public SoundManager SoundManager
    {
        get
        {
            if(soundManager == null)
            
                soundManager = FindFirstObjectByType<SoundManager>();

                return soundManager;
            
        }
        private set
        {
            soundManager = value;
        }  
    }

    private SegmentSpawner segmentSpawner;
    
    public SegmentSpawner  SegmentSpawner
        {
        get
        {
            if (segmentSpawner == null)

                segmentSpawner = FindFirstObjectByType<SegmentSpawner>();

            return segmentSpawner;

        }
        private set
        {
            segmentSpawner = value;
        }
    }

    private PlayerController player;
    public PlayerController Player
      {
        get
        {
            if (player == null)

                player = FindFirstObjectByType<PlayerController>();

            return player;

        }
        private set
        {
            player = value;
        }
    }
    private UIManager uiManager;
    public UIManager UIManager
    {
        get
        {
            if (uiManager == null)

                uiManager = FindFirstObjectByType<UIManager>();

            return uiManager;

        }
        private set
        {
            uiManager = value;
        }
    }

    private BackGroundManager backGroundManager;
    public BackGroundManager BackgroundManager
    {
        get
        {
            if (backGroundManager == null)

                backGroundManager = FindFirstObjectByType<BackGroundManager>();

            return backGroundManager;

        }
        private set
        {
            backGroundManager = value;
        }
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentGameState = GameStates.InMenu;
  
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        CurrentGameState = GameStates.InMenu;

    }

     
    public void PlayGame()
    {
        SetGameState(GameStates.InGame);
        SegmentSpawner.Initialize();
        Player.Initialize();
        UIManager.OnPlayPressed();
        BackgroundManager.Initialize();

            
    }

    public void GameOver()
    {
        float TimePassed = UIManager.GetTimeElapsed();
        SaveSystem.Instance.SetHighTimes(TimePassed);
     
        UIManager.StopTimer();

        SetGameState(GameStates.GameOver);
        SceneManager.LoadScene("GameOverScene");
    }

    public void RestartGame()
    {
        Player.Reset();
        SegmentSpawner.Reset();
        UIManager.OnResetPressed();
        BackgroundManager.Reset();


        SetGameState(GameStates.InMenu);
    }
    private void SetGameState(GameStates state)
    {
        CurrentGameState = state;
    }

    public void PlayerDied()
    {
        GameOver();
    }
}
