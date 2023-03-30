using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMeteor : MonoBehaviour
{
    private Rigidbody rb => GetComponent<Rigidbody>();

    private void Start()
    {
        rb.velocity = Vector3.down;
    }
    void Update()
    {
        transform.Rotate(0, 3, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (TutorialStage.Instance.tutorialNum)
        {
            case 9:

                if (other.CompareTag("Player") && TutorialStage.Instance.isEscape == true)
                {
                    Destroy(gameObject);
                }

                break;

            case 7:
                if (other.CompareTag("Player"))
                {
                    if (other.GetComponent<Player>().isDodge == true)
                    {
                        TutorialStage.Instance.info.sentences[7] = "네 잘하셨습니다.";
                    }
                    else
                    {
                        TutorialStage.Instance.info.sentences[7] = "아깝습니다, 다음번엔 피해 보도록 노력하죠.";
                    }
                    TutorialStage.Instance.tutorialNum = 7;
                    TutorialStage.Instance.Begin();
                }
                break;
            case 5:
                if (other.CompareTag("PlayerBullet"))
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    TutorialStage.Instance.tutorialNum = 5;
                    TutorialStage.Instance.Begin();
                }
                break;
        }


    }
}
