using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRestart : MonoBehaviour
{
    public GameObject platform;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            platform.SetActive(true);
        }
    }
}
