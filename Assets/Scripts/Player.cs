using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using Microsoft.Unity.VisualStudio.Editor;

public class Player : Singleton<Player>
{
    [SerializeField] private GameObject playerImage;
    private SpriteRenderer sr;
    [SerializeField] private GameObject rotObj;

    [SerializeField] private float spd;
    [SerializeField] private float dmg;
    [SerializeField] private float bulletSpd;
    [SerializeField] private float shotCool;
    private int specialShotCnt;
    private float shotT;
    private float z;


    public bool isHit;
    public bool isDodge;
    [SerializeField] private float dodgeCool;
    private float dodgeT;

    [SerializeField] private float boomCool;
    private float boomT;

    [SerializeField] private float durabilityCool;
    private float durabilityT;

    public List<GameObject> Enemys = new List<GameObject>();
    private GameObject target;


    [SerializeField] private Bullet bullet;
    private Vector3 moveVec;

    [SerializeField] private ParticleSystem boomPc;

    private Color basicColor = new Color(255, 255, 255, 255);
    private Color hitColor = new Color(255, 255, 255, 10);

    private void Start()
    {
        sr = playerImage.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMove();
        FindEnemy();
        Attack();
        Dodge();
        Boom();
        DurabilityRepair();
    }

    private void Attack()
    {
        shotT += Time.deltaTime;
        if (Input.GetKey(KeyCode.Z) && shotT >= shotCool)
        {

            if(specialShotCnt >= 3)
            {
                Bullet bulletObj1 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, z));
                bulletObj1.BulletSet(bulletSpd, dmg, z - 75);
                Bullet bulletObj2 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, z));
                bulletObj2.BulletSet(bulletSpd, dmg, z - 90);
                Bullet bulletObj3 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, z));
                bulletObj3.BulletSet(bulletSpd, dmg, z - 105);
                specialShotCnt = 0;
            }
            else
            {
                Bullet bulletObj = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, z));
                bulletObj.BulletSet(bulletSpd, dmg, z - 90);

                specialShotCnt++;
            }

            shotT = 0;
        }
    }


    private GameObject DistanceEnemy()
    {
        var neareastObject = Enemys.OrderBy(obj =>
        {
            return Vector3.Distance(transform.position, obj.transform.position);
        }).FirstOrDefault();

        return neareastObject;
    }
    
    private void FindEnemy()
    {
        if (Enemys.FirstOrDefault() != null)
        {
            DistanceEnemy();
            Vector3 nor = (target.transform.position - transform.position);
            z = Mathf.Atan2(nor.y, nor.x) * Mathf.Rad2Deg;
            rotObj.transform.rotation = Quaternion.Euler(0, 0, z - 90f);
        }
    }

    private void Boom()
    {
        boomT += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C) && boomT >= boomCool)
        {
            boomT = 0;

            ParticleSystem boom = Instantiate(boomPc);
            boom.transform.position = transform.position;
            Destroy(boom.gameObject, 1);
            EnemyObserver.Instance.NotifyObservers();
        }
    }

    private void DurabilityRepair()
    {
        durabilityT += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) && durabilityT >= durabilityCool)
        {
            print("Heal");
            GameManager.Instance.HP++;
            durabilityT = 0;
        }
    }

    

    public IEnumerator PlayerHitEffect()
    {
        print("hit");

        isHit = true;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            sr.color = hitColor;
            yield return new WaitForSeconds(0.2f);
            sr.color = basicColor;
        }
        isHit = false;
    }

    private void Dodge()
    {
        dodgeT += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && dodgeT >= dodgeCool)
        {
            dodgeT = 0;
            StartCoroutine(Dodging());
        }
    }

    private IEnumerator Dodging()
    {
        isDodge = true;
        float dodgeImageT = 0;

        while (true)
        {
            yield return null;

            dodgeImageT += Time.deltaTime;

            float y = Mathf.Lerp(0, 360, dodgeImageT / 0.5f);

            Vector3 nor = new Vector3(0, y, 0);

            playerImage.transform.localRotation = Quaternion.Euler(nor);

            if (dodgeImageT >= 0.5f) break;
        }

        playerImage.transform.localRotation = Quaternion.Euler(Vector3.zero);

        isDodge = false;

    }

    private void PlayerMove()
    {
        if (!Input.anyKey) return;

        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        transform.Translate(moveVec * spd * Time.deltaTime);

        transform.position = PosLimit();

        if (moveVec == Vector3.zero) return;

        z = Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg;
        Quaternion lookRot = Quaternion.Euler(0, 0, z - 90);
        rotObj.transform.rotation = Quaternion.Lerp(rotObj.transform.rotation, lookRot, Time.deltaTime * spd * 2);
    }
    private Vector3 PosLimit()
    {
        Vector3 setPos = new Vector3( Mathf.Clamp(transform.position.x, -5f, 5f), Mathf.Clamp(transform.position.y, -3f, 8f), 0);

        return setPos;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = DistanceEnemy();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemys.Remove(other.gameObject);
        }
    }
}
