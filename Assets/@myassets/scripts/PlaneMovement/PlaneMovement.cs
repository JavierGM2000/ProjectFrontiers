using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneMovement : MonoBehaviour
{

    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public float velocityMultiplier;
    public InputAction thrustAction;

    [SerializeField]
    private float pitchMultiplier;
    [SerializeField]
    private float yawMultiplier;
    [SerializeField]
    private float rollMultiplier;

    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private float maxVel;
    [SerializeField]
    private float thrustIncrementMultiplier;


    private void Awake()
    {
        planeControls = new PlaneControlActions();
        pitchAction = planeControls.PlaneMap.Pitch;
        rollAction = planeControls.PlaneMap.Roll;
        yawAction = planeControls.PlaneMap.Yaw;
        thrustAction = planeControls.PlaneMap.Thrust;
        pitchAction.Enable();
        rollAction.Enable();
        yawAction.Enable();
        thrustAction.Enable();

    }
    // Start is called before the first frame update
    void Start()
    {
        
        velocityMultiplier = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*Debug.Log("Pitch = "+pitchAction.ReadValue<float>());
        Debug.Log("Yaw = " + yawAction.ReadValue<float>());
        Debug.Log("Roll = " + rollAction.ReadValue<float>());
        Debug.Log("Thrust = " + thrustAction.ReadValue<float>());
        Debug.Log("Velocity = " + velocityMultiplier);*/

        velocityMultiplier += thrustAction.ReadValue<float>()*thrustIncrementMultiplier * Time.deltaTime;
        velocityMultiplier = Mathf.Clamp(velocityMultiplier, 0, maxVel);
        transform.position += transform.forward * velocityMultiplier*Time.deltaTime*speedMultiplier;
        transform.Rotate(new Vector3(-1*pitchAction.ReadValue<float>()* Time.deltaTime*pitchMultiplier, yawAction.ReadValue<float>() * Time.deltaTime * yawMultiplier, -1*rollAction.ReadValue<float>() * Time.deltaTime * rollMultiplier));
    }
}
