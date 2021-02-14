using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    [SerializeField] private int _powerUpID;
    private AudioSource _power_up_sound;
    private float _moveSpeed = 5f;
    Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("There is an error");
        }
        _power_up_sound = GameObject.FindWithTag("power_up_sound").GetComponent<AudioSource>();
        if (_power_up_sound == null)
        {
            Debug.LogError("_power_up_sound is null from power_up scripts");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        float minY = -5f;
        if (transform.position.y < minY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        _player.tripleShotPowerUp(5f);
                        break;
                    case 1:
                        _player.moveSpeedPowerUp(3f);
                        break;
                    case 2:
                        _player.shieldPowerUp();
                        break;
                    default:
                        Debug.Log("you don't have any power up");
                        break;
                }
                if (_power_up_sound != null)
                {
                    _power_up_sound.Play();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
