using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragController : MonoBehaviour, IDragHandler
{
    private bool dragEnabled = true;

    private void Awake()
    {
        RoundManager.PlayerDied += OnPlayerDie;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (dragEnabled)
        {
            float positionDiff = (eventData.position.x - (Screen.width / 2)) / (Screen.width / 2) * 45f;
            FindObjectOfType<PlayerController>().transform.parent.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0f, positionDiff, 0f));
        }
    }

    private void OnPlayerDie()
    {
        dragEnabled = false;
    }
    
}
