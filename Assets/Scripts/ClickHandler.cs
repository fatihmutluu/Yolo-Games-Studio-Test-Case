using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private GameControl gameControl;
    private Health healthBar;

    [SerializeField]
    private GameObject missIcon;

    [SerializeField]
    private float missIconTime = 1.0f;

    private void Awake()
    {
        gameControl = GetComponent<GameControl>();
        healthBar = GameObject.Find("HearthBar").GetComponent<Health>();
    }

    public void CheckIfHitDifference(Vector3 mousePosition)
    {
        Debug.Log("Checking Hit");

        // ! look for is mousePosition in hitbox of any difference
        foreach (GameObject difference in gameControl.differences)
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
                Hit(difference);

                // ! namings of 2 differences are the same act for both
                GameObject otherDifference = gameControl.differences.Find(x =>
                    x.name == difference_name
                );
                Hit(otherDifference);

                // ! update left counter
                GameObject.Find("DifferenceLeft").GetComponent<LeftCounter>().UpdateLeftCounter();

                return;
            }
        }

        // ! handling miss if no difference was hit
        Debug.Log("Miss!");
        Miss(mousePosition);
    }

    public void Hit(GameObject difference)
    {
        Debug.Log("Handling hit");

        Animator animator = difference.transform.GetChild(1).GetComponent<Animator>();
        animator.SetTrigger("Hit");

        // check if Black Mask child is active
        if (difference.transform.GetChild(2).gameObject.activeSelf)
        {
            difference.transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Black").GetComponent<Animator>().SetBool("isActive", false);
        }

        gameControl.differences.Remove(difference);
        Debug.Log("Differences left: " + gameControl.differences.Count);
    }

    public void Miss(Vector3 mousePosition)
    {
        Debug.Log("Handling miss");

        //create object from MissIcon prefab at mousePosition in the panel and destroy it after given seconds if healt is greater than 1
        if (healthBar.health > 1)
        {
            GameObject missIconObject = Instantiate(missIcon, mousePosition, Quaternion.identity);
            Destroy(missIconObject, missIconTime);
        }

        healthBar.TakeDamage();
    }
}
