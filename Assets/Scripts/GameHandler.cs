using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Transform torso;
    [SerializeField] private Transform legs;
    [SerializeField] private Transform sword;

    private List<GameObject> enemyList = new List<GameObject>();

    float timer = 0;
    bool timerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        IgnorePlayerToSwordCollisions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void IgnorePlayerToSwordCollisions() {
        //Ignore Collisions
        //Physics2D.ignore
        Physics2D.IgnoreCollision(torso.GetComponent<BoxCollider2D>(), sword.GetComponents<BoxCollider2D>()[0]);
        Physics2D.IgnoreCollision(torso.GetComponent<BoxCollider2D>(), sword.GetComponents<BoxCollider2D>()[1]);
        Physics2D.IgnoreCollision(torso.GetComponent<BoxCollider2D>(), sword.GetComponent<CapsuleCollider2D>());

        Physics2D.IgnoreCollision(legs.GetComponent<CapsuleCollider2D>(), sword.GetComponents<BoxCollider2D>()[0]);
        Physics2D.IgnoreCollision(legs.GetComponent<CapsuleCollider2D>(), sword.GetComponents<BoxCollider2D>()[1]);
        Physics2D.IgnoreCollision(legs.GetComponent<CapsuleCollider2D>(), sword.GetComponent<CapsuleCollider2D>());
    }

    public void addToEnemyList(GameObject enemy) {
        enemyList.Add(enemy);
    }

    public void removeFromEnemyList(GameObject enemy) {
        enemyList.Remove(enemy);
    }

    private GameObject getEnemy(GameObject enemy) {
        int index = enemyList.IndexOf(enemy);
        return enemyList[index];
    }

    public float receiveDamage(GameObject receiver, GameObject giver) {
        float damage = 0.0f;

        if (receiver.tag == "Player") {
            GameObject enemy = getEnemy(giver);
            damage = enemy.GetComponent<EnemyScript>().damage;
        } else if (receiver.tag == "Enemy") {
            damage = sword.GetComponent<SwordScript>().damage;
        }

        return damage;
    }
}
