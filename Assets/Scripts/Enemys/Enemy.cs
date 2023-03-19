using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IObserver
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
    [SerializeField] protected float spd;
    protected Player target;
    protected Vector3 moveVec;
    [SerializeField] private int scoreObjSpawnCount;
    [SerializeField] private ScoreObj scoreObj;
    [SerializeField] private ParticleSystem diePc;
    protected float z;

    [SerializeField] protected EnemyBullet bullet;
    protected Coroutine attackCoroutine;

    [SerializeField] private Items[] Items;
    int spawnItemNum => Random.Range(0, ((int)EItemType.End));

    protected void Start()
    {
        BoomObserver.Instance.ResisterObserver(this);
        HP = maxHp;
        target = GameObject.Find("Player").GetComponent<Player>();
        attackCoroutine = StartCoroutine(Attack());
        Setting();
    }


    protected void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, z - 90);
        transform.Translate(Vector2.up * spd * Time.deltaTime);
    }

    protected abstract void Setting();
    protected abstract IEnumerator Attack();
    private void Die()
    {
        StopCoroutine(attackCoroutine);


        ParticleSystem pc = Instantiate(diePc);
        CreateScore();

        if(Random.Range(0,5) > 3)
        Instantiate(Items[spawnItemNum]).transform.position = transform.position;


        pc.transform.position = transform.position;
        Destroy(pc.gameObject,0.5f);
        Destroy(gameObject);
    }

    private void CreateScore()
    {
        for (int i = 0; i < scoreObjSpawnCount; i++)
        {
            GameObject obj = Instantiate(scoreObj).gameObject;
            obj.transform.position = transform.position 
            + new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-1.5f, 1.5f), 0);
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
            Die();
        }

        if (collision.CompareTag("KillBox"))
        {
            BoomObserver.Instance.RemoveObserver(this);
            Destroy(gameObject);
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
