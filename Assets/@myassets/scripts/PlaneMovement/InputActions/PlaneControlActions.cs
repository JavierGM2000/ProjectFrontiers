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
                    ""id"": ""8d0baf2a-1bba-4b2f-9d3c-ac6fbc17f14a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2f08cbc3-adc1-4a25-851f-ab5dc3c2c4fe"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8d3451c1-7762-4d22-9812-10f0edd8df6d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ee5833fc-6f41-4e02-a1a6-0bd64ff3564a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3063b02e-ad25-4cc1-8b02-210ede2eb276"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2b50e016-db2a-47bd-aa80-9326f2ae4bec"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
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
                    ""name"": """",
                    ""id"": ""4211a9f9-0d26-4b58-bc7b-93560e2f8102"",
                    ""path"": ""<HID::Thrustmaster T.16000M>/rz"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
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
    public struct PlaneMapActions
    {
        private @PlaneControlActions m_Wrapper;
        public PlaneMapActions(@PlaneControlActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pitch => m_Wrapper.m_PlaneMap_Pitch;
        public InputAction @Roll => m_Wrapper.m_PlaneMap_Roll;
        public InputAction @Yaw => m_Wrapper.m_PlaneMap_Yaw;
        public InputAction @Thrust => m_Wrapper.m_PlaneMap_Thrust;
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
    }
}
