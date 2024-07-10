using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using JetBrains.Annotations;

public class Player : MonoBehaviour
{
    public static Player instance;

    // Variáveis de movimento
    public float speed; // Velocidade de movimento do jogador
    public float jumpForce; // Força do pulo normal
    public float doubleJumpForce; // Força do pulo duplo

    // Variáveis de pulo
    public bool isJump; // Verifica se o jogador está pulando
    public bool doubleJump; // Verifica se o jogador pode fazer um segundo pulo     

    // Componentes de gravidade e colisão
    private Rigidbody2D Rigidbody; // Componente Rigidbody2D do jogador

    // Conpenentes de animação
    private Animator Animator; // Componente Animator do jogador

    public SpriteRenderer[] lifeHUD;

    public string currentSceneName;

    int life;

    int phaseScore;

    private bool gameCompleted = false;
    private bool isDead = false;
    public bool controlsEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        life = PlayerPrefs.GetInt("PlayerLife", 3);

        UpdateLifeHUD();
        CompletedGame();

        Rigidbody = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D do GameObject do jogador
        Rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Dectecta a colisão contínua pela física
                                                                                // Evita que objetos passem uns pelos outros quando estão se movendo em alta velocidade
        Animator = GetComponent<Animator>(); // Recebe o componete do animator
    }

    // Update is called once per frame
    void Update()
    {
        Move(); // Chama a função para movimentar o jogador
        Jump(); // Chama a função para o pulo do jogador

        GameOver();
    }

    void Move()
    {
        if (!controlsEnabled)
            return;

        // Criamos agora uma variável para o movimento do objeto
        float movement = Input.GetAxis("Horizontal"); // Obtém o valor do eixo horizontal do jogador

        // Define a velocidade do jogador no eixo X com base no input do jogador
        Rigidbody.velocity = new Vector2(movement * speed, Rigidbody.velocity.y); // Passamos para o velocity o eixo horizontal multiplicado pela velocidade
                                                                                  // No eixo Y passamos o próprio velocity para não movimentarmos o eixo Y (travamos o velocity)

        // Verifica a direção do movimento e atualiza a animação e a rotação do jogador
        if (movement > 0f) // Quando estiver andando para a direita vai ser true (executa a transição)
        {
            Animator.SetBool("Walk", true);
            transform.eulerAngles = new Vector2(0f, 0f); // Confirma direção para a direita (rotaciona o player)
        }

        if (movement < 0f) // Quando estiver andando para a esquerda vai ser true (executa a transição)
        {
            Animator.SetBool("Walk", true);
            transform.eulerAngles = new Vector2(0f, 180f); // Confirma direção para a esquerda (rotaciona o player)
        }

        if (movement == 0f) // Quando estiver parado vai ser false (executa a transição)
        {
            Animator.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        if (!controlsEnabled)
            return;

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump) // Se não está pulando, pode pular
            {
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f); // Zera a velocidade vertical antes do pulo
                Rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Aplica força para o pulo normal
                isJump = true; // Define que o jogador está pulando
                Animator.SetBool("Jump", true); // Ativa a animação de pulo
                doubleJump = true; // Permite o pulo duplo
            }

            else if (doubleJump) // Se o jogador jogador está no ar ainda pode fazer o segundo pulo
            {
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f); // Zera a velocidade vertical antes do segundo pulo
                Rigidbody.AddForce(new Vector2(0f, jumpForce * doubleJumpForce), ForceMode2D.Impulse); // Aplica força para o segundo pulo
                doubleJump = false; // Desabilita o pulo duplo
                Animator.SetBool("DoubleJump", true); // Ativa a animação de segundo pulo
            }
        }
    }

    // Função chamada quando o jogador colide com outro objeto 2D
    private void OnCollisionEnter2D(Collision2D collision) // Player está tocando no chão
    {
        if (collision.gameObject.layer == 6)
        {
            isJump = false; // Define que o jogador não está pulando

            Animator.SetBool("Jump", false); // Se tocou no chão, desativa a animação de pulo
            Animator.SetBool("DoubleJump", false); // Desativa a animação de segundo pulo
        }

        if (collision.gameObject.tag == "Spikes")
        {
            phaseScore = ScoreManager.instance.levelScore;

            life--;
            SetLife(life);
            //Debug.Log("Morreu" + life);

            LevelRestart();

            ScoreManager.instance.totalScore -= phaseScore;
            ScoreManager.instance.UpdateScoreText();
            ScoreManager.instance.ResetLevelScore();

            if (!isDead)
            {
                ScoreManager.instance.SetScore(0);
                isDead = true;
            }

            if (life >= 0)
            {
                lifeHUD[life].enabled = false;
            }

            if (life <= 0)
            {
                ScoreManager.instance.SetGameOverScore();
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    // Função chamada quando o jogador deixa de colidir com outro objeto 2D
    private void OnCollisionExit2D(Collision2D collision) // Player não está tocando no chão
    {
        if (collision.gameObject.layer == 6)
        {
            isJump = true; // Define que o jogador está pulando    
        }
    }

    public void SetLife(int life)
    {
        PlayerPrefs.SetInt("PlayerLife", life);

        if (life <= 0)
        {
            life = 3;

            PlayerPrefs.SetInt("PlayerLife", life);
        }
    }

    void UpdateLifeHUD()
    {
        for (int i = 0; i < lifeHUD.Length; i++)
        {
            if (i < life)
            {
                lifeHUD[i].enabled = true;
            }

            else
            {
                lifeHUD[i].enabled = false;
            }
        }
    }

    public void LevelRestart()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void GameOver()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            speed = 0;
            jumpForce = 0;
            Animator.SetBool("Walk", false);
            Animator.SetBool("Jump", false);
            Animator.SetBool("DoubleJump", false);

            ScoreManager.instance.SetGameOverScore();
        }
    }

    public void CompletedGame()
    {
        if (SceneManager.GetActiveScene().name == "Winner")
        {
            gameCompleted = true;

            life = 3;

            SetLife(life);
            UpdateLifeHUD();

            ScoreManager.instance.SetGameOverScore();
        }
    }

    public void ResetPlayer()
    {
        life = 3;
        SetLife(life);
        UpdateLifeHUD();

        phaseScore = 0;
    }
}
