using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player triggered enter with water");
        GameManager.instance.Respawn();

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player respawned");
            GameManager.instance.Respawn();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Player collided enter with water");

        if (other.gameObject.CompareTag("Player"))
        {

            GameManager.instance.Respawn();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("Player collided stay with water");

        if (other.gameObject.tag == "Player")
        {            
            GameManager.instance.Respawn();
        }
    }
}
