using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BoundLimits
{
    public float upLeft;
    public float upRight;
    public float upTop;
    public float upBottom;
    public float downLeft;
    public float downRight;
    public float downTop;
    public float downBottom;
}

public class GameControl : MonoBehaviour
{
    public List<GameObject> differences;

    private ClickHandler clickHandler;

    [SerializeField]
    private GameObject leftCounter;

    private bool isDragging = false;
    private Vector3 initialMousePosition;

    private EndGameController endGameController;

    private BoundLimits boundLimits;

    private void Awake()
    {
        endGameController = GameObject.Find("End Game Canvas").GetComponent<EndGameController>();
        clickHandler = GetComponent<ClickHandler>();
        differences = GameObject.FindGameObjectsWithTag("Difference").ToList();
        foreach (GameObject difference in differences)
        {
            Debug.Log(difference.transform.position);
        }

        var upSpriteRender = GameObject.Find("Up Background").GetComponent<BoxCollider2D>();
        var upBounds = upSpriteRender.bounds;
        var upTopRight = upBounds.max;
        var upBottomLeft = upBounds.min;

        var downSpriteRender = GameObject.Find("Down Background").GetComponent<BoxCollider2D>();
        var downBounds = downSpriteRender.bounds;
        var downTopRight = downBounds.max;
        var downBottomLeft = downBounds.min;

        boundLimits = new BoundLimits
        {
            upLeft = upBottomLeft.x,
            upRight = upTopRight.x,
            upTop = upTopRight.y,
            upBottom = upBottomLeft.y,
            downLeft = downBottomLeft.x,
            downRight = downTopRight.x,
            downTop = downTopRight.y,
            downBottom = downBottomLeft.y
        };
    }

    private void Update()
    {
        Debug.Log("Game is running");
        if (CheckWin())
        {
            Debug.Log("You Win!");
            endGameController.GameWin();
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

        AlignImages();
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

    private void AlignImages()
    {
        var upObject = GameObject.Find("Up Background");
        var upSpriteRender = upObject.GetComponent<BoxCollider2D>();
        var upBounds = upSpriteRender.bounds;
        var upTopRight = upBounds.max;
        var upBottomLeft = upBounds.min;

        var downObject = GameObject.Find("Down Background");
        var downSpriteRender = downObject.GetComponent<BoxCollider2D>();
        var downBounds = downSpriteRender.bounds;
        var downTopRight = downBounds.max;
        var downBottomLeft = downBounds.min;

        if (upBottomLeft.x > boundLimits.upLeft)
        {
            upObject.transform.position = new Vector3(
                boundLimits.upLeft + upBounds.size.x / 2,
                upObject.transform.position.y,
                upObject.transform.position.z
            );

            downObject.transform.position = new Vector3(
                boundLimits.downLeft + downBounds.size.x / 2,
                downObject.transform.position.y,
                downObject.transform.position.z
            );
        }

        if (upTopRight.x < boundLimits.upRight)
        {
            upObject.transform.position = new Vector3(
                boundLimits.upRight - upBounds.size.x / 2,
                upObject.transform.position.y,
                upObject.transform.position.z
            );

            downObject.transform.position = new Vector3(
                boundLimits.downRight - downBounds.size.x / 2,
                downObject.transform.position.y,
                downObject.transform.position.z
            );
        }

        if (upTopRight.y < boundLimits.upTop)
        {
            upObject.transform.position = new Vector3(
                upObject.transform.position.x,
                boundLimits.upTop - upBounds.size.y / 2,
                upObject.transform.position.z
            );

            downObject.transform.position = new Vector3(
                downObject.transform.position.x,
                boundLimits.downTop - downBounds.size.y / 2,
                downObject.transform.position.z
            );
        }

        if (upBottomLeft.y > boundLimits.upBottom)
        {
            upObject.transform.position = new Vector3(
                upObject.transform.position.x,
                boundLimits.upBottom + upBounds.size.y / 2,
                upObject.transform.position.z
            );

            downObject.transform.position = new Vector3(
                downObject.transform.position.x,
                boundLimits.downBottom + downBounds.size.y / 2,
                downObject.transform.position.z
            );
        }
    }
}
