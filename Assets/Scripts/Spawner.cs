using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemys = new List<Enemy>();
    [SerializeField] private List<Transform> spawnPos = new List<Transform> ();


    private void Start()
    {
        StartCoroutine(Stage1Spawn());
    }


    private IEnumerator Stage1Spawn()
    {
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[5].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[7].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[0].position, 2);
    }


    private IEnumerator EnemySpawn(Enemy enemy, Vector3 pos, int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(enemy).gameObject;
            obj.transform.position = pos;

            yield return new WaitForSeconds(0.5f); 
        }
    }

}
