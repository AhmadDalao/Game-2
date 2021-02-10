using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("cloneFrom & container")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotsPrefab;
    [SerializeField] private GameObject _clonedContainer;
    [SerializeField] private spawnManager spawn;
    private GameObject _clonedLaser;

    [Header("player Movement")]
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _speedBooster = 1.8f;
    [SerializeField] private float _moveSpeed = 8f;

    [Header("FireRate Control")]
    private float _lastShot = 0.0f;
    [SerializeField] private float _fireRate = 0.20f;
    [SerializeField] private bool _isTripleShotingActive = false;
    [SerializeField] private bool _isSpeedBoostActice = false;
    [SerializeField] private bool _isShieldActice = false;

    [Header("Damage Taken")]
    private int numberOfLives = 3;
    [SerializeField] private GameObject shieldUI;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2.5f, 0);
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
        float laserOffset = 1.07f;
        Vector3 clonedPosition = new Vector3(transform.position.x, transform.position.y + laserOffset, 0);
        if (_isTripleShotingActive)
        {
            _clonedLaser = Instantiate(_tripleShotsPrefab, clonedPosition, Quaternion.identity);
            _clonedLaser.transform.SetParent(_clonedContainer.transform);
        }
        else
        {
            _clonedLaser = Instantiate(_laserPrefab, clonedPosition, Quaternion.identity);
            _clonedLaser.transform.SetParent(_clonedContainer.transform);
        }
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
        float minY = -2.7f;
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
        if (_isShieldActice)
        {
            _isShieldActice = false;
            shieldUI.SetActive(false);
            Debug.Log("shield destroyed");
            return;
        }
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

    public void tripleShotPowerUp()
    {
        _isTripleShotingActive = true;
        StartCoroutine(tripleShotCooldown(5f));
    }

    private IEnumerator tripleShotCooldown(float delay)
    {
        while (_isTripleShotingActive)
        {
            yield return new WaitForSeconds(delay);
            _isTripleShotingActive = false;
        }
    }

    public void speedBoostPowerUp()
    {
        _isSpeedBoostActice = true;
        _moveSpeed *= _speedBooster;
        StartCoroutine(speedBoostPowerUpCooldown(3f));
    }

    private IEnumerator speedBoostPowerUpCooldown(float delay)
    {
        while (_isSpeedBoostActice)
        {
            yield return new WaitForSeconds(delay);
            _moveSpeed = 8f;
            _isSpeedBoostActice = false;
        }
    }

    public void shieldPowerUp()
    {
        _isShieldActice = true;
        shieldUI.SetActive(true);
    }
}
