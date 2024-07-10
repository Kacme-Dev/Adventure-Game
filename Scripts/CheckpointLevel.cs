using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointLevel : MonoBehaviour
{
    public string lvlName; // Variável para receber o nome da cena (Level)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(lvlName);
        }
    }
}
