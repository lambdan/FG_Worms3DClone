//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/InputSystem/Default Controls.inputactions
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

public partial class @DefaultControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Default Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""79b38629-8387-4333-be20-ab75d5b65b1b"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""4f2f58e6-2fcd-46c4-ac98-ad2120f4680b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2c6c469e-88bc-438c-a46c-3838a250a193"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Weapon Switch"",
                    ""type"": ""Button"",
                    ""id"": ""0cc8bd0f-7a1d-4761-9c36-93b8e83caa81"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bb081b56-64a4-49f8-9601-c01d9f31fe94"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera Movement"",
                    ""type"": ""Value"",
                    ""id"": ""fc480a5b-1218-4070-90d7-b1ac18f03ecc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Camera Reset"",
                    ""type"": ""Button"",
                    ""id"": ""ab8e9bb8-cac3-44e7-8b8e-2d407732c605"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""d53902fa-0309-4d0d-af89-e85a62807bb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""292b29be-1b22-48dc-b77c-403d78293a18"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02dc82ff-09ec-4863-937f-0c81c799ec95"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""435d9c83-1a76-4344-8816-cbbe69cb39bb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1996817-6eb4-4396-a4f5-329eec8e3882"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""4e10aa01-c7f5-4eb8-8b79-d9cee5499a23"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d0f50720-a975-4643-8828-992ccfa92530"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d318c089-8d3d-4c92-b9e1-3c3226f347b2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d0092dbe-51bf-4013-9e96-227c7598968c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8bc052a2-4fc3-482f-b97f-eb7c85462b78"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""79d3f423-9177-47d9-b7b3-8be091dc8e7b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""14c67a77-80ed-4cb6-b505-a690db56f357"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""56b30fe6-7aae-4576-a255-b33517ab2e98"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cb209597-80bd-45c6-9d42-26eb4673a76a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""98213002-2ebe-4b28-9bfa-63038aa529eb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5d837cc4-65af-425f-9289-0e727eb172b4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8752cdbe-2820-4017-b4b5-990c43ed2986"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fa30f1b-b6e1-441a-bae1-4aac9ba24310"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f6685c1-d91a-4e9e-945b-d98fc3ef4115"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54084ee3-4f0f-4c69-97c5-073fb5988d1b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f28ae76-4867-4c9c-8535-c14a4a89a225"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7ad90c0-8f42-4a67-b001-0d32d8a3aaa3"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1adf356b-77c6-4880-afde-78527dc5064d"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37e709db-a190-4897-9927-04bcbc41bff7"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b2d51ba-1c41-447b-af8e-345f0b089874"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game Actions"",
            ""id"": ""d7693418-d49c-4b2b-8800-d68f38bd4406"",
            ""actions"": [
                {
                    ""name"": ""Pause Menu"",
                    ""type"": ""Button"",
                    ""id"": ""1d591db4-91e1-4960-9c0a-de8d9367ee06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Worm Switch"",
                    ""type"": ""Button"",
                    ""id"": ""b6b4a878-dac7-4f2f-8c23-242e52b89c7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Weapon Cheat"",
                    ""type"": ""Button"",
                    ""id"": ""80141b17-8941-49e4-8477-d360ccaab932"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Invincibility Cheat"",
                    ""type"": ""Button"",
                    ""id"": ""58be5343-b183-4016-8485-c2293775b9fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c6c90072-1dda-406b-aeaf-6308786a0a24"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9327bcef-5b62-4695-9991-f5993558a5f2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""870b3725-a832-4382-948f-87315632975a"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Invincibility Cheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a136d177-700b-462c-ae83-4ad579e11d27"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Weapon Cheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""422b0e36-4d7a-485b-8373-3e482f09f861"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Worm Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""670f4bd8-da04-4dac-a1fa-d2f97c4edb92"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Worm Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_WeaponSwitch = m_Player.FindAction("Weapon Switch", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_CameraMovement = m_Player.FindAction("Camera Movement", throwIfNotFound: true);
        m_Player_CameraReset = m_Player.FindAction("Camera Reset", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        // Game Actions
        m_GameActions = asset.FindActionMap("Game Actions", throwIfNotFound: true);
        m_GameActions_PauseMenu = m_GameActions.FindAction("Pause Menu", throwIfNotFound: true);
        m_GameActions_WormSwitch = m_GameActions.FindAction("Worm Switch", throwIfNotFound: true);
        m_GameActions_WeaponCheat = m_GameActions.FindAction("Weapon Cheat", throwIfNotFound: true);
        m_GameActions_InvincibilityCheat = m_GameActions.FindAction("Invincibility Cheat", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_WeaponSwitch;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_CameraMovement;
    private readonly InputAction m_Player_CameraReset;
    private readonly InputAction m_Player_Reload;
    public struct PlayerActions
    {
        private @DefaultControls m_Wrapper;
        public PlayerActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @WeaponSwitch => m_Wrapper.m_Player_WeaponSwitch;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @CameraMovement => m_Wrapper.m_Player_CameraMovement;
        public InputAction @CameraReset => m_Wrapper.m_Player_CameraReset;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @WeaponSwitch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponSwitch;
                @WeaponSwitch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponSwitch;
                @WeaponSwitch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponSwitch;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @CameraMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraMovement;
                @CameraReset.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraReset;
                @CameraReset.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraReset;
                @CameraReset.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraReset;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @WeaponSwitch.started += instance.OnWeaponSwitch;
                @WeaponSwitch.performed += instance.OnWeaponSwitch;
                @WeaponSwitch.canceled += instance.OnWeaponSwitch;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraReset.started += instance.OnCameraReset;
                @CameraReset.performed += instance.OnCameraReset;
                @CameraReset.canceled += instance.OnCameraReset;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Game Actions
    private readonly InputActionMap m_GameActions;
    private IGameActionsActions m_GameActionsActionsCallbackInterface;
    private readonly InputAction m_GameActions_PauseMenu;
    private readonly InputAction m_GameActions_WormSwitch;
    private readonly InputAction m_GameActions_WeaponCheat;
    private readonly InputAction m_GameActions_InvincibilityCheat;
    public struct GameActionsActions
    {
        private @DefaultControls m_Wrapper;
        public GameActionsActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseMenu => m_Wrapper.m_GameActions_PauseMenu;
        public InputAction @WormSwitch => m_Wrapper.m_GameActions_WormSwitch;
        public InputAction @WeaponCheat => m_Wrapper.m_GameActions_WeaponCheat;
        public InputAction @InvincibilityCheat => m_Wrapper.m_GameActions_InvincibilityCheat;
        public InputActionMap Get() { return m_Wrapper.m_GameActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActionsActions set) { return set.Get(); }
        public void SetCallbacks(IGameActionsActions instance)
        {
            if (m_Wrapper.m_GameActionsActionsCallbackInterface != null)
            {
                @PauseMenu.started -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnPauseMenu;
                @WormSwitch.started -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnWormSwitch;
                @WormSwitch.performed -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnWormSwitch;
                @WormSwitch.canceled -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnWormSwitch;
                @WeaponCheat.started -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnWeaponCheat;
                @WeaponCheat.performed -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnWeaponCheat;
                @WeaponCheat.canceled -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnWeaponCheat;
                @InvincibilityCheat.started -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnInvincibilityCheat;
                @InvincibilityCheat.performed -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnInvincibilityCheat;
                @InvincibilityCheat.canceled -= m_Wrapper.m_GameActionsActionsCallbackInterface.OnInvincibilityCheat;
            }
            m_Wrapper.m_GameActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @WormSwitch.started += instance.OnWormSwitch;
                @WormSwitch.performed += instance.OnWormSwitch;
                @WormSwitch.canceled += instance.OnWormSwitch;
                @WeaponCheat.started += instance.OnWeaponCheat;
                @WeaponCheat.performed += instance.OnWeaponCheat;
                @WeaponCheat.canceled += instance.OnWeaponCheat;
                @InvincibilityCheat.started += instance.OnInvincibilityCheat;
                @InvincibilityCheat.performed += instance.OnInvincibilityCheat;
                @InvincibilityCheat.canceled += instance.OnInvincibilityCheat;
            }
        }
    }
    public GameActionsActions @GameActions => new GameActionsActions(this);
    public interface IPlayerActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnWeaponSwitch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraReset(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
    }
    public interface IGameActionsActions
    {
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnWormSwitch(InputAction.CallbackContext context);
        void OnWeaponCheat(InputAction.CallbackContext context);
        void OnInvincibilityCheat(InputAction.CallbackContext context);
    }
}