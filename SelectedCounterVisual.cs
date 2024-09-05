using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        playerscript.Instance.selectedcounterChanged += Instance_selectedcounterChanged;
    }

    private void Instance_selectedcounterChanged(object sender, playerscript.SelectedcounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
           Show();
        }
        else
        {
           Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }

}
