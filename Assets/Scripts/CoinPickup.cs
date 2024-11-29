using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1; // ค่าเหรียญที่ Player จะได้รับ
    public GameObject pickupEffect; // เอฟเฟกต์ตอนเก็บ
    public int soundToPlay; // เสียงที่จะเล่นตอนเก็บ
    public float coinRotateSpeed = 1f; // ความเร็วในการหมุนของ Coin

    private CoinPoolManager poolManager; // อ้างถึง CoinPoolManager

    // Start is called before the first frame update
    void Start()
    {
        poolManager = FindObjectOfType<CoinPoolManager>();

        if (poolManager == null)
        {
            Debug.LogError("CoinPoolManager not found in the scene.");
        }
        else
        {
            poolManager.RegisterCoin(gameObject); // ลงทะเบียน Coin ใน Pool
        }
    }

    void Update()
    {
        // หมุน Coin
        transform.Rotate(new Vector3(0f, coinRotateSpeed, 0f), Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddCoins(value); // เพิ่มค่าเหรียญใน GameManager

            // ส่ง Coin กลับ Pool Manager
            if (poolManager != null)
            {
                poolManager.ReturnCoin(gameObject);
            }
            else
            {
                gameObject.SetActive(false); // ปิด Coin หากไม่มี Pool Manager
            }

            // สร้าง Pickup Effect และเล่นเสียง
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }

            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
