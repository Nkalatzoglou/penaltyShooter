using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TargetScoreHandler : MonoBehaviour
{
    public int BonusScore=20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))
        {
            StartCoroutine(waitoDisactivate());
            puffEffect.instance.instantiateOnPos(other.gameObject.transform.position);
            GameManager.gameManager.addGoal_Bonus(BonusScore);
        }
    }

    IEnumerator waitoDisactivate()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
