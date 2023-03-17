using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showFryingVisuals = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showFryingVisuals);
        particlesGameObject.SetActive(showFryingVisuals);
    }
}
