using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject cpOn, cpOff; // the checkpoint on and off sprites
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //for the player to respawn at the checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(transform.position);

            Checkpoint[] allCP = FindObjectsOfType<Checkpoint>(); // find all the checkpoints in the scene
            for (int i = 0; i < allCP.Length; i++) // loop through all the checkpoints
            {
                allCP[i].cpOff.SetActive(true);
                allCP[i].cpOn.SetActive(false);

            }

            cpOff.SetActive(false);
            cpOn.SetActive(true);

        }
    }
}
