using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
    }

    void OnRoundStart()
    {
        Debug.Log("Yo!");
        GetComponent<Animator>().SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
