using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("cloneFrom & container")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _clonedContainer;
    [SerializeField] private spawnManager spawn;
    private GameObject _clonedLaser;

    [Header("player Movement")]
    private float _horizontal;
    private float _vertical;
    private float _moveSpeed = 8f;

    [Header("FireRate Control")]
    private float _lastShot = 0.0f;
    private float _fireRate = 0.20f;

    [Header("Damage Taken")]
    private int numberOfLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3f, 0);
        spawn = GameObject.FindWithTag("spawnManager").GetComponent<spawnManager>();
        if (spawn == null)
        {
            Debug.LogError("Spawn is not here");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // control player movement
        playerMovement();
        // control firing laser and  fire rate control
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _lastShot)
        {
            fireRate();
        }
    }

    private void fireRate()
    {
        _lastShot = Time.time + _fireRate;
        float laserOffset = 1f;
        Vector3 clonedPosition = new Vector3(transform.position.x, transform.position.y + laserOffset, 0);
        _clonedLaser = Instantiate(_laserPrefab, clonedPosition, Quaternion.identity);
        _clonedLaser.transform.SetParent(_clonedContainer.transform);
    }

    private void playerMovement()
    {
        //get user input
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        Vector3 directions = new Vector3(_horizontal, _vertical, 0);
        // move game object
        transform.Translate(directions * _moveSpeed * Time.deltaTime);
        // restrictions on the edges
        float maxY = 5.5f;
        float minY = -3.7f;
        if (transform.position.y >= maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, 0);
        }
        else if (transform.position.y <= minY)
        {
            transform.position = new Vector3(transform.position.x, minY, 0);
        }
        int minX = -11;
        int maxX = 11;
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX * -1f, transform.position.y, 0);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX * -1f, transform.position.y, 0);
        }
    }

    public void damagePlayer()
    {
        numberOfLives--;
        Debug.Log("lives left " + numberOfLives);
        // if player lives are 0 destroy player
        if (numberOfLives < 1)
        {
            Destroy(this.gameObject);
            // stop spawning enemies on player death
            spawn.isPlayerDead();
        }
    }

}
