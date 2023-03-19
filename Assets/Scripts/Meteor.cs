using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour,IObserver
{
    [SerializeField] private float hp;

    private Rigidbody rb => GetComponent<Rigidbody>();

    [SerializeField] private Items[] Items;
    int spawnItemNum => Random.Range(0, ((int)EItemType.End));

    private bool isDie;

    private void Start()
    {
        BoomObserver.Instance.ResisterObserver(this);
        rb.velocity = Vector2.down;
    }

    private void Update()
    {
        transform.Rotate(0,3,0);
    }

    public void OnDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            if (isDie) return;
            BoomObserver.Instance.RemoveObserver(this);
            Die();
        }                              
    }

    private void SpawnItem()
    {
        Instantiate(Items[spawnItemNum]).transform.position = transform.position;
    }


    private void Die()
    {
        isDie = true;
        print("die");
        SpawnItem();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoomObserver.Instance.RemoveObserver(this);
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
