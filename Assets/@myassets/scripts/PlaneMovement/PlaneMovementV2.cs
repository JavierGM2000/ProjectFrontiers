using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlaneMovementV2 : MonoBehaviour
{
    public  float deltaTimeFixMultiplier = 10f;

    public float throttleIncrement = 0.1f;

    public float maxThrust = 1000f;

    public float responsiveness = 10f;

    public float lift = 135f;

    public float gravity = 9.81f;

    public float potencial = 0f;
    public float maxPotencial = 1f;

    private float currentSpeed;
    public float accelerationSpeed = 100f;

    [SerializeField]
    private TMP_Text throttleText;
    [SerializeField]
    private TMP_Text velocityText;
    [SerializeField]
    private TMP_Text angleWithFloorText;
    [SerializeField]
    private Transform joystick;
    [SerializeField]
    private AudioSource engine;
    [SerializeField]
    private AudioSource windSound;
    private float volumeMultiplier;


    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public InputAction throttleAction;


    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;

    private bool stall;
    public float stallVelocity = 0f;
    public GameObject stallObject;
    private Vector3 lookDirection = Vector3.down;




    private float responseModifier {
        get {
            return (rb.mass / 10f) * responsiveness;
        }
        
    }

    Rigidbody rb;


    private void Awake()
    {
        //stallObject.gameObject.active = false;
        stall = true;
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
        throttle = 0;
        volumeMultiplier = engine.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (stall)
        {
            if (stallObject.gameObject.active == false) {
                stallObject.gameObject.active = true;
            }
        }
        else
        {
            if (stallObject.gameObject.active == true)
            {
                stallObject.gameObject.active = false;
            }
            

        }
        HandleInput();
        handleThrottle();
        Debug.Log("Velocity = " + rb.velocity.magnitude);
        
        float angleDown = Vector3.Angle(Vector3.down, transform.forward);
        throttleText.text = "Throttle: " + throttle;// + " gravForce : " + (gravity - (rb.velocity.magnitude * 9.8f / 30f) + " gravAngle : "+ (gravity - (angleDown * 9.8f / 90)).ToString("F1"));
        velocityText.text = "Velocity: " + rb.velocity.magnitude /*+ "Potencial: " + potencial*/; 
        angleWithFloorText.text = "Angle: " + Vector3.Angle(Vector3.down, transform.forward) + "Stall: " + stall;
        
        
        Vector3 rotation = new Vector3(-45*pitchAction.ReadValue<float>(),45*yawAction.ReadValue<float>()  ,-45 * rollAction.ReadValue<float>() );
       
        Quaternion newRotation = Quaternion.Euler(rotation);
        joystick.localRotation = newRotation;

        engine.volume = throttle / 100 * volumeMultiplier;
        windSound.volume = rb.velocity.magnitude / 100;

    }
    private void FixedUpdate()
    {
        detectStall();
        float angleDown = Vector3.Angle(Vector3.down, transform.forward);

        float appliedGravityForce = gravity - (rb.velocity.magnitude * 9.8f / 100f);

        if (stall)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), 0.4f*Time.deltaTime);

            rb.AddTorque(transform.up * yaw * responseModifier * Time.deltaTime * deltaTimeFixMultiplier / 4.4f);
            rb.AddTorque(-transform.right * pitch * responseModifier * Time.deltaTime * deltaTimeFixMultiplier/2);
            rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier/2);



        }
        else { 
            
            rb.AddTorque(transform.up * yaw * responseModifier *Time.deltaTime * deltaTimeFixMultiplier/2.2f);
            rb.AddTorque(-transform.right * pitch * responseModifier *Time.deltaTime * deltaTimeFixMultiplier);
            rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier);
        
        }
        float throttleAdjusted = Mathf.Pow(throttle, 2) / 100f;
        rb.AddForce(transform.forward * (maxThrust * throttleAdjusted) * Time.deltaTime, ForceMode.Acceleration);
        //float targetSpeed = throttleAction.ReadValue<float>() * maxThrust;
        //currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accelerationSpeed * Time.deltaTime);
        //rb.AddForce(transform.forward * currentSpeed * Time.deltaTime*deltaTimeFixMultiplier, ForceMode.Acceleration);
        //rb.AddForce(Vector3.up * rb.velocity.magnitude * lift );

        if (angleDown < 80)
        {
            if (potencial >= maxPotencial)
            {
                potencial = maxPotencial;
            }
            else { 
                 potencial += (1-(angleDown * 1f / 90))* Time.deltaTime/2;
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
            potencial -= (angleDown * 1f / 180) * Time.deltaTime/2;
        }
       
        rb.AddForce(transform.forward * gravity * potencial*5*Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
    
    
    }




    private void HandleInput() {
        pitch = pitchAction.ReadValue<float>();
        yaw = yawAction.ReadValue<float>();
        roll = rollAction.ReadValue<float>();

       
    }
    private void handleThrottle() {
        throttle += throttleAction.ReadValue<float>() * throttleIncrement * Time.deltaTime;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }



    public void detectStall()
    {
        if (stall)
        {
            if (rb.velocity.magnitude >60)
            {
                
                stall = false;
            }
            
        }
        else { 
        
        if (rb.velocity.magnitude < 40)
        {
            
            stall = true;
        }
        
        
        }
        
    
    }
}
