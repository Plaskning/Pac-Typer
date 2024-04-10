using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int currentScore;
    public int lastScore;
    public int highScore;

    private string sceneName;

    [SerializeField] private TextMeshProUGUI scoreText;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkHighScore();
        updateHighScore();
    }

    public void checkHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
    }

    private void updateHighScore()
    {
        if (sceneName == "MainScreen")
        {
            scoreText.text = ("Last Score: " + lastScore + "<br>High Score: " + highScore);
        }
        else if (sceneName == "MainLevel")
        {
            scoreText.text = ("Current Score: " + currentScore + "<br>High Score: " + highScore);
        }
    }
}
