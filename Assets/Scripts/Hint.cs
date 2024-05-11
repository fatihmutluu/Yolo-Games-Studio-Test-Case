using System.Collections;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private GameControl controller;

    public int counter = 4;

    private void Awake()
    {
        controller = GameObject.Find("Images").GetComponent<GameControl>();
    }

    public void HintButtonClicked()
    {
        GameObject upBackground = GameObject.Find("Up Background");
        GameObject downBackground = GameObject.Find("Down Background");
        StartCoroutine(ScaleWithLerp(upBackground.transform, Drag.initialScale, 0.3f));
        StartCoroutine(ScaleWithLerp(downBackground.transform, Drag.initialScale, 0.3f));

        ShowHint();
    }

    // ! Scale Slowly (ChatGPT)
    public IEnumerator ScaleWithLerp(Transform target, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = target.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            target.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = targetScale;
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

    public void setHintCount(int count)
    {
        counter = count;
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = counter.ToString();
    }
}
