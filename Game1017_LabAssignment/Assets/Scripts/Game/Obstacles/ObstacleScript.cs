using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) GameManager.Instance.PlayerDied();
    }
}
