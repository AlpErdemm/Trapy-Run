using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject DragCanvas;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;

    private void Awake()
    {
        RoundManager.FinishCrossed += SwitchWin;
    }

    public void SwitchDrag()
    {
        StartCanvas.GetComponent<Canvas>().enabled = false;
        DragCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void SwitchWin()
    {
        StartCanvas.GetComponent<Canvas>().enabled = false;
        DragCanvas.GetComponent<Canvas>().enabled = false;
        WinCanvas.GetComponent<Canvas>().enabled = true;
    }
    public void SwitchLose()
    {
        StartCanvas.GetComponent<Canvas>().enabled = false;
        DragCanvas.GetComponent<Canvas>().enabled = false;
        LoseCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void RestartRound()
    {
        FindObjectOfType<GameManager>().RestartRound();
    }

    public void NextRound()
    {
        FindObjectOfType<GameManager>().NexRound();
    }
}
