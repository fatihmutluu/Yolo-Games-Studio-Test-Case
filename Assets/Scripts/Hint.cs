using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Hint : MonoBehaviour
{
    private GameControl controller;

    private int counter = 4;

    private void Awake()
    {
        controller = GameObject.Find("Images").GetComponent<GameControl>();
    }

    public void HintButtonClicked()
    {
        ShowHint();
    }

    public void ShowHint()
    {
        if (controller.differences.Count < 2 || counter == 0)
        {
            return;
        }

        GameObject first_difference = controller.differences[0];
        GameObject match_difference = controller.differences.Find(x =>
            x.name == first_difference.name && x != first_difference
        );

        if (GameObject.Find("Black").GetComponent<Animator>().GetBool("isActive"))
            return;

        BlackOut(first_difference, "hint button clicked");
        BlackOut(match_difference, "hint button clicked");

        UpdateCountLeft();
    }

    private void BlackOut(GameObject difference, string trigger)
    {
        difference.transform.GetChild(2).gameObject.SetActive(true);
        difference.transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Black").GetComponent<Animator>().SetTrigger(trigger);
        GameObject.Find("Black").GetComponent<Animator>().SetBool("isActive", true);
    }

    private void UpdateCountLeft()
    {
        counter--;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = counter.ToString();
    }
}
