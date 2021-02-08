using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour
{
    // Start is called before the first frame update
    private float _moveSpeed = 3f;
    void Start()
    {

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
            Debug.Log("You picked power up");
            Player player = other.transform.GetComponent<Player>();
            player.power_upPicked();
            Destroy(this.gameObject); // destroy power up
        }
    }
}
