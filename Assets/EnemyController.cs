using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isRunning = false;
    public bool isJumping = false;
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
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            transform.parent.Translate(new Vector3(0, 0, 0.15f));
        }
        else if (isJumping)
        {
            Debug.Log("Jump");
            transform.LookAt(player.transform);
            transform.parent.Translate((player.transform.position - transform.position) * 0.1f);
        }
    }

    private void Update()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>().gameObject;

        if (!alreadyJumped)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 3f || (transform.position.z > player.transform.position.z && transform.position.y >= player.transform.position.y))
            {
                JumpOnPlayer();
                alreadyJumped = true;
            }
        }
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

}
