using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;

            if (hp <= 0)
            {
                Die();
            }
        }
    }
    [SerializeField] private float spd;
    private Player target;
    private Vector3 moveVec;
    [SerializeField] private int scoreObjSpawnCount;
    [SerializeField] private ScoreObj scoreObj;
    [SerializeField] private ParticleSystem diePc;
    private float z;

    [SerializeField] private EnemyBullet bullet;
    private Coroutine mainAttackCoroutine;
    private Coroutine subAttackCoroutine;

    [SerializeField] private GameObject Head;
    [SerializeField] private Transform headShotPos;
    [SerializeField] private GameObject[] subHead;
    [SerializeField] private Transform[] subShotPos;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    [SerializeField] private float maxDuration = 6;
    [SerializeField] private float duration = 0;
    [SerializeField] private float moveDir = 1;
    private void Start()
    {
        HP = maxHp;
        target = GameObject.Find("Player").GetComponent<Player>();


        mainAttackCoroutine = StartCoroutine(BasicAttack());
        subAttackCoroutine = StartCoroutine(SubAttack());
    }

    private void Update()
    {
        moveVec = target.transform.position - transform.position;
        z = (Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg);

        Move();
    }

    private void Move()
    {
        duration += moveDir * Time.deltaTime;
        if (duration > maxDuration || duration <= 0) moveDir *= -1;
        transform.position = Vector3.Lerp(startPos, endPos, duration / maxDuration);
    }

    private IEnumerator BasicAttack()
    {
        for (int i = 0; i < 6; i++)
        {

            for (int j = 0; j < 3; j++)
            {
                EnemyBullet bulletObj1 = Instantiate(bullet);
                bulletObj1.transform.position = headShotPos.position;

                float dir = 0;

                switch (j)
                {
                    case 0:
                        dir = 15;
                        break;
                    case 1:
                        dir = 0;
                        break;
                    default:
                        dir = -15;
                        break;
                }

                bulletObj1.BulletSet(5, 0, z + dir - 90);
                Head.transform.rotation = Quaternion.Euler(0, 0, z + 90);
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);
        BossPatton();
    }

    private IEnumerator Skill()
    {
        for (int p = 0; p < 2; p++)
        {

            for (int i = 0; i < 360; i += 360 / 20)
            {

                for (int j = 0; j < 3; j++)
                {
                    EnemyBullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.transform.position = headShotPos.position;

                    float dir = 0;

                    switch (j)
                    {
                        case 0:
                            dir = 35;
                            break;
                        case 1:
                            dir = 0;
                            break;
                        default:
                            dir = -35;
                            break;
                    }

                    bulletObj1.BulletSet(5, 0, i + dir + 90);
                    Head.transform.rotation = Quaternion.Euler(0, 0, i - 90);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        BossPatton();
    }
    private IEnumerator Skill2()
    {


        for (int k = 0; k < 3; k++)
        {
            float shotDir = z;

            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    EnemyBullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.transform.position = headShotPos.position;

                    float dir = 0;

                    switch (i)
                    {
                        case 0:
                            dir = 5;
                            break;
                        case 1:
                            dir = 0;
                            break;
                        default:
                            dir = -5;
                            break;
                    }

                    bulletObj1.BulletSet(10, 0, shotDir - 90 + dir);
                    Head.transform.rotation = Quaternion.Euler(0, 0, shotDir + 90);
                }
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.5f);
        BossPatton();
    }

    private void BossPatton()
    {
        if (mainAttackCoroutine != null) StopCoroutine(mainAttackCoroutine);
        switch (Random.Range(0, 3))
        {
            case 0:
                mainAttackCoroutine = StartCoroutine(Skill2());
                break;
            case 1:
                mainAttackCoroutine = StartCoroutine(Skill());
                break;
            case 2:
                mainAttackCoroutine = StartCoroutine(BasicAttack());
                break;
            default:
                break;
        }
    }

    private IEnumerator SubAttack()
    {
        yield return new WaitForSeconds(0.5f);

        for (int j = 0; j < 2; j++)
        {
            EnemyBullet bulletObj1 = Instantiate(bullet);
            bulletObj1.transform.position = subShotPos[j].position;
            bulletObj1.BulletSet(10, 0, z - 90);
            subHead[j].transform.rotation = Quaternion.Euler(0, 0, z + 90);
        }
        subAttackCoroutine = StartCoroutine(SubAttack());
    }
    private void Die()
    {
        StopCoroutine(mainAttackCoroutine);


        ParticleSystem pc = Instantiate(diePc);
        CreateScore();
        pc.transform.position = transform.position;
        Destroy(pc.gameObject, 0.5f);
        Destroy(gameObject);
    }

    private void CreateScore()
    {
        for (int i = 0; i < scoreObjSpawnCount; i++)
        {
            GameObject obj = Instantiate(scoreObj).gameObject;
            obj.transform.position = transform.position
            + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        }
    }
    public void OnDamage(float dmg)
    {
        HP -= dmg;
    }
}
