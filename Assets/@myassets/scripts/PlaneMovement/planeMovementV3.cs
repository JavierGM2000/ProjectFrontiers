using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class planeMovementV3 : MonoBehaviour
{


    public float maxThrust = 1000f;
    public float throttleIncrement = 15f;
    public float responsiveness = 10f;
    public float maxSpeed = 300;


    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public InputAction throttleAction;
    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    public float targetVelocity = 100;
    public float forceIncrement = 1;
    public float appliedForce = 0;
    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }

    }

    Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        planeControls = new PlaneControlActions();
        pitchAction = planeControls.PlaneMap.Pitch;
        rollAction = planeControls.PlaneMap.Roll;
        yawAction = planeControls.PlaneMap.Yaw;
        throttleAction = planeControls.PlaneMap.Thrust;
        pitchAction.Enable();
        rollAction.Enable();
        yawAction.Enable();
        throttleAction.Enable();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        throttle = 0;
    }
    // Update is called once per frame
    void Update()
    {
        handleThrottle();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude <= targetVelocity)
        {
            appliedForce += forceIncrement*Time.deltaTime;
        }
        else {

            appliedForce -= forceIncrement*Time.deltaTime;
        }
    }



    private void handleThrottle()
    {
        throttle += throttleAction.ReadValue<float>() * throttleIncrement * Time.deltaTime;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
        targetVelocity = maxSpeed * throttle / 100;
    }
}
