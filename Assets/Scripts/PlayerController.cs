using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameHandler gameHandler;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator legsAnimator;
    [SerializeField] private CapsuleCollider2D legsCollider;

    //Stats
    private float health;
    private float vitality;
    private int strength;
    private int resistance;

    private bool _running = false;
    private bool _lunging = false;
    private bool _movingRight = true;
    private float lungeSpeed = 20.0f;

    private bool isDamaged = false;
    private float damagedTime = 0.0f;

    private bool isKnockedback = false;
    private float knockback = 16.0f;
    private float falloff = -10.0f;
    private bool knockbackRight = false;
    private bool knockbackLeft = false;
    private float knockbackCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        vitality = 0;
        strength = 0;
        resistance = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            Debug.Log("Dead sir");
        }

        if (isDamaged) {
            playerDamaged();
            if (damagedTime > 0)
                damagedTime -= Time.deltaTime;
            else if (damagedTime <= 0)
                isDamaged = false;
        }
;
        if (knockbackCount > 0) {
            if (knockbackRight) {
                rb.velocity = new Vector2(-knockback, knockback / 2);

            } else if (knockbackLeft) {
                rb.velocity = new Vector2(knockback, knockback / 2);

            }
            knockbackCount -= Time.deltaTime;
        } else if (isKnockedback) {
            float fallX = falloff / 2;
            if (knockbackLeft)
                fallX = Mathf.Abs(fallX);
            rb.velocity = new Vector2(fallX, falloff);
        }
    }

    public void move(float speed, bool isMovingRight) {
        if (!isKnockedback) {
            Vector3 movePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (!_lunging) {
                movePos.x += speed;
                transform.position = movePos;
            }
            _running = true;
            _movingRight = isMovingRight;
            legsAnimator.SetBool("running", _running);
        }
    }

    public void toggleRunning(bool isRunning) {
        _running = isRunning;
        legsAnimator.SetBool("running", _running);
    }

    public void lunge(Vector2 mouseDir) {
        rb.velocity = new Vector2(mouseDir.x * lungeSpeed, mouseDir.y * lungeSpeed);
        _lunging = true;
        legsCollider.enabled = false;
        legsAnimator.SetBool("lunging", _lunging);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Enemy") {
            float damageReceived = gameHandler.receiveDamage(this.gameObject, collision.collider.gameObject);

            //Knockback
            Vector2 difference = collision.collider.gameObject.transform.position - this.transform.position;

            difference.Normalize();

            if (difference.x >= 0)
                knockbackRight = true;
            else if (difference.x < 0)
                knockbackLeft = true;

            isKnockedback = true;
            isDamaged = true;

            damagedTime = 2.0f;
            knockbackCount = 0.2f;

            health -= damageReceived;

        }
    }

    private IEnumerator playerDamaged() {
        SpriteRenderer torsoRenderer = gameObject.transform.Find("Torso").GetComponent<SpriteRenderer>();
        SpriteRenderer legsRenderer = gameObject.transform.Find("Legs").GetComponent<SpriteRenderer>();

        Debug.Log("damaged");

        torsoRenderer.color = new Color(1f, 1f, 1f, 0f);
        legsRenderer.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        torsoRenderer.color = new Color(1f, 1f, 1f, 1f);
        legsRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Floor") {
            if (lunging) {
                _lunging = false;
                legsCollider.enabled = true;
                legsAnimator.SetBool("lunging", _lunging);
            }
            if (isKnockedback) {
                isKnockedback = false;
                knockbackLeft = false;
                knockbackRight = false;
            }
        }
    }

    public bool lunging {
        get {
            return _lunging;
        }
    }

    public bool running {
        get {
            return _running;
        }
    }

    public bool movingRight {
        get {
            return _movingRight;
        }
    }

}
