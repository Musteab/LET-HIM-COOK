using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCountersVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject stoveOn;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOn.SetActive(showVisual);
        particles.SetActive(showVisual);
    }
}
