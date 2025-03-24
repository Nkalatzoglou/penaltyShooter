using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHandler : MonoBehaviour
{
    public List<GameObject> PowerUps;

    public int currentUnlocked=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnLockNextOne()
    {
        PowerUps[currentUnlocked].GetComponent<Button>().enabled=true;
        PowerUps[currentUnlocked].transform.GetChild(0).gameObject.SetActive(true);
    }

}
