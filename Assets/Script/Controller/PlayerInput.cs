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
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cf598ddf-7fa9-4fe6-88ad-5b09ee5d5114"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpRelease"",
                    ""type"": ""Button"",
                    ""id"": ""b21cc446-5f88-4503-9b23-21c070e8190f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwordSwing"",
                    ""type"": ""Button"",
                    ""id"": ""ff8c6cab-a322-43e8-b9ee-3dc02250c395"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Element Special 1"",
                    ""type"": ""Button"",
                    ""id"": ""ca9c08ef-2248-4f5f-9c5d-1348b304c40a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""a453bfd2-487c-47b7-b09c-86a94543fe7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Guard Release"",
                    ""type"": ""Button"",
                    ""id"": ""e38d4642-0ae6-4cf3-bb9d-3d59f45152f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""9520fbd5-f796-4757-8642-98b3a507f0cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""fe94e74e-2574-4df0-89c4-0305ea0478d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
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
                    ""id"": ""431820a0-37c8-4b77-9080-c565a080538d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwordSwing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3fdd414-0724-42a9-a6c3-324ada27c141"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Element Special 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c7e4c52-c213-4339-9e47-b4c0203a631e"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1dc6e02-8059-4a3f-9881-aed797533fd5"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40bc7c63-2552-4792-83a0-3216fe03c238"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9ed5be7-296f-46a9-b9da-8788cadb6975"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f2f4320-dbff-40a6-82aa-ea89a3460b98"",
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
                    ""id"": ""654a7d94-5b12-4aa7-8cbc-667f5dcfe622"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpRelease"",
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
                },
                {
                    ""name"": ""SwordSwing"",
                    ""type"": ""Button"",
                    ""id"": ""4f3b4f0b-0438-4ece-bfdf-f8f756f5ac21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Element Special 1"",
                    ""type"": ""Button"",
                    ""id"": ""0c03d2b8-c007-40dd-a3e5-9db4e7e033b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""214a03ef-9ca3-4469-8068-a1ef415012a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard Release"",
                    ""type"": ""Button"",
                    ""id"": ""a41f893c-2021-4d06-a3a8-cd6a4c2a1f77"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""c27c59cc-2151-48d9-9ab1-2fa121d9499d"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""effed0dc-d7f5-4250-8a87-9cc688489c37"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwordSwing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e343ef40-0f67-48ed-9c64-f0aa3e5be582"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Element Special 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93d57159-b81e-450d-a038-aca2d9516f85"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e9df7e9-7e31-44e1-9c32-a15ae16a0e88"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c729b9a6-a44c-4173-bdac-5028c08d0afa"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                },
                {
                    ""name"": ""Sword Swing"",
                    ""type"": ""Button"",
                    ""id"": ""7bd3abc5-2220-4f78-9aea-76c640ad1220"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Element Special 1"",
                    ""type"": ""Button"",
                    ""id"": ""3b1818a7-b225-413f-acbf-1daf77a96f3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""bbff89fb-c808-4860-9465-d54d775b18a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard Release"",
                    ""type"": ""Button"",
                    ""id"": ""ab93e9c2-82fb-494e-abbb-41023a5716ce"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""f3bf3690-f900-4d2d-b24d-7a70fb14fb3e"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Element Special 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ed531df-46e8-45b4-abd7-17ce6f4581f5"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sword Swing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4253cc08-9c9c-4131-989d-ded338a0cc8a"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbcc10a8-c475-4fd7-a2e8-5c09a991b141"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard Release"",
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
        m_KeyboardMouse_JumpRelease = m_KeyboardMouse.FindAction("JumpRelease", throwIfNotFound: true);
        m_KeyboardMouse_SwordSwing = m_KeyboardMouse.FindAction("SwordSwing", throwIfNotFound: true);
        m_KeyboardMouse_ElementSpecial1 = m_KeyboardMouse.FindAction("Element Special 1", throwIfNotFound: true);
        m_KeyboardMouse_Guard = m_KeyboardMouse.FindAction("Guard", throwIfNotFound: true);
        m_KeyboardMouse_GuardRelease = m_KeyboardMouse.FindAction("Guard Release", throwIfNotFound: true);
        m_KeyboardMouse_Pause = m_KeyboardMouse.FindAction("Pause", throwIfNotFound: true);
        m_KeyboardMouse_Dash = m_KeyboardMouse.FindAction("Dash", throwIfNotFound: true);
        // PS4
        m_PS4 = asset.FindActionMap("PS4", throwIfNotFound: true);
        m_PS4_Move = m_PS4.FindAction("Move", throwIfNotFound: true);
        m_PS4_Jump = m_PS4.FindAction("Jump", throwIfNotFound: true);
        m_PS4_SwordSwing = m_PS4.FindAction("SwordSwing", throwIfNotFound: true);
        m_PS4_ElementSpecial1 = m_PS4.FindAction("Element Special 1", throwIfNotFound: true);
        m_PS4_Guard = m_PS4.FindAction("Guard", throwIfNotFound: true);
        m_PS4_GuardRelease = m_PS4.FindAction("Guard Release", throwIfNotFound: true);
        m_PS4_Pause = m_PS4.FindAction("Pause", throwIfNotFound: true);
        // XBOX
        m_XBOX = asset.FindActionMap("XBOX", throwIfNotFound: true);
        m_XBOX_Move = m_XBOX.FindAction("Move", throwIfNotFound: true);
        m_XBOX_Jump = m_XBOX.FindAction("Jump", throwIfNotFound: true);
        m_XBOX_SwordSwing = m_XBOX.FindAction("Sword Swing", throwIfNotFound: true);
        m_XBOX_ElementSpecial1 = m_XBOX.FindAction("Element Special 1", throwIfNotFound: true);
        m_XBOX_Guard = m_XBOX.FindAction("Guard", throwIfNotFound: true);
        m_XBOX_GuardRelease = m_XBOX.FindAction("Guard Release", throwIfNotFound: true);
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
    private readonly InputAction m_KeyboardMouse_JumpRelease;
    private readonly InputAction m_KeyboardMouse_SwordSwing;
    private readonly InputAction m_KeyboardMouse_ElementSpecial1;
    private readonly InputAction m_KeyboardMouse_Guard;
    private readonly InputAction m_KeyboardMouse_GuardRelease;
    private readonly InputAction m_KeyboardMouse_Pause;
    private readonly InputAction m_KeyboardMouse_Dash;
    public struct KeyboardMouseActions
    {
        private @PlayerInput m_Wrapper;
        public KeyboardMouseActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_KeyboardMouse_Move;
        public InputAction @Jump => m_Wrapper.m_KeyboardMouse_Jump;
        public InputAction @JumpRelease => m_Wrapper.m_KeyboardMouse_JumpRelease;
        public InputAction @SwordSwing => m_Wrapper.m_KeyboardMouse_SwordSwing;
        public InputAction @ElementSpecial1 => m_Wrapper.m_KeyboardMouse_ElementSpecial1;
        public InputAction @Guard => m_Wrapper.m_KeyboardMouse_Guard;
        public InputAction @GuardRelease => m_Wrapper.m_KeyboardMouse_GuardRelease;
        public InputAction @Pause => m_Wrapper.m_KeyboardMouse_Pause;
        public InputAction @Dash => m_Wrapper.m_KeyboardMouse_Dash;
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
                @JumpRelease.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJumpRelease;
                @JumpRelease.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJumpRelease;
                @JumpRelease.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnJumpRelease;
                @SwordSwing.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnSwordSwing;
                @ElementSpecial1.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnElementSpecial1;
                @ElementSpecial1.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnElementSpecial1;
                @ElementSpecial1.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnElementSpecial1;
                @Guard.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnGuard;
                @GuardRelease.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnGuardRelease;
                @GuardRelease.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnGuardRelease;
                @GuardRelease.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnGuardRelease;
                @Pause.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnPause;
                @Dash.started -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_KeyboardMouseActionsCallbackInterface.OnDash;
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
                @JumpRelease.started += instance.OnJumpRelease;
                @JumpRelease.performed += instance.OnJumpRelease;
                @JumpRelease.canceled += instance.OnJumpRelease;
                @SwordSwing.started += instance.OnSwordSwing;
                @SwordSwing.performed += instance.OnSwordSwing;
                @SwordSwing.canceled += instance.OnSwordSwing;
                @ElementSpecial1.started += instance.OnElementSpecial1;
                @ElementSpecial1.performed += instance.OnElementSpecial1;
                @ElementSpecial1.canceled += instance.OnElementSpecial1;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
                @GuardRelease.started += instance.OnGuardRelease;
                @GuardRelease.performed += instance.OnGuardRelease;
                @GuardRelease.canceled += instance.OnGuardRelease;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public KeyboardMouseActions @KeyboardMouse => new KeyboardMouseActions(this);

    // PS4
    private readonly InputActionMap m_PS4;
    private IPS4Actions m_PS4ActionsCallbackInterface;
    private readonly InputAction m_PS4_Move;
    private readonly InputAction m_PS4_Jump;
    private readonly InputAction m_PS4_SwordSwing;
    private readonly InputAction m_PS4_ElementSpecial1;
    private readonly InputAction m_PS4_Guard;
    private readonly InputAction m_PS4_GuardRelease;
    private readonly InputAction m_PS4_Pause;
    public struct PS4Actions
    {
        private @PlayerInput m_Wrapper;
        public PS4Actions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PS4_Move;
        public InputAction @Jump => m_Wrapper.m_PS4_Jump;
        public InputAction @SwordSwing => m_Wrapper.m_PS4_SwordSwing;
        public InputAction @ElementSpecial1 => m_Wrapper.m_PS4_ElementSpecial1;
        public InputAction @Guard => m_Wrapper.m_PS4_Guard;
        public InputAction @GuardRelease => m_Wrapper.m_PS4_GuardRelease;
        public InputAction @Pause => m_Wrapper.m_PS4_Pause;
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
                @SwordSwing.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnSwordSwing;
                @ElementSpecial1.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnElementSpecial1;
                @ElementSpecial1.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnElementSpecial1;
                @ElementSpecial1.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnElementSpecial1;
                @Guard.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnGuard;
                @GuardRelease.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnGuardRelease;
                @GuardRelease.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnGuardRelease;
                @GuardRelease.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnGuardRelease;
                @Pause.started -= m_Wrapper.m_PS4ActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PS4ActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PS4ActionsCallbackInterface.OnPause;
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
                @SwordSwing.started += instance.OnSwordSwing;
                @SwordSwing.performed += instance.OnSwordSwing;
                @SwordSwing.canceled += instance.OnSwordSwing;
                @ElementSpecial1.started += instance.OnElementSpecial1;
                @ElementSpecial1.performed += instance.OnElementSpecial1;
                @ElementSpecial1.canceled += instance.OnElementSpecial1;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
                @GuardRelease.started += instance.OnGuardRelease;
                @GuardRelease.performed += instance.OnGuardRelease;
                @GuardRelease.canceled += instance.OnGuardRelease;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PS4Actions @PS4 => new PS4Actions(this);

    // XBOX
    private readonly InputActionMap m_XBOX;
    private IXBOXActions m_XBOXActionsCallbackInterface;
    private readonly InputAction m_XBOX_Move;
    private readonly InputAction m_XBOX_Jump;
    private readonly InputAction m_XBOX_SwordSwing;
    private readonly InputAction m_XBOX_ElementSpecial1;
    private readonly InputAction m_XBOX_Guard;
    private readonly InputAction m_XBOX_GuardRelease;
    public struct XBOXActions
    {
        private @PlayerInput m_Wrapper;
        public XBOXActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_XBOX_Move;
        public InputAction @Jump => m_Wrapper.m_XBOX_Jump;
        public InputAction @SwordSwing => m_Wrapper.m_XBOX_SwordSwing;
        public InputAction @ElementSpecial1 => m_Wrapper.m_XBOX_ElementSpecial1;
        public InputAction @Guard => m_Wrapper.m_XBOX_Guard;
        public InputAction @GuardRelease => m_Wrapper.m_XBOX_GuardRelease;
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
                @SwordSwing.started -= m_Wrapper.m_XBOXActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.performed -= m_Wrapper.m_XBOXActionsCallbackInterface.OnSwordSwing;
                @SwordSwing.canceled -= m_Wrapper.m_XBOXActionsCallbackInterface.OnSwordSwing;
                @ElementSpecial1.started -= m_Wrapper.m_XBOXActionsCallbackInterface.OnElementSpecial1;
                @ElementSpecial1.performed -= m_Wrapper.m_XBOXActionsCallbackInterface.OnElementSpecial1;
                @ElementSpecial1.canceled -= m_Wrapper.m_XBOXActionsCallbackInterface.OnElementSpecial1;
                @Guard.started -= m_Wrapper.m_XBOXActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_XBOXActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_XBOXActionsCallbackInterface.OnGuard;
                @GuardRelease.started -= m_Wrapper.m_XBOXActionsCallbackInterface.OnGuardRelease;
                @GuardRelease.performed -= m_Wrapper.m_XBOXActionsCallbackInterface.OnGuardRelease;
                @GuardRelease.canceled -= m_Wrapper.m_XBOXActionsCallbackInterface.OnGuardRelease;
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
                @SwordSwing.started += instance.OnSwordSwing;
                @SwordSwing.performed += instance.OnSwordSwing;
                @SwordSwing.canceled += instance.OnSwordSwing;
                @ElementSpecial1.started += instance.OnElementSpecial1;
                @ElementSpecial1.performed += instance.OnElementSpecial1;
                @ElementSpecial1.canceled += instance.OnElementSpecial1;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
                @GuardRelease.started += instance.OnGuardRelease;
                @GuardRelease.performed += instance.OnGuardRelease;
                @GuardRelease.canceled += instance.OnGuardRelease;
            }
        }
    }
    public XBOXActions @XBOX => new XBOXActions(this);
    public interface IKeyboardMouseActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnJumpRelease(InputAction.CallbackContext context);
        void OnSwordSwing(InputAction.CallbackContext context);
        void OnElementSpecial1(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnGuardRelease(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IPS4Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwordSwing(InputAction.CallbackContext context);
        void OnElementSpecial1(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnGuardRelease(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IXBOXActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwordSwing(InputAction.CallbackContext context);
        void OnElementSpecial1(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnGuardRelease(InputAction.CallbackContext context);
    }
}
