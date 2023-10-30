using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "StrightLineMove", menuName = "CustomScriptableObject/Stratrgy/StrightLineMove")]
public class OffensveStrategy : StrategyMove
{
    //Move Straight to the target pose
    public override Vector2 Act(Rigidbody rb, Transform target)
    { 
        Vector2 TargetPos = target.position;
        return Act(rb, TargetPos);
    }
    
    //Move Straight to the target pose in a random speed between interval
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
