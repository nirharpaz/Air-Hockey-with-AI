using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class StrategyMove: ScriptableObject
{
    [SerializeField] protected Vector2 speedRange;
    [SerializeField] public float waitingTime;

    public abstract Vector2 Act(Rigidbody rb, Transform target);
    public abstract Vector2 Act(Rigidbody rb, Vector2 target);

    public abstract Vector2 Wait(Rigidbody rb, Transform target, Vector2 refferancePos);

}
