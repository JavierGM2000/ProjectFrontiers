using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlaneMovement : MonoBehaviour
{
    public float deltaTimeFixMultiplier = 10f;
    public float maxThrust = 1000f;
    public float throttleIncrement = 0.1f;
    public float responsiveness = 10f;

    public float speedFactor = 1.0f;
 
    private float maxSpeed = 300;
    public float sigmoidOffset = 0.3f;
    public float lowThrottleSpeedFactor = 1.5f;



    public PlaneControlActions planeControls;
    public InputAction pitchAction;
    public InputAction rollAction;
    public InputAction yawAction;
    public InputAction throttleAction;
    private float throttle;
    private float roll;
    private float pitch;
    private float yaw;
    

    [SerializeField]
    private TMP_Text throttleText;
    [SerializeField]
    private TMP_Text velocityText;
    private float responseModifier
    {
        get
        {
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

    private void Start()
    {
        throttle = 0;
    }


    private void Update()
    {
        HandleInput();
        handleThrottle();
        throttleText.text = "Throttle: " + throttle;
        velocityText.text = "Velocity: " + rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        
        float currentSpeed = rb.velocity.magnitude;
        float adjustedThrottle = Mathf.Clamp01(rb.velocity.magnitude / maxSpeed);
        float adjustedThrust = maxThrust * throttle * CustomSigmoid(adjustedThrottle);
       
        rb.AddTorque(transform.up * yaw * responseModifier * Time.deltaTime * deltaTimeFixMultiplier / 1.4f);
        rb.AddTorque(-transform.right * pitch * responseModifier * Time.deltaTime * deltaTimeFixMultiplier);
        rb.AddTorque(-transform.forward * roll * responseModifier * Time.deltaTime * deltaTimeFixMultiplier);

        rb.AddForce(transform.forward * adjustedThrust * Time.deltaTime, ForceMode.Acceleration);
    }
    private float CustomSigmoid(float x)
    {
        return 1.0f / (1.0f + Mathf.Exp(-responsiveness * (x - sigmoidOffset)));
    }


    private void HandleInput()
    {
        pitch = pitchAction.ReadValue<float>();
        yaw = yawAction.ReadValue<float>();
        roll = rollAction.ReadValue<float>();


    }
    private void handleThrottle()
    {
        throttle += throttleAction.ReadValue<float>() * throttleIncrement * Time.deltaTime;
        throttle = Mathf.Clamp(throttle, 0f, 100f);
    }
}
