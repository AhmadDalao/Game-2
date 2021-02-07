using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("clonedEnemy & container")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private GameObject _clonedEnemy;

    [Header("properties")]
    private float _delayTime = 5f;
    private float _minX = -10;
    private float _maxX = 10;
    private float _maxY = 8f;
    private bool _isAlive = false;

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator spawnEnemy()
    {
        while (_isAlive == false)
        {
            float randomPosition = Random.Range(_minX, _maxX);
            Vector3 clonePosition = new Vector3(randomPosition, _maxY, 0);
            _clonedEnemy = Instantiate(_enemyPrefab, clonePosition, Quaternion.identity);
            _clonedEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(_delayTime);
        }
        if (_isAlive)
        {
            // we no longer need these objects of enemies so we destroy them.
            yield return new WaitForSeconds(0.5f);
            destroyEnemyClones();
        }
    }
    private void destroyEnemyClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
    public void isPlayerDead()
    {
        _isAlive = true;
    }
}
