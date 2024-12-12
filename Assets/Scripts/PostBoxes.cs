using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostBoxes : MonoBehaviour
{
    Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        bounds=GetComponent<MeshRenderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getRandomPoint()
    {
        Vector3 randomPoint = Vector3.zero;

        float minX = bounds.size.x * -0.5f;
        float minY = bounds.size.y * -0.5f;
        float minZ = bounds.size.z * -0.5f;

    return (Vector3)this.gameObject.transform.TransformPoint(
        new Vector3(Random.Range (minX, -minX),
            Random.Range (minY, -minY),
            Random.Range (minZ, -minZ))
    );
    }
}
