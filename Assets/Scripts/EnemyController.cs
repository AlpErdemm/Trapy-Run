using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isRunning = false;
    public bool isJumping = false;
    private bool isFallen = false;
    private bool isReturned = false;
    private bool alreadyJumped = false;
    private GameObject player;

    void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
    }

    public void OnRoundStart()
    {
        GetComponent<Animator>().SetBool("isRunning", true);
        isRunning = true;
        isReturned = false;
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            transform.parent.Translate(new Vector3(0, 0, 0.15f));
        }
        else if (isJumping)
        {
            transform.parent.Translate((player.transform.position - transform.position) * 0.1f);
        }
        else
        {
            //Preparing to jump
            transform.LookAt(player.transform);
        }
    }

    private void Update()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>().gameObject;

        if (!alreadyJumped)
        {
            if ((transform.position.z > player.transform.position.z && transform.position.y + 1f >= player.transform.position.y) && FindObjectOfType<Spawner>().isSpawning)
            {
                JumpOnPlayer();
                alreadyJumped = true;
            }
        }

        if(transform.position.y < 0f && !isFallen)
        {
            isRunning = false;
            alreadyJumped = true;   //Don't jump while falling
            GetComponent<Animator>().SetBool("isFalling", true);
            isFallen = true;
        }

        if (transform.position.y < -25f && !isReturned)
        {
            isReturned = true;
            ReturnPool();       
        }
    }

    private void ReturnPool()
    {
        GetComponent<Animator>().SetBool("isJumping", false);
        GetComponent<Animator>().SetBool("isRunning", false);
        GetComponent<Animator>().SetBool("isFalling", false);
        isRunning = false;
        isJumping = false;
        isFallen = false;
        alreadyJumped = false;
        FindObjectOfType<Spawner>().ReturnToPool(gameObject);
    }

    private void JumpOnPlayer()
    {
        RoundManager.PlayerDied.Invoke();
        GetComponent<Animator>().SetBool("isJumping", true);
        isRunning = false;
        StartCoroutine(Jump());
    }
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.5f);
        isJumping = true;
    }


    public void MakePlayerFall()
    {
        FindObjectOfType<PlayerController>().Fall();
        isJumping = false;
    }

    public void Stop()
    {
        isJumping = false;
        isRunning = false;
        StopAllCoroutines();
        GetComponent<Animator>().SetBool("isRunning", false);
    }

}
