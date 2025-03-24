using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpItemHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public PowerUp scriptableData;

    void Start()
    {
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

    public void UnLock()
    {
        this.gameObject.GetComponent<Button>().enabled=true;


        transform.GetChild(0).gameObject.SetActive(true);
    }
}
