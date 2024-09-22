//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.2
//     from Assets/Scripts/Characters/Player/InputSystem/PlayerInput.inputactions
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
using UnityEngine;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerMotionMap"",
            ""id"": ""46a673d0-4cbf-4ca7-826f-501094d81c09"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""40a47e63-15a3-4560-8af6-99c98f19ced2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""Value"",
                    ""id"": ""6a66bb74-729d-4fa2-8f20-56a45ab4e3c5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""38d8bb20-1317-4154-aa8c-abe4fa1f1565"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""22d15072-63f9-4d76-b4ad-41258e68d8e6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""49a91c8b-09e9-49bc-9ebd-ebce873dca33"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""87f11141-b30f-4579-bef3-5389e53b319b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d1859c77-b78b-43f9-b373-c6816cffbce1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HideWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""cc66d0ba-7eeb-40a4-8401-7226215d9d55"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TakeWeapon1"",
                    ""type"": ""Button"",
                    ""id"": ""2a6442e0-e8a5-46e4-8595-f50f99a55462"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TakeWeapon2"",
                    ""type"": ""Button"",
                    ""id"": ""aee6fe4e-f62c-461b-a7be-66bbed78a632"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9e061338-ab66-455c-9e8c-6a6e40f32c4f"",
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
                    ""id"": ""94a6095a-abf1-4ef2-a2fe-8450243069f8"",
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
                    ""id"": ""828d1ec5-7908-4324-8db6-7182149d73c4"",
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
                    ""id"": ""a90dc31b-b58b-4007-a76e-73ed0d832c6d"",
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
                    ""id"": ""8173c58c-19ea-4d9f-ad98-a2fee18da5f4"",
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
                    ""id"": ""672b93f2-f39d-437d-aed0-f1b92c93b06b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b834ecfa-4748-4623-993b-0b36e0290d87"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21b91264-8b38-47fa-a069-6ffaefa91ace"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea9f9d91-daa7-418d-8ce5-f87b69435011"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20ba9f0a-3c3c-4686-8e12-64a083d16dcb"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b11e1809-6717-4b3f-a23f-10a815d0111c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7060adc-efd9-4486-aa9b-3db2dea19bb5"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HideWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4ede9c1-7dc6-47e1-af7d-0e5ea2d70802"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeWeapon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e06869fd-5c35-4808-b980-153108e91bfd"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TakeWeapon2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMotionMap
        m_PlayerMotionMap = asset.FindActionMap("PlayerMotionMap", throwIfNotFound: true);
        m_PlayerMotionMap_Movement = m_PlayerMotionMap.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMotionMap_MouseLook = m_PlayerMotionMap.FindAction("MouseLook", throwIfNotFound: true);
        m_PlayerMotionMap_Aim = m_PlayerMotionMap.FindAction("Aim", throwIfNotFound: true);
        m_PlayerMotionMap_Shoot = m_PlayerMotionMap.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerMotionMap_Run = m_PlayerMotionMap.FindAction("Run", throwIfNotFound: true);
        m_PlayerMotionMap_Crouch = m_PlayerMotionMap.FindAction("Crouch", throwIfNotFound: true);
        m_PlayerMotionMap_Interact = m_PlayerMotionMap.FindAction("Interact", throwIfNotFound: true);
        m_PlayerMotionMap_HideWeapon = m_PlayerMotionMap.FindAction("HideWeapon", throwIfNotFound: true);
        m_PlayerMotionMap_TakeWeapon1 = m_PlayerMotionMap.FindAction("TakeWeapon1", throwIfNotFound: true);
        m_PlayerMotionMap_TakeWeapon2 = m_PlayerMotionMap.FindAction("TakeWeapon2", throwIfNotFound: true);
    }

    ~@PlayerInput()
    {
        Debug.Assert(!m_PlayerMotionMap.enabled, "This will cause a leak and performance issues, PlayerInput.PlayerMotionMap.Disable() has not been called.");
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

    // PlayerMotionMap
    private readonly InputActionMap m_PlayerMotionMap;
    private List<IPlayerMotionMapActions> m_PlayerMotionMapActionsCallbackInterfaces = new List<IPlayerMotionMapActions>();
    private readonly InputAction m_PlayerMotionMap_Movement;
    private readonly InputAction m_PlayerMotionMap_MouseLook;
    private readonly InputAction m_PlayerMotionMap_Aim;
    private readonly InputAction m_PlayerMotionMap_Shoot;
    private readonly InputAction m_PlayerMotionMap_Run;
    private readonly InputAction m_PlayerMotionMap_Crouch;
    private readonly InputAction m_PlayerMotionMap_Interact;
    private readonly InputAction m_PlayerMotionMap_HideWeapon;
    private readonly InputAction m_PlayerMotionMap_TakeWeapon1;
    private readonly InputAction m_PlayerMotionMap_TakeWeapon2;
    public struct PlayerMotionMapActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerMotionMapActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMotionMap_Movement;
        public InputAction @MouseLook => m_Wrapper.m_PlayerMotionMap_MouseLook;
        public InputAction @Aim => m_Wrapper.m_PlayerMotionMap_Aim;
        public InputAction @Shoot => m_Wrapper.m_PlayerMotionMap_Shoot;
        public InputAction @Run => m_Wrapper.m_PlayerMotionMap_Run;
        public InputAction @Crouch => m_Wrapper.m_PlayerMotionMap_Crouch;
        public InputAction @Interact => m_Wrapper.m_PlayerMotionMap_Interact;
        public InputAction @HideWeapon => m_Wrapper.m_PlayerMotionMap_HideWeapon;
        public InputAction @TakeWeapon1 => m_Wrapper.m_PlayerMotionMap_TakeWeapon1;
        public InputAction @TakeWeapon2 => m_Wrapper.m_PlayerMotionMap_TakeWeapon2;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMotionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMotionMapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMotionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMotionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMotionMapActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @MouseLook.started += instance.OnMouseLook;
            @MouseLook.performed += instance.OnMouseLook;
            @MouseLook.canceled += instance.OnMouseLook;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @Shoot.started += instance.OnShoot;
            @Shoot.performed += instance.OnShoot;
            @Shoot.canceled += instance.OnShoot;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @HideWeapon.started += instance.OnHideWeapon;
            @HideWeapon.performed += instance.OnHideWeapon;
            @HideWeapon.canceled += instance.OnHideWeapon;
            @TakeWeapon1.started += instance.OnTakeWeapon1;
            @TakeWeapon1.performed += instance.OnTakeWeapon1;
            @TakeWeapon1.canceled += instance.OnTakeWeapon1;
            @TakeWeapon2.started += instance.OnTakeWeapon2;
            @TakeWeapon2.performed += instance.OnTakeWeapon2;
            @TakeWeapon2.canceled += instance.OnTakeWeapon2;
        }

        private void UnregisterCallbacks(IPlayerMotionMapActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @MouseLook.started -= instance.OnMouseLook;
            @MouseLook.performed -= instance.OnMouseLook;
            @MouseLook.canceled -= instance.OnMouseLook;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @Shoot.started -= instance.OnShoot;
            @Shoot.performed -= instance.OnShoot;
            @Shoot.canceled -= instance.OnShoot;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @HideWeapon.started -= instance.OnHideWeapon;
            @HideWeapon.performed -= instance.OnHideWeapon;
            @HideWeapon.canceled -= instance.OnHideWeapon;
            @TakeWeapon1.started -= instance.OnTakeWeapon1;
            @TakeWeapon1.performed -= instance.OnTakeWeapon1;
            @TakeWeapon1.canceled -= instance.OnTakeWeapon1;
            @TakeWeapon2.started -= instance.OnTakeWeapon2;
            @TakeWeapon2.performed -= instance.OnTakeWeapon2;
            @TakeWeapon2.canceled -= instance.OnTakeWeapon2;
        }

        public void RemoveCallbacks(IPlayerMotionMapActions instance)
        {
            if (m_Wrapper.m_PlayerMotionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMotionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMotionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMotionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMotionMapActions @PlayerMotionMap => new PlayerMotionMapActions(this);
    public interface IPlayerMotionMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnHideWeapon(InputAction.CallbackContext context);
        void OnTakeWeapon1(InputAction.CallbackContext context);
        void OnTakeWeapon2(InputAction.CallbackContext context);
    }
}
