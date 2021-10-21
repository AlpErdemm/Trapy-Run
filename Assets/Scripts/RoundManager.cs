using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public bool isRoundStarted = false;
    public static UnityAction RoundStarted;
    

    public void StartRound()
    {
        RoundStarted.Invoke();
    }
}
