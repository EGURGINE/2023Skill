using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Boss : Boss
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject robot;
    [SerializeField] private Transform shotPos;
    [SerializeField] private Vector3[] movePos;

    [SerializeField] private float moveCool;

    private bool isMoving;

    private bool is2Phase = false;

    protected override IEnumerator BasicAttack()
    {
        if(is2Phase == false)
        {
            for (int i = 0; i < 360; i += 360 / 20)
            {
                for (int j = 0; j < 6; j++)
                {

                    EnemyBullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.transform.position = shotPos.position;

                    bulletObj1.BulletSet(5, 0, i + 90);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        else
        {
            for (int j = 0; j < 6; j++)
            {
                for (int i = 60; i < 120; i += 120 / 20)
                {

                    EnemyBullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.transform.position = shotPos.position;

                    bulletObj1.BulletSet(5, 0, i + 90);
                }
                    yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(3f);
        PattonAndMove();
    }

    protected override void Move()
    {
        if (isStart == true && HP <= 250 && is2Phase == false) StartCoroutine(Phase2());
    }

    private IEnumerator Phase2()
    {
        is2Phase = true;

        float t = 0;
        Vector3 basicScale = new Vector3(1, 1, 1);
        Vector3 pos = transform.position;
        while (true)
        {
            yield return null;

            t += Time.deltaTime;

            robot.transform.localScale = Vector3.Lerp(basicScale, Vector3.zero, t / 1f);
            robot.transform.position = Vector3.Lerp(pos, pos + Vector3.down * 5, t / 1f);
        }


    }

    private void PattonAndMove()
    {
        BossPatton();
        StartCoroutine(Moving());
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
        if(is2Phase == false)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 360; i += 360 / 20)
                {

                    EnemyBullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.transform.position = shotPos.position;

                    bulletObj1.BulletSet(5, 0, i + 90);
                }
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {

        }
        yield return new WaitForSeconds(3f);
        PattonAndMove();
    }

    protected override IEnumerator Skill2()
    {
        if (is2Phase == false)
        {
            yield return new WaitForSeconds(3f);
            PattonAndMove();
        }
    }

    protected override IEnumerator SubAttack()
    {
        yield return null;
    }
}
