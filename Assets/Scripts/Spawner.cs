using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
    [SerializeField] private List<GameObject> boss = new List<GameObject>();
    [SerializeField] private List<GameObject> enemys = new List<GameObject>();
    [SerializeField] private List<Transform> spawnPos = new List<Transform>();

    [SerializeField] private Transform stage1BossPos;
    private void Start()
    {
        StartCoroutine(Stage1Spawn());
    }

    private IEnumerator MeteorSpawn()
    {
        yield return new WaitForSeconds(8f);

        if(Random.Range(0,2) > 0)
        Instantiate(meteor, spawnPos[Random.Range(0,6)]);

        StartCoroutine(MeteorSpawn());
    }
    private IEnumerator Stage1Spawn()
    {
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[0], spawnPos[4].position, 1);
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[2].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[3].position, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemySpawn(enemys[0], spawnPos[0].position, 1));
        StartCoroutine(EnemySpawn(enemys[2], spawnPos[1].position, 1));
        StartCoroutine(EnemySpawn(enemys[1], spawnPos[3].position, 1));
        yield return EnemySpawn(enemys[2], spawnPos[4].position, 1);
        yield return new WaitForSeconds(5f);
        StartCoroutine(EnemySpawn(boss[0], stage1BossPos.position, 1));

    }


    private IEnumerator EnemySpawn(GameObject enemy, Vector3 pos, int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemy, pos, enemy.transform.rotation);

            yield return new WaitForSeconds(0.5f);
        }
    }

}
