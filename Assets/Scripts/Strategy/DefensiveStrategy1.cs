using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "DefensiveStrategy1", menuName = "CustomScriptableObject/Stratrgy/Defensive/Defensive")]
public class DefensiveStrategy1 : StrategyMove
{
    private bool isRunningCoroutine = false;
    
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
    IEnumerator ReleaseAndReturn(Rigidbody rb, Vector2 refferancePos)
    {
        float startTime = Time.time; 
        isRunningCoroutine = true;
        float speed = Random.Range(speedRange.x, speedRange.y);

        while (Time.time - startTime < waitingTime)
        {
            Vector2 transitionPos = Vector2.MoveTowards(rb.position, refferancePos, speed * Time.fixedDeltaTime);
        }

        yield return null;
        MonoInstance.instance.StopAllCoroutines();
        isRunningCoroutine = false;
    }
}

    