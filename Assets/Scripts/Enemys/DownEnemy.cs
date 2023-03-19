using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownEnemy : Enemy
{
    protected override void Setting()
    {
        moveVec = target.transform.position - transform.position;
        attackCoroutine = StartCoroutine(Attack());
        z = -90;
    }

    protected override IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        EnemyBullet bulletObj1 = Instantiate(bullet);
        bulletObj1.transform.position = transform.position;
        bulletObj1.BulletSet(10, 0, z - 90);
        print("shot");
        attackCoroutine = StartCoroutine(Attack());
    }
}
