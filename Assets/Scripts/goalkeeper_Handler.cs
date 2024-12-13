using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalkeeper_Handler : MonoBehaviour
{
    public GameManager gamemanger;
    [Header("Choose Dive Position")]
    //public GameObject LowDownRight;
    //public GameObject LargeDownRight;

    public Animator goalKeepAnimator;

    public List<string> AnimationNames;

    public BoxCollider GoalKeeperCollider;

    //public float CapsuleRadius;
    public Vector3 Collidersize;
    public Vector3 CapsuleCenter;

    [Header("CollisionActivasio")]
    public BoxCollider LargeTopHand1;
    public BoxCollider LargeTopHand2;
    public string Side;

    // Start is called before the first frame update

    public void Activate_OneHandCollider()
    {
        if(Side=="Left")
        {
            LargeTopHand2.enabled=true;
            LargeTopHand1.enabled=false;
        }
        else if( Side=="Right")
        {
            LargeTopHand1.enabled=true;
            LargeTopHand1.enabled=false;
        }
    }
    public void Activate_LargeTopColliders()
    {
        LargeTopHand1.enabled=true;
        LargeTopHand2.enabled=true;
    }

    public void Diactivate_LargeTopColliders()
    {
        LargeTopHand1.enabled=false;
        LargeTopHand2.enabled=false;
        //activateOneHand=false;
    }
    void Start()
    {
        gamemanger = GameManager.gameManager;

        GoalKeeperCollider = transform.Find("mixamorig:Hips").GetComponent<BoxCollider>();

        if(GoalKeeperCollider!=null)
        {
            //CapsuleRadius=GoalKeeperCollider.radius;
            Collidersize=GoalKeeperCollider.size;
            CapsuleCenter=GoalKeeperCollider.center;
        } 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishedAnimation()
    {
        gamemanger.GoalKeeperFinished=true;
    }
    
    public void ChooseSide(string PostHelper,string LeftOrRight)
    {
        //Double Animation 
         Side=LeftOrRight;
         
        if(PostHelper =="LargeDown")
        {
            //Here we have two animation
            //LargeDown and LargeDown2
            float fliCoin= Random.Range(0,100);
            if(fliCoin>50)
            {
                PostHelper=PostHelper+"2";

                //Reverse since animation is reversed
                if(LeftOrRight == "Left")
                {
                    LeftOrRight="Right";
                    
                }
                else
                {
                    LeftOrRight = "Left";
                }   
               
            }
            
        }

        //GameManager.gameManager.Activate_SaveEverything();

        if(PostHelper!="NoTarget")
        {
            if(GoalKeeperCollider!=null)
            {
                GoalKeeperCollider.enabled=true;
            }

            //Save everything
            if(!GameManager.gameManager.LooseEverything)
            {
                Debug.Log("Save");
                if(LeftOrRight == "Left")
                {
                    //Debug.Log("Left");
                    var x = goalKeepAnimator.transform.localScale.x;

                    
                    if(x>0)
                    {
                        goalKeepAnimator.transform.localScale = new Vector3(-x,goalKeepAnimator.transform.localScale.y,goalKeepAnimator.transform.localScale.z);
                    }


                }
                else
                {
                    //Debug.Log("Right");
                    var x = goalKeepAnimator.transform.localScale.x;

                    
                    if(x<0)
                    {
                        goalKeepAnimator.transform.localScale = new Vector3(-x,goalKeepAnimator.transform.localScale.y,goalKeepAnimator.transform.localScale.z);
                    }
                    
                }
                goalKeepAnimator.SetTrigger(PostHelper);

            }
            //LooseEveryting Dive on Other Side
            else
            {
                Debug.Log("Loose");
                if(GoalKeeperCollider!=null)
                {
                    GoalKeeperCollider.enabled=false;
                }

                var flipCoin4Dir = Random.Range(0,100);

                var x = goalKeepAnimator.transform.localScale.x;

                if(flipCoin4Dir>50)
                {
                    goalKeepAnimator.transform.localScale = new Vector3(-x,goalKeepAnimator.transform.localScale.y,goalKeepAnimator.transform.localScale.z);
                }
                else
                {
                    goalKeepAnimator.transform.localScale = new Vector3(x,goalKeepAnimator.transform.localScale.y,goalKeepAnimator.transform.localScale.z);
                }
                
                AnimationNames.Remove(PostHelper);

                var randomAnim = Random.Range((int)0,(int)AnimationNames.Count);

                //Debug.Log("Dive Wrong" + randomAnim);

                goalKeepAnimator.SetTrigger(AnimationNames[randomAnim]);
                //Debug.Log("Dive Wrong" + AnimationNames[randomAnim]);
                AnimationNames.Add(PostHelper);
            }
                        
        }
        else
        //No target jump random
        {
            Debug.Log("Loose");
                if(GoalKeeperCollider!=null)
                {
                    GoalKeeperCollider.enabled=false;
                }

                var flipCoin4Dir = Random.Range(0,100);

                var x = goalKeepAnimator.transform.localScale.x;

                if(flipCoin4Dir>50)
                {
                    goalKeepAnimator.transform.localScale = new Vector3(-x,goalKeepAnimator.transform.localScale.y,goalKeepAnimator.transform.localScale.z);
                }
                else
                {
                    goalKeepAnimator.transform.localScale = new Vector3(x,goalKeepAnimator.transform.localScale.y,goalKeepAnimator.transform.localScale.z);
                }
                
                AnimationNames.Remove(PostHelper);

                var randomAnim = Random.Range((int)0,(int)AnimationNames.Count);

                //Debug.Log("Dive Wrong" + randomAnim);

                goalKeepAnimator.SetTrigger(AnimationNames[randomAnim]);
                //Debug.Log("Dive Wrong" + AnimationNames[randomAnim]);
                AnimationNames.Add(PostHelper);
        }
        
        /*
        switch (PostHelper)
        {            
            case "LowDownRight":
                goalKeepAnimator.SetTrigger("LowDownRight");
                break;
            case "LowDownLeft":
                goalKeepAnimator.SetTrigger("LowDownLeft");
                break;
            case "LargeDownRight":
                goalKeepAnimator.SetTrigger("LargeDownRight");
                break;
            case "LargeMidRight":
                goalKeepAnimator.SetTrigger("LargeMidRight");
                break;

        }
        */
    }

    public void StepChooseSide(string PostHelper)
    {
        switch (PostHelper)
        {            
            //case "LowDownRight":
                //goalKeepAnimator.SetTrigger("LowDownRight");
                //break;
            //case "LowDownLeft":
                //goalKeepAnimator.SetTrigger("LowDownLeft");
                //break;
            //case "LargeDownRight":
                //StartCoroutine(waitTime(0.5f,"LargeDownRight"));
                //break;

        }
    }


    IEnumerator waitTime(float timer,string animaname)
    {
        yield return new WaitForSeconds(timer);
        goalKeepAnimator.SetTrigger(animaname);
    }

}
