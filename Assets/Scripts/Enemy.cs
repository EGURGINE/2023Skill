using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;

            if (hp > 0)
            {
                Die();
            }
        }
    }
    [SerializeField] private float spd;
    private Player target;
    private Vector3 moveVec;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Player>();
    }


    private void Update()
    {
        moveVec = target.transform.position - transform.position;

        float z = (Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg);

        transform.rotation = Quaternion.Euler(0, 0, z - 90);

        transform.position = Vector3.Lerp(transform.position, target.transform.position , spd * Time.deltaTime);
    }

    private void Die()
    {
        target.Enemys.Remove(this.gameObject);
        Destroy(gameObject);
    }


    public void OnDamage(float dmg)
    {
        HP -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }
}
