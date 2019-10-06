using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPivotScript : MonoBehaviour
{

    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90) {
            if (player.transform.eulerAngles.y == 0)
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            else if (player.transform.eulerAngles.y == 180)
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            
        }

        Vector3 playerDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        playerDifference.Normalize();

        if (playerDifference.x >= 0)
            transform.localPosition = new Vector3(-0.09f, transform.localPosition.y, transform.localPosition.z);
        else if (playerDifference.x < 0)
            transform.localPosition = new Vector3(0.09f, transform.localPosition.y, transform.localPosition.z);


    }
}
