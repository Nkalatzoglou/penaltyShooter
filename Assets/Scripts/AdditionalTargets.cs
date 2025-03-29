using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTargets : MonoBehaviour
{
    public static AdditionalTargets instance;
    public List<Transform> TotalTargets;

    void Awake()
    {
        instance=this;
    }

    public void ActicateRandomTarget()
    {
        GetRandomTarget().gameObject.SetActive(true);
    }

    public void dissactivateAll()
    {
        foreach(Transform target in TotalTargets)
        {
            target.gameObject.SetActive(false);
        }
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private Transform GetRandomTarget()
    {
        int index = Random.Range(0, TotalTargets.Count); // upper bound is exclusive
        return TotalTargets[index];
    }
}
