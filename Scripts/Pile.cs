using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public GameObject pile;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pile.SetActive(true);
        }
    }
}
