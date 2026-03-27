using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashTemplates;   // Array instead of single prefab
    public float spawnInterval = 3f;
    public float mapWidth = 8f;
    public float mapHeight = 5f;
    public GameObject[] seashellTemplates;
    //for seashells
    public float seashellSpawnInterval = 5f;
    private float seashellTimer;


    private float timer_val;

    void Update()
    {
        timer_val += Time.deltaTime;
        if (timer_val >= spawnInterval)
        {
            SpawnTrash();
            timer_val = 0f;
        }

        seashellTimer += Time.deltaTime;
        if (seashellTimer >= seashellSpawnInterval)
        {
            SpawnSeashell();
            seashellTimer = 0f;
        }
    }
    void SpawnTrash()
    {
        float x = Random.Range(-mapWidth, mapWidth);
        float y = Random.Range(-mapHeight, mapHeight);
        int randomIndex = Random.Range(0, trashTemplates.Length);
        Instantiate(trashTemplates[randomIndex], new Vector2(x, y), Quaternion.identity);
    }

    void SpawnSeashell()
    {
        if (seashellTemplates.Length == 0) return;
        float x = Random.Range(-mapWidth, mapWidth);
        float y = Random.Range(-mapHeight, mapHeight);
        int randomIndex = Random.Range(0, seashellTemplates.Length);
        Instantiate(seashellTemplates[randomIndex], new Vector2(x, y), Quaternion.identity);
    }
}