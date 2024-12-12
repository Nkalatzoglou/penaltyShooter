using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_Handler : MonoBehaviour
{
    public LayerMask PostHelpers;
    public GameObject currentSelection;
    public Vector3 currentPos;

    public Color TargetColor;

    public Color origColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var fromPosition = new Vector3 (transform.position.x,transform.position.y,transform.position.z+5);
        var toPosition = transform.position;

        var direction = toPosition-fromPosition;

        RaycastHit hit;

        if(Physics.Raycast(fromPosition,direction,out hit,7f,PostHelpers))
        {
            currentSelection =hit.collider.gameObject;
            currentPos=hit.point;
        }
        else
        {
            currentSelection =null;
        }
    }


    public void markSide()
    {
        if(currentSelection!=null)
        {
            origColor= currentSelection.GetComponent<MeshRenderer>().material.color;
            currentSelection.GetComponent<MeshRenderer>().material.color=TargetColor;
        }

    }

    public void resetToOriginal()
    {
        if(currentSelection!=null)
        {
            currentSelection.GetComponent<MeshRenderer>().material.color=origColor;
        }

    }

    
    public string whatSide()
    {        
        if(currentSelection!=null)
        {
            return currentSelection.name;
        }
        return "NoTarget";

    }

    public string whatSideLeftRight()
    {
        if(currentSelection!=null)
        {
            return currentSelection.transform.parent.name;
        }
        return "NoTarget";
    }



    /*

    private void OntriggerEnter(Collider other) {
        Debug.Log("NO helpers : " + other.gameObject.name);
        if(other.gameObject.CompareTag("PostHelpers"))
        {
            Debug.Log("Post helper Name : " + other.gameObject.name);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("NO helpers 2: " + other.gameObject.name);
        if(other.gameObject.CompareTag("PostHelpers"))
        {
            Debug.Log("Post helper Name : " + other.gameObject.name);
        }

    }
    */
}
