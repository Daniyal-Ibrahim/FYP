using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    [SerializeField] float Move_Speed = 10f;
    public Animator animator;
    private Vector2 inputVector = new Vector2(0, 0);
    private Rigidbody rb;
    private float range = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        Debug.Log("X: " + inputVector.x.ToString() + " Y: " + inputVector.y.ToString());
    }

    private void Update()
    {
        animator.SetFloat("Vertical", inputVector.x);
        animator.SetFloat("Horizontal", inputVector.y);
    }

    private void FixedUpdate()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            Debug.Log("Get a mouse");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit , range))
        {
            Vector3 direction = hit.point - transform.position;
            Quaternion Qdirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            rb.MoveRotation(Qdirection);
        }
        if (inputVector.y < 0)
        {
            rb.velocity = transform.forward * inputVector.y * (Move_Speed/2);
        }
        else
        {
            rb.velocity = transform.forward * inputVector.y * Move_Speed;
        }

        if (inputVector.x == 1)
        {
            Debug.Log("strafe right");
            rb.velocity = transform.right * inputVector.x * (Move_Speed / 2);
        }
        else if (inputVector.x == -1)
        {
            Debug.Log("Strafe left");
            rb.velocity = (transform.right ) * inputVector.x * (Move_Speed / 2);
        }

    }
}