using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public int pickupSound = 7;
    public float jumpUpFactor = 2f;
    public float playerScale = 2f;
    public float effectDuration = 1f;
    public GameObject canvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Pickup(other));

        }
    }

    IEnumerator Pickup(Collider player)
    {
        Debug.Log("Power Up Collected");

        Instantiate(pickupEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(pickupSound);
        PlayerController.instance.jumpForce *= jumpUpFactor;
        PlayerController.instance.transform.localScale *= playerScale;


        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Destroy(gameObject);
        Destroy(canvas);

        yield return new WaitForSeconds(effectDuration);

        PlayerController.instance.jumpForce /= jumpUpFactor;
        PlayerController.instance.transform.localScale /= playerScale;

        Destroy(gameObject);
    }


}

