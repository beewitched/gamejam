using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PreasurePlate : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    private UnityEvent OnActivate;
    [SerializeField]
    private UnityEvent OnDeActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnActivate.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnDeActivate.Invoke();
    }
}
