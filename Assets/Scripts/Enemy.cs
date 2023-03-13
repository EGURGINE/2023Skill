using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObserver
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
                EnemyObserver.Instance.RemoveObserver(this);
                Die();
            }
        }
    }
    [SerializeField] private float spd;
    private Player target;
    private Vector3 moveVec;
    [SerializeField] private int score;
    [SerializeField] private ParticleSystem diePc;

    private void Start()
    {
        HP = maxHp;
        EnemyObserver.Instance.ResisterObserver(this);
        target = GameObject.Find("Player").GetComponent<Player>();
    }


    private void Update()
    {
        moveVec = target.transform.position - transform.position; 

        float z = (Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg);

        transform.rotation = Quaternion.Euler(0, 0, z - 90);

        transform.Translate(Vector2.up * spd * Time.deltaTime);
    }

    private void Die()
    {
        target.Enemys.Remove(gameObject);
        GameManager.Instance.Score += score;



        ParticleSystem pc = Instantiate(diePc);
        pc.transform.position = transform.position;
        Destroy(pc.gameObject,0.5f);
        Destroy(gameObject);
    }


    public void OnDamage(float dmg)
    {
        HP -= dmg;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.Instance.isDodge == true) return;

            Die();
            GameManager.Instance.HP--;
        }
    }

    public void DestroyObj()
    {
        Die();
    }
}
