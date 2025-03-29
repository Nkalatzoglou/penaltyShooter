using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpItemHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public PowerUp scriptableData;

    public int amount=0;

    public TextMeshProUGUI amountText;

    void Start()
    {
        amountText=transform.Find("Amount").GetChild(0).GetComponent<TextMeshProUGUI>();
        amountText.text=amount.ToString();
        if(scriptableData.powerUpImage!=null)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = scriptableData.powerUpImage; 
        }       

        this.transform.name=scriptableData.powerUpName;        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void UpdateTextAmount()
    {
        amountText.text=amount.ToString();
    }

    public void UnLock()
    {
        this.gameObject.GetComponent<Button>().enabled=true;


        transform.GetChild(0).gameObject.SetActive(true);
    }
}
