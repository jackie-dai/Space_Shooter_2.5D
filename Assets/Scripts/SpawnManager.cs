using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool isSpawning = true;
    /* PREFABS */
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] powerups;
    /* EDITABLE VARIABLES */
    [SerializeField]
    private float _spawnRate = 3f;
    private float powerUpSpawnRateMAX = 10f;
    private float powerUpSpawnRateMIN = 5f;
    [SerializeField]
    private GameObject _enemyContainer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPlayerDeath() 
    {
        isSpawning = false;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (isSpawning)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (isSpawning)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 7.5f, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(powerups[randomPowerup], spawnPos, Quaternion.identity);
            Debug.Log("POWER UP AVALIABLE");
            yield return new WaitForSeconds(Random.Range(powerUpSpawnRateMIN, powerUpSpawnRateMAX));
        }
    }
}
