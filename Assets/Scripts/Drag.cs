using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private GameObject revert;

    private Vector3 offset,
        offset_revert;

    public static Vector3 initialScale;

    private bool dragging = false;

    void Start()
    {
        // Store the initial scale at the start
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (dragging)
        {
            Debug.Log(
                "Bounds: "
                    + GetComponent<SpriteRenderer>().bounds.size.x
                    + ", "
                    + GetComponent<SpriteRenderer>().bounds.size.y
            );
            // Only update the positions if dragging is enabled
            transform.position =
                Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)
                ) + offset;

            revert.transform.position =
                Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)
                ) + offset_revert;
        }
    }

    private void OnMouseDown()
    {
        // Check if the scale has been changed from the initial scale
        bool scaleChanged = transform.localScale != initialScale;
        if (scaleChanged)
        {
            offset_revert =
                revert.transform.position
                - Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)
                );

            offset =
                transform.position
                - Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)
                );

            dragging = true;
        }
        // If the scale hasn't changed, prevent dragging
        else
        {
            dragging = false;
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}
