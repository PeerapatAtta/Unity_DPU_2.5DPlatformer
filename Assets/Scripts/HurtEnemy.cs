using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.name);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().TakeDamage();
        }
    }
}
