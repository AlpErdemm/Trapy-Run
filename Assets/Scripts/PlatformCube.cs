using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlatformCube : MonoBehaviour
{
    private bool isFallStarted = false;
    private bool isFalling = false;
    private bool isFallEnabled = false;

    private void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
        RoundManager.PlayerDied += OnPlayerDie;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!isFallStarted && isFallEnabled && collision.gameObject.CompareTag("Player"))
        {
            isFallStarted = true;
            StartCoroutine(Fall());
        }
    }

    private void FixedUpdate()
    {
        if(isFalling)
            transform.Translate(new Vector3(0, -0.1f, 0));
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.3f);
        isFalling = true;
        Material mat = GetComponent<MeshRenderer>().material;
        mat.color = new Color(1f, 0f, 0f);
        mat.DOColor(new Color(1f, 1f, 1f), 0.5f);
        yield return new WaitForSeconds(4f);
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
