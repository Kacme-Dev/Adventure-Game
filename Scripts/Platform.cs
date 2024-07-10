using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float fallingTime;

    private BoxCollider2D boxCollider;
    private TargetJoint2D targetJoint;


    // Start is called before the first frame update
    void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Falling() // Referenciamos tudo o que vai acontecer com a plataforma
    {
        targetJoint.enabled = false; // Vamos iniciar o Joint desabilitado (não cai)
        boxCollider.isTrigger = true; // Vamos ativar o trigger da plataforma
    }

    private void OnCollisionEnter2D(Collision2D collision) // Vamos criar a colisão e chamar a queda da plataforma
    {
        if (collision.gameObject.tag == "Player") // Se colidir com um objeto com a Tag Player
        {
            Invoke("Falling", fallingTime); // Invoca o tempo de queda da plataforma. Será referenciado na Unity
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
