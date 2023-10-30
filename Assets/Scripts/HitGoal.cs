using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitGoal : MonoBehaviour
{
    public UnityEvent OnHit;

    private void OnTriggerEnter(Collider other)
    {
        OnHit.Invoke();
    }

}
