using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;
    public GameObject deathEffect;
    public int maxHealth = 5;
    public int levelEndMusic = 8;
    public string sceneName;
    public bool isRespawning;

    // ใช้ Static Variables สำหรับ HP และ Coins
    public static int currentCoins = 0;
    public static int currentHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;         
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ตรวจสอบว่า currentHealth และ currentCoins ถูกเซ็ตไว้แล้วหรือยัง
        if (currentHealth == 0) // ถ้ายังไม่มีค่า (กรณีที่เป็น 0 คือค่าเริ่มต้น)
        {
            currentHealth = maxHealth; // เซ็ตค่า HP เริ่มต้น
        }

        if (currentCoins == 0) // ตรวจสอบค่า Coins ว่าเคยถูกเซ็ตหรือไม่
        {
            currentCoins = 0; // กำหนดค่าเริ่มต้นถ้าไม่มีค่า
        }

        respawnPosition = PlayerController.instance.transform.position; // เซ็ตตำแหน่ง Spawn
        UIManager.instance.UpdateUI(); // อัปเดต UI
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void Respawn()
    {
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
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn point set to: " + respawnPosition);
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.UpdateUI();
    }

    public void ResetCoinsAndHealth()
    {
        currentCoins = 0;
        currentHealth = maxHealth;
        UIManager.instance.UpdateUI();
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;
        }
    }

    public IEnumerator LevelEndCo()
    {
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(2f);

        Debug.Log("Level has ended");
        SceneManager.LoadScene(sceneName);
    }
}
