using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float delay = Random.Range(spawnDelayMin, spawnDelayMax);
            yield return new WaitForSeconds(delay);
            //spawn me
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            
        }
    }
}
