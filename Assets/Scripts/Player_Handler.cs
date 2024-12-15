using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Handler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gamemanager;
    public Shooter_Handler shooter;
    public goalkeeper_Handler goalKeeper;

    public Transform selectedTarget;

    [Header("AI shooter")]
    public bool is_Shooter_AI;
    public float MinWaitHor=2.5f;
    public float MaxwaitHor=6f;
    public float MinWaitVert=1.5f;
    public float MaxwaitVert=4f;

    [Header("AI GoalKeeper")]
    public bool is_Goalkeeper_AI;

    public enum PlayerType
    {
        shooter,
        goalKeeper
    }

    public PlayerType currentType;

    public int ButtonPress=0;

    [Header("Ball Kicker")]
    public float horizontalSpeed=1.5f;
    public float VecticalTimeO=300f;


    //public IEnumerator firstKeyPress;
    //public IEnumerator SecondKeyPress;
    private Vector3 threshold_parent_origPos;
    private Vector3 target_origPos;

    //Bool to run AI once and reset to rerun when needed
    private bool AICor;
    
    void Start()
    {
        gamemanager=GameManager.gameManager;
        target_origPos = gamemanager.input_Target.transform.position;
        threshold_parent_origPos = gamemanager.XMaxLeft.parent.position;
        
        StartHorizonalPMovement();
    }

    public void StartHorizonalPMovement()
    {
        var startPos = gamemanager.XMaxRight;
        var endPos = gamemanager.XMaxLeft;



        //Always Start From Shoouter;
        StartCoroutine(Start_Movement(gamemanager.input_Target,startPos,endPos,horizontalSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if(gamemanager.CurrentStatus == GameManager.GameStatus.Runtime && TrackMouseArea.instance.isMouseOver)
        {
            if(currentType == PlayerType.shooter)
            {
                Update_Shooter();
            }
            else
            {
                Update_GoalKeeper();
            }
        }
        
        
    }

    public void Update_Shooter()
    {
        //if not AI
        if(!is_Shooter_AI)
        {
            if(Input.GetMouseButtonDown(0) && !shooter.shooting)
            {
                if(ButtonPress==0)
                {
                    //Debug.Log("First Button");
                    ButtonPress=+1;

                    var threshold = gamemanager.XMaxLeft.parent;
                    //Start Moving Upwards
                    var newYpos= new Vector3(threshold.position.x,gamemanager.YMaxTop.position.y,threshold.position.z);
                    StartCoroutine(MoveObject(threshold,threshold,newYpos,VecticalTimeO));
                }
            }
            if(Input.GetMouseButtonUp(0) && ButtonPress==1)
            {
                StopAllCoroutines();
                gamemanager.execute_Shoot();
            }
        }
        else
        {
            if(AICor==false)
            {
                //Debug.Log("Shooter is AI");
                var threshold = gamemanager.XMaxLeft.parent;
                //Start Moving Upwards
                var newYpos= new Vector3(threshold.position.x,gamemanager.YMaxTop.position.y,threshold.position.z);

                var coroutineAI = AIpickHorizontalPOS(threshold,threshold,newYpos,VecticalTimeO);
                StartCoroutine(coroutineAI);
                
            }
            

        }            

    }

    public void Update_GoalKeeper()
    {
        //Debug.Log("I am GoalKeeper");
    }

    IEnumerator Start_Movement(Transform thisTransform,Transform pointA, Transform pointB, float time)
     {
        //Debug.Log("Start MoveOBject");
         //var pointA = transform.position;
         while (true) {
             yield return StartCoroutine(MoveObject(thisTransform, pointA, pointB, time));
             yield return StartCoroutine(MoveObject(thisTransform, pointB, pointA, time));
         }
         
     }

    IEnumerator MoveObject(Transform thisTransform, Transform startPos, Transform endPos, float time)
    {        
        
        var i= 0.0f;
        var rate= 1.0f/time;
        while(i < 1.0f)
        {
            

            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos.position, endPos.position, i);
            yield return null;
        }
        //Debug.Log("End Rutine on");
    }

    IEnumerator AIpickHorizontalPOS(Transform thisTransform, Transform startPos, Vector3 endPos, float time)
    {
        AICor=true;
        var timeToWaitHor= Random.Range(MinWaitHor,MaxwaitHor);
        yield return new WaitForSeconds(timeToWaitHor);
        Debug.Log("Move!");
        var moveCor = MoveObject(thisTransform,startPos, endPos, time);
        StartCoroutine(moveCor);

        var timeToWaitVert= Random.Range(MinWaitVert,MaxwaitVert);
        yield return new WaitForSeconds(timeToWaitVert);

        StopCoroutine(moveCor);
        StopAllCoroutines();
        gamemanager.execute_Shoot();

    }
    IEnumerator MoveObject(Transform thisTransform, Transform startPos, Vector3 endPos, float time)
    {        
        
        var i= 0.0f;
        var rate= 1.0f/time;
        while(i < 1.0f)
        {
            

            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos.position, endPos, i);
            yield return null;
        }
        //Debug.Log("End Rutine on");
    }

    public void resetControlers()
    {
        gamemanager.XMaxLeft.parent.position = threshold_parent_origPos;
        gamemanager.input_Target.transform.position=target_origPos;
        ButtonPress=0;
        StartHorizonalPMovement();
    }

    public void Activate_AI()
    {
        //Run Coroutine again
        AICor=false;
    }

    public void Disactivate_AI()
    {
        //Coroutine already running
        AICor=true;
    }
}
