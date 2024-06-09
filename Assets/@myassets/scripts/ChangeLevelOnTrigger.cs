using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeLevelOnTrigger : MonoBehaviour
{
    public PlaneControlActions planeControls;
    public InputAction missileAction;
    public InputAction machinegunAction;
    public InputAction cameraAction;

    // Start is called before the first frame update
    void Start()
    {
        planeControls = new PlaneControlActions();
        missileAction = planeControls.PlaneMap.Missile;
        machinegunAction = planeControls.PlaneMap.Shoot;
        cameraAction = planeControls.PlaneMap.ResetCam;
        machinegunAction.Enable();
        missileAction.Enable();
        cameraAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (missileAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene("EscenaFinal");
        }
        if (machinegunAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene("EscenaFinalConIA");
        }
        if (machinegunAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene("lobyMultiplayer");
        }
    }
}
