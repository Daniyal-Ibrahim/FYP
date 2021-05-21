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
    private Vector2 inputVector = new Vector2(0, 0);
    private Rigidbody rb;
    private float range = 100f;
    // Animations
    public Animator animator;
    public float input_Delay = 1f;
    private bool Cast_Start = false;
    private bool Cast_Loop = false;
    private bool Cast_End = false;
    public float nextinput = 0f;
    public bool isTalking;
    public bool canRotate = true;
    // UI
    //public GameObject Inventroy;
    //public GameObject Equiptment;
    //public bool Active = false;
    public GameObject pauseMenu;
    // Attack
    private int combo = 0;
    private float reset; // if we should reset 
    private float reset_time; // time before resert is triggred
    private float resert_delay; // delay between reserts 
    List<string> Trigger_list = new List<string>(new string[] { "Trigger 1", "Trigger 2", "Trigger 3" });
    // Item pickup
    public bool pickup = false;
    public DialogTrigger dialog;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public AdvancedAudioManager audioManager;

    public void FootStep()
    {
        audioManager = GameObject.Find("Advanced Audio Manager").GetComponent<AdvancedAudioManager>();
        audioManager.PlaySound("FootStep");
    }

    //public void Inventory(InputAction.CallbackContext context)
    //{
    //    if (Inventroy.activeInHierarchy == false && Equiptment.activeInHierarchy == false)
    //        Active = false;

    //    if (Active)
    //    {
    //        Inventroy.gameObject.SetActive(false);
    //        Equiptment.gameObject.SetActive(false);
    //        Active = false;
    //    }
    //    else
    //    {
    //        Inventroy.gameObject.SetActive(true);
    //        Equiptment.gameObject.SetActive(true);
    //        Active = true;
    //    }
    //    Debug.Log("Input");
    //}
    private void Reset()
    {
        
    }
    public void Movement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
        //Debug.Log("X: " + inputVector.x.ToString() + " Y: " + inputVector.y.ToString());
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed == true)
            animator.SetBool("Sprint", true);

        if (context.canceled == true)
            animator.SetBool("Sprint", false);
    }
    IEnumerator ExampleCoroutine(InputAction.CallbackContext context)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.01f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    IEnumerator Cast_Delay(InputAction.CallbackContext context)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(0.3f);
        if (context.performed == true)
            animator.SetBool("Start", true);
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    IEnumerator Attack_Delay(InputAction.CallbackContext context)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for x seconds.
        if (combo == 3)
        {
            combo = 0;
            resert_delay = 0;
            animator.SetTrigger("Reset");
        }
        yield return new WaitForSecondsRealtime(resert_delay);
        if (context.performed != true)
        {
            combo = 0;
            resert_delay = 0;
            animator.SetTrigger("Reset");
        }
        //After we have waited x seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    public void Primary_Attack(InputAction.CallbackContext context)
    {
        if (context.performed == true && combo < 3)
        {
            StopAllCoroutines();
            Debug.Log("Combo :" + combo);
            animator.SetTrigger(Trigger_list[combo]);
            combo++;
            resert_delay = 1.5f;

            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSecondsRealtime(resert_delay/2);
                animator.SetTrigger("Reset");
            }

        }
        if (context.canceled == true)
        {
            StartCoroutine(Attack_Delay(context));
        }
    }
    public void Secondary_Attack(InputAction.CallbackContext context)
    {
        if (animator.GetBool("Can Cast") == true)
        {
            animator.SetBool("Secondary",true);

            if (context.performed == true )
            {
            
                Cast_Loop = true;
                animator.SetBool("Loop", true);
                animator.SetFloat("S_ID", 2);
                StartCoroutine(Cast_Delay(context));
            }

            if (context.canceled == true)
            {
                Cast_Loop = false;
                animator.SetBool("Loop", false);
                animator.SetBool("Start", false);
                animator.SetFloat("S_ID", 0);
                Debug.Log("Triggred");
            }

        }
        else
        {
            if (context.started == true && animator.GetBool("Can Cast") == false /* && Time.time >= nextattack */)
            {
                animator.SetBool("Secondary", true);
                animator.SetFloat("S", context.ReadValue<float>());
                nextinput = Time.time + 1f / input_Delay;
            }
            // casting
            else if (context.started == true && animator.GetBool("Can Cast") == true)
            {
                animator.SetBool("Secondary", true);
                animator.SetFloat("S", context.ReadValue<float>());
                nextinput = Time.time + 1f / input_Delay;
            }
            if (context.canceled == true)
            {
                animator.SetFloat("S", context.ReadValue<float>());
                animator.SetBool("Secondary", false);
            }
        }
    }
    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (pauseMenu.active == true)
        {
            pauseMenu.SetActive(false);
        }
        else
            pauseMenu.SetActive(true);
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed == true && isTalking)
        {
            Debug.Log("Interact");
            dialog.talking = true;
            dialog.TriggerDialog();
            canRotate = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        dialog = other.GetComponent<DialogTrigger>();
        if (dialog)
        {
            isTalking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialog = other.GetComponent<DialogTrigger>();
        if (dialog)
        {
            isTalking = false;
            dialog.talking = false;
            FindObjectOfType<DialogManager>().EndDialog();
            canRotate = true;
        }
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
        
        if (canRotate)
        {
            PlayerRoation();
        }
        
        

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
    /* physics based movement
    private void PlayerMovement()
    {
        //if (inputVector.y < 0)
        //{
        //    rb.velocity = transform.forward * inputVector.y * (Move_Speed / 2);
        //}
        //else if (inputVector.y > 0)
        //{
        //    rb.velocity = transform.forward * inputVector.y * Move_Speed;
        //}

        //else if (inputVector.x < 0)
        //{
        //    Debug.Log("strafe right");
        //    rb.velocity = transform.right * inputVector.x * (Move_Speed / 2);
        //}
        //else if (inputVector.x > 0)
        //{
        //    Debug.Log("Strafe left");
        //    rb.velocity = (transform.right) * inputVector.x * (Move_Speed / 2);
        //}
    }
    */
    private void PlayerAnimation()
    {
        animator.SetFloat("Vertical", inputVector.y);
        animator.SetFloat("Horizontal", inputVector.x);
    }
}