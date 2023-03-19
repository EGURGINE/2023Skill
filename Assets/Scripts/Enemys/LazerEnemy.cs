using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnemy : Enemy
{
    new private void Update()
    {
        transform.Translate(Vector2.up * spd * Time.deltaTime);
    }

    protected override void Setting()
    {
        moveVec = target.transform.position - transform.position;

        z = -90;

        attackCoroutine = StartCoroutine(Attack());
        transform.rotation = Quaternion.Euler(0, 0, z - 90);
    }

    protected override IEnumerator Attack()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 50; i++)
        {
            EnemyBullet bulletObj1 = Instantiate(bullet);
            bulletObj1.transform.position = transform.position;
            bulletObj1.BulletSet(10, 0, z - 90);
            yield return new WaitForSeconds(0.01f);
        }
        print("shot");
        attackCoroutine = StartCoroutine(Attack());
    }
}
