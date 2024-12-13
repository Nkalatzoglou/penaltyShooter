using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Debug_Handler : MonoBehaviour
{


    public List<Vector3> DebugPosition;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePosition()
    {
        DebugPosition.Add(GameManager.gameManager.Player_Handler.selectedTarget.position);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        var data = new PlayerData();

        

        foreach(Vector3 pos in DebugPosition)
        {
            data.DebugPositionSaved_x.Add(pos.x);
            data.DebugPositionSaved_y.Add(pos.y);
            data.DebugPositionSaved_z.Add(pos.z);
        }

        bf.Serialize(file,data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath+"/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/playerInfo.dat",FileMode.Open);


            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            for(int i=0;i<data.DebugPositionSaved_x.Count;i++)
            {
                var pos = new Vector3(data.DebugPositionSaved_x[i],data.DebugPositionSaved_y[i],data.DebugPositionSaved_z[i]);
                DebugPosition.Add(pos);
            }

        }
    }

    void SaveButton()
    {
        Save();
    }    
}

[Serializable]
public class PlayerData
{
    [SerializeField]public List<float> DebugPositionSaved_x;
    [SerializeField]public List<float> DebugPositionSaved_y;
    [SerializeField]public List<float> DebugPositionSaved_z;

    public PlayerData()
    {
        DebugPositionSaved_x = new List<float>();
        DebugPositionSaved_y = new List<float>();
        DebugPositionSaved_z = new List<float>();
    }
}

