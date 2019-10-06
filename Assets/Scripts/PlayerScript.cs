using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private bool isMovingRight = false;
    private bool lunging = false;
    private float speed = 2.5f;
    private float launchSpeed = 15.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move= new Vector3(transform.position.x, transform.position.y, transform.position.y); ;
        /*
        if (isMovingRight) {
            move.x += speed * Time.deltaTime;
        } else {
            move.x -= speed * Time.deltaTime;
        }
        */
        transform.position = move;

        if (Input.GetKeyUp(KeyCode.Space)) {
            isMovingRight = !isMovingRight;
        }

        
    }

    private void FixedUpdate() {
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDir.z = 0.0f;
        mouseDir.Normalize();

        faceMouse();

        if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("lunging", true);
            rb.velocity = new Vector2(mouseDir.x * launchSpeed, mouseDir.y * launchSpeed);
            lunging = true;
        }
    }

    private void faceMouse() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();

        difference.Normalize();

        if (difference.x >= 0)
            renderer.flipX = false;
        else if (difference.x < 0)
            renderer.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (lunging && collision.tag == "Floor") {
            lunging = false;
            animator.SetBool("lunging", false);
        }
    }
}
