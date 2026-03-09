using UnityEngine;
using UnityEngine.UI;
using System;

public enum GameManagerAction
{
    StartGame,
    PlayGame,
    GameOver,
    RestartGame
}
public class GameManagerButton : MonoBehaviour
{
    [SerializeField] private GameManagerAction gamemanageraction;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PreformGameManagerAction);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(PreformGameManagerAction);
    }

    private void PreformGameManagerAction()
    {
        switch (gamemanageraction)
        {
            case GameManagerAction.StartGame:
            GameManager.Instance.StartGame(); break;
            case GameManagerAction.PlayGame:
            GameManager.Instance.PlayGame(); 
            break;
            case GameManagerAction.GameOver:
            GameManager.Instance.GameOver(); 
            break;
            case GameManagerAction.RestartGame:
            GameManager.Instance.RestartGame(); 
            break;
        }
    }
}
