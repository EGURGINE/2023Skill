using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMeteor : MonoBehaviour
{
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void Start()
    {
        rb.velocity = Vector3.down * 2;
        if (TutorialStage.Instance.tutorialNum == 6) StartCoroutine(DestroyObj());
    }
    void Update()
    {
        transform.Rotate(0, 3, 0);
    }

    private IEnumerator DestroyObj()
    {
        float t = 0;
        while (t < 10)
        {
            yield return null;
            t += Time.deltaTime;
        }
        Destroy(gameObject);
        TutorialStage.Instance.Begin();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (TutorialStage.Instance.tutorialNum)
        {
            case 10:

                if (other.CompareTag("Player") && TutorialStage.Instance.isEscape == true)
                {
                    Destroy(gameObject);
                }

                break;

            case 8:
                if (other.CompareTag("Player"))
                {
                    if (other.GetComponent<Player>().isDodge == true)
                    {
                        TutorialStage.Instance.info.sentences[8] = "네 잘하셨습니다.";
                    }
                    else
                    {
                        TutorialStage.Instance.info.sentences[8] = "아깝습니다, 다음번엔 피해 보도록 노력하죠.";
                    }
                    TutorialStage.Instance.tutorialNum = 8;
                    TutorialStage.Instance.Begin();
                }
                break;
            case 6:
                if (other.CompareTag("PlayerBullet"))
                {
                    StopCoroutine(DestroyObj());
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    TutorialStage.Instance.tutorialNum = 6;
                    TutorialStage.Instance.Begin();
                }
                break;
        }


    }
}
