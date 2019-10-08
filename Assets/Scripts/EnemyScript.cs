using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;

    private string type = "Ground";

    private float health = 100.0f;
    private float _damage = 20.0f;
    private float speed = 5.0f;
    private Vector2 difference;

    private bool playerInRange = false;

    private Vector3 playerPos = new Vector2();

    private void Awake() {
        gameHandler.addToEnemyList(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            gameHandler.removeFromEnemyList(this.gameObject);
            Destroy(this.gameObject);
        }

        Vector2 move = new Vector2(transform.position.x, transform.position.y);
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();

        if (playerInRange) {
            if (difference.x > 0) {
                move.x += speed * Time.deltaTime;
                renderer.flipX = true;
            } else if (difference.x < 0) { 
                move.x -= speed * Time.deltaTime;
                renderer.flipX = false;
            }

            difference = playerPos - transform.position;
        }

        transform.position = move;

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Sword") {
            float damageReceived = gameHandler.receiveDamage(this.gameObject, collision.collider.gameObject);
            health -= damageReceived;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerPos = collision.transform.position;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerInRange = false;
        }
    }

    public float damage {
        get {
            return _damage;
        }
    }
}
