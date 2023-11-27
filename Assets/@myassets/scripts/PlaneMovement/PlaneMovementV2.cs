using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlaneMovementV2 : MonoBehaviour
{
    public  float deltaTimeFixMultiplier = 10f;

    public float throttleIncrement = 0.1f;

    public float maxThrust = 200f;

    public float responsiveness = 10f;

    public float lift = 135f;

    public float gravity = 9.81f;

    public float potencial = 0f;
    public float maxPotencial = 1f;

    [SerializeField]
    private TMP_Text throttleText;
    [SerializeField]
    private TMP_Text velocityText;
    [SerializeField]
    private TMP_Text angleWithFloorText;
    [SerializeField]
    private Transform joystick;


    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public InputAction throttleAction;


    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;

    private float responseModifier {
        get {
            return (rb.mass / 10f) * responsiveness;
        }
        
    }

    Rigidbody rb;


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
        float angleDown = Vector3.Angle(Vector3.down, transform.forward);
        throttleText.text = "Throttle: " + throttle;// + " gravForce : " + (gravity - (rb.velocity.magnitude * 9.8f / 30f) + " gravAngle : "+ (gravity - (angleDown * 9.8f / 90)).ToString("F1"));
        velocityText.text = "Velocity: " + rb.velocity.magnitude /*+ "Potencial: " + potencial*/; 
        angleWithFloorText.text = "Angle: " + Vector3.Angle(Vector3.down, transform.forward);
        
        
        Vector3 rotation = new Vector3(-45*pitchAction.ReadValue<float>(),45*yawAction.ReadValue<float>()  ,-45 * rollAction.ReadValue<float>() );
       
        Quaternion newRotation = Quaternion.Euler(rotation);
        joystick.localRotation = newRotation;
    }
    private void FixedUpdate()
    {   float angleDown = Vector3.Angle(Vector3.down, transform.forward);

        float appliedGravityForce = gravity - (rb.velocity.magnitude * 9.8f / 40f);

        
        rb.AddForce(transform.forward * (maxThrust * throttle)*Time.deltaTime* deltaTimeFixMultiplier*2);
        rb.AddTorque(transform.up * yaw * responseModifier *Time.deltaTime * deltaTimeFixMultiplier/2.2f);
        rb.AddTorque(-transform.right * pitch * responseModifier *Time.deltaTime * deltaTimeFixMultiplier);
        rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier);


        //rb.AddForce(Vector3.up * rb.velocity.magnitude * lift );
        
        if (angleDown < 80)
        {
            if (potencial >= maxPotencial)
            {
                potencial = maxPotencial;
            }
            else { 
                 potencial += (1-(angleDown * 1f / 90))* Time.deltaTime;
            }
            if (potencial <= 0.06f && potencial > 0) {
                rb.AddForce(Vector3.up * -appliedGravityForce * 2 * Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
            }

            //rb.AddForce(transform.forward * (gravity - (angleDown * 9.8f / 90))*5, ForceMode.Acceleration);
        }
        else {

            rb.AddForce(Vector3.up * -appliedGravityForce*2*Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
            rb.AddForce(transform.forward * gravity * potencial * 2 * Time.deltaTime * deltaTimeFixMultiplier);
           // rb.AddForce(-rb.velocity.normalized * ((angleDown * 3f / 90)), ForceMode.Acceleration);

        }
        if (potencial <=0)
        {
            potencial = 0;
        }
        else
        {
            potencial -= (angleDown * 1f / 180) * Time.deltaTime/4;
        }
       
        rb.AddForce(transform.forward * gravity * potencial*5*Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
    
    
    }




    private void HandleInput() {
        pitch = pitchAction.ReadValue<float>();
        yaw = yawAction.ReadValue<float>();
        roll = rollAction.ReadValue<float>();

        throttle += throttleAction.ReadValue<float>() * throttleIncrement*Time.deltaTime;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }
}
