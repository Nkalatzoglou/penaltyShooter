using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter_Handler : MonoBehaviour
{
    private GameManager gamemanger;

    private Animator animator;

    public bool shooting;

    //Used not to run animation again 
    //Cause already started on different timespot
    private bool animationStarted;

    public Animator FadeOutScene;

    public Vector3 forceChangeDirection;

    public AudioClip shootBall;
    public AudioClip EehhSound;

    public AudioSource audioSource;

    public AudioSource audioSourceVoice;

    public GameObject LeafsEffect;

    public List<AudioClip> LostVoice;

    public List<AudioClip> WinVoice;
    //public RigBuilder rig;
    // Start is called before the first frame update
    void Start()
    {
        gamemanger = GameManager.gameManager;
        animator = GetComponent<Animator>();
        audioSource=GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void playLostSound()
    {
        audioSource.pitch=1f;
        if (LostVoice.Count == 0 || audioSource == null) return;

        int randomIndex = Random.Range(0, LostVoice.Count);
        audioSourceVoice.PlayOneShot(LostVoice[randomIndex]);
    }

     public void playWinSound()
    {
        audioSource.pitch=1f;
        if (WinVoice.Count == 0 || audioSource == null) return;

        int randomIndex = Random.Range(0, WinVoice.Count);
        audioSourceVoice.PlayOneShot(WinVoice[randomIndex]);
    }


    public void playEeehSound()
    {
        var pitchSound = Mathf.Round(Random.Range(0.6f, 1.4f) * 10f) / 10f;
        Debug.Log("pitchSound " + pitchSound);
        audioSource.pitch = pitchSound;
        audioSource.PlayOneShot(EehhSound);
    }

    public void StartFadeOut()
    {
        FadeOutScene.SetTrigger("SceneTransition");
    }

    public void StartCameraMovement()
    {
        //from Fild of view 23  to 18.5
        //from z -25.18 to -22.18  
        var targetpos = new Vector3(gamemanger.mainCam.transform.position.x, gamemanger.mainCam.transform.position.y,-22.18f);
        gamemanger.StartCameraZoomEffect(targetpos,18.5f,1.4f);

    }

    public void FinishedAnimation()
    {
        gamemanger.PenaltyKickerFinished=true;
    }

    
    public void ShootBall()
    {
        GameManager.gameManager.currentBall.ShootForce_Forward();        
    }
    
    public void ShootBallWithTarget()
    {
        var target= gamemanger.Player_Handler.selectedTarget;
        
        
        //If Shoot On mid shoot ball on goalkeepers hug
        if(gamemanger.target_handler.whatSide()=="MidMidCatch")
        {
            target=gamemanger.MidMidHug;
            gamemanger.currentBall.ballWillBeCatched();
        }

        gamemanger.currentBall.ShootForce_Direction(target,forceChangeDirection);

        if(!animationStarted && gamemanger.target_handler.whatSide()!="LowTop")
        {
            activateAI_Save();
        }   
  
    }

    public void ShootBallWithTarget_Sooner1()
    {
        if(GameManager.gameManager.target_handler.whatSide()=="LargeTop")
        {            
            var target= gamemanger.Player_Handler.selectedTarget;
            //gamemanger.currentBall.ShootForce_Direction(target);
            activateAI_Save();
            animationStarted=true;

        }

        
       
    }

    
    public void ShootBallWithTarget_Sooner2()
    {
        //audioSource.pitch=1;
        audioSource.PlayOneShot(shootBall);
        LeafsEffect.gameObject.SetActive(true); 

        if(GameManager.gameManager.target_handler.whatSide()=="LowMiddle")
        {            
            var target= gamemanger.Player_Handler.selectedTarget;
            //gamemanger.currentBall.ShootForce_Direction(target);
            activateAI_Save();
            animationStarted=true;            
        }
        
    }

    public void ShootBallWithTarget_Later_LowTop()
    {
        if(gamemanger.target_handler.whatSide()=="LowTop")
        {            
            var target= gamemanger.Player_Handler.selectedTarget;
            //gamemanger.currentBall.ShootForce_Direction(target);
            activateAI_Save();
            animationStarted=true;
 
        }        
    }

    public void ShootBallWithTarget_Sooner_Mid()
    {
        if(GameManager.gameManager.target_handler.whatSide()=="LargeTop" || gamemanger.target_handler.whatSide()=="MidMidCatch" )
        {            
            var target= gamemanger.Player_Handler.selectedTarget;
            if(gamemanger.target_handler.whatSide()=="MidMidCatch")
            {
                //Debug.Log("Change Target");
                target=gamemanger.MidMidHug;
            }
            //gamemanger.currentBall.ShootForce_Direction(target);
            activateAI_Save();
            animationStarted=true;

        }        
    }


    public void activateAI_Save()
    {
        //Activate AI 
        gamemanger.goalKeeperSave_AI();
    }

    public void startShootBallAnimation()
    {
        shooting = true;
        animator.SetTrigger("Shoot");
    }


    public void stopShootBallAnimation()
    {
        animationStarted=false;
        //shooting = false;
        animator.ResetTrigger("Shoot");
    }

    public void resetShooter()
    {
        animator.ResetTrigger("Reset");
    }
}
