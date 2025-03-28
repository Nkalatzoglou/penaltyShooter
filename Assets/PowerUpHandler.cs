using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHandler : MonoBehaviour
{
    public static PowerUpHandler instnace;
    public AudioSource audioSource;
    public List<PowerUpItemHandler> PowerUps;

    public List<PowerUp> ListOfPowerUps;

    public int currentUnlocked=0;

    [Header("Golden ball")]
    public BoxCollider mainCollider;
    public BoxCollider LeftCollider;
    public BoxCollider RightCollider;
    public GameObject particleCollisionPassThrought;
    public GameObject BallParticle;    
    private Material BallOriginMat;
    public Material GoldenBall;
    public MeshRenderer Ball;
    public GameObject particleBall_smoke;

    public AudioClip GoldenBallSound;

    public Transform parentOfExplosion;

    [Header("Double Points")]
    public AudioClip DoublePoints;

    [Header("Calm Down")]

    public float speed=0.5f;

    [Header("2nd Chance")]

    public Transform RefereHumanoid;

    public AudioClip sfirixta;
    public Transform UISfirixta2ndChance;

    public float secondToShowUi=5f;

    [Header("FlyingBird")]
    public GameObject Bird; //Logic to Move around and Detect Collision

    public int additionalPoints=100;

    private float originalGamePlaySpeed;

    // Start is called before the first frame update

    private void Awake() 
    {
        instnace=this;
        ShuffleAndAssignPowerUps();
        audioSource=GetComponent<AudioSource>();
    }
    void Start()
    {
        originalGamePlaySpeed=GameManager.gameManager.gamespeed;
        AssignTwoAmount();
         AssignTwoAmount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFlyingBird()
    {
        GameManager.gameManager.AdditionalScore=additionalPoints;
        Bird.gameObject.SetActive(true);
    }

    public void DisableFlyingBird()
    {
        GameManager.gameManager.AdditionalScore=0;
        Bird.gameObject.SetActive(false);
    }

    //When PRess
    public void Activate2ndChance_Vol1()
    {
        RefereHumanoid.gameObject.SetActive(true);
        GameManager.gameManager.SecondChance=true;

        RefereHumanoid.GetComponent<AudioSource>().PlayOneShot(sfirixta);
    }


    //When Reset Scene
    public void Activate2ndChance_Vol2()
    {
        UISfirixta2ndChance.gameObject.SetActive(true);

        RefereHumanoid.gameObject.SetActive(false);  

        StartCoroutine(SecondChance());      
    }

    IEnumerator SecondChance()
    {        

        yield return new WaitForSeconds(secondToShowUi);

        CanvasHandler.instance.PowerUpIndicator.GetComponent<Animator>().SetTrigger("Hide"); 
        UISfirixta2ndChance.gameObject.SetActive(false);
        GameManager.gameManager.SecondChance=false;
    }

    public void ActivateCalmDown()
    {
        GameManager.gameManager.gamespeed=originalGamePlaySpeed*0.65f;
        GameManager.gameManager.calmDownMode=true;
        GameManager.gameManager.mainSound.pitch=0.9f;
    }

    public void DisableCalmDown()
    {
        GameManager.gameManager.gamespeed=originalGamePlaySpeed;
        GameManager.gameManager.calmDownMode=false;
        GameManager.gameManager.mainSound.pitch=1f;
    }

    public void ActivateDoublePoints()
    {
        GameManager.gameManager.multiplyScore=2;
        audioSource.PlayOneShot(DoublePoints);  
    }

    public void DisactivateDoublePoints()
    {
        GameManager.gameManager.multiplyScore=1;
    }

    //add 2 to all for testing
    public void AssignTwoAmount()
    {
        foreach(PowerUpItemHandler pUp in PowerUps)
        {
            AddPowerUp(pUp.name);
        }
    }

    public void AddPowerUp(string nameOfPowerUp)
    {
        foreach(PowerUpItemHandler pUp in PowerUps)
        {
            if(pUp.name==nameOfPowerUp)
            {
                pUp.amount++;
                Debug.Log("test");
                pUp.UpdateTextAmount();
            }
        }
    }

    public void ActivateGoldenBall()
    {
        audioSource.PlayOneShot(GoldenBallSound);
        Ball=GameManager.gameManager.currentBall.transform.GetChild(0).GetComponent<MeshRenderer>();
        particleBall_smoke = GameManager.gameManager.currentBall.transform.GetChild(1).gameObject;
        particleBall_smoke.SetActive(true);

        //Gaolkeeper will go to correct place but the ball will pass through him
        GameManager.gameManager.saveShoot=true;
        GameManager.gameManager.LooseShoot=false;

        //dissable Colliders
        GameManager.gameManager.goldenBall=true;
        mainCollider.enabled=false;
        LeftCollider.enabled=false;
        RightCollider.enabled=false;

        BallOriginMat= Ball.material;
        Ball.material=GoldenBall;
        particleBall_smoke.SetActive(true);

        BallParticle.gameObject.SetActive(true);

        var ballObj=Ball.transform.parent.gameObject;
        var Goalkeeper = mainCollider.transform.parent.gameObject;
        
    }



    public void DisableGoldenBall()
    {
        //enable Colliders
        mainCollider.enabled=true;
        LeftCollider.enabled=true;
        RightCollider.enabled=true; 

        BallParticle.gameObject.SetActive(false);

        particleCollisionPassThrought.transform.SetParent(parentOfExplosion);
        particleCollisionPassThrought.transform.localPosition=Vector3.zero;
    

        //Ball.material=BallOriginMat;
        //particleBall_smoke.SetActive(false);         
    
        particleCollisionPassThrought.gameObject.SetActive(false);   
    }

     public void DissableAllPowerUps()
    {
        foreach(PowerUpItemHandler pUp in PowerUps)
        {
            pUp.GetComponent<Button>().interactable=false;
        }
    }

     public void ActivateAllPowerUps()
    {
        foreach(PowerUpItemHandler pUp in PowerUps)
        {
            if(pUp.amount>0)
            {
                pUp.GetComponent<Button>().interactable=true;
            }            
        }
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
                CanvasHandler.instance.ActivateIndicator(powerUp.scriptableData);
                //ActivatePowerUp
                switch(powerUp.name)
                {
                    case "Golden Ball":
                        ActivateGoldenBall();
                        break;
                    case "Double Points":
                        ActivateDoublePoints();
                        break;
                    case "Catch the Bird":
                        break;
                    case "Calm Down":
                        ActivateCalmDown();
                        break;
                    case "2nd Chance":
                        Activate2ndChance_Vol1();
                        break;                        
                }
                pUp.amount--;
                pUp.UpdateTextAmount();
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
            else
            {
                pUp.GetComponent<Button>().interactable=false;
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
