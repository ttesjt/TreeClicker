using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EarthHealthEvents : MonoBehaviour
{
    public List<float> eventsHealthRatio;
    public List<UnityEvent> events;

    public Animator earthContainerAnimator;

    public void ShowEarthContainer() {
        earthContainerAnimator.SetBool("Appeared", true);
        GameRunner.currentInstance.EarthShowUp = true;
    }

    public void CheckForEvents(float healthRatio) { // 0 - 1
        healthRatio = Mathf.Clamp(healthRatio, 0, 1f);
        if (eventsHealthRatio.Count > 0 && events.Count > 0) {
            if (eventsHealthRatio[0] >= healthRatio) {
                events[0].Invoke();
                eventsHealthRatio.RemoveAt(0);
                events.RemoveAt(0);
            }
        }
    }
}
