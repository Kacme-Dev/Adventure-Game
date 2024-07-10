using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer sr; // Variável que referencia o SpriteRenderer
    private CircleCollider2D circle; // Variável que referencia o CircleCollider2D

    public GameObject smoke; // Objeto que referencia o Smoke

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // Recebe o componente Spreite Renderer
        circle = GetComponent<CircleCollider2D>();  // Recebe o componente Circle Collider 2D    
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            sr.enabled = false; // Desativa o componente do objeto
            circle.enabled = false; // Desativa o componente do objeto
            smoke.SetActive(true); // Após destivar o collider a fumaça é ativada

            ScoreManager.instance.SetScore(score); // Chamamos a instância da classe ScoreManager
                                                   // A variável totalScore da classe ScoreManager será somada ao valor do Score
                                                   // Definido em cada fruta (na Unity)

            ScoreManager.instance.UpdateScoreText();

            Destroy(gameObject, 0.3f); // Destrói a fruta depois de certo tempo
        }
    }
}
