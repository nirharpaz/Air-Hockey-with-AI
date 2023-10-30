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

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _puck = GameObject.FindWithTag("Puck").transform;
        _initPosition = _transform.position;
        _initPosition.z = 0;
    }

    // Update is called once per frame
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
                if (_rb.position == lastKnownPosition)
                {
                    StuckTimeCheck +=Time.deltaTime;
                    if (StuckTimeCheck>= StuckMove.waitingTime)
                    {
                        _disengaging = true;
                    }
                }
            }
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
        
       
        //go Active
        else if (CheckIfPuckIsInMyHalf() && !CheckIfPuckIsAboveMe())
        {
            // Defend gate right after Hitting the puck
            if (_hitPuck)
            {
                React(Reaction.Guard);
                Invoke("ResetHitPuck",ReAttackCounter);
            }
            // hit the puck if not done so already
            else
            {
                React(Reaction.Attack);
            }
        }
      
    }

    private void LateUpdate()
    {
        lastKnownPosition = _rb.position;
    }

    protected bool CheckIfPuckIsInMyHalf()
    {
        if (_puck.position.y < _bounderies.Down)
            return false;
        return true;
    }
    
    protected bool CheckIfPuckIsAboveMe()
    {
        if (_puck.position.y > _transform.position.y)
            return true;
        return false;
    }
    
    // toggle from defensive to offensive after each hit
    protected void ResetHitPuck()
    {
        _hitPuck = false;
    }  
    

    enum Reaction
    {
        Idle, Attack,Guard, stuck
    }

    private void React(Reaction r)
    {
        switch (r)
        {
            case Reaction.Idle:
                SetPosition(IdleMove.Wait(_rb, _puck, _initPosition));
                break;
            case Reaction.Attack:
                SetPosition(OffensiveMove.Act(_rb, _puck));
                break;
            case Reaction.Guard:
                if (GatePos != null)
                    SetPosition(DefensiveMove.Act(_rb, _puck));
                break;
            case Reaction.stuck:
                if (ResetPos != null)
                    SetPosition(StuckMove.Act(_rb,ResetPos));
                break;
        }

    }
    

    // When Hitting puck
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Puck")
        {
            _hitPuck=true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Gate")
        {
            _inGate = true;
            _hitPuck = false;        }    
    }

}
