using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : Singleton<Player>
{
    [SerializeField] private GameObject playerImage;
    private SpriteRenderer sr;

    [SerializeField] private float spd;
    [SerializeField] private float dmg;
    [SerializeField] private float bulletSpd;
    [SerializeField] private float shotCool;

    public int powerLevel;
    private int specialShotCnt;
    private float shotT;

    public bool isHit;

    public bool isDodge;
    [SerializeField] private float dodgeCool;
    private float dodgeT;

    [SerializeField] private float boomCool;
    private float boomT;

    [SerializeField] private float durabilityCool;
    private float durabilityT;

    [SerializeField] private Bullet bullet;
    private Vector3 moveVec;

    [SerializeField] private ParticleSystem boomPc;

    private Color basicColor = Color.white;
    private Color hitColor = new Color(255, 255, 255, 10);

    private Coroutine shildCoroutine;

    private void Start()
    {
        sr = playerImage.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMove();
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

            BulletPower();


            if (specialShotCnt >= 3)
            {
                for (int i = 0; i < 2; i++)
                {
                    Bullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.transform.position = transform.position;

                    int rot = (i == 0) ? 1 : -1;
                    bulletObj1.BulletSet(bulletSpd, dmg, 15 * rot);
                }
                specialShotCnt = 0;
            }
            specialShotCnt++;

            shotT = 0;
        }
    }

    private void BulletPower()
    {
        int xDir = 0;

        switch (powerLevel)
        {
            case 0:
                Bullet bulletObj = Instantiate(bullet);
                bulletObj.transform.position = transform.position;
                bulletObj.BulletSet(bulletSpd, dmg, 0);
                break;
            case 1:

                for (int i = 0; i < 2; i++)
                {
                    Bullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.BulletSet(bulletSpd, dmg, 0);

                    xDir = (i == 0) ? 1 : -1;

                    bulletObj1.transform.position = transform.position + new Vector3(0.25f * xDir, 0);
                }
                break;
            case 2:

                for (int i = 0; i < 3; i++)
                {
                    Bullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.BulletSet(bulletSpd, dmg, 0);

                    switch (i)
                    {
                        case 0:
                            xDir = 1;
                            break;
                        case 1:
                            xDir = 0;
                            break;
                        case 2:
                            xDir = -1;
                            break;
                    }

                    bulletObj1.transform.position = transform.position + new Vector3(0.25f * xDir, 0);
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    Bullet bulletObj1 = Instantiate(bullet);
                    bulletObj1.BulletSet(bulletSpd, dmg, 0);

                    switch (i)
                    {
                        case 0:
                            xDir = -2;
                            break;
                        case 1:
                            xDir = -1;
                            break;
                        case 2:
                            xDir = 1;
                            break;
                        case 3:
                            xDir = 2;
                            break; 
                    }

                        bulletObj1.transform.position = transform.position + new Vector3(0.25f * xDir, 0);
                }
                break;
        }
    }

    private void Boom()
    {
        boomT += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C) && boomT >= boomCool)
        {
            boomT = 0;

            ParticleSystem boom = Instantiate(boomPc, transform);
            Destroy(boom.gameObject, 1);
            BoomObserver.Instance.NotifyObservers();
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

    private IEnumerator PlayerShild()
    {
        isHit = true;
        yield return new WaitForSeconds(3f);
        isHit = false;
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
        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        transform.Translate(moveVec * spd * Time.deltaTime);

        transform.position = PosLimit();
    }
    private Vector3 PosLimit()
    {
        Vector3 setPos = new Vector3(Mathf.Clamp(transform.position.x, -6.25f, 6.25f), Mathf.Clamp(transform.position.y, -1f, 4.5f), 0);

        return setPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Item1":
                if (powerLevel > 2) return;

                powerLevel++;
                Destroy(other.gameObject);
                break;
            case "Item2":

                if (shildCoroutine != null) StopCoroutine(shildCoroutine);

                shildCoroutine = StartCoroutine(PlayerShild());
                break;
            case "Item3":
                GameManager.Instance.HP++;
                break;
            case "Item4":
                GameManager.Instance.Fuel += 30;
                break;
        }

    }
}
