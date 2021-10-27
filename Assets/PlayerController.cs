using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool isRunning = false;
    void Awake()
    {
        RoundManager.RoundStarted += OnRoundStart;
        RoundManager.FinishCrossed += OnFinishCross;
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

    public void Fall()
    {
        isRunning = false;
        GetComponent<Animator>().SetBool("isFalling", true);
        FindObjectOfType<CanvasController>().SwitchLose();
    }

    public void ObstacleCollision()
    {
        Fall();
    }

    private void OnFinishCross()
    {
        GameObject heliObject = FindObjectOfType<Heli>().gameObject;
        isRunning = false;
        transform.DOMove(heliObject.transform.position, 3f).SetEase(Ease.InFlash).OnComplete(() => {
            GetComponent<Animator>().SetBool("isSitting", true);
            transform.DOMove(transform.position + new Vector3(1f, 2f, 0f), 2f);
            transform.DORotate(new Vector3(0f, -90f, 0f), 2f).OnComplete(() => {
                transform.parent = heliObject.transform;
                heliObject.GetComponent<Heli>().Fly();
            });
        });       
    }
}
