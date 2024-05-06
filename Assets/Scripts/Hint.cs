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
        controller = GameObject.Find("Panel").GetComponent<GameControl>();
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

        if (first_difference.transform.GetChild(2).GetComponent<Animator>().GetBool("isActive"))
            return;

        BlackOut(first_difference, "hint button clicked");
        BlackOut(match_difference, "hint button clicked");

        UpdateCountLeft();
    }

    private void BlackOut(GameObject difference, string trigger)
    {
        difference.transform.GetChild(2).GetComponent<Animator>().SetTrigger(trigger);
        difference.transform.GetChild(2).GetComponent<Animator>().SetBool("isActive", true);
    }

    private void UpdateCountLeft()
    {
        counter--;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = counter.ToString();
    }
}
