// GENERATED AUTOMATICALLY FROM 'Assets/Script/Controller/Player Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""id"": ""b4fe58d2-d73b-4ea5-ae3b-51d7675781a8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""02683b98-c112-480f-8549-a8fc03aaf85d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""PassThrough"",
                    ""id"": ""14af9891-6418-4298-ba20-ab63c669ba15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""SwordSwing"",
                    ""type"": ""Button"",
                    ""id"": ""ff8c6cab-a322-43e8-b9ee-3dc02250c395"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Movement"",
                    ""id"": ""e7ae236d-cb9b-4487-9e27-6659da3476ab"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4e2e5fd5-1017-401d-a2ed-37801c3510cc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e6992ed2-9292-4c71-bbde-b49bb2c871d1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""532f78f6-5674-4ef9-9ebc-059ba078157e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""431820a0-37c8-4b77-9080-c565a080538d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwordSwing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PS4"",
            ""id"": ""be5e47c0-57fe-4a58-8497-9646f46bd3a6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""239e2f80-f503-4ee4-a9f9-66fef06b36f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7b40b742-70ea-4356-8198-2b8884428694"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9f7f383f-4f9b-4129-aea0-ad9399d4e05d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0382bee5-4116-41ed-ab02-86a4a696c3fc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""98694b70-9945-48d6-948c-a09ecb8625d2"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""79ea4a89-bd93-4936-b4d2-7e1c8e479aa5"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""XBOX"",
            ""id"": ""512af26d-948a-4aa3-8cb9-a80c1d19a2c9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""b6f4346f-162b-4a61-b94d-d4b25e3e849a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9d01b67d-4216-48cd-97a8-d5b81e065256"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""34666b4a-f91e-4e6f-9627-be771161b5b2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""60cd5c32-cffb-4000-a173-5dc055678404"",
                    ""path"": ""<XInputController>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b437c5ef-c523-40be-838b-df3c31ae04e6"",
                    ""path"": ""<XInputController>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cd9db592-b9c0-471e-8fbd-3da6869ca521"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard&Mouse
        m_KeyboardMouse = asset.FindActionMap("Keyboard&Mouse", throwIfNotFound: true);
        m_KeyboardMouse_Move = m_KeyboardMouse.FindAction("Move", throwIfNotFound: true);
        m_KeyboardMouse_Jump = m_KeyboardMouse.FindAction("Jump", throwIfNotFound: true);
        m_KeyboardMouse_SwordSwing = m_KeyboardMouse.FindAction("SwordSwing", throwIfNotFound: true);
        // PS4
        m_PS4 = asset.FindActionMap("PS4", throwIfNotFound: true);
        m_PS4_Move = m_PS4.FindAction("Move", throwIfNotFound: true);
        m_PS4_Jump = m_PS4.FindAction("Jump", throwIfNotFound: true);
        // XBOX
        m_XBOX = asset.FindActionMap("XBOX", throwIfNotFound: true);
        m_XBOX_Move = m_XBOX.FindAction("Move", throwIfNotFound: true);
        m_XBOX_Jump = m_XBOX.FindAction("Jump", throwIfNotFound: true);
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

    // Keyboard&Mouse
    private readonly InputActionMap m_KeyboardMouse;
    private IKeyboardMouseActions m_KeyboardMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardMouse_Move;
    private readonly InputAction m_KeyboardMouse_Jump;
    private readonly InputAction m_KeyboardMouse_SwordSwing;
    public struct KeyboardMouseActions
    {
        private @PlayerInput m_Wrapper;
        public KeyboardMouseActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_KeyboardMouse_Move;
        public InputAction @Jump => m_Wrapper.m_KeyboardMouse_Jump;
        public InputAction @SwordSwing => m_Wrapper.m_KeyboardMouse_SwordSwing;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardMouseActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJump;
                @SwordSwing.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSwordSwing;
            }
            m_Wrapper.m_KeyboardMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @SwordSwing.started += instance.OnSwordSwing;
                @SwordSwing.performed += instance.OnSwordSwing;
                @SwordSwing.canceled += instance.OnSwordSwing;
            }
        }
    }
    public KeyboardMouseActions @KeyboardMouse => new KeyboardMouseActions(this);

    // PS4
    private readonly InputActionMap m_PS4;
    private IPS4Actions m_PS4ActionsCallbackInterface;
    private readonly InputAction m_PS4_Move;
    private readonly InputAction m_PS4_Jump;
    public struct PS4Actions
    {
        private @PlayerInput m_Wrapper;
        public PS4Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PS4_Move;
        public InputAction @Jump => m_Wrapper.m_PS4_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PS4; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PS4Actions set) { return set.Get(); }
        public void SetCallbacks(IPS4Actions instance)
        {
            if (m_Wrapper.m_PS4ActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PS4ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PS4Actions @PS4 => new PS4Actions(this);

    // XBOX
    private readonly InputActionMap m_XBOX;
    private IXBOXActions m_XBOXActionsCallbackInterface;
    private readonly InputAction m_XBOX_Move;
    private readonly InputAction m_XBOX_Jump;
    public struct XBOXActions
    {
        private @PlayerInput m_Wrapper;
        public XBOXActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_XBOX_Move;
        public InputAction @Jump => m_Wrapper.m_XBOX_Jump;
        public InputActionMap Get() { return m_Wrapper.m_XBOX; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XBOXActions set) { return set.Get(); }
        public void SetCallbacks(IXBOXActions instance)
        {
            if (m_Wrapper.m_XBOXActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_XBOXActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_XBOXActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_XBOXActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_XBOXActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_XBOXActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_XBOXActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_XBOXActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public XBOXActions @XBOX => new XBOXActions(this);
    public interface IKeyboardMouseActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwordSwing(InputAction.CallbackContext context);
    }
    public interface IPS4Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IXBOXActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
