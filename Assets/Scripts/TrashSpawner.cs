using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashTemplates;   // Array instead of single prefab
    public float spawnInterval = 3f;
    public float mapWidth = 8f;
    public float mapHeight = 5f;

    private float timer_val;

    void Update()
    {
        // Only spawn while the game is playing.
        if (TrashGameManager.gameInstance == null || TrashGameManager.gameInstance.gameCurrentState != TrashGameManager.GameState.Playing)
            return;

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

        int randomIndex = Random.Range(0, trashTemplates.Length);

        Instantiate(trashTemplates[randomIndex],
                    new Vector2(x_position, y_position),
                    Quaternion.identity);
    }
}