using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Boss : Boss
{
    [SerializeField] private GameObject Head;
    [SerializeField] private Transform headShotPos;
    [SerializeField] private GameObject[] subHead;
    [SerializeField] private Transform[] subShotPos;

    [SerializeField] private float maxDuration = 6;
    [SerializeField] private float duration = 0;
    [SerializeField] private float moveDir = 1;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    protected override IEnumerator BasicAttack()
    {
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

            yield return new WaitForSeconds(1.5f);
            BossPatton();
        }
    }

    protected override void Move()
    {
        duration += moveDir * Time.deltaTime;
        if (duration > maxDuration || duration <= 0) moveDir *= -1;
        transform.position = Vector3.Lerp(startPos, endPos, duration / maxDuration);
    }

    protected override IEnumerator Skill()
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
        yield return new WaitForSeconds(1.5f);
        BossPatton();
    }

    protected override IEnumerator Skill2()
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

        yield return new WaitForSeconds(1.5f);
        BossPatton();
    }

    protected override IEnumerator SubAttack()
    {
        yield return new WaitForSeconds(1f);

        for (int j = 0; j < 2; j++)
        {
            EnemyBullet bulletObj1 = Instantiate(bullet);
            bulletObj1.transform.position = subShotPos[j].position;
            bulletObj1.BulletSet(10, 0, z - 90);
            subHead[j].transform.rotation = Quaternion.Euler(0, 0, z + 90);
        }
        subAttackCoroutine = StartCoroutine(SubAttack());
    }
}
