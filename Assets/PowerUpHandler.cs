using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHandler : MonoBehaviour
{
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

    [Header("Double Points")]

    public Animator UIAnimator;

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

    // Start is called before the first frame update

    private void Awake() 
    {
        ShuffleAndAssignPowerUps();
        audioSource=GetComponent<AudioSource>();
    }
    void Start()
    {
        
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
    }

    //When release ball
    public void Activate2ndChance_Vol2()
    {
        audioSource.PlayOneShot(sfirixta);
    }

    //When Reset Scene
    public void Activate2ndChance_Vol3()
    {
        UISfirixta2ndChance.gameObject.SetActive(true);
        RefereHumanoid.gameObject.SetActive(false);  

        StartCoroutine(SecondChance());      
    }

    IEnumerator SecondChance()
    {
        yield return new WaitForSeconds(secondToShowUi);

        UISfirixta2ndChance.gameObject.SetActive(false);
        GameManager.gameManager.SecondChance=false;
    }

    public void ActivateCalmDown()
    {
        ForceBarHandler.instance.speed=0.5f;
        ForceBarHandler.instance.SoundBar.pitch=0.8f;
    }

    public void DisableCalmDown()
    {
        ForceBarHandler.instance.speed=ForceBarHandler.instance.OriginSpeed;
        ForceBarHandler.instance.SoundBar.pitch=1f;
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
        Ball=GameManager.gameManager.currentBall.transform.GetChild(0).GetComponent<MeshRenderer>();
        particleBall_smoke = GameManager.gameManager.currentBall.transform.GetChild(1).gameObject;
        particleBall_smoke.SetActive(true);

        //Gaolkeeper will go to correct place but the ball will pass through him
        GameManager.gameManager.saveEverything=true;

        //dissable Colliders
        mainCollider.enabled=false;
        LeftCollider.enabled=false;
        RightCollider.enabled=false;

        BallOriginMat= Ball.material;
        Ball.material=GoldenBall;
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
