using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _power_ups;
    private GameObject _clonedEnemy;
    private bool _isPlayerDead = false;
    private Vector3 _clonedEnemyPosition;
    private bool _isAsteroidDestroyed = false;

    private IEnumerator spawnEnemyCoroutine()
    {
        float delay = 3.5f;
        while (_isPlayerDead == false)
        {
            float randomPosition = Random.Range(-9f, 9f);
            float max_y = 8f;
            _clonedEnemyPosition = new Vector3(randomPosition, max_y, 0);
            _clonedEnemy = Instantiate(_enemyPrefab, _clonedEnemyPosition, Quaternion.identity);
            _clonedEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(delay);
        }
        if (_isPlayerDead)
        {
            yield return new WaitForSeconds(0.2f);
            destroyEnemyClones();
        }
    }

    private IEnumerator spawnPowerUpCoroutine()
    {
        while (_isPlayerDead == false)
        {
            float max_y = 8f;
            float randomPosition = Random.Range(-9f, 9f);
            int randomPowerUp = Random.Range(0, 3);
            Vector3 powerUpPosition = new Vector3(randomPosition, max_y, 0);
            Instantiate(_power_ups[randomPowerUp], powerUpPosition, Quaternion.identity);
            float randomSpawnTime = Random.Range(3f, 9f);
            yield return new WaitForSeconds(randomSpawnTime);
        }
    }

    private void destroyEnemyClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in clones)
        {
            Destroy(enemy);
        }
    }

    public void isPlayerDead()
    {
        _isPlayerDead = true;
    }

    public void isAsteroidDestroyed()
    {
        _isAsteroidDestroyed = true;
        if (_isAsteroidDestroyed)
        {
            Invoke("startSpawning", 1f);
        }
    }
    private void startSpawning()
    {
        StartCoroutine(spawnEnemyCoroutine());
        StartCoroutine(spawnPowerUpCoroutine());
    }
}
