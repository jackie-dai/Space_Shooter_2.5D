using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnRate = 5f;
    [SerializeField]
    private GameObject _enemyContainer;
    public bool isSpawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPlayerDeath() 
    {
        isSpawning = false;
    }
    IEnumerator SpawnRoutine()
    {
        while (isSpawning)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f, 9f), 7.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
