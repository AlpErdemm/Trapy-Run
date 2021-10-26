using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Heli : MonoBehaviour
{
    public GameObject RotateBig;
    public GameObject RotateSmall;

    private bool isSpinning = false;

    private void Start()
    {
        //Fly();
    }
    public void Fly()
    {
        StartCoroutine(FlyCo());
    }

    private void FixedUpdate()
    {
        if (isSpinning)
        {
            RotateBig.transform.Rotate(new Vector3(0, 0, 5f));
            RotateSmall.transform.Rotate(new Vector3(0, 0, 5f));
        }
    }

    IEnumerator FlyCo()
    {
        yield return new WaitForSeconds(2f);
        isSpinning = true;
        yield return new WaitForSeconds(2f);
        transform.DOMove(transform.position + new Vector3(0, 5f, 0), 3f).OnComplete(() => {
        });
        transform.DORotate(new Vector3(0f, 0f, 15f), 3f).OnComplete(() => {
            transform.DOMove(transform.position + new Vector3(-25f, 25), 4f).SetEase(Ease.InExpo);
        });
    }
}
