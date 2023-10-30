using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int WinCondition = 3;

    private int P1Score = 0;
    private int P2Score = 0;

    private GamaManager _gm;
    // Start is called before the first frame update
    void Awake()
    {
        _gm = FindObjectOfType<GamaManager>();
    }

    public void Goal(int playerNum)
    {
        if (playerNum == 1)
        {
            P1Score++;
            if(P1Score == WinCondition) _gm.PlayerWon(1);
            _gm.UpdatedScore(playerNum,P1Score) ;
        }

        if (playerNum == 2)
        {
            P2Score++;
            if (P2Score == WinCondition) _gm.PlayerWon(2);
            _gm.UpdatedScore(playerNum,P2Score) ;
        }
    }
    
}
