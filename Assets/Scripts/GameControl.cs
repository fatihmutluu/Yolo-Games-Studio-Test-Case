using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private GameObject leftCounter;
    public List<GameObject> differences;

    private ClickHandler clickHandler;

    private Vector3 initialMousePosition;

    private EndGameController endGameController;

    private void Awake()
    {
        endGameController = GameObject.Find("End Game Canvas").GetComponent<EndGameController>();
        clickHandler = GetComponent<ClickHandler>();
        differences = GameObject.FindGameObjectsWithTag("Difference").ToList();
        foreach (GameObject difference in differences)
        {
            Debug.Log(difference.transform.position);
        }
    }

    private void Update()
    {
        Debug.Log("Game is running");

        if (CheckWin())
            endGameController.GameWin();

        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Calculate the distance moved by the mouse
            float distance = Vector3.Distance(initialMousePosition, Input.mousePosition);

            // If the distance is small, treat it as a click, otherwise consider it dragging
            if (distance < 10f)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0;

                Debug.Log(mousePosition);

                if (
                    mousePosition.x < BoundLimits.upLeft
                    || mousePosition.x > BoundLimits.upRight
                    || mousePosition.y > BoundLimits.upTop
                    || mousePosition.y < BoundLimits.downBottom
                )
                {
                    Debug.Log("Out of bounds");
                    return;
                }

                clickHandler.CheckIfHitDifference(mousePosition);
            }
        }
    }

    private bool CheckWin()
    {
        return differences.Count == 0;
    }
}
