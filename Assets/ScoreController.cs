using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public List<Image> UIScore;

    public static ScoreController instance;

    public int ShootCounter;

    public bool HaveResult;
    // Start is called before the first frame update
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootResult(string result)
    {
        if(!HaveResult)
        {
            GameManager.gameManager.currentBall.result=true;
            if(result=="Goal")
            {
                UIScore[ShootCounter].color=Color.green;
                PenaltyManager.instance.OnPlayerShoots(true);
                Crowd.istance.UpdateState("Cheer");
            }
            else
            {
                UIScore[ShootCounter].color=Color.red;
                PenaltyManager.instance.OnPlayerShoots(false);
            }

            ShootCounter++;
            HaveResult=true;
        }
        
    }

    public void substructOne()
    {
        ShootCounter--;
        UIScore[ShootCounter].color=Color.white;
    }
}
