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
    // Start is called before the first frame update

    private GameManager gameManager;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");

        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
        CalculateDistance();
        //distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < distanceToAttack)
        {
            gameManager.GameOver();
        }
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
