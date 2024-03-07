
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;

public class SaveFileManager : MonoBehaviour
{
    SaveData saveData;   

    // Start is called before the first frame update
    void Start()
    {
        checkForJSON();
    }

    void checkForJSON()
    {
        var jsonTextFile = Resources.Load<TextAsset>("savedata/saveFile");
        if (jsonTextFile == null)
        {
            createBasicJSON();        }
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
}
