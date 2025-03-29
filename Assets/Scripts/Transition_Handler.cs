using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition_Handler : MonoBehaviour
{
    // Start is called before the first frame update
    public Image UIFadeOut;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScreen()
    {
        GameManager.gameManager.SceneChanger();
        GetComponent<Animator>().ResetTrigger("FadeOut");
        UIFadeOut.GetComponent<Animator>().ResetTrigger("FadeOut");

    }


    public void SecondChance()
    {
        if(GameManager.gameManager.SecondChance && !GameManager.gameManager.GoalMade)
        {
            PowerUpHandler.instnace.Activate2ndChance_Vol2();
        }
    }
}
