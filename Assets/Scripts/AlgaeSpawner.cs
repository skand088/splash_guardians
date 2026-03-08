using UnityEngine;

public class AlgaeSpawner : MonoBehaviour
{
    public GameObject algaeTemplate;
    public float spawnInterval = 3f;
    public float mapWidth = 8f;
    public float mapHeight = 5f;

    private float timer_val;

    void Update()
    {
        timer_val += Time.deltaTime;
        if (timer_val >= spawnInterval)
        {
            SpawnAlgae();
            timer_val = 0f;
        }
    }

    void SpawnAlgae()
    {
        float x_position = Random.Range(-mapWidth, mapWidth);
        float y_position = Random.Range(-mapHeight, mapHeight);
        Instantiate(algaeTemplate, new Vector2(x_position, y_position), Quaternion.identity);
    }
}