using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
 
    [SerializeField] private GameObject segmentPrefab, segmentPrefab2;
    private float gapSize;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private int segmentListSize;
    [SerializeField] private float minSpawnY = -3.5f;
    [SerializeField] private float maxSpawnY = 2.5f;
    private Renderer lastRender, currentRender;
    private GameObject lastGameObject, currentGameObject;

    private GameObject player;

    private List<GameObject> segments = new();

    

  
    public void Initialize()
    {
        player = GameManager.Instance.Player.gameObject;

        //Segment 1
        lastGameObject = Instantiate(segmentPrefab, new Vector3(player.transform.position.x, player.transform.position.y -1, 0), Quaternion.identity, transform);
        lastRender = lastGameObject.GetComponent<Renderer>();
        segments.Add(lastGameObject);


        //Segment 2
        currentGameObject = Instantiate(segmentPrefab, transform);
        currentRender = currentGameObject.GetComponent<Renderer>();
        segments.Add(currentGameObject);
        TryToSpawnObstaclesOn(currentGameObject);


        //Last Render and Current Render
        float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x / 2) + gapSize;
        currentGameObject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y - 1, 0);


        lastGameObject = currentGameObject;
        lastRender = currentRender;

        
     
    }
        
    private void Update()
    {
        if (lastRender == null || player == null) return;
        if (lastRender.bounds.max.x < player.transform.position.x + maxDistanceFromPlayer)
        {
            gapSize = Random.Range(1.5f, 2.5f);

            float minY = minSpawnY - player.transform.position.y;
            float maxY = maxSpawnY - player.transform.position.y;
            float heightOffset = Mathf.Clamp(Random.Range(-2.5f, 0.5f),minY, maxY); //Make sure platforms dont spawn too high or too low for the player to reach
                                                                                        //Or offscreen

            currentGameObject = Instantiate(segmentPrefab2, transform);
            currentRender = currentGameObject.GetComponent<Renderer>();

            float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x /2 ) + gapSize;
            currentGameObject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y + heightOffset, 0);
            TryToSpawnObstaclesOn(currentGameObject);
            segments.Add(currentGameObject);
            if(segments.Count > segmentListSize)
            {
                Destroy(segments[0]);
                segments.RemoveAt(0);
            }
            lastGameObject = currentGameObject;
            lastRender = currentRender;
        }
    }
    private void TryToSpawnObstaclesOn(GameObject segment)
    {
        ObstacleSpawner obstacleSpawner = segment.GetComponentInChildren<ObstacleSpawner>();
        if (obstacleSpawner != null)
        {
            obstacleSpawner.SpawnObstacles();
        }
    }
    public void Reset()
    {
        lastRender = null;
        currentRender = null;

        lastGameObject = null;
        currentGameObject = null;


        foreach(GameObject gameobject in segments)
        {
            ObstacleSpawner obstacleSpawner = gameobject.GetComponentInChildren<ObstacleSpawner>();
            if (obstacleSpawner != null) obstacleSpawner.Reset();
            Destroy(gameobject);
            
        }

        segments.Clear();

    }

}
