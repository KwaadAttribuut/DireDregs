using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySlime;
    [SerializeField] private float waveInterval = 5f;
    private bool enemySpawnActive = false;

    void Start()
    {
        StartCoroutine(spawnEnemy(waveInterval, enemySlime));
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        enemySpawnActive = true;
    }

    void OTriggerExit2D(Collider2D collision)
    {
        enemySpawnActive = false;
    }
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        if (enemySpawnActive)
        {
            GameObject newEnemy = Instantiate(enemy, transform.position + new Vector3(Random.Range(-12f, 12f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        }
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
