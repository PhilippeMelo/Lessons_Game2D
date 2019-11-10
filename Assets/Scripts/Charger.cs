using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public GameObject soundEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("Charge", 1f);
            Instantiate(soundEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
