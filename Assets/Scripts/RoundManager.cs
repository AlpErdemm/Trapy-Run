using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public bool isRoundStarted = false;
    public static UnityAction RoundStarted;
    public static UnityAction PlayerDied;
    public static UnityAction FinishCrossed;
    

    public void StartRound()
    {
        RoundStarted.Invoke();
        isRoundStarted = true;
    }
    public void EndRound()
    {
        System.Delegate[] delegates = RoundStarted.GetInvocationList();
        foreach (System.Delegate del in delegates)
            RoundStarted -= (del as UnityAction);

        System.Delegate[] delegates2 = PlayerDied.GetInvocationList();
        foreach (System.Delegate del in delegates2)
            PlayerDied -= (del as UnityAction);

        System.Delegate[] delegates3 = FinishCrossed.GetInvocationList();
        foreach (System.Delegate del in delegates3)
            FinishCrossed -= (del as UnityAction);
    }

}
