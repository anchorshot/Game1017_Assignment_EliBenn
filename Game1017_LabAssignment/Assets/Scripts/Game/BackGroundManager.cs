using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private float Xbuffer = 3f;

    private Transform lastBackground;
    private Renderer lastRenderer;

    private float backGroundWidth;
    private float nextSpawnAtCamRightX; //World x where camera-right must reach to spawn again

    private List<GameObject> backgrounds = new List<GameObject>();
    private int objectPoolSize = 5;

    private void Start()
    {
        if (!cam) cam = Camera.main;
        //Create Object Pool
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject go = Instantiate(backgroundPrefab, transform);
            ReturnToPool(go);
            backgrounds.Add(go);

        }
    }
    public void Initialize()
    {
     
        lastBackground = GetNextObject().transform;
        lastRenderer = lastBackground.GetComponent<Renderer>();
       
        backGroundWidth = lastRenderer.bounds.size.x;
        lastRenderer.sortingOrder = 0;

        UpdateNextSpawnTrigger();

    }
    public void Reset()
    {
        foreach (GameObject background in backgrounds)
        {
            ReturnToPool(background);
        }
    }
    private GameObject GetNextObject()
    {
        foreach (GameObject go in backgrounds)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }
        return null;
    }

    private void ReturnToPool(GameObject Background)
    {
        Background.SetActive(false);
    }
    private void Update()
    {


        float halfCamWidth = cam.orthographicSize * cam.aspect;
        float camRightEdge = cam.transform.position.x + halfCamWidth;

        if(camRightEdge >= nextSpawnAtCamRightX)
        {
            SpawnNextToRight();
            UpdateNextSpawnTrigger();
        }
    }

    private void SpawnNextToRight()
    {
        if (lastBackground == null) return;
        Vector3 SpawnPos = lastBackground.position;
        SpawnPos.x += backGroundWidth;

        
        //if 3 objects are active -> return 1st spawned object to pool
        if (GetNextObject() == null)
        {
            float previousdistance = 0f;
            float distance;
            GameObject objecttoreturn = null;

         
            foreach (GameObject go in backgrounds)
            {
                distance = Vector3.Distance(go.transform.position, cam.transform.position);
                if (distance > previousdistance)
                {
                    previousdistance = distance;
                    objecttoreturn = go;
                }
            }
         ReturnToPool(objecttoreturn);
      

        }

        lastBackground = GetNextObject().transform;
        lastBackground.position = SpawnPos;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        lastRenderer.sortingOrder = 0;
    }

    private void UpdateNextSpawnTrigger()
    {
        if(lastRenderer == null) return;
        nextSpawnAtCamRightX = lastRenderer.bounds.max.x - Xbuffer;
    }




}
