using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Transform torso;
    [SerializeField] private Transform legs;
    [SerializeField] private Transform sword;

    // Start is called before the first frame update
    void Start()
    {

        Physics2D.IgnoreCollision(torso.GetComponent<BoxCollider2D>(), sword.GetComponents<BoxCollider2D>()[0]);
        Physics2D.IgnoreCollision(torso.GetComponent<BoxCollider2D>(), sword.GetComponents<BoxCollider2D>()[1]);
        Physics2D.IgnoreCollision(torso.GetComponent<BoxCollider2D>(), sword.GetComponent<CapsuleCollider2D>());

        Physics2D.IgnoreCollision(legs.GetComponent<CapsuleCollider2D>(), sword.GetComponents<BoxCollider2D>()[0]);
        Physics2D.IgnoreCollision(legs.GetComponent<CapsuleCollider2D>(), sword.GetComponents<BoxCollider2D>()[1]);
        Physics2D.IgnoreCollision(legs.GetComponent<CapsuleCollider2D>(), sword.GetComponent<CapsuleCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
