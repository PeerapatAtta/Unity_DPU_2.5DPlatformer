using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private Vector3 respawnPosition;
    public GameObject deathEffect;

    private void Awake()
    {
        instance = this; // set the instance to this game manager
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false; // hide the cursor      
        // Cursor.lockState = CursorLockMode.Locked; // lock the cursor  

        respawnPosition = PlayerController.instance.transform.position; // set the respawn position to the player's position

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        Debug.Log("I am respawning");
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false); 
        CameraController.instance.theCMBrain.enabled = false;
        UIManager.instance.fadeToBlack = true; 
        Instantiate(deathEffect, PlayerController.instance.transform.position+new Vector3(0f,1f,0f), PlayerController.instance.transform.rotation); 

        yield return new WaitForSeconds(2f);

        HealthManager.instance.ResetHealth(); 
        UIManager.instance.fadeFromBlack = true; // fade from black
        PlayerController.instance.transform.position = respawnPosition; // set the player's position to the respawn position
        CameraController.instance.theCMBrain.enabled = true; // enable the cinemachine brain        
        PlayerController.instance.gameObject.SetActive(true); // activate the player
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint; // set the respawn position to the new spawn point
        Debug.Log("Spawn point set to: " + respawnPosition);
    }

}
