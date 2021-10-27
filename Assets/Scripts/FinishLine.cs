using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera first, second;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && FindObjectOfType<PlayerController>().isRunning == true)
        {
            second.Priority = first.Priority + 1;
            RoundManager.FinishCrossed.Invoke();
        }
    }
}
