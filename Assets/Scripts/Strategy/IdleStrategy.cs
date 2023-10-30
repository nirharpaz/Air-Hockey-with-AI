using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleStrategy1", menuName = "CustomScriptableObject/Stratrgy/Defensive/Idle")]
public class IdleStrategy : StrategyMove
{
    public override Vector2 Act(Rigidbody rb, Transform target)
    {
        throw new System.NotImplementedException();
    }

    public override Vector2 Act(Rigidbody rb, Vector2 target)
    {
        throw new System.NotImplementedException();
    }

    public override Vector2 Wait(Rigidbody rb, Transform target, Vector2 refferancePos)
    {
        float speed = Random.Range(speedRange.x, speedRange.y);
        Vector2 TargetPos = target.position;
        TargetPos.y = refferancePos.y;
        
        Vector2 transitionPos = Vector2.MoveTowards(rb.position, TargetPos, speed * Time.fixedDeltaTime);
        return transitionPos;
    }
}
