using UnityEngine;

public class SpriteZoomPan : MonoBehaviour
{
    public float zoomSpeed = 0.1f;
    public float maxZoom = 3.0f;
    public float minZoom = 1.0f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Zoom based on mouse scroll and maintain the original aspect ratio
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scaleChange = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            float currentScaleX = transform.localScale.x + scaleChange;
            float newScale = Mathf.Clamp(
                currentScaleX,
                minZoom * originalScale.x,
                maxZoom * originalScale.x
            );

            transform.localScale = new Vector3(
                newScale,
                newScale * originalScale.y / originalScale.x,
                1f
            );
        }
    }
}
