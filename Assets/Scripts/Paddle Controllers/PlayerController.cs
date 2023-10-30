using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : PaddleController
{
    private bool PlayerPaddleIsHeld = false;
    protected SphereCollider _playerCollider;
    [SerializeField] protected LayerMask PlayerMask; 

    // Start is called before the first frame update
    protected override  void Awake()
    {
        base.Awake();
        _playerCollider = GetComponent<SphereCollider>();

    }

    private void Update()
    {
        if (!CanMove) return;
        
        if (Input.GetMouseButton(0))
        {
            GetPaddle();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            PlayerPaddleIsHeld = false;
        }

        if (PlayerPaddleIsHeld)
        {
            MoveToPosition();
        }
    }

    protected void GetPaddle()
    {
    RaycastHit FirstHit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out FirstHit, Mathf.Infinity, PlayerMask) &&  FirstHit.transform != null)
        {
            PlayerPaddleIsHeld = true;
        }
    }
    

    // Update is called once per frame
    void MoveToPosition()
    {
        RaycastHit hit;

        if (PlayerPaddleIsHeld)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, PlayerMask) && hit.transform != null)
            {
                Vector3 newPos = hit.point;
                SetPosition(newPos);

            }
        }

    }
    
    
}
