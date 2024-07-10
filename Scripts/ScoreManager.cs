using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Vari�vel est�tica acess�vel de qualquer lugar do c�digo

    public int totalScore; // Armazenar� a pontua��o total
    public int levelScore;

    public TextMeshProUGUI scoreText; // Mostrar o texto da pontua��o

    private bool isRestarting = false; // Controla se o jogo est� sendo reiniciado ou n�o

    // Start is called before the first frame update
    void Start()
    {
        instance = this; // A vari�vel recebe o pr�prio script

        if (isRestarting)
        {
            ResetScore(); // Reseta o score apenas se o jogo estiver sendo reiniciado
        }

        totalScore = PlayerPrefs.GetInt("TotalScore", 0); // Inicializa a pontua��o total como zero

        UpdateScoreText(); // Atualiza o texto da pontua��o
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString(); // Atualiza o texto da pontua��o para string
    }

    public void SetScore(int score)
    {
        levelScore += score; // Adiciona a pontua��o atual da fase � pontua��o local da fase
        totalScore += score; // Adiciona a nova pontua��o � pontua��o total
        PlayerPrefs.SetInt("TotalScore", totalScore);
    }

    public void SetGameOverScore()
    {
        PlayerPrefs.SetInt("FinalScore", totalScore); // Define a pontua��o final do jogo no PlayerPrefs
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("FinalScore");
    }

    public void ResetLevelScore()
    {
        levelScore = 0; // Reseta a pontua��o local da fase
    }

    public void ResetAllScores()
    {
        ResetScore(); // Reseta a pontua��o total
        ResetLevelScore(); // Reseta a pontua��o local da fase
    }

    public void ResetTotalScore()
    {
        PlayerPrefs.DeleteKey("TotalScore");
    }

    public void SetRestartFlag(bool value)
    {
        isRestarting = value; // Define a flag de rein�cio do jogo
    }
}
