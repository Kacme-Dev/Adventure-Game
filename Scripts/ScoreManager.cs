using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Variável estática acessível de qualquer lugar do código

    public int totalScore; // Armazenará a pontuação total
    public int levelScore;

    public TextMeshProUGUI scoreText; // Mostrar o texto da pontuação

    private bool isRestarting = false; // Controla se o jogo está sendo reiniciado ou não

    // Start is called before the first frame update
    void Start()
    {
        instance = this; // A variável recebe o próprio script

        if (isRestarting)
        {
            ResetScore(); // Reseta o score apenas se o jogo estiver sendo reiniciado
        }

        totalScore = PlayerPrefs.GetInt("TotalScore", 0); // Inicializa a pontuação total como zero

        UpdateScoreText(); // Atualiza o texto da pontuação
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString(); // Atualiza o texto da pontuação para string
    }

    public void SetScore(int score)
    {
        levelScore += score; // Adiciona a pontuação atual da fase à pontuação local da fase
        totalScore += score; // Adiciona a nova pontuação à pontuação total
        PlayerPrefs.SetInt("TotalScore", totalScore);
    }

    public void SetGameOverScore()
    {
        PlayerPrefs.SetInt("FinalScore", totalScore); // Define a pontuação final do jogo no PlayerPrefs
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("TotalScore");
        PlayerPrefs.DeleteKey("FinalScore");
    }

    public void ResetLevelScore()
    {
        levelScore = 0; // Reseta a pontuação local da fase
    }

    public void ResetAllScores()
    {
        ResetScore(); // Reseta a pontuação total
        ResetLevelScore(); // Reseta a pontuação local da fase
    }

    public void ResetTotalScore()
    {
        PlayerPrefs.DeleteKey("TotalScore");
    }

    public void SetRestartFlag(bool value)
    {
        isRestarting = value; // Define a flag de reinício do jogo
    }
}
