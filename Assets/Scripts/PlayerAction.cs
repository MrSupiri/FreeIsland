//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.1.1
//     from Assets/Scripts/PlayerAction.inputactions
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

public partial class @PlayerAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerAction"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""da43d6f7-70b9-4ceb-ae81-0b90caeee2b4"",
            ""actions"": [
                {
                    ""name"": ""Chat"",
                    ""type"": ""Button"",
                    ""id"": ""bdc4ba16-4a88-4e38-be0b-a161de022e5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8bce8b97-8e6b-4a0d-816c-e3fec5af8c87"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Chat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""DummyAnimation"",
            ""id"": ""80b11504-8ae1-4465-acd6-f2cd3d82fd08"",
            ""actions"": [
                {
                    ""name"": ""Sit"",
                    ""type"": ""Button"",
                    ""id"": ""204f3ba6-a4a3-42ee-86d8-6a6c6cf1b69a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""00e5d48c-853a-4a5c-a332-7ee738b7b94e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EnterInteract"",
                    ""type"": ""Button"",
                    ""id"": ""6b7dd0d7-d41d-4b8f-9533-5093eb2a8961"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ExitInteract"",
                    ""type"": ""Button"",
                    ""id"": ""bc3cf06f-f398-4e03-bc2a-84e0beefdf07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f47ed06e-ba18-41ed-a443-4c00efe6b0f6"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41a5c5ef-3d22-4285-a254-c9f182ec2dc2"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91284783-d82a-43e0-a3a1-1972bfac9fa5"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnterInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc46fe44-c514-4374-8706-e33cbf7bc78c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Chat = m_PlayerControls.FindAction("Chat", throwIfNotFound: true);
        // DummyAnimation
        m_DummyAnimation = asset.FindActionMap("DummyAnimation", throwIfNotFound: true);
        m_DummyAnimation_Sit = m_DummyAnimation.FindAction("Sit", throwIfNotFound: true);
        m_DummyAnimation_Walk = m_DummyAnimation.FindAction("Walk", throwIfNotFound: true);
        m_DummyAnimation_EnterInteract = m_DummyAnimation.FindAction("EnterInteract", throwIfNotFound: true);
        m_DummyAnimation_ExitInteract = m_DummyAnimation.FindAction("ExitInteract", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Chat;
    public struct PlayerControlsActions
    {
        private @PlayerAction m_Wrapper;
        public PlayerControlsActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Chat => m_Wrapper.m_PlayerControls_Chat;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Chat.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnChat;
                @Chat.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnChat;
                @Chat.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnChat;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Chat.started += instance.OnChat;
                @Chat.performed += instance.OnChat;
                @Chat.canceled += instance.OnChat;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // DummyAnimation
    private readonly InputActionMap m_DummyAnimation;
    private IDummyAnimationActions m_DummyAnimationActionsCallbackInterface;
    private readonly InputAction m_DummyAnimation_Sit;
    private readonly InputAction m_DummyAnimation_Walk;
    private readonly InputAction m_DummyAnimation_EnterInteract;
    private readonly InputAction m_DummyAnimation_ExitInteract;
    public struct DummyAnimationActions
    {
        private @PlayerAction m_Wrapper;
        public DummyAnimationActions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Sit => m_Wrapper.m_DummyAnimation_Sit;
        public InputAction @Walk => m_Wrapper.m_DummyAnimation_Walk;
        public InputAction @EnterInteract => m_Wrapper.m_DummyAnimation_EnterInteract;
        public InputAction @ExitInteract => m_Wrapper.m_DummyAnimation_ExitInteract;
        public InputActionMap Get() { return m_Wrapper.m_DummyAnimation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DummyAnimationActions set) { return set.Get(); }
        public void SetCallbacks(IDummyAnimationActions instance)
        {
            if (m_Wrapper.m_DummyAnimationActionsCallbackInterface != null)
            {
                @Sit.started -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnSit;
                @Sit.performed -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnSit;
                @Sit.canceled -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnSit;
                @Walk.started -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnWalk;
                @EnterInteract.started -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnEnterInteract;
                @EnterInteract.performed -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnEnterInteract;
                @EnterInteract.canceled -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnEnterInteract;
                @ExitInteract.started -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnExitInteract;
                @ExitInteract.performed -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnExitInteract;
                @ExitInteract.canceled -= m_Wrapper.m_DummyAnimationActionsCallbackInterface.OnExitInteract;
            }
            m_Wrapper.m_DummyAnimationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Sit.started += instance.OnSit;
                @Sit.performed += instance.OnSit;
                @Sit.canceled += instance.OnSit;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @EnterInteract.started += instance.OnEnterInteract;
                @EnterInteract.performed += instance.OnEnterInteract;
                @EnterInteract.canceled += instance.OnEnterInteract;
                @ExitInteract.started += instance.OnExitInteract;
                @ExitInteract.performed += instance.OnExitInteract;
                @ExitInteract.canceled += instance.OnExitInteract;
            }
        }
    }
    public DummyAnimationActions @DummyAnimation => new DummyAnimationActions(this);
    public interface IPlayerControlsActions
    {
        void OnChat(InputAction.CallbackContext context);
    }
    public interface IDummyAnimationActions
    {
        void OnSit(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnEnterInteract(InputAction.CallbackContext context);
        void OnExitInteract(InputAction.CallbackContext context);
    }
}
