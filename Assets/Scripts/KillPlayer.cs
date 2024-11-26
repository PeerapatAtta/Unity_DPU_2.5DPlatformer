using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    // public GameManager theGM; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //for the player to die when it touches the kill plane
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has been killed");

        if (other.tag == "Player")
        {
            // Debug.Log("Player has been killed");
            // theGM.Respawn();
            GameManager.instance.Respawn();
        }
    }
}
