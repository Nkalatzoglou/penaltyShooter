using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    public static CanvasHandler instance;
    // Start is called before the first frame update
    public ForceBarHandler forceBarHandler;

    public Transform PowerUpIndicator;
    public Animator PowerUpAnimator;
    public TextMeshProUGUI PowerUpText;
    public Image PowerUpImage;

    public Transform PowerUpExplanation;
    public TextMeshProUGUI PowerUpExplnationName;
    public TextMeshProUGUI PowerUpExplnationDesription;
    public Image PowerUpSecondaryImage;


    private void Awake() {
        instance=this;
    }
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateExplanation(PowerUp currentSelected)
    {
        PowerUpExplanation.gameObject.SetActive(true);
        SetExplanation(currentSelected);
    }
        

    public void SetExplanation(PowerUp currentSelected)
    {
        PowerUpExplnationName.text=currentSelected.name;
        PowerUpExplnationDesription.text=currentSelected.description;
        PowerUpSecondaryImage.sprite=currentSelected.powerUpImage;        
    }

    public void ActivateIndicator(PowerUp currentSelected)
    {
        PowerUpIndicator.gameObject.SetActive(true);
        SetIndicator(currentSelected);
    }

    public void SetIndicator(PowerUp currentSelected)
    {
        PowerUpText.text=currentSelected.name;
        PowerUpImage.sprite=currentSelected.powerUpImage;
        PowerUpIndicator.GetComponent<Animator>().SetTrigger("Show");
        PowerUpIndicator.GetComponent<AudioSource>().Play();

        GameManager.gameManager.hideIndicator=true;
    }

    public void ActivateForceBar()
    {
        forceBarHandler.StartBar();
    }

    public float StopBar()
    {
        return forceBarHandler.stopBar();
    }
}
