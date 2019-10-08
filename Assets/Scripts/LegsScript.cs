using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsScript : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();

        if (controller.running) {
            if (controller.movingRight)
                renderer.flipX = false;
            else
                renderer.flipX = true;
        } else {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            difference.Normalize();

            if (difference.x >= 0)
                renderer.flipX = false;
            else if (difference.x < 0)
                renderer.flipX = true;
        }
    }
}
