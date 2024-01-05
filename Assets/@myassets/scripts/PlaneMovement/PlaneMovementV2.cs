using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlaneMovementV2 : MonoBehaviour
{
    public throttleController throttleObject;

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
    private float targetThrottle = 0f;
    public float throttleSmoothSpeed = 2f;

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

    [SerializeField]
    private GameObject gatling;
    private GatlinBehaviour gatlinBehaviour;


    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public InputAction throttleAction;
    public InputAction SshootAction;

    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    private float shoot;

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
        stall = false;
        planeControls = new PlaneControlActions();
        pitchAction = planeControls.PlaneMap.Pitch;
        rollAction = planeControls.PlaneMap.Roll;
        yawAction = planeControls.PlaneMap.Yaw;
        throttleAction = planeControls.PlaneMap.Thrust;
        SshootAction = planeControls.PlaneMap.Shoot;
        pitchAction.Enable();
        rollAction.Enable();
        yawAction.Enable();
        throttleAction.Enable();
        SshootAction.Enable();
        rb = GetComponent<Rigidbody>();
        gatlinBehaviour = gatling.GetComponent<GatlinBehaviour>();
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
               // stallObject.gameObject.active = true;
            }
        }
        else
        {
            if (stallObject.gameObject.active == true)
            {
                //stallObject.gameObject.active = false;
            }
            

        }
        HandleInput();
        handleThrottle();
        handleShooting();
        Debug.Log("Velocity = " + rb.velocity.magnitude);
        
        float angleDown = Vector3.Angle(Vector3.down, transform.forward);
        // + " gravForce : " + (gravity - (rb.velocity.magnitude * 9.8f / 30f) + " gravAngle : "+ (gravity - (angleDown * 9.8f / 90)).ToString("F1"));
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
        // appliedGravityForce = Mathf.Clamp(appliedGravityForce, 0f, gravity);
        /*
         if (stall)
         {

             rb.AddForce(Vector3.up * -gravity  * Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
             rb.AddForce(-rb.velocity.normalized * 10f * Time.deltaTime, ForceMode.Acceleration);
             rb.angularDrag = 4f;

             rb.AddTorque(transform.up * yaw * responseModifier * Time.deltaTime * deltaTimeFixMultiplier / 4.4f);
             rb.AddTorque(-transform.right * pitch * responseModifier * Time.deltaTime * deltaTimeFixMultiplier/2);
             rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier/2);
         }
         else {
             rb.angularDrag = 1.5f;
             rb.AddTorque(transform.up * yaw * responseModifier *Time.deltaTime * deltaTimeFixMultiplier/2.2f);
             rb.AddTorque(-transform.right * pitch * responseModifier *Time.deltaTime * deltaTimeFixMultiplier);
             rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier);

         }
        */
        //rb.angularDrag = 1.5f;
        rb.AddTorque(transform.up * yaw * responseModifier * Time.deltaTime * deltaTimeFixMultiplier / 2.2f);
        rb.AddTorque(-transform.right * pitch * responseModifier * Time.deltaTime * deltaTimeFixMultiplier);
        rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier * 2);

        float throttleAdjusted = Mathf.Pow(throttle, 2) / 100f;
        //float acceleration = CalculateAcceleration(rb.velocity.magnitude);

       // rb.AddForce(transform.forward * (maxThrust * throttleAdjusted * acceleration) * Time.deltaTime, ForceMode.Acceleration);
        rb.AddForce(transform.forward * (maxThrust * throttle) * Time.deltaTime, ForceMode.Acceleration);
        

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
    private float CalculateAcceleration(float velocityMagnitude)
    {
        // Ajusta estos valores según tus necesidades
        float a = 1f;  // Altura máxima de la función
        float b = 0.5f;  // Altura mínima de la función
        float k = 1f;  // Pendiente de la curva
        float x0 = 50f;  // Punto medio de la transición

        // Aplica la función sigmoidal modificada
        float sigmoidal = a / (0 + Mathf.Exp(-k * (velocityMagnitude - x0))) + b;

        return sigmoidal;
    }



    private void HandleInput() {
        pitch = pitchAction.ReadValue<float>();
        yaw = yawAction.ReadValue<float>();
        roll = rollAction.ReadValue<float>();

       
    }
    private void handleThrottle() {
        float targetThrottleInput = throttleAction.ReadValue<float>();
        targetThrottle += targetThrottleInput * throttleIncrement * Time.deltaTime;
        targetThrottle = Mathf.Clamp(targetThrottle, 0f,  100);

        // Smoothly adjust the current throttle towards the target
        throttle = Mathf.Lerp(throttle, targetThrottle, Time.deltaTime * throttleSmoothSpeed);
        throttleText.text = "Throttle: " + targetThrottle;
    }

    private void handleShooting() {
        float shooting = SshootAction.ReadValue<float>();
        
        if (shooting > 0)
        {
            gatlinBehaviour.rotatePlatForm();
            float rayDistance = 50f; // Puedes ajustar esto según tus necesidades

            // Realizar el raycast
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, 10))
            {
                Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.green);
                // Verificar si el objeto golpeado pertenece a la capa objetivo (capa 10)
                if (hitInfo.collider.gameObject.layer == 10)
                {
                    
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
        

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            return;
        Destroy(gameObject);
    }
}
