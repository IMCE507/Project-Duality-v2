using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool IsWaiting;

    public static HitStop instance;

    private void Awake()
    {
        instance = this;
    }

    public void Stop(float Duration)
    {
        if (IsWaiting)
            return;
        Time.timeScale = 0f;
        StartCoroutine(Wait(Duration));
    }

    IEnumerator Wait(float Duration)
    {
        IsWaiting = true;
        yield return new WaitForSecondsRealtime(Duration);
        Time.timeScale = 1f;
        IsWaiting = false;
    }
}
