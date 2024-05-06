using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public List<GameObject> differences;

    private ClickHandler clickHandler;

    [SerializeField]
    private GameObject leftCounter;

    private bool isDragging = false;
    private Vector3 initialMousePosition;

    private void Awake()
    {
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
        {
            Debug.Log("You Win!");
        }

        // Check for mouse input only if not dragging
        if (!isDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = Input.mousePosition; // Store initial mouse position
            }
            else if (Input.GetMouseButtonUp(0))
            {
                // Calculate the distance moved by the mouse
                float distance = Vector3.Distance(initialMousePosition, Input.mousePosition);

                // If the distance is small, treat it as a click, otherwise consider it dragging
                if (distance < 10f) // Adjust this threshold as needed
                {
                    Vector3 mousePosition = Input.mousePosition;
                    Debug.Log(mousePosition);

                    if (
                        mousePosition.x < 52.01f
                        || mousePosition.x > 1027.99f
                        || mousePosition.y > 1641.15f
                        || mousePosition.y < 322.42f
                    )
                    {
                        Debug.Log("Out of bounds");
                        return;
                    }

                    CheckIfHitDifference(mousePosition);
                }
            }
        }
    }

    private bool CheckWin()
    {
        return differences.Count == 0;
    }

    private void CheckIfHitDifference(Vector3 mousePosition)
    {
        Debug.Log("Checking Hit");

        // Convert to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;

        Debug.Log("Mouse position: " + mousePosition);

        // ! look for is mousePosition in hitbox of any difference
        foreach (GameObject difference in differences)
        {
            Debug.Log("difference: " + difference.name);
            CapsuleCollider2D hitBox = difference
                .transform.GetChild(0)
                .GetComponent<CapsuleCollider2D>();

            Debug.Log("hitBox: " + hitBox.bounds);

            if (hitBox != null && hitBox.bounds.Contains(mousePosition))
            {
                Debug.Log("Hit!");

                string difference_name = difference.name;
                clickHandler.Hit(difference);

                // ! namings of 2 differences are the same act for both
                GameObject otherDifference = differences.Find(x => x.name == difference_name);
                clickHandler.Hit(otherDifference);

                // ! update left counter
                leftCounter.GetComponent<LeftCounter>().UpdateLeftCounter();

                return;
            }
        }

        // ! handling miss if no difference was hit
        Debug.Log("Miss!");
        clickHandler.Miss(mousePosition);
    }
}
