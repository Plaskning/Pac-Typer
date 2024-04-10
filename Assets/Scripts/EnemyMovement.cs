using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameObject target;
    [SerializeField] float distanceToAttack;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
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
            Debug.Log("------------------------GAME OVER------------------------");
        }
    }

    public float CalculateDistance()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        return distance;
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
