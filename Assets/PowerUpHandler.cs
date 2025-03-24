using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpHandler : MonoBehaviour
{
    public List<PowerUpItemHandler> PowerUps;

    public List<PowerUp> ListOfPowerUps;

    public int currentUnlocked=0;
    // Start is called before the first frame update

    private void Awake() {
        ShuffleAndAssignPowerUps();
            }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnLockNextOne()
    {
        PowerUps[currentUnlocked].UnLock();
    }

     public void ShuffleAndAssignPowerUps()
    {
        if (PowerUps.Count != ListOfPowerUps.Count)
        {
            Debug.LogError("PowerUps and ListOfPowerUps must be the same size!");
            return;
        }

        // Create a copy of the list to shuffle
        List<PowerUp> shuffledList = new List<PowerUp>(ListOfPowerUps);

        // Fisher–Yates shuffle
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            PowerUp temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }

        // Assign shuffled data
        for (int i = 0; i < PowerUps.Count; i++)
        {
            PowerUps[i].scriptableData = shuffledList[i];
        }
    }

    

}
