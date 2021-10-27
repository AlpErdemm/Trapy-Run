using DG.Tweening;
using UnityEngine;

public class Finger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(-160, -466), 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
