using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlaneMovementV2 : MonoBehaviour
{


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
        throttleText.text = "Throttle: " + throttle + " gravForce : " + (gravity - (rb.velocity.magnitude * 9.8f / 30f) + " gravAngle : "+ (gravity - (angleDown * 9.8f / 90)).ToString("F1"));
        velocityText.text = "Velocity: " + rb.velocity.magnitude + "Potencial: " + potencial; 
        angleWithFloorText.text = "Angle: " + Vector3.Angle(Vector3.down, transform.forward);
     
    }
    private void FixedUpdate()
    {   float angleDown = Vector3.Angle(Vector3.down, transform.forward);

        float appliedGravityForce = gravity - (rb.velocity.magnitude * 9.8f / 30f);

        
        rb.AddForce(transform.forward * (maxThrust * throttle));
        rb.AddTorque(transform.up * yaw * responseModifier );
        rb.AddTorque(-transform.right * pitch * responseModifier );
        rb.AddTorque(-transform.forward * roll * responseModifier);


        //rb.AddForce(Vector3.up * rb.velocity.magnitude * lift );
        
        if (angleDown < 90)
        {
            if (potencial >= maxPotencial)
            {
                potencial = maxPotencial;
            }
            else { 
                 potencial += (1-(angleDown * 1f / 90))* Time.deltaTime/2;
            }
           

            //rb.AddForce(transform.forward * (gravity - (angleDown * 9.8f / 90))*5, ForceMode.Acceleration);
        }
        else {

            rb.AddForce(Vector3.up * -appliedGravityForce, ForceMode.Acceleration);

        }
        if (potencial <=0)
        {
            potencial = 0;
        }
        else
        {
            potencial -= (angleDown * 1f / 180) * Time.deltaTime/2;
        }
       
        rb.AddForce(transform.forward * gravity * potencial*5, ForceMode.Acceleration);
    
    
    }




    private void HandleInput() {
        pitch = pitchAction.ReadValue<float>();
        yaw = yawAction.ReadValue<float>();
        roll = rollAction.ReadValue<float>();
        throttle += throttleAction.ReadValue<float>() * throttleIncrement;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }
}
