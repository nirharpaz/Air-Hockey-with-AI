using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleController : MonoBehaviour
{
    //Inspector Variables
    [SerializeField] protected Transform Constraints;
    
    //Private Variables
    protected Bounderies _bounderies;
    protected Transform _transform;
    protected Rigidbody _rb;

    public bool CanMove = true;
    
    // Get BOunderies on Awake
    protected virtual void  Awake()
    {
        if (Constraints != null)
        {
            _bounderies = new Bounderies(
                Constraints.FindChildbyTag("Top").position.y,
                Constraints.FindChildbyTag("Buttom").position.y,
                Constraints.FindChildbyTag("Left").position.x,
                Constraints.FindChildbyTag("Right").position.x
            );
        }
        else
        {
            Debug.LogError($"In {transform.name} Constraints field have not been assigned");
            throw new NullReferenceException();
        }
        _transform = transform;
        _rb = _transform.GetComponent<Rigidbody>();
    }
    
    //Main positioning method, set position with respect to the bounderies constraints
    protected void SetPosition(Vector3 newPos)
    {
        newPos.z = 0f;
        newPos.x = Math.Clamp(newPos.x, _bounderies.Left, _bounderies.Right);
        newPos.y = Math.Clamp(newPos.y, _bounderies.Down, _bounderies.Up);
        
        _rb.MovePosition(newPos);
    }
    
    // Allow or disallow movement toggle
    public void EnableMovement(bool allowed)
    {
        CanMove = allowed;
    }
    
}
