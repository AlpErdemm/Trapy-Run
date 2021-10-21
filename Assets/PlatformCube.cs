using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlatformCube : MonoBehaviour
{
    private bool isFalling = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(!isFalling && FindObjectOfType<RoundManager>().isRoundStarted && collision.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Material mat = GetComponent<MeshRenderer>().material;
        mat.color = new Color(1f, 0f, 0f);
        mat.DOColor(new Color(1f, 1f, 1f), 0.5f);
    }
}
