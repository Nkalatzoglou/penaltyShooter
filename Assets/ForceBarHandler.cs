using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForceBarHandler : MonoBehaviour
{
    public Transform movingTransform; // The Transform that will move
    public float speed = 1.0f;        // Speed of movement

    public float currentValue = 0.0f; // Tracks the float value between -1 and 1
    private bool isStopped = false;   // Whether the movement is stopped
    private Vector3 direction;        // Direction of movement

    private const float minX = -155; // X position corresponding to -1
    private const float maxX = 155;  // X position corresponding to +1

    void Start()
    {
        // Set the initial direction to move towards the positive side        

        direction = Vector3.right; // Move towards +X
    }

    public void ResetPositions()
    {
        movingTransform.localPosition = new Vector3(0,movingTransform.localPosition.y,movingTransform.localPosition.z);
        currentValue=0.0f;
    }

    public float stopBar()
    {
        var force=currentValue;
        isStopped=true;
        return force;
    }

    public void StartBar()
    {
        ResetPositions();
        isStopped=false;
    }
        

    void Update()
    {
        if (!isStopped && movingTransform != null)
        {
            // Move the movingTransform in the current direction
            movingTransform.localPosition += direction * speed * Time.deltaTime;

            // Calculate the currentValue based on the X position
            float currentX = movingTransform.localPosition.x;
            currentValue = Mathf.Clamp(currentX / maxX, -1.0f, 1.0f);

            // Check if the movingTransform has reached the limits and reverse direction
            if (currentX >= maxX)
            {
                movingTransform.localPosition = new Vector3(maxX, movingTransform.localPosition.y, movingTransform.localPosition.z);
                direction = Vector3.left; // Reverse direction to move towards -X
            }
            else if (currentX <= minX)
            {
                movingTransform.localPosition = new Vector3(minX, movingTransform.localPosition.y, movingTransform.localPosition.z);
                direction = Vector3.right; // Reverse direction to move towards +X
            }
        }
    }

    // Call this method to stop the movement
    public void StopMovement()
    {
        isStopped = true;
    }

    // Call this method to start/resume the movement
    public void StartMovement()
    {
        isStopped = false;
    }

    // Get the current float value (-1 to 1)
    public float GetCurrentValue()
    {
        return currentValue;
    }
}
