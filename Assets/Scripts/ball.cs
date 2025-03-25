using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float force;
    public Rigidbody rig;

    private Vector3 origTrans;

    public Transform spawnerParent;

    public float DespawnTime =5f;

    public float opposite_force = 100f;

    bool applyForceOnce=true;

    public bool catchBall;
    public bool Scored;

    // Start is called before the first frame update
    void Start()
    {
        spawnerParent =transform.parent;
        rig= GetComponent<Rigidbody>();
        origTrans = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootForce_Forward()
    {
        rig.AddForce(Vector3.forward * force,ForceMode.Impulse);
    }

    public void ShootForce_Direction(Transform target,Vector3 OffPositionXY)
    {
        transform.SetParent(null);
        Vector3 Shoot = ((target.position+OffPositionXY) - this.transform.position).normalized;
        rig.AddForce(Shoot * force + new Vector3(0,2f,0),ForceMode.Impulse);
        StartCoroutine(despawnAfterTime(DespawnTime));
    }

    public void ShootWithCurl(Transform target, Vector3 offPositionXY, Vector3 curlOffset, float curlForceMultiplier)
    {
        transform.SetParent(null);

        // Step 1: Define positions
        Vector3 startPosition = this.transform.position;
        Vector3 curlPosition = startPosition + curlOffset; // Add an offset for the curl position
        Vector3 targetPosition = target.position + offPositionXY;

        // Step 2: Apply force to curl position
        Vector3 directionToCurl = (curlPosition - startPosition).normalized;
        rig.AddForce(directionToCurl * (force * curlForceMultiplier), ForceMode.Impulse);

        // Step 3: Wait for the ball to reach or pass near the curl position
        StartCoroutine(ProceedToTarget(curlPosition, targetPosition));
    }

    private IEnumerator ProceedToTarget(Vector3 curlPosition, Vector3 targetPosition)
    {
        // Wait for ball to pass the curl position
        while (Vector3.Distance(this.transform.position, curlPosition) > 0.5f)
        {
            yield return null; // Wait for the next frame
        }

        // Step 4: Apply force toward the target position
        Vector3 directionToTarget = (targetPosition - this.transform.position).normalized;
        rig.AddForce(directionToTarget * force, ForceMode.Impulse);
    }


    public void ballWillBeCatched()
    {
        catchBall=true;
        rig.useGravity=false;
    }


    //Cause
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Test");
        if(other.gameObject.CompareTag("GoalPost") && Scored==false)
        {
            if(GameManager.gameManager.goldenBall)
            {
                PowerUpHandler.instnace.particleCollisionPassThrought.transform.SetParent(null);
                PowerUpHandler.instnace.particleCollisionPassThrought.gameObject.SetActive(true);
                PowerUpHandler.instnace.particleBall_smoke.gameObject.SetActive(false);                
            }
            Scored = true;
            GameManager.gameManager.isPenaltyScored();
            //Debug.Log("Goaaal");
            GameManager.gameManager.addGoal();
            //Debug.Break();
            StartCoroutine(despawnAfterTime(2.0f));
        }
        else if(other.gameObject.CompareTag("OutPost"))
        {
            Debug.Log("OutTrigger" + other.gameObject.name);
            GameManager.gameManager.isPenaltyOutOrDokari();
        }

    }

    
    //wait 3 seconds to check if its goal
    IEnumerator waitForGoal()
    {
        Debug.Log("waiting results");
        yield return new WaitForSeconds(3);
        if(!Scored)
        {
            GameManager.gameManager.isPenaltyOutOrDokari();
        }
        Debug.Log("are results");
    }

    private void OnCollisionEnter(Collision other) {
        //|| other.gameObject.name == "mixamorig:LeftHand" || other.gameObject.name == "mixamorig:RightHand"
        if(other.gameObject.name == "mixamorig:Hips")
        {
            //
            //Debug.Break();
            if(!catchBall)
            {
                //Activate line stopper
                GameManager.gameManager.activate_Stopper();
                //Debug.Log("Collide with Goalkeeper");
                
                if(applyForceOnce)
                {
                    //var opposite = -rig.velocity;
                    //var opposite = other.contacts[0].point - transform.position;
                    var opposite = other.transform.position - transform.position;
                    opposite = - opposite.normalized;

                    //Force on right
                    var vector = Quaternion.Euler(0, 0, 45) * (-Vector3.forward);
                    var brakeForce = vector * opposite_force ;

                    //force based on opposite force
                    var oriOpos = opposite;
                    opposite = opposite + (-Vector3.forward);                
                    //opposite=Quaternion.Euler(90, 0, 0) *opposite;
                    brakeForce = opposite * opposite_force ;

                    //Debug.DrawRay(transform.position, oriOpos,Color.red);
                    //Debug.DrawRay(transform.position, -Vector3.forward,Color.yellow);
                    //Debug.DrawRay(transform.position, opposite,Color.blue);
                    
                    //Debug.Break();

                    rig.AddForce(brakeForce,ForceMode.VelocityChange);
                    applyForceOnce=false;
                }
            }
            else
            {
                //Debug.Break();
                 rig.velocity = Vector3.zero;
                 rig.angularVelocity = Vector3.zero;
                 //rig.position = Vector3.zero;
                 var hugTrans= other.gameObject.transform.GetChild(0); 
                 

                 transform.SetParent(hugTrans);
                 transform.localPosition = Vector3.zero;
                 
            }

            GameManager.gameManager.isPenaltySaved();
        }
        else
        {
            if(other.gameObject.CompareTag("Dokari"))
            {
                    Debug.Log("Dokari");
                    StartCoroutine(waitForGoal());
            }
        }
        
        
    }    
    
    
    IEnumerator despawnAfterTime(float DespawnTime)
    {
        yield return new WaitForSeconds(DespawnTime);
        DespawnBall();
    }
    public void DespawnBall()
    {
        catchBall=false;
        Scored=false;
        rig.useGravity=true;
        transform.SetParent(null);


        applyForceOnce=false;
        //remove force
        rig.velocity = Vector3.zero;
        rig.angularVelocity = Vector3.zero; 
        transform.position=origTrans;

        
        transform.SetParent(spawnerParent);
        
        
        gameObject.SetActive(false);

    }
}
