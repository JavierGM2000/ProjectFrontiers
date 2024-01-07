using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.XR.CoreUtils;

public class PlaneMovementV2 : MonoBehaviour
{
    public throttleController throttleObject;

    public float deltaTimeFixMultiplier = 10f;

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
    [SerializeField]
    private WeaponPlayerControler weaponControler;
    
    private XROrigin MainCamera;
    [SerializeField]
    private Transform cameraCenter;

    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public InputAction throttleAction;
    public InputAction SshootAction;
    public InputAction changeAction;
    public InputAction resetAction;
    public InputAction missileAction;

    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    private float shoot;

    private bool stall;
    public float stallVelocity = 0f;
    public GameObject stallObject;
    private Vector3 lookDirection = Vector3.down;
    public ParticleSystem speedParticles;

    private bool audioPlaying = false;
    private bool startSoundCounter;
    private float soundCounter;
    public float soundCooldown;

    private float responseModifier {
        get {
            return (rb.mass / 10f) * responsiveness;
        }

    }

    Rigidbody rb;

    [SerializeField]
    MainGun gun;


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
        changeAction = planeControls.PlaneMap.ChangeTarget;
        resetAction = planeControls.PlaneMap.ResetCam;
        missileAction = planeControls.PlaneMap.Missile;
        pitchAction.Enable();
        rollAction.Enable();
        yawAction.Enable();
        throttleAction.Enable();
        SshootAction.Enable();
        changeAction.Enable();
        resetAction.Enable();
        missileAction.Enable();
        rb = GetComponent<Rigidbody>();
        gatlinBehaviour = gatling.GetComponent<GatlinBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = FindObjectOfType<XROrigin>();
        throttle = 0;
        volumeMultiplier = engine.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (startSoundCounter)
        {
            soundCounter += Time.deltaTime;
        }
        if (stall)
        {
            if (stallObject.gameObject.active == false)
            {
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
        handleChange();
        handleReset();
        handleMissile();
        

        float angleDown = Vector3.Angle(Vector3.down, transform.forward);
        // + " gravForce : " + (gravity - (rb.velocity.magnitude * 9.8f / 30f) + " gravAngle : "+ (gravity - (angleDown * 9.8f / 90)).ToString("F1"));
        velocityText.text = "Velocity: " + rb.velocity.magnitude /*+ "Potencial: " + potencial*/;
        angleWithFloorText.text = "Angle: " + Vector3.Angle(Vector3.down, transform.forward) + "Stall: " + stall;


        Vector3 rotation = new Vector3(-45 * pitchAction.ReadValue<float>(), 45 * yawAction.ReadValue<float>(), -45 * rollAction.ReadValue<float>());

        Quaternion newRotation = Quaternion.Euler(rotation);
        joystick.localRotation = newRotation;

        engine.volume = throttle / 100 * volumeMultiplier;
        windSound.volume = rb.velocity.magnitude / 100;

    }

    private void handleMissile()
    {
        if (missileAction.WasPressedThisFrame())
        {
            weaponControler.launchWeapon();
        }
    }
    private void handleReset()
    {
        float reseting = resetAction.ReadValue<float>();
        if (reseting > 0)
        {

            MainCamera.MoveCameraToWorldLocation(cameraCenter.position);
            MainCamera.MatchOriginUpCameraForward(cameraCenter.up, cameraCenter.forward);
        }
    }
    private void handleChange()
    {
        float changing = changeAction.ReadValue<float>();
        if (changing > 0)
        {
            weaponControler.findNewTarget();
        }
    }

    private void FixedUpdate()
    {
        detectStall();
        float angleDown = Vector3.Angle(Vector3.down, transform.forward);

        float appliedGravityForce = gravity - (rb.velocity.magnitude * gravity / 100f);
        // appliedGravityForce = Mathf.Clamp(appliedGravityForce, 0f, gravity);
        float appliedTurnForce = (responseModifier * Time.deltaTime * deltaTimeFixMultiplier);
        float speedFactor = 1f - Mathf.Clamp01(rb.velocity.magnitude / 300f);
        if (rb.velocity.magnitude >= 120 && !speedParticles.gameObject.active)
        {

            speedParticles.gameObject.SetActive(true);
        }
        else if(rb.velocity.magnitude < 120 && speedParticles.gameObject.active)
        {
            speedParticles.gameObject.SetActive(false);
        }
        appliedTurnForce *= speedFactor;
         if (stall)
         {

             rb.AddForce(Vector3.up * -gravity  * Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
             rb.AddForce(-rb.velocity.normalized * 10f * Time.deltaTime, ForceMode.Acceleration);
             rb.angularDrag = 4f;

            rb.AddTorque(transform.up * yaw * appliedTurnForce / 4.4f);
            rb.AddTorque(-transform.right * pitch * appliedTurnForce/2);
            rb.AddTorque(-transform.forward * roll * appliedTurnForce  /2);
           
         }
         else {
             rb.angularDrag = 1.5f;
            rb.AddTorque(transform.up * yaw * appliedTurnForce / 2.6f);
            rb.AddTorque(-transform.right * pitch * appliedTurnForce);
            rb.AddTorque(-transform.forward * roll * appliedTurnForce * 3.3f);

        }
        
        //rb.angularDrag = 1.5f;

        
        

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

            rb.AddForce(transform.forward * (gravity - (angleDown * gravity / 90))/2, ForceMode.Acceleration);
        }
        else {
            rb.AddForce(Vector3.up * -appliedGravityForce*2*Time.deltaTime * deltaTimeFixMultiplier, ForceMode.Acceleration);
            rb.AddForce(transform.forward * gravity * potencial * 2 * Time.deltaTime * deltaTimeFixMultiplier);
            

            

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
        float b = 0.5f;  // Altura mú‹ima de la función
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
            gun.fire();

            if (!audioPlaying)
            {
                gun.firingSound.Play();  
                startSoundCounter = true;
                audioPlaying = true;
            }
        }
        else
        {
            if (audioPlaying && soundCounter >= soundCooldown)
            {
                gun.firingSound.Stop();  
                audioPlaying = false;
                soundCounter = 0;
                startSoundCounter = false;
            }
        }
    





}

    public void detectStall()
    {
        if (stall)
        {
            if (rb.velocity.magnitude > stallVelocity + 20)
            {
                
                stall = false;
            }
            
        }
        else { 
        
        if (rb.velocity.magnitude < stallVelocity)
        {
            
            stall = true;
        }
        
        
        }
        
    
    }


   
}
