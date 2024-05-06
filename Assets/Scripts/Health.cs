using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;

    private List<Image> hearts;

    private EndGameController endGameController;

    private void Awake()
    {
        endGameController = GameObject.Find("End Game Canvas").GetComponent<EndGameController>();
        Debug.Log("Health is awake");
        health = 3;
        hearts = GetComponentsInChildren<Image>().ToList();
    }

    public void TakeDamage()
    {
        Debug.Log("Taking damage");
        health--;

        // ! full protection to gain health -> can be simplified (maybe gain health is not needed at all)
        switch (health)
        {
            case 3:
                hearts[0].color = Color.white;
                hearts[1].color = Color.white;
                hearts[2].color = Color.white;
                break;
            case 2:
                hearts[0].color = Color.white;
                hearts[1].color = Color.white;
                hearts[2].color = Color.black;
                break;
            case 1:
                hearts[0].color = Color.white;
                hearts[1].color = Color.black;
                hearts[2].color = Color.black;
                break;
            case 0:
                hearts[0].color = Color.black;
                hearts[1].color = Color.black;
                hearts[2].color = Color.black;
                endGameController.GameLose();
                break;
            default:
                break;
        }
    }
}
