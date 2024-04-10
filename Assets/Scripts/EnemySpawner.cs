using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] float spawnDelayMin;
    [SerializeField] float spawnDelayMax;
    [SerializeField] AnimationCurve spawnRateMultiplier;
    [SerializeField] float timer;
    private float tempMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float tempMultiplier = spawnRateMultiplier.Evaluate(timer);
            float delay = Random.Range(spawnDelayMin, spawnDelayMax) * tempMultiplier;
            yield return new WaitForSeconds(delay);
            //spawn me
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            
        }
    }
}
