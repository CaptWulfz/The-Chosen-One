using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    private bool isMovingRight = true;
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.running) {
            if (isMovingRight) {
                controller.move(speed * Time.deltaTime, isMovingRight);
            } else {
                controller.move(-speed * Time.deltaTime, isMovingRight);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            controller.toggleRunning(!controller.running);

        if (Input.GetKeyDown(KeyCode.Space))
            isMovingRight = !isMovingRight;
        
    }

    private void FixedUpdate() {
        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDir.z = 0.0f;
        mouseDir.Normalize();

        if (Input.GetMouseButtonDown(0) && !controller.lunging && mouseDir.y > 0.1f) {
            controller.lunge(mouseDir);
        }
    }
}
