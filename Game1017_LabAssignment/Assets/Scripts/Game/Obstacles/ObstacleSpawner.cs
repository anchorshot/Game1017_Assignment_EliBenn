using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    
    public float spawnChance = 0.5f; // Chance to spawn an obstacle (0 to 1)
    public float[] validspawnpointsX = { 0f, 1f, 2f }; //Valid X positions for spawning obstacles
    public float yOffset = 1f; // Vertical offset from the platforms position
    public float minSpaceBetweenObstacles = 2.5f; // Minimum spacing between obstacles

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // List to keep track of spawned obstacles

    //This is called through the segments, they get cleaned up together so its nice and clean and wonderful and awesome
    public void SpawnObstacles()
    {
        if (obstaclePrefab == null) return;

        //Shuffle n pick the spawn points

        if (Random.value > spawnChance) return; // Check if we should spawn an obstacle based on the spawn chance 

        float[] shuffledArray = ShuffleArray(validspawnpointsX);
        float lastSpawnedX = float.MinValue;

        foreach (float localX in shuffledArray)
        {
            //Force minimum spacing between obstacles
            if (Mathf.Abs(localX - lastSpawnedX) < minSpaceBetweenObstacles)
            {
                continue; // Skip this spawn point if it's too close to the last spawned obstacle
            }

            Vector3 worldPosition = transform.position + new Vector3(localX, yOffset, 0);
            //Check if the current spawn point isnt occupied
            if (IsSpawnPointOccupied(worldPosition))
            {
                continue; // Skip this spawn point if it's occupied
            }

            //Spawn the obstacle
            GameObject SpawnedObstacle = Instantiate(obstaclePrefab, worldPosition, Quaternion.identity, transform);
            Vector3 parentScale = transform.lossyScale;
            SpawnedObstacle.transform.localScale = new Vector3(
                1f / parentScale.x,
                1f / parentScale.y,
                1f / parentScale.z); // Adjust the scale of the spawned obstacle to counteract the parent's scale (I screwed up the prefabs scale and this is a cute lil bandaid)

            spawnedObstacles.Add(SpawnedObstacle);
            lastSpawnedX = localX; // Update the last spawned X position

                
        }

    }

    //Check if the spawn point is occupied by another collider that isnt the platform
    private bool IsSpawnPointOccupied(Vector3 position)
    {
        Collider2D pointCheck = Physics2D.OverlapCircle(position, 0.5f); // Check for colliders in a small area around the spawn point
        return pointCheck != null && !pointCheck.CompareTag("Ground"); // Return true if there's a collider that isn't the platform itself
    }

    //Shuffle algorithm to randomize the order of the spawn points
    private float[] ShuffleArray(float[] array)
    {
        float[] arrayCopy = (float[])array.Clone();
        for (int i = arrayCopy.Length - 1; i > 0; i--)//for each element in the array, starting from the end
        {
            int j = Random.Range(0, i + 1); // Generate a random index from 0 to i
            // Swap elements at indices i and j
            float temp = arrayCopy[i];
            arrayCopy[i] = arrayCopy[j];
            arrayCopy[j] = temp;
        }
        return arrayCopy;
    }

    public void Reset() 

 
    {
        foreach (GameObject obstacle in spawnedObstacles)
        {
            if (obstacle != null) Destroy(obstacle);
        }
        spawnedObstacles.Clear();

        }

}
