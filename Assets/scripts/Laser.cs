using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _moveSpeed = 12f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            moveLaserUp();
        }
        else
        {
            moveLaserDown();
        }
    }

    private void moveLaserUp()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        if (transform.position.y > 7f)
        {
            if (transform.parent.gameObject.tag == "triple_shot_container" && transform.gameObject != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    private void moveLaserDown()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            if (transform.parent.gameObject.tag == "triple_shot_container" && transform.gameObject != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void isEnemyLaserFired()
    {
        _isEnemyLaser = true;
        _moveSpeed = 4f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.playerTakeDamage();
                Destroy(this.gameObject);
            }
        }
    }

}
