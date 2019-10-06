using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Animator legsAnimator;

    //Stats
    private float health;
    private float vitality;
    private int strength;
    private int resistance;

    private bool _running = false;
    private bool _lunging = false;
    private bool _movingRight = true;
    private float launchSpeed = 20.0f;

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
        
    }

    public void move(float speed, bool isMovingRight) {
        Vector3 movePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (!_lunging) {
            movePos.x += speed;
            transform.position = movePos;
        }
        _running = true;
        _movingRight = isMovingRight;
        legsAnimator.SetBool("running", _running);
    }

    public void toggleRunning(bool isRunning) {
        _running = isRunning;
        legsAnimator.SetBool("running", _running);
    }

    public void lunge(Vector2 mouseDir) {
        rb.velocity = new Vector2(mouseDir.x * launchSpeed, mouseDir.y * launchSpeed);
        _lunging = true;
        legsAnimator.SetBool("lunging", _lunging);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_lunging && collision.tag == "Floor") {
            _lunging = false;
            legsAnimator.SetBool("lunging", _lunging);
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
