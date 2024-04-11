using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private float restartTime = 2.0f;
    private GameObject scoreManagerObject;
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManagerObject = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("------------------------GAME OVER------------------------");
        Destroy(player);
        Instantiate(deathParticle, player.transform.position, Quaternion.identity);
        scoreManager.lastScore = scoreManager.currentScore;
        scoreManager.currentScore = 0;
        Invoke("MainScreenLoad", restartTime);
    }

    public void MainScreenLoad()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
