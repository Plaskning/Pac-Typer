using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject target;
    [SerializeField] float distanceToAttack;
    public float distance;
    [SerializeField] private GameObject deathParticle;
    // Start is called before the first frame update

    private GameObject scoreManagerObject;
    private ScoreManager scoreManager;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        scoreManagerObject = GameObject.FindGameObjectWithTag("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
        CalculateDistance();
        //distance = Vector3.Distance(transform.position, target.transform.position);
        Debug.DrawLine(transform.position, target.transform.position, Color.green);
        if (distance < distanceToAttack)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("------------------------GAME OVER------------------------");
        Destroy(target);
        Instantiate(deathParticle, target.transform.position, Quaternion.identity);
        scoreManager.lastScore = scoreManager.currentScore;
        scoreManager.currentScore = 0;
        Invoke("MainScreenLoad", 2.0f);
    }

    public float CalculateDistance()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        return distance;
    }

    public void MainScreenLoad()
    {
        SceneManager.LoadScene("MainScreen");
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("enemy is colliding");
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //      Debug.Log("------------------------GAME OVER------------------------");
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    GUI.color = Color.black;
    //    Handles.Label(transform.position - (transform.position - target.transform.position) / 2, distance.ToString());
    //}
}
