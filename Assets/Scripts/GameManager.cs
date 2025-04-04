using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{    public bool GoalMade;
    public bool DisActivateHelpers;
    public bool round2ndChance17;
    public bool round2ndChance9;
    public PowerUp GoldenBall;
    public PowerUp DoublePoints;
    public PowerUp FlyingBird;
    public PowerUp SecondChange;
    public PowerUp Slowtime;
    
    public bool pauseTimer=true;
    public List<Transform> fireWorks;
    public bool calmDownMode;
    public bool goldenBall;
    public bool hideIndicator;
    public AudioSource mainSound;
    public bool SecondChance;
    public int scoreCounter=0;
    public int multiplyScore=1;
    public int BasicScore=10;
    public int AdditionalScore=0;

    [Header("Keep Scores - UI")]
    //public int shoot_Counter;
    public int goal_Counter;
    public TextMeshProUGUI ScoreCounter;

    public TextMeshProUGUI countdownText;
     public Shooter_Handler PenaltyKicker;

    public Player_Handler Player_Handler;

    public target_Handler target_handler;

    public goalkeeper_Handler goalkeep_Handler;

    public static GameManager gameManager;

    public ball currentBall;

    public BallSpawner ballSpawner;

    public float TimerPerScene;

    public Transform input_Target;

    public Transform XMaxRight;
    public Transform XMaxLeft;

    public Transform YMaxTop;

    public string TargetName;

    public GameObject stopper;
    public bool stopperEnabled;

    public bool saveEverything;
    public bool saveShoot;
    
    public bool LooseShoot;

    public Transform MidMidHug;

    [Range(0, 2)]
    public float gamespeed=1;


    public bool checkSave;
    public bool checkGoal;

    public Camera mainCam;
    public bool StartCameraMov;

    Vector3 cameraOrigPos;
    float fieldOfView;

    public bool GoalKeeperFinished;
    public bool PenaltyKickerFinished;

    public Transition_Handler trans_Handler;


    public enum GameStatus{
        Runtime,
        Pause
    }

    public GameStatus CurrentStatus;

    public GameObject PauseMenu;

    public LevelManager levelManager;
    
    public Transform endScreen;

    public Shooter_Handler shooter_Handler;

    public string SideSelected;

    public float pointsPerGoal=10;

    public Coroutine coroutineTimer;

    public Transform tibanaAudioSource;

    public Transform helpersWindow;


    public void EnableHelpers()
    {
        pauseTimer=false;
        helpersWindow.gameObject.SetActive(false);
        DisActivateHelpers=false;

        CanvasHandler.instance.ActivateHelper("TargetTutorial");
    }

    public void DissableHelpers()
    {
        pauseTimer=false;
        helpersWindow.gameObject.SetActive(false);
        DisActivateHelpers=true;
    }
    //public bool canshoot;
    // Start is called before the first frame update
    private void Awake() {
        gameManager=this;
        mainCam = Camera.main;

        levelManager = FindObjectOfType<LevelManager>();

        cameraOrigPos= mainCam.transform.position;
        fieldOfView=mainCam.fieldOfView;
    }
    void Start()
    {
        DisActivateHelpers=true;
        PowerUpHandler.instnace.DissableAllPowerUps();

        if(!DisActivateHelpers)
        {
            CanvasHandler.instance.ActivateHelper("TargetTutorial");
        }
        

        
        if(coroutineTimer==null)
        {
            coroutineTimer=StartCoroutine(CountdownCoroutine(15f,false));
        }
    }
    private void FixedUpdate() {
        Time.timeScale = gamespeed;  

        /*
        if(StartCameraMov)
        {
            //from Fild of view 23  to 18.5
            //from z -25.18 to -22.18  
            mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, Mathf.Lerp(-25.18f, -22.18f, t));
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView,19f,t);

            t += 0.3f * Time.deltaTime;

            if (t > 1.0f)
            {
                StartCameraMov=false;
                t = 0.0f;
            }
        }   
        */   
    }

    public void BackMainMenu()
    {
        Time.timeScale=1;
        Debug.Log("MainMenu");
        LevelManager.instance.LoadScene("MainMenu",3f);
    }

    public void RestartLevel()
    {
        Time.timeScale=1;
        LevelManager.instance.LoadScene("GameScene",3f);
    }

    public void PauseScreen()
    {
        CurrentStatus=GameStatus.Pause;
        Time.timeScale=0;
        PauseMenu.SetActive(true);        
    }

    public void UnPauseScreen()
    {      
        CurrentStatus=GameStatus.Runtime;  
        Time.timeScale=1;
        PauseMenu.SetActive(false);

    }

    public void resetCamera()
    {

        mainCam.transform.position = cameraOrigPos;
        mainCam.transform.GetChild(0).position =cameraOrigPos;

        mainCam.fieldOfView = fieldOfView;
          mainCam.transform.GetChild(0).GetComponent<Camera>().fieldOfView= fieldOfView;

        StartCoroutine(CountdownCoroutine(15,true));
    }

    public void StartCameraZoomEffect(Vector3 targetPosition,float endValue,float duration)
    {
        StartCoroutine(LerpCameraValues(targetPosition,duration));
        StartCoroutine(LerpCmeraZoom(endValue,duration));
    }

    IEnumerator LerpCameraValues(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = mainCam.transform.position;
        while (time < duration)
        {
            mainCam.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            mainCam.transform.GetChild(0).position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.transform.position = targetPosition;
        mainCam.transform.GetChild(0).position = targetPosition;
    }

    IEnumerator LerpCmeraZoom(float endValue, float duration)
    {
        float time = 0;
        float startValue = mainCam.fieldOfView;
        while (time < duration)
        {
            mainCam.fieldOfView = Mathf.Lerp(startValue, endValue, time / duration);
             mainCam.transform.GetChild(0).GetComponent<Camera>().fieldOfView = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.fieldOfView = endValue;
         mainCam.transform.GetChild(0).GetComponent<Camera>().fieldOfView= endValue;
    }

    public void isPenaltySaved()
    {
        checkSave = true;
        var dice = Random.Range(0,100);
        if(dice<60)
        {
            PenaltyKicker.GetComponent<Animator>().SetTrigger("Defeat2");
        }
        else
        {
            PenaltyKicker.GetComponent<Animator>().SetTrigger("Defeat1");
        }

        ScoreController.instance.ShootResult("Lost");


    }

    public void isPenaltyScored()
    {
        checkSave = true;
        PenaltyKicker.GetComponent<Animator>().SetTrigger("Victory");
        ScoreController.instance.ShootResult("Goal");

    }

    public void isPenaltyOutOrDokari()
    {
        //change to out Animation
        
        checkSave = true;
        var dice = Random.Range(0,100);
        if(dice<60)
        {
            PenaltyKicker.GetComponent<Animator>().SetTrigger("Defeat2");
        }
        else
        {
            PenaltyKicker.GetComponent<Animator>().SetTrigger("Defeat1");
        }

        ScoreController.instance.ShootResult("Lost");

    }


    public IEnumerator ShootCoroutine()
    {
        //here is handled as or since if on turns true coroutine ends.
        //It works for now but maybe we should change it later
        while(!GoalKeeperFinished && !PenaltyKickerFinished)
        {
            //Debug.Log("Penalty Instnace");
            yield return null;
        }

        

        trans_Handler.GetComponent<Animator>().SetTrigger("FadeOut");
        trans_Handler.UIFadeOut.GetComponent<Animator>().SetTrigger("FadeOut");
        
        PowerUpHandler.instnace.DissableAllPowerUps();
        
        if(ScoreController.instance.ShootCounter==0)
        {
            var coinflip = Random.Range(0,100);
            if(coinflip<50)
            {
                saveShoot=true;
                LooseShoot=false;
            }
            else
            {
                saveShoot=false;
                LooseShoot=true;
            }
        }


        if(saveEverything)
        {
            LooseShoot=false;
            saveShoot=true;
        }

        if(ScoreController.instance.ShootCounter==20)
        {
            endScreen.gameObject.SetActive(true);
            Time.timeScale=0;
            CurrentStatus= GameStatus.Pause;
        }

        
        //SceneChanger();
        
    }

    public void PauseGame()
    {
        gamespeed=0;
    }

    public void activate_Stopper()
    {
        if(stopperEnabled)
        {
            stopper.SetActive(true);
        }

    }

    public void DisActivate_Stopper()
    {
        if(stopperEnabled)
        {
            stopper.SetActive(false);
        }

    }

    public void setTargetName(GameObject target)
    {
        TargetName=target.name;
    }
    
    IEnumerator WaitAndRun(System.Action action)
    {
        yield return new WaitForSeconds(1.5f);
        action();
    }

    public void execute_Shoot(bool applyForce,float force)
    {      
        
        PowerUpHandler.instnace.DisableCalmDown(); 

        if(hideIndicator)
        {
            CanvasHandler.instance.PowerUpIndicator.GetComponent<Animator>().SetTrigger("Hide"); 
            hideIndicator=false;          
        }

        StopAllCoroutines();
        coroutineTimer=null;

        if(!applyForce)
        {
            if(!PenaltyKicker.shooting)
            {
                target_handler.markSide();
                PenaltyKicker.startShootBallAnimation();
                goalKeeperSave_AI_Step();
            }
            
        }
        else
        {
            if(!PenaltyKicker.shooting)
            {
                //Calculate diretction
                var offDire = CalculateNewPositionY(force);
                gameManager.shooter_Handler.forceChangeDirection=offDire;

                //Currently not recognized by goalkeeer, no ray to detect
                target_handler.recalculatePosition(offDire);

                target_handler.markSide();
                PenaltyKicker.startShootBallAnimation();
                goalKeeperSave_AI_Step();

            }
        }

        GoalKeeperFinished=false;
        PenaltyKickerFinished=false;
        StartCoroutine(ShootCoroutine());    


    }

    public Vector3 CalculateNewPositionY(float force)
    {
        var offDire = new Vector3(0,0,0);        
        //maximum off on Y 
        if(Mathf.Abs(force)>0.15f)
        {
            var newY= Mathf.Abs(force)*4.5f;
            offDire= new Vector3(offDire.x,newY,offDire.z);
            Debug.Log("Changes");
        }
        else
        {
            Debug.Log("No Change");
        }
        //else dont change is on green
        
        return offDire;
    }

/*     public void Activate_SaveEverything()
    {
        if(saveShoot &&  target_handler.currentSelection!=null)
        {
            target_handler.currentSelection.GetComponent<BoxCollider>().isTrigger=false;
        }
    }

    public void Disactivate_SaveEverything()
    {
        if(saveShoot &&  target_handler.currentSelection!=null)
        {
            target_handler.currentSelection.GetComponent<BoxCollider>().isTrigger=true;
        }
    } */

    public void goalKeeperSave_AI()
    {
        goalkeep_Handler.ChooseSide(target_handler.whatSide(),target_handler.whatSideLeftRight());
    }

    public void goalKeeperSave_AI_Step()
    {
        goalkeep_Handler.StepChooseSide(target_handler.whatSide());
    }

    public void addGoal()
    {
        GoalMade=true;        
        goal_Counter =goal_Counter+1;
        scoreCounter = scoreCounter + multiplyScore*BasicScore+AdditionalScore;
        ScoreCounter.text = scoreCounter.ToString();

        CanvasHandler.instance.animationPoints();

    }

    public void addGoal_Bonus(int value)
    {
        scoreCounter = scoreCounter + value;
        ScoreCounter.text = scoreCounter.ToString();

        CanvasHandler.instance.animationPoints();

    }

    private IEnumerator CountdownCoroutine(float startTime,bool applyDelay)
    {
        if(applyDelay)
        {
            yield return new WaitForSeconds(1);
        }
        float timer = startTime;

        // Loop until the timer reaches zero
        while (timer > 0)
        {
            if(!pauseTimer)
            {
                // Update the countdown text
                var timerText=(int)timer;
                countdownText.text = timerText.ToString(); // Displaying one decimal place

                

                // Decrease the timer by 0.1 each time (you can adjust this to your needs)
                timer -= 0.1f;
            }

            // Wait for a short period before updating again
            yield return new WaitForSeconds(0.1f); 
            
        }

        // Ensure the countdown reaches zero exactly and display "0"
        countdownText.text = "0";
        
        // Optionally, you can add a callback or trigger an event when the countdown is complete
        Debug.Log("Countdown finished!");

        execute_Shoot(false,0);
    }

    /*
    IEnumerator SceneChanger()
    {
        Debug.Log("Start Shooting Coroutine");
        
        //before shooting
        yield return new WaitForSeconds(TimerPerScene);

        //scene changer coroutine
        //Reset Scene
        Disactivate_SaveEverything();
        ballSpawner.SpawnBall();
        DisActivate_Stopper();
        //continue shooting
        Player_Handler.resetControlers();

        Debug.Log("Reset Shooting  Coroutine");
        target_handler.resetToOriginal();
    }
    */

    public void SceneChanger()
    {
        AdditionalTargets.instance.dissactivateAll();
        PenaltyManager.instance.DecideGoalkeeperBehavior( ScoreController.instance.ShootCounter,20,GameManager.gameManager.goal_Counter);
        ScoreController.instance.HaveResult=false;
        //Debug.Log("Reset Scene");
        
        //before shooting
        //yield return new WaitForSeconds(TimerPerScene);

        //scene changer coroutine
        //Reset Scene
        goldenBall=false;

        PowerUpHandler.instnace.DisableGoldenBall();
        PowerUpHandler.instnace.DisactivateDoublePoints();
         PowerUpHandler.instnace.DisableFlyingBird();

        PenaltyKicker.GetComponent<Animator>().ResetTrigger("Defeat1");
        PenaltyKicker.GetComponent<Animator>().ResetTrigger("Defeat2");
        PenaltyKicker.GetComponent<Animator>().ResetTrigger("Victory");
        PenaltyKicker.GetComponent<Animator>().SetTrigger("Reset");

        resetCamera();        

        checkSave = false;
        checkGoal = false;

        PenaltyKicker.shooting=false;
        //Disactivate_SaveEverything();
        ballSpawner.SpawnBall();
        DisActivate_Stopper();
        //continue shooting
        Player_Handler.resetControlers();

        //Debug.Log("Reset Shooting  Coroutine");
        target_handler.resetToOriginal();

        CanvasHandler.instance.forceBarHandler.gameObject.SetActive(false);

        if(Player_Handler.is_Shooter_AI)
        {
            Player_Handler.Activate_AI();
        }

        if(SecondChance && !GoalMade)
        {            
            ScoreController.instance.substructOne();

        }
        GoalMade=false;
        StartCoroutine(waitBeforeActivate());
        
    }


    public IEnumerator waitBeforeActivate()
    {
        yield return new WaitForSeconds(1);
        PowerUpHandler.instnace.ResetPowerUp();
        Crowd.istance.UpdateState("Idle");


        switch(ScoreController.instance.ShootCounter)
                {
                    case 0:
                        
                        break;
                    case 1:
                        Player_Handler.horizontalSpeed=2.4f;
                        break;
                    case 2:
                        Player_Handler.horizontalSpeed=2.3f;
                        CanvasHandler.instance.ActivateHelper("TargetHit");
                        AdditionalTargets.instance.ActicateRandomTarget();
                        break;
                    case 3:
                        CanvasHandler.instance.ActivateHelper("ForceTutorial");
                        Player_Handler.ApplyForce=true;
                        Player_Handler.horizontalSpeed=2f;
                        break;
                    case 4:

                        break;
                    case 5:
                        Player_Handler.horizontalSpeed=1.9f;
                        AdditionalTargets.instance.ActicateRandomTarget();
                        CanvasHandler.instance.ActivateExplanation(DoublePoints);
                        break;
                    case 6:
                        Player_Handler.horizontalSpeed=1.8f;
                        AdditionalTargets.instance.ActicateRandomTarget();
                        CanvasHandler.instance.ActivateExplanation(Slowtime);
                        break;     
                    case 7:
                        AdditionalTargets.instance.ActicateRandomTarget();
                        break;   
                    case 8:
                        CanvasHandler.instance.ActivateExplanation(GoldenBall);
                        Player_Handler.horizontalSpeed=1.7f;
                        break;
                    case 9:
                        if(!round2ndChance9)
                        {
                            round2ndChance9=true;
                            AdditionalTargets.instance.ActicateRandomTarget();
                            CanvasHandler.instance.ActivateExplanation(SecondChange);
                        }                        
                        break;     
                    case 10:
                        Player_Handler.horizontalSpeed=1.6f;
                        break; 
                    case 11:
                      
                        break;
                    case 12:
                        CanvasHandler.instance.ActivateExplanation(FlyingBird);
                        Player_Handler.horizontalSpeed=1.5f;
                        break;
                    case 13:
                        
                        break;
                    case 14:
                        CanvasHandler.instance.showPowerUpAdd(GoldenBall);
                        AdditionalTargets.instance.ActicateRandomTarget();
                        PowerUpHandler.instnace.AddPowerUp(GoldenBall.name);
                        break;
                    case 15:
                        Player_Handler.horizontalSpeed=1.45f;
                        CanvasHandler.instance.showPowerUpAdd(Slowtime);
                        PowerUpHandler.instnace.AddPowerUp(Slowtime.name);
                        break;
                    case 16:
                        AdditionalTargets.instance.ActicateRandomTarget();
                        break;     
                    case 17:
                        if(!round2ndChance17)
                        {
                            round2ndChance17=true;
                            CanvasHandler.instance.showPowerUpAdd(SecondChange);
                            PowerUpHandler.instnace.AddPowerUp(SecondChange.name);
                        }
                        
                        break;   
                    case 18:
                        AdditionalTargets.instance.ActicateRandomTarget();
                        PowerUpHandler.instnace.AddPowerUp(DoublePoints.name);
                        break;
                    case 19:  

                        break;               
                                
                }

        //method to add new things each turn.
        if(ScoreController.instance.ShootCounter>=15)
        {
            foreach(Transform fire in fireWorks)
            {
                fire.gameObject.SetActive(true);
            }
            mainSound.volume=mainSound.volume*0.5f;
            tibanaAudioSource.gameObject.SetActive(true);
            
        }
    }
}
