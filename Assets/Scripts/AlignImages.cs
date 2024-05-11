using UnityEngine;

public static class BoundLimits
{
    public static float upLeft;
    public static float upRight;
    public static float upTop;
    public static float upBottom;
    public static float downLeft;
    public static float downRight;
    public static float downTop;
    public static float downBottom;
}

public class AlignImages : MonoBehaviour
{
    private GameObject upObject;
    private GameObject downObject;

    private Vector3 upNewPosition;
    private Vector3 downNewPosition;

    private void Awake()
    {
        upObject = GameObject.Find("Up Background");
        downObject = GameObject.Find("Down Background");
        SetupBoundLimits();
    }

    private void Update()
    {
        Align();
    }

    private void Align()
    {
        Bounds upBounds = upObject.GetComponent<BoxCollider2D>().bounds;
        Vector3 upTopRight = upBounds.max;
        Vector3 upBottomLeft = upBounds.min;

        upNewPosition = upObject.transform.position;
        downNewPosition = downObject.transform.position;

        // Check if the objects are out of bounds
        if (upBottomLeft.x > BoundLimits.upLeft)
        {
            upNewPosition.x = BoundLimits.upLeft + upBounds.size.x / 2;
            downNewPosition.x = BoundLimits.downLeft + upBounds.size.x / 2;
        }
        if (upTopRight.x < BoundLimits.upRight)
        {
            upNewPosition.x = BoundLimits.upRight - upBounds.size.x / 2;
            downNewPosition.x = BoundLimits.downRight - upBounds.size.x / 2;
        }
        if (upTopRight.y < BoundLimits.upTop)
        {
            upNewPosition.y = BoundLimits.upTop - upBounds.size.y / 2;
            downNewPosition.y = BoundLimits.downTop - upBounds.size.y / 2;
        }
        if (upBottomLeft.y > BoundLimits.upBottom)
        {
            upNewPosition.y = BoundLimits.upBottom + upBounds.size.y / 2;
            downNewPosition.y = BoundLimits.downBottom + upBounds.size.y / 2;
        }

        // Set the new positions
        upObject.transform.position = upNewPosition;
        downObject.transform.position = downNewPosition;
    }

    private void SetupBoundLimits()
    {
        var upSpriteRender = upObject.GetComponent<BoxCollider2D>();
        var upBounds = upSpriteRender.bounds;
        var upTopRight = upBounds.max;
        var upBottomLeft = upBounds.min;

        var downSpriteRender = downObject.GetComponent<BoxCollider2D>();
        var downBounds = downSpriteRender.bounds;
        var downTopRight = downBounds.max;
        var downBottomLeft = downBounds.min;

        BoundLimits.upLeft = upBottomLeft.x;
        BoundLimits.upRight = upTopRight.x;
        BoundLimits.upTop = upTopRight.y;
        BoundLimits.upBottom = upBottomLeft.y;
        BoundLimits.downLeft = downBottomLeft.x;
        BoundLimits.downRight = downTopRight.x;
        BoundLimits.downTop = downTopRight.y;
        BoundLimits.downBottom = downBottomLeft.y;
    }
}
