using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeftCounter : MonoBehaviour
{
    public int counter = 0;
    private List<Image> lefts;

    private void Awake()
    {
        lefts = GetComponentsInChildren<Image>().ToList();
    }

    public void UpdateLeftCounter()
    {
        lefts[counter].GetComponent<Image>().color = Color.green;
        counter++;
    }

    void Update() { }
}
