using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnContact : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            // Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Player hit me");
            Destroy(gameObject);
        }
    }
}
