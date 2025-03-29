using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutUiHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPowerUps()
    {
        PowerUpHandler.instnace.ResetPowerUp();
    }
}
