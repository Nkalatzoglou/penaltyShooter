using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPointOnMesh : MonoBehaviour
{
    public MeshCollider lookupCollider;

    public bool bangGetPoint;
    private Vector3 randomPoint;

    public List<Vector3> debugPoints; 
    
    private void Start() 
    {

    }
	void Update () {

        //click the checkbox to generate a point, and have it shown in a debug gizmo.
        //here's a blogpost on it http://nottheinternet.com/blog/banging-things-in-Unity/
	    if (bangGetPoint)
	    {
            var point = GetRandomPointInsideCollider(transform.GetComponent<BoxCollider>());
            debugPoints.Add(point);
            Debug.Log(point);
            

            var test = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
            var scale = new  Vector3(0.1f,0.1f,0.1f);
            test.transform.localScale = scale;
            test.transform.position = point;
            bangGetPoint = false;
            /*
	        Vector3 randomPoint = GetRandomPointOnMesh(lookupCollider.sharedMesh);
	        randomPoint += lookupCollider.transform.position;
	        debugPoints.Add(randomPoint);
            Debug.Log(randomPoint);
	        bangGetPoint = false;
            */
	    }
	} 
 
 

    public void OnDrawGizmos()
    {
        foreach (Vector3 debugPoint in debugPoints)
        {
            Gizmos.DrawSphere(debugPoint, 1f);
        }
    }

    
    public Vector3 GetRandomPointInsideCollider( BoxCollider boxCollider )
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(Random.Range( -extents.x, extents.x ),Random.Range( -extents.y, extents.y ),Random.Range( -extents.z, extents.z ));
    
        return boxCollider.transform.TransformPoint( point );
    } 


}