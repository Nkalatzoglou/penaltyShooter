using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public List<Image> UIScore;

    public static ScoreController instance;

    public int ShootCounter;
    // Start is called before the first frame update
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

    public void ShootResult(string result)
    {
        if(result=="Goal")
        {
            UIScore[ShootCounter].color=Color.green;
        }
        else
        {
            UIScore[ShootCounter].color=Color.red;
        }

        ShootCounter++;
    }
}
