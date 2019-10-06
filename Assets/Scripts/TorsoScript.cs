using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate() {
        faceMouse();
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
}
