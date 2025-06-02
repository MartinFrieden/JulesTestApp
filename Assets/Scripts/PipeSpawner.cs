using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab; // Assign your Pipe Pair Prefab here
    public float spawnRate = 2f;  // Time between spawns
    public float heightOffsetRange = 2f; // Max random offset for pipe height
    public float spawnPosX = 10f; // X position where pipes will spawn

    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnPipe();
            timer = 0;
        }
    }

    void SpawnPipe()
    {
        if (pipePrefab == null)
        {
            Debug.LogError("Pipe Prefab not assigned in PipeSpawner!");
            return;
        }

        float randomHeightOffset = Random.Range(-heightOffsetRange, heightOffsetRange);
        GameObject newPipe = Instantiate(pipePrefab, new Vector3(spawnPosX, randomHeightOffset, 0), Quaternion.identity);
    }
}
