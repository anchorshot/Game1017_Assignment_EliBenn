using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
 
    [SerializeField] private GameObject segmentPrefab, segmentPrefab2;
    [SerializeField] private float gapSize = 0.5f;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private int segmentListSize;
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
            gapSize = Random.Range(0.5f, 1.5f);
            float heightOffset = Random.Range(-2.5f, 0.5f);


           currentGameObject = Instantiate(segmentPrefab2, transform);
            currentRender = currentGameObject.GetComponent<Renderer>();

            float xSpawnPosition = lastRender.bounds.max.x + (currentRender.bounds.size.x /2 ) + gapSize;
            currentGameObject.transform.position = new Vector3(xSpawnPosition, player.transform.position.y + heightOffset, 0);
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

    public void Reset()
    {
        lastRender = null;
        currentRender = null;

        lastGameObject = null;
        currentGameObject = null;

        foreach(GameObject gameobject in segments)
        {
            Destroy(gameobject);
            
        }

        segments.Clear();

    }

}
