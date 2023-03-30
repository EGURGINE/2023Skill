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
                        TutorialStage.Instance.info.sentences[7] = "�� ���ϼ̽��ϴ�.";
                    }
                    else
                    {
                        TutorialStage.Instance.info.sentences[7] = "�Ʊ����ϴ�, �������� ���� ������ �������.";
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
