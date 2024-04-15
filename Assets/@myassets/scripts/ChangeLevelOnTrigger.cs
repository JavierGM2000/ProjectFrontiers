using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChangeLevelOnTrigger : MonoBehaviour
{
    public PlaneControlActions planeControls;
    public InputAction missileAction;

    // Start is called before the first frame update
    void Start()
    {
        planeControls = new PlaneControlActions();
        missileAction = planeControls.PlaneMap.Missile;
        missileAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (missileAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene("EscenaFinal");
        }
    }
}
