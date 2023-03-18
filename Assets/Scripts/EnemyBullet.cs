using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IObserver
{
    private float spd;
    private float dmg;

    private Vector3 moveVec = Vector3.up;

    [SerializeField] private ScoreObj scoreObj;

    private void Start()
    {
        Destroy(gameObject, 5);
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
        if (other.CompareTag("KillBox"))
        {
            BoomObserver.Instance.RemoveObserver(this);
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            BoomObserver.Instance.RemoveObserver(this);
        }
    }

    public void DestroyObj()
    {
        GameObject obj = Instantiate(scoreObj).gameObject;
        obj.transform.position = transform.position
        + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
    }

    public Vector3 ThisTransform()
    {
        return transform.position;
    }
}
