using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPoolManager : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab ของ Coin
    public float respawnTime = 5f; // เวลาสำหรับการ Respawn
    private List<GameObject> coinPool = new List<GameObject>(); // เก็บรายการ Coin ที่ถูกสร้างไว้

    // Start is called before the first frame update
    void Start()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("Coin Prefab is not assigned in CoinPoolManager.");
        }
    }

    // To register a new coin in the pool
    public void RegisterCoin(GameObject coin)
    {
        if (coin == null)
        {
            Debug.LogError("Attempted to register a null Coin.");
            return;
        }

        if (!coinPool.Contains(coin))
        {
            coinPool.Add(coin);
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        if (coin != null)
        {
            StartCoroutine(RespawnCoin(coin));
        }
    }

    private IEnumerator RespawnCoin(GameObject coin)
    {
        coin.SetActive(false); // ปิด Coin
        yield return new WaitForSeconds(respawnTime); // รอเวลาสำหรับ Respawn
        coin.SetActive(true); // เปิด Coin อีกครั้ง
    }
}
