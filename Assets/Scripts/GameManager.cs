using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // reference to the game manager

    private Vector3 respawnPosition; // the respawn position

    private void Awake()
    {
        instance = this; // set the instance to this game manager
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; // hide the cursor      
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor  

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
        PlayerController.instance.gameObject.SetActive(false); // deactivate the player for a moment
        CameraController.instance.theCMBrain.enabled = false; // disable the cinemachine brain
        UIManager.instance.fadeToBlack = true; // fade to black

        yield return new WaitForSeconds(2f); // wait for 2 seconds

        UIManager.instance.fadeFromBlack = true; // fade from black
        PlayerController.instance.transform.position = respawnPosition; // set the player's position to the respawn position
        CameraController.instance.theCMBrain.enabled = true; // enable the cinemachine brain        
        PlayerController.instance.gameObject.SetActive(true); // activate the player
    }

}
