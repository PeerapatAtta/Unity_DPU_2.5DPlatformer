using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thophy : MonoBehaviour
{
    public int coinValue = 10; // ค่าเหรียญที่ Player จะได้รับ
    public GameObject pickupEffect; // เอฟเฟกต์ตอนเก็บ
    public int soundToPlay; // เสียงที่จะเล่นตอนเก็บ    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddCoins(coinValue); // เพิ่มค่าเหรียญใน GameManager
            Instantiate(pickupEffect, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(soundToPlay);

            //detroy the object
            Destroy(gameObject);

        }
    }
}
