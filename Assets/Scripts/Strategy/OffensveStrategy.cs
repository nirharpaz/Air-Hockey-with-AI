using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "OffensiveStrategy1", menuName = "CustomScriptableObject/Stratrgy/Offensive")]
public class OffensveStrategy : StrategyMove
{
    public override Vector2 Act(Rigidbody rb, Transform target)
    { 
        Vector2 TargetPos = target.position;
        return Act(rb, TargetPos);
    }

    public override Vector2 Act(Rigidbody rb, Vector2 target)
    {
        float speed = Random.Range(speedRange.x, speedRange.y);

        Vector2 transitionPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        return transitionPos;
    }

    public override Vector2 Wait(Rigidbody rb, Transform target, Vector2 refferancePos)
    {
        throw new System.NotImplementedException();
    }
}
