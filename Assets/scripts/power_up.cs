using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    [SerializeField] int powerUpID;
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
                switch (powerUpID)
                {
                    case 0:
                        _player.tripleShotPowerUp(5f);
                        Destroy(this.gameObject);
                        break;
                    case 1:
                        _player.moveSpeedPowerUp(3f);
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        _player.shieldPowerUp();
                        Destroy(this.gameObject);
                        break;
                    default:
                        Debug.Log("you don't have any power up");
                        break;
                }
            }
        }
    }
}
