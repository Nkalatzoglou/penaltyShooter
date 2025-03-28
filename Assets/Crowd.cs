using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public static Crowd istance;
    [Range(0,1)] public float defaultSpeed;
    [Range(1,5)] public float cheeringSpeed;
    [Range(1,5)] public float randomnessFactor;

    public float maximumHeight;

    [HideInInspector] public float currentSpeedFactor;

    public AudioSource audioSource;
    public AudioClip cheerSound;

    private void Awake()
    {
        istance=this;
        currentSpeedFactor = defaultSpeed;
    }

    public void UpdateState(string state)
    {
        switch(state)
        {
            case "Idle":
                currentSpeedFactor = defaultSpeed;
                stopCrowd();
                // Set the speed to default value
                break;
            case "Cheer":
                currentSpeedFactor = cheeringSpeed;
                // Set the speed to cheering value
                // Here you can play anything, maybe a cheering sound, fireworks, animations ... Anything!
                audioSource.PlayOneShot(cheerSound);
                break;
        }
    }

    public void stopCrowd()
    {
        audioSource.Stop();
    }
}
