using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float speed = 200;
    public Animator anim;
    float horizontal;
    float vertical;
    float horizontalRaw;
    float vericalRaw;
    Vector3 input;
    Vector3 inputRaw;
    [SerializeField] float gravityScale;

    // Translates the inputs 
    Vector3 targetRotation;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerInput();
        PlayerAnimation();
    }

    void FixedUpdate()
    {
        PlayerRotation();
    }

    void PlayerInput()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        horizontalRaw = Input.GetAxisRaw("Horizontal");
        vericalRaw = Input.GetAxisRaw("Vertical");

        // Takes in input from controller
        input = new Vector3(horizontal, 0, vertical);
        inputRaw = new Vector3(horizontalRaw, 0, vericalRaw);

        if (input.sqrMagnitude > 1f) { input.Normalize(); }
        if (inputRaw.sqrMagnitude > 1f) { inputRaw.Normalize(); }
        if (inputRaw != Vector3.zero) { targetRotation = Quaternion.LookRotation(input).eulerAngles; }
    }

    void PlayerRotation()
    {
        //Rotates the player
        rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x, Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), Time.deltaTime * rotationSpeed);

        Vector3 vel = (input * speed) * Time.deltaTime;
        rb.velocity = vel;

    }

    void PlayerAnimation()
    {   // Triggers the animations
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
    }

}
