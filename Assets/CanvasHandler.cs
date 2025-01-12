using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public static CanvasHandler instance;
    // Start is called before the first frame update
    public ForceBarHandler forceBarHandler;

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

    public void ActivateForceBar()
    {
        forceBarHandler.StartBar();
    }

    public float StopBar()
    {
        return forceBarHandler.stopBar();
    }
}
