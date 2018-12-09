using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private bool startOnStart = false;
    [SerializeField]
    private float time = 2f;
    private float currTime = 0F;

    [Header("Events")]
    [SerializeField]
    private UnityEvent OnTimerEnd;

    private void Start()
    {
        if (startOnStart)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        currTime -= Time.deltaTime;
        if (currTime < 0f)
        {
            currTime = 0f;
            OnTimerEnd.Invoke();
        }
    }

    public void StartTimer()
    {
        currTime = time;
    }
}
