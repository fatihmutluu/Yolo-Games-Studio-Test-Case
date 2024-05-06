using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    public GameObject firstImage;
    public GameObject secondImage;

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 scale = firstImage.transform.localScale;
        if (scroll != 0.0f)
        {
            if (scroll > 0.0f && scale.x >= 1.3f)
                return;

            if (scroll < 0.0f && scale.x <= 1f)
                return;

            scale.x += scroll;
            scale.y += scroll;
            firstImage.transform.localScale = scale;
            secondImage.transform.localScale = scale;
        }
    }
}
