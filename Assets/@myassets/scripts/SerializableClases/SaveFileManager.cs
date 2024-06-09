
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    public static SaveFileManager instance;

    SaveData saveData;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        checkForJSON();
    }

    void checkForJSON()
    {
        var jsonTextFile = Resources.Load<TextAsset>("savedata/saveFile");
        if (jsonTextFile == null)
        {
            createBasicJSON();       
        }
        try
        {
            saveData = JsonUtility.FromJson<SaveData>(jsonTextFile.text);
        }
        catch
        {
            // TODO: Borrar archivo, crear nuevo y cargar datos
            createBasicJSON();
            saveData = JsonUtility.FromJson<SaveData>(jsonTextFile.text);
        }
    }

    void createBasicJSON()
    {
        FileStream fs = File.Create("Resources/savedata/saveFile.json");
        byte[] newJSON = new UTF8Encoding(true).GetBytes("{ \"multiplayerskill\": 0, \"selectedChar\": 0 }");
        fs.Write(newJSON,0, newJSON.Length);
    }

    void saveToFIle()
    {
        FileStream fs = File.Create("Resources/savedata/saveFile.json");
        byte[] newJSON = new UTF8Encoding(true).GetBytes(JsonUtility.ToJson(saveData));
        fs.Write(newJSON, 0, newJSON.Length);
    }
}
