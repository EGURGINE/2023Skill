using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMoveCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialStage.Instance.Begin();
            gameObject.SetActive(false);
        }
    }
}
