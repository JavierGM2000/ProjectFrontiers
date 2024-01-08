//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/@myassets/scripts/PlaneMovement/InputActions/PlaneControlActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlaneControlActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlaneControlActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlaneControlActions"",
    ""maps"": [
        {
            ""name"": ""PlaneMap"",
            ""id"": ""f4312a16-b8a8-42cf-8e62-a9c5799fe542"",
            ""actions"": [
                {
                    ""name"": ""Pitch"",
                    ""type"": ""Value"",
                    ""id"": ""b8386964-e4b7-48c1-a042-3be95d916a79"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Value"",
                    ""id"": ""7939d547-0e1c-422b-aa99-68c111bf3fa7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Yaw"",
                    ""type"": ""Value"",
                    ""id"": ""12923167-4590-4368-ab7b-d950e518781a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Thrust"",
                    ""type"": ""Value"",
                    ""id"": ""14567627-5db7-4b6b-a156-c6cf7014e598"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""5f31643f-996d-43f0-804a-8187a8370b9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangeTarget"",
                    ""type"": ""Button"",
                    ""id"": ""3f05a338-6760-4d2a-88f6-6cf82ec1ee57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ResetCam"",
                    ""type"": ""Button"",
                    ""id"": ""e6c8197d-4167-48f2-84dd-bed9f52eacaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Missile"",
                    ""type"": ""Button"",
                    ""id"": ""704b341f-4672-4944-96c5-fbfcbe30f9a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""4046b487-bb12-4134-b4e3-56d6ee550315"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pitch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2786088d-a764-4be8-95b1-ab6a3b4b7ea9"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f0ef9740-d68e-4b04-885b-efce5846de93"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""16c11a52-7e11-4b47-87ba-efa4dfd57f3f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e7714025-1d11-45e6-8fca-c1e14b128921"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b562b343-2c57-436c-9ba7-8447a194e2ba"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""4c7bfada-bb9a-411e-af2d-753f718510ab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3690456f-129b-4df7-89c3-b898abe311a8"",
                    ""path"": ""<XRController>{LeftHand}/grip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d286737f-1631-46a4-ab8b-6abf8647a2b0"",
                    ""path"": ""<XRController>{LeftHand}/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""fa8d47a1-f5fb-470c-b35d-e7c715657264"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1825cb04-93fa-4466-8ba0-61fc59f7f84c"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/hat/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""66dbc74b-3437-4838-87d5-1b14e60f46ed"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/hat/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""4591d6c1-f1f4-45af-b8e2-5de69b84731d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""dd5e46d7-20b0-4293-9361-931808a87a82"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e69d23fe-4424-4a57-8ceb-ba52745b042c"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""74224072-c049-451f-a4ed-1ce6e41d7ba7"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/slider"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""Thrust"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8765c65-369a-4b64-8572-3551499234ee"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88724eac-aeba-4dba-8a2f-321ab4076774"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""591d9e31-269f-4204-80a4-e537a344b32f"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2c6c564-db13-407f-97e8-2b1bd2461ece"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""3ba92890-caa4-4835-847a-458facdbab6a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3fef1b84-8ae4-4e50-a832-fcf430690078"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9012745a-1df5-4d88-b939-fbbc40c1e473"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""edf9778d-bb5a-46c1-9cd2-20e0d94a16b3"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77595e30-2f6e-4bf5-ba7d-a42a07d0ed7f"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""694ee6fe-4e23-422a-a548-2130dbea4392"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0876f4f0-c99e-4e41-82ee-9f2227d317e7"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2540d05-c821-421a-a616-ae9bb0b595e4"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e51e63a4-7169-4bd2-8b5f-31f1bc1a8d49"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b42be054-1537-461a-8c5d-05082ce367a4"",
                    ""path"": ""<HID::Logitech Logitech Extreme 3D>/button11"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98d90737-de50-4028-a64c-e817ff66bd24"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c94a47a9-8817-4451-8ab4-f57fd387c042"",
                    ""path"": ""<Joystick>/trigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Missile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlaneMap
        m_PlaneMap = asset.FindActionMap("PlaneMap", throwIfNotFound: true);
        m_PlaneMap_Pitch = m_PlaneMap.FindAction("Pitch", throwIfNotFound: true);
        m_PlaneMap_Roll = m_PlaneMap.FindAction("Roll", throwIfNotFound: true);
        m_PlaneMap_Yaw = m_PlaneMap.FindAction("Yaw", throwIfNotFound: true);
        m_PlaneMap_Thrust = m_PlaneMap.FindAction("Thrust", throwIfNotFound: true);
        m_PlaneMap_Shoot = m_PlaneMap.FindAction("Shoot", throwIfNotFound: true);
        m_PlaneMap_ChangeTarget = m_PlaneMap.FindAction("ChangeTarget", throwIfNotFound: true);
        m_PlaneMap_ResetCam = m_PlaneMap.FindAction("ResetCam", throwIfNotFound: true);
        m_PlaneMap_Missile = m_PlaneMap.FindAction("Missile", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlaneMap
    private readonly InputActionMap m_PlaneMap;
    private IPlaneMapActions m_PlaneMapActionsCallbackInterface;
    private readonly InputAction m_PlaneMap_Pitch;
    private readonly InputAction m_PlaneMap_Roll;
    private readonly InputAction m_PlaneMap_Yaw;
    private readonly InputAction m_PlaneMap_Thrust;
    private readonly InputAction m_PlaneMap_Shoot;
    private readonly InputAction m_PlaneMap_ChangeTarget;
    private readonly InputAction m_PlaneMap_ResetCam;
    private readonly InputAction m_PlaneMap_Missile;
    public struct PlaneMapActions
    {
        private @PlaneControlActions m_Wrapper;
        public PlaneMapActions(@PlaneControlActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pitch => m_Wrapper.m_PlaneMap_Pitch;
        public InputAction @Roll => m_Wrapper.m_PlaneMap_Roll;
        public InputAction @Yaw => m_Wrapper.m_PlaneMap_Yaw;
        public InputAction @Thrust => m_Wrapper.m_PlaneMap_Thrust;
        public InputAction @Shoot => m_Wrapper.m_PlaneMap_Shoot;
        public InputAction @ChangeTarget => m_Wrapper.m_PlaneMap_ChangeTarget;
        public InputAction @ResetCam => m_Wrapper.m_PlaneMap_ResetCam;
        public InputAction @Missile => m_Wrapper.m_PlaneMap_Missile;
        public InputActionMap Get() { return m_Wrapper.m_PlaneMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlaneMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlaneMapActions instance)
        {
            if (m_Wrapper.m_PlaneMapActionsCallbackInterface != null)
            {
                @Pitch.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnPitch;
                @Pitch.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnPitch;
                @Pitch.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnPitch;
                @Roll.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnRoll;
                @Yaw.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnYaw;
                @Yaw.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnYaw;
                @Yaw.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnYaw;
                @Thrust.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnThrust;
                @Thrust.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnThrust;
                @Thrust.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnThrust;
                @Shoot.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnShoot;
                @ChangeTarget.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnChangeTarget;
                @ChangeTarget.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnChangeTarget;
                @ChangeTarget.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnChangeTarget;
                @ResetCam.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnResetCam;
                @ResetCam.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnResetCam;
                @ResetCam.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnResetCam;
                @Missile.started -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnMissile;
                @Missile.performed -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnMissile;
                @Missile.canceled -= m_Wrapper.m_PlaneMapActionsCallbackInterface.OnMissile;
            }
            m_Wrapper.m_PlaneMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pitch.started += instance.OnPitch;
                @Pitch.performed += instance.OnPitch;
                @Pitch.canceled += instance.OnPitch;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Yaw.started += instance.OnYaw;
                @Yaw.performed += instance.OnYaw;
                @Yaw.canceled += instance.OnYaw;
                @Thrust.started += instance.OnThrust;
                @Thrust.performed += instance.OnThrust;
                @Thrust.canceled += instance.OnThrust;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ChangeTarget.started += instance.OnChangeTarget;
                @ChangeTarget.performed += instance.OnChangeTarget;
                @ChangeTarget.canceled += instance.OnChangeTarget;
                @ResetCam.started += instance.OnResetCam;
                @ResetCam.performed += instance.OnResetCam;
                @ResetCam.canceled += instance.OnResetCam;
                @Missile.started += instance.OnMissile;
                @Missile.performed += instance.OnMissile;
                @Missile.canceled += instance.OnMissile;
            }
        }
    }
    public PlaneMapActions @PlaneMap => new PlaneMapActions(this);
    public interface IPlaneMapActions
    {
        void OnPitch(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnYaw(InputAction.CallbackContext context);
        void OnThrust(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnChangeTarget(InputAction.CallbackContext context);
        void OnResetCam(InputAction.CallbackContext context);
        void OnMissile(InputAction.CallbackContext context);
    }
}
