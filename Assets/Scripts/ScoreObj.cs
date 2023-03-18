using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObj : MonoBehaviour
{
    [SerializeField] private GameObject spriteObj;
    [SerializeField] private float spd;
    private GameObject target;
    private bool isMove = false;

    void Start()
    {
        target = GameObject.Find("Player");

        StartCoroutine(StartMoeve());
    }

    private IEnumerator StartMoeve()
    {
        Vector3 startPos = transform.position;

        Vector3 endPos = transform.position + new Vector3(Random.Range(-0.25f, 0.25f),3f,0);

        float t = 0;
        while (true)
        {
            yield return null;
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, t / 0.5f);

            if (t >= 0.5f) break;
        }

        isMove = true;
    }
    void Update()
    {
        spriteObj.transform.Rotate(Vector3.up * spd);

        if (isMove == false) return;

        var moveVec = target.transform.position - transform.position;
        transform.Translate(moveVec * spd * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.Score += 50;
            Destroy(transform.gameObject);
        }
    }
}
