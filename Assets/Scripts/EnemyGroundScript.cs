using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundScript : MonoBehaviour
{
    [SerializeField] private SwordScript sword;
    private float health = 100.0f;
    private float speed = 5.0f;
    private Vector2 difference;

    private bool playerInRange = false;

    private Vector3 playerPos = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
            Destroy(this.gameObject);

        Vector2 move = new Vector2(transform.position.x, transform.position.y);

        if (playerInRange) {
            if (difference.x > 0)
                move.x -= speed * Time.deltaTime;
            else if (difference.x < 0)
                move.x += speed * Time.deltaTime;

            difference = transform.position - playerPos;
        }

        transform.position = move;

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Sword")
            health -= sword.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerPos = collision.transform.position;
            difference = transform.position - playerPos;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerInRange = false;
        }
    }
}
