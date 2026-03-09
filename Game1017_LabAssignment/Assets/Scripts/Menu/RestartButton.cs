using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ReturnToTitleScreen);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ReturnToTitleScreen);
    }
    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

}