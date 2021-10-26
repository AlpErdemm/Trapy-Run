using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlatformCube : MonoBehaviour
{
    private bool isFalling = false;
    private bool isFallEnabled = false;

    private void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
        RoundManager.PlayerDied += OnPlayerDie;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!isFalling && isFallEnabled && collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Material mat = GetComponent<MeshRenderer>().material;
        mat.color = new Color(1f, 0f, 0f);
        mat.DOColor(new Color(1f, 1f, 1f), 0.5f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void OnRoundStart()
    {
        isFallEnabled = true;
    }

    private void OnPlayerDie()
    {
        isFallEnabled = false;
    }
}
