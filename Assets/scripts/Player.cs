using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotLaserPrefab;
    [SerializeField] private GameObject _laserContainer;
    [SerializeField] private GameObject _shieldUI;

    private GameObject _clonedLaser;
    private float _moveSpeed = 8f;
    private float _horizontalMove;
    private float _verticalMove;
    private float _fireRate = 0.20f;
    private float _lastShotFired = 0.0f;
    private Vector3 _clonedLaserPosition;
    private float _offset = 1.05f;
    private int _numberOfLives = 3;
    private bool _isTripleShotActice = false;
    private bool _isSpeedUpActice = false;
    private bool _isShieldActice = false;
    private int _score = 0;
    private int _newLiveLimit = 100;
    private spawnManager _spawn;
    private UI_manager _UI_manager;

    // Start is called before the first frame update
    void Start()
    {
        float startingY = -3f;
        transform.position = new Vector3(0, startingY, 0);
        _spawn = GameObject.FindWithTag("spawn_manager").GetComponent<spawnManager>();
        if (_spawn == null)
        {
            Debug.LogError("spawn manager not found");
        }
        _UI_manager = GameObject.FindWithTag("UI_manager").GetComponent<UI_manager>();
        if (_UI_manager == null)
        {
            Debug.LogError("ui manager not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _lastShotFired)
        {
            shotingLaser();
        }
    }


    public void updatePlayerScore(int points)
    {
        _score += points;
        addNewLive();
        _UI_manager.updateScoreText(_score);
    }

    private void addNewLive()
    {
        if (_score >= _newLiveLimit && _numberOfLives != 3)
        {
            if (_numberOfLives < 3 && _numberOfLives > 0)
            {
                _numberOfLives++;
                _UI_manager.updateLivesOnDamage(_numberOfLives);
                Debug.Log("new live added you have " + _numberOfLives);
                _score = 0;
            }
        }
    }

    public void playerTakeDamage()
    {
        if (_isShieldActice)
        {
            _shieldUI.SetActive(false);
            _isShieldActice = false;
            return;
        }
        _score = 0;
        _UI_manager.updateScoreText(_score);
        _numberOfLives--;

        Debug.Log("lives remaning " + _numberOfLives);
        _UI_manager.updateLivesOnDamage(_numberOfLives);

        if (_numberOfLives < 1)
        {
            Destroy(this.gameObject);
            _spawn.isPlayerDead();
            _UI_manager.gameOverScreen();
        }
    }

    private void shotingLaser()
    {
        if (_isTripleShotActice)
        {
            _lastShotFired = Time.time + _fireRate;
            _clonedLaserPosition = new Vector3(transform.position.x, transform.position.y + _offset, 0);
            _clonedLaser = Instantiate(_tripleShotLaserPrefab, _clonedLaserPosition, Quaternion.identity);
            _clonedLaser.transform.SetParent(_laserContainer.transform);
        }
        else
        {
            _lastShotFired = Time.time + _fireRate;
            _clonedLaserPosition = new Vector3(transform.position.x, transform.position.y + _offset, 0);
            _clonedLaser = Instantiate(_laserPrefab, _clonedLaserPosition, Quaternion.identity);
            _clonedLaser.transform.SetParent(_laserContainer.transform);
        }
    }

    public void tripleShotPowerUp(float cooldownDelay)
    {
        _isTripleShotActice = true;
        StartCoroutine(tripleShotCooldown(cooldownDelay));
    }

    public void moveSpeedPowerUp(float cooldownDelay)
    {
        _isSpeedUpActice = true;
        _moveSpeed *= 1.5f;
        StartCoroutine(moveSpeedCooldown(cooldownDelay));
    }

    public void shieldPowerUp()
    {
        _isShieldActice = true;
        _shieldUI.SetActive(true);
    }

    private IEnumerator moveSpeedCooldown(float delay)
    {
        while (_isSpeedUpActice)
        {
            yield return new WaitForSeconds(delay);
            _isSpeedUpActice = false;
            _moveSpeed = 8f;
        }
    }

    private IEnumerator tripleShotCooldown(float delay)
    {
        while (_isTripleShotActice)
        {
            yield return new WaitForSeconds(delay);
            _isTripleShotActice = false;
        }
    }

    private void playerMovement()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");
        Vector3 directions = new Vector3(_horizontalMove, _verticalMove, 0);
        transform.Translate(directions * _moveSpeed * Time.deltaTime);
        float maxY = 5.3f;
        float minY = -3.5f;
        if (transform.position.y >= maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, 0);
        }
        else if (transform.position.y <= minY)
        {
            transform.position = new Vector3(transform.position.x, minY, 0);
        }
        float max_x = 11f;
        float min_x = -11f;
        if (transform.position.x > max_x)
        {
            transform.position = new Vector3(max_x * -1f, transform.position.y, 0);
        }
        else if (transform.position.x < min_x)
        {
            transform.position = new Vector3(min_x * -1f, transform.position.y, 0);
        }
    }

}
