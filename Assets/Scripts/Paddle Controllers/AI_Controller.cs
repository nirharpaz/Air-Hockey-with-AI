using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AI_Controller : PaddleController
{
    [SerializeField] private float ReAttackCounter = 0.5f;
    [SerializeField] private Transform GatePos;
    [SerializeField] private Transform ResetPos;

    [SerializeField] private StrategyMove OffensiveMove;
    [SerializeField] private StrategyMove DefensiveMove;
    [SerializeField] private StrategyMove IdleMove;
    [SerializeField] private StrategyMove StuckMove;

    private Transform _puck;
    [SerializeField] private Vector3 lastKnownPosition;
    [SerializeField] private float StuckTimeCheck;
    private Vector3 _initPosition;
    private bool _inGate = false;
    [SerializeField] private bool _disengaging = false;

    private bool _hitPuck = false;
    
    //Reaction Types
    enum Reaction
    {
        Idle, Attack,Guard, stuck
    }
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _puck = GameObject.FindWithTag("Puck").transform;
        _initPosition = _transform.position;
        _initPosition.z = 0;
    }
    
#region AI
    // Main tracking on the AI situation is handled from FixedUpdate
    //It is then executed from the method React
    void FixedUpdate()
    {
        if (!CanMove) return;
        // puck is in enemy part
        if (!CheckIfPuckIsInMyHalf())
        {
            _inGate = false;
            React(Reaction.Idle);
        }
        // puck is above me, go defensive if not reached gate
        else if (CheckIfPuckIsAboveMe())
        {
            if (!_inGate && !_disengaging)
            {
                React(Reaction.Guard);
                // Is the paddle is in the same place for over a given amount of time,
                // assume that the situation is stuck and retreat to predefined init pos to try again. 
                if (_rb.position == lastKnownPosition)
                {
                    StuckTimeCheck +=Time.deltaTime;
                    if (StuckTimeCheck>= StuckMove.waitingTime)
                    {
                        _disengaging = true;
                    }
                }
            }// Perform the disengagement
            else if(_disengaging) 
            {
                React(Reaction.stuck);
                if (_rb.position == ResetPos.position)
                {
                    lastKnownPosition = _rb.position;
                    StuckTimeCheck = 0;
                    _disengaging = false;
                }
            }
        }

        //Go aggrassive
        else if (CheckIfPuckIsInMyHalf() && !CheckIfPuckIsAboveMe())
        {
            // Move thowards the gate for defense right after Hitting the puck
            if (_hitPuck)
            {
                React(Reaction.Guard);
                Invoke("ResetHitPuck",ReAttackCounter);
            }
            // Hit the puck if not done so already
            else
            {
                React(Reaction.Attack);
            }
        }
    }
    
    
    // Execute dediated reaction
    private void React(Reaction r)
    {
        switch (r)
        {
            // Behaviour when the puck is on the opponent side
            case Reaction.Idle:
                SetPosition(IdleMove.Wait(_rb, _puck, _initPosition));
                break;
            // Attack behaviour
            case Reaction.Attack:
                SetPosition(OffensiveMove.Act(_rb, _puck));
                break;
            // Defensive behaviour
            case Reaction.Guard:
                if (GatePos != null)
                    SetPosition(DefensiveMove.Act(_rb, _puck));
                break;
            // Handle situations where AI is stuck in place for any reason
            case Reaction.stuck:
                if (ResetPos != null)
                    SetPosition(StuckMove.Act(_rb,ResetPos));
                break;
        }

    }
#endregion

#region player and puck position checks

    // Update last known position
    private void LateUpdate()
    {
        lastKnownPosition = _rb.position;
    }
    
    // checks if the pack is in player's half or opponent's half
    protected bool CheckIfPuckIsInMyHalf()
    {
        if (_puck.position.y < _bounderies.Down)
            return false;
        return true;
    }
    
    // Checks the position of the puck relative to player
    protected bool CheckIfPuckIsAboveMe()
    {
        if (_puck.position.y > _transform.position.y)
            return true;
        return false;
    }
    
    // Check if near the gate
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Gate")
        {
            _inGate = true;
            _hitPuck = false;        
        }    
    }
    
#endregion
    
#region Hitting puck
    // When Hitting puck
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Puck")
        {
            _hitPuck=true;
        }
    }
    // Toggle from defensive to offensive after each hit
    protected void ResetHitPuck()
    {
        _hitPuck = false;
    }
#endregion

}
