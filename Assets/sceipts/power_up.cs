using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private int power_upID;
    private Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (power_upID)
                {
                    case 0:
                        Debug.Log("You picked: triple shot");
                        player.tripleShotPowerUp();
                        break;
                    case 1:
                        Debug.Log("You picked: speed up");
                        player.speedBoostPowerUp();
                        break;
                    case 2:
                        Debug.Log("You picked:  shield");
                        player.shieldPowerUp();
                        break;
                    default:
                        Debug.Log("there is an error");
                        break;
                }
                Destroy(this.gameObject); // destroy power up 
            } // player is not null 
        }
    }
}
