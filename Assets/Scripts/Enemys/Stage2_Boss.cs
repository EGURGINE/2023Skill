using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Boss : Boss
{
    [SerializeField] private GameObject model;
    [SerializeField] private Transform shotPos;
    [SerializeField] private Vector3[] movePos;

    [SerializeField] private float moveCool;
    private float moveT;

    private bool isMoving;

    protected override IEnumerator BasicAttack()
    {

        for (int i = 0; i < 360; i += 360 / 10)
        {

            for (int j = 0; j < 3; j++)
            {
                EnemyBullet bulletObj1 = Instantiate(bullet);
                bulletObj1.transform.position = shotPos.position;

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


        yield return new WaitForSeconds(1.5f);
        BossPatton();
    }

    protected override void Move()
    {
        moveT += Time.deltaTime;
        if(moveT >= moveCool)
        {

            StartCoroutine(Moving());

            moveT = 0;
        }
    }


    private IEnumerator Moving()
    {
        isMoving = true;

        Vector3 basicScale = new Vector3(1, 1, 1);
        float t = 0;


        while (t <= 0.5f)
        {
            yield return null;
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(basicScale, Vector3.zero, t / 0.5f);
            model.transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, Vector3.up * 360, t/0.5f));
        }

        Vector3 targetPos = movePos[Random.Range(0,3)];
        transform.position = targetPos;
        t = 0;

        while (t <= 0.5f)
        {
            yield return null;
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, basicScale, t / 0.5f);
            model.transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.up * 360, Vector3.zero, t / 0.5f));
        }

        yield return new WaitForSeconds(2f);
        isMoving = false;
    }


    protected override IEnumerator Skill()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Skill2()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator SubAttack()
    {
        throw new System.NotImplementedException();
    }
}
