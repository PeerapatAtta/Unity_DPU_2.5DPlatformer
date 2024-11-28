using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;
    public GameObject deathEffect;
    public int currentCoins;
    public int levelEndMusic = 8;
    public string levelToLoad;
    public bool isRespawning;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false; // hide the cursor      
        // Cursor.lockState = CursorLockMode.Locked; // lock the cursor  
        respawnPosition = PlayerController.instance.transform.position; // set the respawn position to the player's position
        AddCoins(0); // add 0 coins
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // if the escape key is pressed
        {
            PauseUnpause(); // pause or unpause the game
        }
    }

    public void Respawn()
    {
        // Debug.Log("Player is respawning");
        StartCoroutine(RespawnCo());
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.theCMBrain.enabled = false;
        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        isRespawning = true;
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

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd; // add the coins to the current coins
        UIManager.instance.coinText.text = "" + currentCoins; // update the coin text

    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy) // if the pause screen is active
        {
            UIManager.instance.pauseScreen.SetActive(false); // deactivate the pause screen
            Time.timeScale = 1f; // set the time scale to 1
            // Cursor.visible = false; // hide the cursor
            // Cursor.lockState = CursorLockMode.Locked; // lock the cursor
        }
        else // if the pause screen is not active
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;
            // Cursor.visible = true; // show the cursor
            // Cursor.lockState = CursorLockMode.None; // unlock the cursor
        }
    }

    public IEnumerator LevelEndCo()
    {
        AudioManager.instance.PlayMusic(levelEndMusic); 
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(2f); 

        Debug.Log("Level has ended");
        SceneManager.LoadScene(levelToLoad); 

    }
}
