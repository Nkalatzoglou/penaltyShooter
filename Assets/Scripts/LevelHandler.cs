using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [Header("References")]

    public GameManager gameManager;
    public Player_Handler player_Handler;

    [Header("Variables")]
    public int ShowBarAfterPenalty=7;
    public int StartProvidingPowerUps;
    // Start is called before the first frame update
    void Start()
    {
        gameManager=GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        /* if(ScoreController.instance.ShootCounter>=ShowBarAfterPenalty)
        {
            player_Handler.ApplyForce=true;
        } */
    }
}
