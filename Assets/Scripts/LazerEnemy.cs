using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerEnemy : MonoBehaviour, IObserver
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
                BoomObserver.Instance.RemoveObserver(this);
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
    private void Start()
    {
        BoomObserver.Instance.ResisterObserver(this);
        HP = maxHp;
        target = GameObject.Find("Player").GetComponent<Player>();
        moveVec = target.transform.position - transform.position;

        z = (Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg);
    }


    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, z - 90);
        transform.Translate(Vector2.up * spd * Time.deltaTime);
    }

    private void Die()
    {
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (target.isDodge == true || target.isHit == true) return;
            GameManager.Instance.HP--;
            StartCoroutine(target.PlayerHitEffect());
            Die();
        }
    }

    public void DestroyObj()
    {
        Die();
    }

    public Vector3 ThisTransform()
    {
        return transform.position;
    }
}
