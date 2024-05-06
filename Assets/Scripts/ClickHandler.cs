using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private GameControl gameControl;
    private GameObject healthBar;

    [SerializeField]
    private GameObject missIcon;

    [SerializeField]
    private float missIconTime = 1.0f;

    private void Awake()
    {
        gameControl = GetComponent<GameControl>();
        healthBar = GameObject.Find("HearthBar");
    }

    public void Hit(GameObject difference)
    {
        Debug.Log("Handling hit");

        Animator animator = difference.transform.GetChild(1).GetComponent<Animator>();
        animator.SetTrigger("Hit");

        Animator hintAnimator = difference.transform.GetChild(2).GetComponent<Animator>();
        hintAnimator.SetBool("isActive", false);

        gameControl.differences.Remove(difference);
        Debug.Log("Differences left: " + gameControl.differences.Count);
    }

    public void Miss(Vector3 mousePosition)
    {
        Debug.Log("Handling miss");

        //create object from MissIcon prefab at mousePosition in the panel and destroy it after given seconds
        GameObject missIconObject = Instantiate(missIcon, mousePosition, Quaternion.identity);
        GameObject panel = GameObject.Find("Images");
        missIconObject.transform.SetParent(panel.transform, true);
        Destroy(missIconObject, missIconTime);

        healthBar.GetComponent<Health>().TakeDamage();
    }
}
