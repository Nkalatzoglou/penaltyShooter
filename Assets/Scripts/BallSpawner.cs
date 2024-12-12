using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private GameManager gameManager;

    public List<ball> Balls;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;

        foreach(Transform child in transform)
        {
            Balls.Add(child.GetComponent<ball>());
            child.gameObject.SetActive(false);
        }

        //choose First balll
        gameManager.currentBall = Balls[0];
        Balls[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBall()
    {
        //choose First balll 
               
        gameManager.currentBall = transform.GetChild(0).GetComponent<ball>();
        gameManager.currentBall.gameObject.SetActive(true);
    }


}
