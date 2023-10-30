using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GamaManager : MonoBehaviour
{
    [SerializeField] private PaddleController[] _players;
    [SerializeField] private Transform Puck;

    private Vector3 _puckInitPos;
    private Vector3 LastPuckVelocity;

    private UIManager _ui_mgr;
    private Rigidbody _puck_rb;

    
    //Audio
    [SerializeField] private AudioClip PuckSound;
    [SerializeField] private AudioClip MainSound;
    // Start is called before the first frame update
    void Awake()
    {
        _puckInitPos = Puck.position;
        _ui_mgr = FindObjectOfType<UIManager>();
        _puck_rb = Puck.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(PuckSound!=null)
            _puck_rb.GetComponent<HitSound>().SetHitSound(PuckSound);
        
        // main Audio
        if (MainSound != null)
        {
            AudioSource audio =  gameObject.AddComponent<AudioSource>();
            audio.clip = MainSound;
            audio.loop = true;
            audio.Play();
        }
    }

    // telling the UI manager to show the updated score
    public void UpdatedScore(int player, int score)
    {
        print("update player " + player + " with score " + score);
        
        _ui_mgr.AddScore(player,score);   
    }

    // pausing the game as player has won
    public void PlayerWon(int winner)
    {
        AllowPaddlesMovement(false);
        ResetPuck();
        
        _ui_mgr.AnnounceWinner((winner));
    }
    
    // resets puck to its 0 position
    public void ResetPuck()
    {
        _puck_rb.velocity=Vector3.zero;
        Puck.position = _puckInitPos;
    }

    public void OpenMenu()
    {
        LastPuckVelocity = _puck_rb.velocity;
        _puck_rb.velocity=Vector3.zero;
        AllowPaddlesMovement(false);
        _ui_mgr.ViewMenu(true);
    }
    
    public void CloseMenu()
    {
        _ui_mgr.ViewMenu(false);
        AllowPaddlesMovement(true);
        _puck_rb.velocity = LastPuckVelocity;
    }

    private void AllowPaddlesMovement(bool canMove)
    {
        foreach (PaddleController Paddle in _players)
        {
            Paddle.EnableMovement(canMove);
        }
    }
    
    //Game Menu Buttons    
    
    // Restart the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
    
    // Restart the game
    public void Quit()
    {
        Application.Quit();
    }
}

