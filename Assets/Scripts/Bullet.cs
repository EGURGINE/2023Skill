using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float spd;
    private float dmg;

    private Vector3 moveVec = Vector3.up;

    [SerializeField] ParticleSystem hitPc;
    private void Start()
    {
        Destroy(gameObject,5);
    }

    private void Update()
    {
        transform.Translate(moveVec * spd * Time.deltaTime);
    }


    public void BulletSet(float _spd, float _dmg, float z)
    {
        transform.rotation = Quaternion.Euler(0, 0, z);

        spd = _spd;
        dmg = _dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject pc = Instantiate(hitPc).gameObject;
            pc.transform.position = transform.position;
            Destroy(pc, 0.1f);
            other.GetComponent<Enemy>().OnDamage(dmg);
            Destroy(gameObject);
        }
        if (other.CompareTag("Boss"))
        {
            GameObject pc = Instantiate(hitPc).gameObject;
            pc.transform.position = transform.position;
            Destroy(pc, 0.1f);
            other.GetComponent<Boss>().OnDamage(dmg);
            Destroy(gameObject);
        }
        if (other.CompareTag("Meteor"))
        {
            other.GetComponent<Meteor>().OnDamage(dmg);
            Destroy(gameObject);
        }

        if (other.CompareTag("KillBox"))
        {
            Destroy(gameObject);
        }
    }
}
