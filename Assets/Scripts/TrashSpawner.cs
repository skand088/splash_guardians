using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject trashTemplate;
    public float spawnInterval = 3f;
    public float mapWidth = 8f;
    public float mapHeight = 5f;

    private float timer_val;

    void Update()
    {
        timer_val += Time.deltaTime;
        if (timer_val >= spawnInterval)
        {
            SpawnTrash();
            timer_val = 0f;
        }
    }

    void SpawnTrash()
    {
        float x_position = Random.Range(-mapWidth, mapWidth);
        float y_position = Random.Range(-mapHeight, mapHeight);
        Instantiate(trashTemplate, new Vector2(x_position, y_position), Quaternion.identity);
    }
}
