using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Boss : MonoBehaviour, IObserverBoos
{
    [SerializeField] private GameObject bossHPUI;
    [SerializeField] private Image bossHPImage;

    [SerializeField] protected float maxHp;
    [SerializeField] private float hp;
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

            bossHPImage.fillAmount = hp / maxHp;
        }
    }
    [SerializeField] private float spd;
    private Player target;
    private Vector3 moveVec;
    [SerializeField] private int scoreObjSpawnCount;
    [SerializeField] private ScoreObj scoreObj;
    [SerializeField] private ParticleSystem diePc;
    protected float z;

    [SerializeField] protected EnemyBullet bullet;
    protected Coroutine mainAttackCoroutine;
    protected Coroutine subAttackCoroutine;

    protected bool isStart = false;
    

    

    
    private void Start()
    {
        BoomObserver.Instance.ResisterObserver(this);

        bossHPUI.SetActive(true);
        StartCoroutine(StartHP());
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

    protected abstract void Move();
   

    private IEnumerator StartHP()
    {
        float time = 0;
        while (time < 1)
        {
            yield return null;
            time += Time.deltaTime;
            HP = Mathf.Lerp(1, maxHp, time / 1);
        }
        isStart = true;
    }

    protected abstract IEnumerator BasicAttack();
    

    protected abstract IEnumerator Skill();

    protected abstract IEnumerator Skill2();
    

    protected void BossPatton()
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

    protected abstract IEnumerator SubAttack();
    
    private void Die()
    {
        BoomObserver.Instance.RemoveObserver(this);
        if(mainAttackCoroutine != null)
        StopCoroutine(mainAttackCoroutine);

        ParticleSystem pc = Instantiate(diePc);
        CreateScore();
        pc.transform.position = transform.position;
        Destroy(pc.gameObject, 0.5f);
        Destroy(gameObject);

        GameManager.Instance.isClear();
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

    public void OnBoomDamage()
    {
        OnDamage(80);
    }
}
