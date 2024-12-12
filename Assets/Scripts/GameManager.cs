using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Keep Scores - UI")]
    public int goal_Counter;
    public TextMeshProUGUI ScoreCounter;
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
    
    public bool LooseEverything;

    public Transform MidMidHug;

    [Range(0, 2)]
    public float gamespeed=1;


    public bool checkSave;
    public bool checkGoal;

    public Camera mainCam;
    public bool StartCameraMov;
    static float t = 0.0f;

    Vector3 cameraOrigPos;
    float fieldOfView;

    public bool GoalKeeperFinished;
    public bool PenaltyKickerFinished;

    public Transition_Handler trans_Handler;



    //public bool canshoot;
    // Start is called before the first frame update
    private void Awake() {
        gameManager=this;
        mainCam = Camera.main;

        cameraOrigPos= mainCam.transform.position;
        fieldOfView=mainCam.fieldOfView;
    }
    void Start()
    {

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

    public void resetCamera()
    {

        mainCam.transform.position = cameraOrigPos;
        mainCam.fieldOfView = fieldOfView;
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
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.transform.position = targetPosition;
    }

    IEnumerator LerpCmeraZoom(float endValue, float duration)
    {
        float time = 0;
        float startValue = mainCam.fieldOfView;
        while (time < duration)
        {
            mainCam.fieldOfView = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCam.fieldOfView = endValue;
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

    }

    public void isPenaltyScored()
    {
        checkSave = true;
        PenaltyKicker.GetComponent<Animator>().SetTrigger("Victory");
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
    }
    // Update is called once per frame
    void Update()
    {
        
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

    public void execute_Shoot()
    {
        
        if(!PenaltyKicker.shooting)
        {
            target_handler.markSide();
            PenaltyKicker.startShootBallAnimation();
            goalKeeperSave_AI_Step();
            
            //StartCoroutine(SceneChanger());

        }
        GoalKeeperFinished=false;
        PenaltyKickerFinished=false;
        StartCoroutine(ShootCoroutine());


    }

    public void Activate_SaveEverything()
    {
        if(saveEverything &&  target_handler.currentSelection!=null)
        {
            target_handler.currentSelection.GetComponent<BoxCollider>().isTrigger=false;
        }
    }

    public void Disactivate_SaveEverything()
    {
        if(saveEverything &&  target_handler.currentSelection!=null)
        {
            target_handler.currentSelection.GetComponent<BoxCollider>().isTrigger=true;
        }
    }

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
        goal_Counter =goal_Counter+1;
        ScoreCounter.text = goal_Counter.ToString();

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
        //Debug.Log("Reset Scene");
        
        //before shooting
        //yield return new WaitForSeconds(TimerPerScene);

        //scene changer coroutine
        //Reset Scene
        PenaltyKicker.GetComponent<Animator>().ResetTrigger("Defeat1");
        PenaltyKicker.GetComponent<Animator>().ResetTrigger("Defeat2");
        PenaltyKicker.GetComponent<Animator>().ResetTrigger("Victory");
        PenaltyKicker.GetComponent<Animator>().SetTrigger("Reset");

        resetCamera();

        

        checkSave = false;
        checkGoal = false;

        PenaltyKicker.shooting=false;
        Disactivate_SaveEverything();
        ballSpawner.SpawnBall();
        DisActivate_Stopper();
        //continue shooting
        Player_Handler.resetControlers();

        //Debug.Log("Reset Shooting  Coroutine");
        target_handler.resetToOriginal();

        if(Player_Handler.is_Shooter_AI)
        {
            Player_Handler.Activate_AI();
        }
    }
}
