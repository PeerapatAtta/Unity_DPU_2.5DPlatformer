using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Transform pointA; // จุดเริ่มต้น
    public Transform pointB; // จุดปลายทาง
    public float speed = 2f; // ความเร็ว
    private Transform target;

    void Start()
    {
        target = pointB; // เริ่มเคลื่อนที่ไปยัง Point B
    }


   private void FixedUpdate()
    {
        // เคลื่อนที่แพลตฟอร์มระหว่าง Point A และ B
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // เปลี่ยนเป้าหมายเมื่อถึงจุด
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
         other.transform.SetParent(transform);

        // if (other.CompareTag("Player"))
        // {
        //     other.transform.SetParent(transform); // ทำให้ Player เป็นลูกของแพลตฟอร์ม
        // }
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("TriggerStay");
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerExit");
        other.transform.SetParent(null);

        // if (other.CompareTag("Player"))
        // {
        //     other.transform.SetParent(null); // ปลด Player ออกจากแพลตฟอร์ม
        // }
    }
}
