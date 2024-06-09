using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ConnectionManager : NetworkBehaviour
{
    public Transform startPositionHost;
    public Transform startPositionClient;
    bool started = false;

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            StartHost();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            StartClient();

        if (!IsServer)
            return;
        if (NetworkManager.Singleton.ConnectedClientsIds.Count < 2) //
            return;
        else if(!started)
        {
            started = true;

           // NetworkManager.Singleton.SceneManager.LoadScene("Carrera", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }



    }
}
