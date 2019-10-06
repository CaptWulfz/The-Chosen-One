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
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();

        if (controller.movingRight)
            renderer.flipX = false;
        else
            renderer.flipX = true;
    }
}
