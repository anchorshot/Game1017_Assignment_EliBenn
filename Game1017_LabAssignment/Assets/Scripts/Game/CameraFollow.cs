using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private float FixedY, FixedZ;
    private float xOffset;

    private void Start()
    {
        FixedY = transform.position.y; FixedZ = transform.position.z;
        xOffset = transform.position.x;

        if (Player == null)
        {
            Player = (GameObject)FindFirstObjectByType(typeof(PlayerController)).GameObject();
        }
    }

    private void LateUpdate()
    {
        if (Player == null) { return; }

        float newXPosition = Player.transform.position.x + xOffset;

        transform.position = new Vector3(newXPosition, FixedY, FixedZ);
    }
}
