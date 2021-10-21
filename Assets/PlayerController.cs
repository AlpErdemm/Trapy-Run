using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool isRunning = false;
    void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
    }

    void OnRoundStart()
    {
        GetComponent<Animator>().SetBool("isRunning", true);
        isRunning = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRunning)
            transform.parent.Translate(new Vector3(0, 0, speed));
    }
}
