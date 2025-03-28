using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puffEffect : MonoBehaviour
{
    public static puffEffect instance;
    public GameObject prefabEffect;

    public GameObject currentEffect;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        instance=this;
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instantiateOnPos(Vector3 position)
    {
        if(currentEffect == null)
        {
            currentEffect = Instantiate(prefabEffect, position, Quaternion.identity);
        }
        else
        {
            currentEffect.transform.position = position;
        }    
        currentEffect.GetComponent<AudioSource>().pitch = Mathf.Round(Random.Range(0.7f, 1.1f) * 10f) / 10f;
        currentEffect.gameObject.SetActive(true);
    }

}
