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

    public Animator pointAnimator;

    public Transform TargetTutorial;
    public Transform TargetHit;
    public Transform ForceTutorial;
    public Transform AddPowerUp;
    public Image AddPowerUp_Image;

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

    public void animationPoints()
    {
        pointAnimator.enabled=true;
        
        pointAnimator.Play("ScorePopUp", 0, 0f);
    }

    public void showPowerUpAdd(PowerUp item)
    {
        AddPowerUp_Image.sprite=item.powerUpImage;
        AddPowerUp.gameObject.SetActive(true);
         StartCoroutine(DisableExaplnationFast(AddPowerUp));
    }


    public void ActivateHelper(string helperName)
    {
        if(!GameManager.gameManager.DisActivateHelpers)
        {
            Transform UIObject=null;
            if(helperName=="TargetTutorial")
            {
                UIObject=TargetTutorial;
            }
            else if(helperName=="TargetHit")
            {
                UIObject=TargetHit;
            }
            else if(helperName=="ForceTutorial")
            {
                UIObject=ForceTutorial;
            }        
            

            UIObject.gameObject.SetActive(true);
            StartCoroutine(DisableExaplnation(UIObject));
        }
        
    }

    public IEnumerator DisableExaplnationFast(Transform UIObject)
    {
        GameManager.gameManager.pauseTimer=true;
        yield return new WaitForSeconds(3);
        GameManager.gameManager.pauseTimer=false; 
        UIObject.gameObject.SetActive(false);     
    }

    public IEnumerator DisableExaplnation(Transform UIObject)
    {
        GameManager.gameManager.pauseTimer=true;
        yield return new WaitForSeconds(9);
        GameManager.gameManager.pauseTimer=false; 
        UIObject.gameObject.SetActive(false);     
    }

    public void ActivateExplanation(PowerUp currentSelected)
    {
        if(!GameManager.gameManager.DisActivateHelpers)
        {
            PowerUpExplanation.gameObject.SetActive(true);
            SetExplanation(currentSelected);
        }
        else
        {
            PowerUpHandler.instnace.AddPowerUp(currentSelected.name);
        }
        
    }
        

    public void SetExplanation(PowerUp currentSelected)
    {
        PowerUpExplnationName.text=currentSelected.name;
        PowerUpExplnationDesription.text=currentSelected.description;
        PowerUpSecondaryImage.sprite=currentSelected.powerUpImage;
        StartCoroutine(DisableExaplnation(currentSelected));     
    }

    public IEnumerator DisableExaplnation(PowerUp currentSelected)
    {
        GameManager.gameManager.pauseTimer=true;
        yield return new WaitForSeconds(9);
        PowerUpHandler.instnace.AddPowerUp(currentSelected.name);
        GameManager.gameManager.pauseTimer=false; 
        PowerUpExplanation.gameObject.SetActive(false);     
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
