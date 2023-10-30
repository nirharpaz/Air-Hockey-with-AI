using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text Player1Text;
    [SerializeField] TMP_Text Player2Text;
    [SerializeField] TMP_Text Winner_Playe_Text;

    
    [SerializeField] GameObject WinnerScene;
    [SerializeField] GameObject MenuScene;
    
    private void Awake()
    {
        WinnerScene?.SetActive(false);
        MenuScene?.SetActive(false);
        
    }

    public void AddScore(int player,int score)
    {
        if (player == 1)
            Player1Text.text = score.ToString();
        if (player == 2)
            Player2Text.text = score.ToString();
    }

    public void AnnounceWinner(int player)
    {
        WinnerScene.SetActive(true);
        Winner_Playe_Text.text = player.ToString();
    }

    public void ViewMenu(bool open)
    {
        MenuScene.SetActive(open);
    }
}
