using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    // Input Controls
    public InputMaster controls;
    // Movement
    [SerializeField] float Move_Speed = 10f;
    private Vector2 inputVector = new Vector2(0, 0);
    private Rigidbody rb;
    private float range = 100f;
    // Animations
    public Animator animator;
    public float f = 0f;
    // UI
    public GameObject Inventroy;
    public GameObject Equiptment;
    public bool Active = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Inventory(InputAction.CallbackContext context)
    {
        if (Inventroy.activeInHierarchy == false && Equiptment.activeInHierarchy == false)
            Active = false;

        if (Active)
        {
            Inventroy.gameObject.SetActive(false);
            Equiptment.gameObject.SetActive(false);
            Active = false;
        }
        else
        {
            Inventroy.gameObject.SetActive(true);
            Equiptment.gameObject.SetActive(true);
            Active = true;
        }
        Debug.Log("Input");
    }
    public void Movement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        Debug.Log("X: " + inputVector.x.ToString() + " Y: " + inputVector.y.ToString());
    }
    public void Attack(InputAction.CallbackContext context)
    {
        f = context.ReadValue<float>();
        animator.SetFloat("Attack", context.ReadValue<float>());
    }

    private void Update()
    {
        // Animation Based Movement
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        // physics based movement
        //PlayerMovement();
        // physics based rotation
        PlayerRoation();
    }

    private void PlayerRoation()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            Debug.Log("Get a mouse");
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            //Debug.Log("Ray casted");
            Vector3 direction = hit.point - transform.position;
            Quaternion Qdirection = (Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)));
            rb.MoveRotation(Qdirection);
        }
    }
    private void PlayerMovement()
    {
        if (inputVector.y < 0)
        {
            rb.velocity = transform.forward * inputVector.y * (Move_Speed / 2);
        }
        else if (inputVector.y > 0)
        {
            rb.velocity = transform.forward * inputVector.y * Move_Speed;
        }

        else if (inputVector.x < 0)
        {
            //Debug.Log("strafe right");
            rb.velocity = transform.right * inputVector.x * (Move_Speed / 2);
        }
        else if (inputVector.x > 0)
        {
            //Debug.Log("Strafe left");
            rb.velocity = (transform.right) * inputVector.x * (Move_Speed / 2);
        }
    }
    private void PlayerAnimation()
    {
        animator.SetFloat("Vertical", inputVector.y);
        animator.SetFloat("Horizontal", inputVector.x);
        animator.SetFloat("Attack", f);
    }
}