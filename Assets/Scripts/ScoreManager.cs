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
    public static int highScore;

    private string sceneName;

    private Scene currentScene;
    private Scene nextScene;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("ScoreText");
        scoreText = temp.GetComponent<TextMeshProUGUI>();

        currentScene = SceneManager.GetActiveScene();

        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkHighScore();
        updateHighScore();
        Debug.Log(currentScore);

        currentScene = SceneManager.GetActiveScene();
        if (currentScene != nextScene)
        {
            if (scoreText == null)
            {
                GameObject temp = GameObject.FindGameObjectWithTag("ScoreText");
                scoreText = temp.GetComponent<TextMeshProUGUI>();
            }
        }
        nextScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
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
        else if (sceneName == "MainScene")
        {
            scoreText.text = ("Current Score: " + currentScore + "<br>High Score: " + highScore);               
        }
    }
}
