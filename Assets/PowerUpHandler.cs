using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHandler : MonoBehaviour
{
    public List<PowerUpItemHandler> PowerUps;

    public List<PowerUp> ListOfPowerUps;

    public int currentUnlocked=0;

    [Header("Golden ball")]
    public BoxCollider mainCollider;
    public BoxCollider LeftCollider;
    public BoxCollider RightCollider;
    public GameObject particleCollisionPassThrought;
    public MeshRenderer Ball;
    private Material BallOriginMat;
    public Material GoldenBall;
    public GameObject particleBall;
    public GameObject particleBall_smoke;

    [Header("Double Points")]

    public Animator UIAnimator;

    // Start is called before the first frame update

    private void Awake() {
        ShuffleAndAssignPowerUps();
            }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateDoublePoints()
    {
        GameManager.gameManager.multiplyScore=2;
        UIAnimator.SetTrigger("Double");
    }

    public void DisactivateDoublePoints()
    {
        GameManager.gameManager.multiplyScore=1;
        UIAnimator.SetTrigger("Normal");
    }

    public void ActivateGoldenBall()
    {
        //Gaolkeeper will go to correct place but the ball will pass through him
        GameManager.gameManager.saveEverything=true;

        //dissable Colliders
        mainCollider.enabled=false;
        LeftCollider.enabled=false;
        RightCollider.enabled=false;

        BallOriginMat= Ball.material;
        Ball.material=GoldenBall;
        particleBall.SetActive(true);
        particleBall_smoke.SetActive(true);
        
        particleCollisionPassThrought.gameObject.SetActive(true);
    }

    public void DisableGoldenBall()
    {
        //enable Colliders
        mainCollider.enabled=true;
        LeftCollider.enabled=true;
        RightCollider.enabled=true; 

        Ball.material=BallOriginMat;
        particleBall.SetActive(false);
        particleBall_smoke.SetActive(false);         
    
        particleCollisionPassThrought.gameObject.SetActive(false);   
    }

    public void SelectPowerUp(PowerUpItemHandler powerUp)
    {
        foreach(PowerUpItemHandler pUp in PowerUps)
        {
            if(powerUp!=pUp)
            {
                pUp.GetComponent<Button>().interactable=false;

            }
            else
            {
                pUp.amount--;
            }
        }
    }

    public void ResetPowerUp()
    {
        foreach(PowerUpItemHandler pUp in PowerUps)
        {
            if(pUp.amount>0)
            {
                pUp.GetComponent<Button>().interactable=true;
            }
        }
    }

    

    public void UnLockNextOne()
    {
        PowerUps[currentUnlocked].UnLock();
    }

     public void ShuffleAndAssignPowerUps()
    {
        if (PowerUps.Count != ListOfPowerUps.Count)
        {
            Debug.LogError("PowerUps and ListOfPowerUps must be the same size!");
            return;
        }

        // Create a copy of the list to shuffle
        List<PowerUp> shuffledList = new List<PowerUp>(ListOfPowerUps);

        // Fisher–Yates shuffle
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            PowerUp temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }

        // Assign shuffled data
        for (int i = 0; i < PowerUps.Count; i++)
        {
            PowerUps[i].scriptableData = shuffledList[i];
        }
    }

    

}
