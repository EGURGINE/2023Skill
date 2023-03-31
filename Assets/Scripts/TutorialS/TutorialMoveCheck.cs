using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialStage.Instance.tutorialNum = 4;
            TutorialStage.Instance.Begin();
            gameObject.SetActive(false);
        }
    }
}
