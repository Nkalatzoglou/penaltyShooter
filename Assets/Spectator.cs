using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    private Crowd crowd;

    private float angle;
    private float startingYPosition;
    private float yOffset;
    private float randomSpeed;

    public Transform skinColor;

    public List<Material> skiMat;
    

    private void Start()
    {
        crowd = Crowd.istance;

        var skincolor= Random.Range(0,3);
        Material pickMat = skiMat[skincolor];
        foreach(Transform child in skinColor)
        {
            child.GetComponent<Renderer>().material=pickMat;
        }

        

        startingYPosition = transform.position.y;
        randomSpeed = Random.Range(crowd.defaultSpeed - crowd.randomnessFactor,
                                   crowd.defaultSpeed + crowd.randomnessFactor);

        ChooseRandomColor();
    }

    private void FixedUpdate()
    {
        yOffset = startingYPosition + crowd.maximumHeight;
        angle += crowd.currentSpeedFactor * 0.1f * randomSpeed;

        Vector3 newPos = new Vector3(transform.position.x,
            yOffset + Mathf.Sin(angle) * crowd.maximumHeight,
            transform.position.z);

        transform.position = newPos;
    }

    private void ChooseRandomColor()
    {
        Material newMaterial = transform.GetChild(0).GetComponent<Renderer>().material;
        newMaterial.color = new Color(Random.Range(0, 256) / 255f,
                                        Random.Range(0, 256) / 255f,
                                        Random.Range(0, 256) / 255f);
        
        Renderer renderer = transform.Find("Clothes").GetComponent<Renderer>();          
        renderer.material = newMaterial;
        
        
    }
}
