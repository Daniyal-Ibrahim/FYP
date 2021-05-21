// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""51063c05-4bf4-404e-aaa0-6389d109aaa7"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""929d1c8c-3317-49d6-9666-041040290f7d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""e7fb679a-92cb-43fc-ab39-0173653a8024"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""48e5f0e9-a28d-42bb-b4e4-f46ce5251d68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary_Attack"",
                    ""type"": ""Button"",
                    ""id"": ""b56fbcd2-ecbe-4122-884e-9a043eab1ed8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary_Attack"",
                    ""type"": ""Button"",
                    ""id"": ""6fcf5173-5823-4162-b707-ec99a1b40464"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenMenu"",
                    ""type"": ""Button"",
                    ""id"": ""8df7786c-dfd4-4efb-87ba-832436a2396a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""16094e58-c459-4a3a-a04a-fd40cbf79740"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""fd6f1f4a-7acd-4d37-a15f-bcf82059de55"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""208e5e2f-c4bc-448b-a92a-05e8cf031a05"",
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
                    ""id"": ""4808d456-0673-47a5-a2db-4f9cab9cbe3e"",
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
                    ""id"": ""f868e46b-2c65-49f2-9364-87cb900e4a2c"",
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
                    ""id"": ""b0e96808-7667-431d-9023-a5d01108b9cf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ARROW"",
                    ""id"": ""4961c518-f476-4d36-ba4f-0f49fc48eb58"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3c25a2b0-6a6c-4d17-aa7f-3fdcb49a52fd"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""44d274b4-6be0-4853-9609-338e4f3e2533"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cd6432ff-4f32-4dab-9795-8acfb6a9be27"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e0834e1b-1abf-466c-bce4-d9652b1f5d00"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""868f50a5-a419-49b0-957f-8a695563cc40"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a48aedb4-1844-498c-ace3-2aac5cb83b20"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56dcaf19-4a02-4714-b7d7-cb506e7ec521"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3759847f-3373-49c4-8ca6-b5fcf9e5d08e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Secondary_Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9bc6d6c-7ba0-4034-bee0-00ca24b8a93d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Primary_Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19609f32-0cb7-4f6f-be76-da0e41c449d6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be6bc563-d85b-45b7-b5a4-933364c7b0ed"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""6b5a8a00-814e-495b-86f9-cf079f1403df"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""a073f618-4c23-4d7c-b6ff-2dca6d492859"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""38edb80e-a5ae-4d50-9da7-7163518155ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8bfc916e-a75f-4e28-bf80-f4c1c70db53c"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8edf617-8ce7-4da4-9721-a50ffc38a8a7"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce359770-d768-4fc2-9b7f-2534ae6379c2"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
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
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Inventory = m_Player.FindAction("Inventory", throwIfNotFound: true);
        m_Player_Primary_Attack = m_Player.FindAction("Primary_Attack", throwIfNotFound: true);
        m_Player_Secondary_Attack = m_Player.FindAction("Secondary_Attack", throwIfNotFound: true);
        m_Player_OpenMenu = m_Player.FindAction("OpenMenu", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Newaction = m_Inventory.FindAction("New action", throwIfNotFound: true);
        m_Inventory_Inventory = m_Inventory.FindAction("Inventory", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Inventory;
    private readonly InputAction m_Player_Primary_Attack;
    private readonly InputAction m_Player_Secondary_Attack;
    private readonly InputAction m_Player_OpenMenu;
    private readonly InputAction m_Player_Interact;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Inventory => m_Wrapper.m_Player_Inventory;
        public InputAction @Primary_Attack => m_Wrapper.m_Player_Primary_Attack;
        public InputAction @Secondary_Attack => m_Wrapper.m_Player_Secondary_Attack;
        public InputAction @OpenMenu => m_Wrapper.m_Player_OpenMenu;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                @Primary_Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimary_Attack;
                @Primary_Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimary_Attack;
                @Primary_Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimary_Attack;
                @Secondary_Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondary_Attack;
                @Secondary_Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondary_Attack;
                @Secondary_Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondary_Attack;
                @OpenMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Primary_Attack.started += instance.OnPrimary_Attack;
                @Primary_Attack.performed += instance.OnPrimary_Attack;
                @Primary_Attack.canceled += instance.OnPrimary_Attack;
                @Secondary_Attack.started += instance.OnSecondary_Attack;
                @Secondary_Attack.performed += instance.OnSecondary_Attack;
                @Secondary_Attack.canceled += instance.OnSecondary_Attack;
                @OpenMenu.started += instance.OnOpenMenu;
                @OpenMenu.performed += instance.OnOpenMenu;
                @OpenMenu.canceled += instance.OnOpenMenu;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_Newaction;
    private readonly InputAction m_Inventory_Inventory;
    public struct InventoryActions
    {
        private @InputMaster m_Wrapper;
        public InventoryActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Inventory_Newaction;
        public InputAction @Inventory => m_Wrapper.m_Inventory_Inventory;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnNewaction;
                @Inventory.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnInventory;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnPrimary_Attack(InputAction.CallbackContext context);
        void OnSecondary_Attack(InputAction.CallbackContext context);
        void OnOpenMenu(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnNewaction(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
    }
}
