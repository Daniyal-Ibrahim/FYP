using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour 
{

    // Input Controls
    public InputMaster controls;
    // Movemen
    [SerializeField] float Move_Speed = 10f;
    private Vector2 inputVector = new Vector2(0, 0);
    private Rigidbody rb;
    private float range = 100f;
    // Animations
    public Animator animator;
    public float input_Delay = 1f;
    private bool Cast_Start = false;
    private bool Cast_Loop = false;
    private bool Cast_End = false;
    float nextinput = 0f;
    // UI
    //public GameObject Inventroy;
    //public GameObject Equiptment;
    //public bool Active = false;

    public int combo = 0;
    public float reset; // if we should reset 
    public float reset_time; // time before resert is triggred
    public float resert_delay; // delay between reserts 
    List<string> Trigger_list = new List<string>(new string[] { "Trigger 1", "Trigger 2", "Trigger 3" });

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        Debug.Log("X: " + inputVector.x.ToString() + " Y: " + inputVector.y.ToString());
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
        Debug.Log("cancled");
        animator.SetFloat("Attack", context.ReadValue<float>());
        animator.SetBool("Primary", false);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator Cast_Delay(InputAction.CallbackContext context)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(0.5f);
        if (context.performed == true)
            animator.SetBool("Start", true);
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator Attack_Delay(InputAction.CallbackContext context)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 5 seconds.
        if (combo == 3)
        {
            resert_delay -= resert_delay;
        }
        yield return new WaitForSecondsRealtime(resert_delay);
        if (context.performed != true)
        {
            combo = 0;
            resert_delay = 0;
            animator.SetTrigger("Reset");
        }
        //After we have waited 5 seconds print the time again.
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
            resert_delay += 1f;

        }
        if (context.canceled == true)
        {
            StartCoroutine(Attack_Delay(context));
        }


        //if ((context.performed == true) && (combo < 3))
        //{

        //    animator.SetTrigger(Trigger_list[combo]);
        //    combo++;
        //    reset = 0f;
        //    Debug.Log("Combo =" + combo + " reset =" + reset);
        //}
        //if (combo > 0 && combo < 3)
        //{
        //    reset += Time.deltaTime;
        //    Debug.Log("Combo =" + combo + " reset =" + reset);
        //    if (reset > reset_time)
        //    {
        //        reset = 1;
        //        animator.SetTrigger("Reset");
        //        combo = 0;
        //        Debug.Log("Combo =" + combo + " reset =" + reset);
        //    }
        //}
        //if (combo == 3)
        //{
        //    reset_time = 1f;
        //    combo = 0;
        //    reset = 1;
        //    Debug.Log("Combo =" + combo + " reset =" + reset);
        //}
        //else
        //{
        //    reset_time = 1f;
        //}
        //if (context.started == true /*&& context.control.IsActuated() == true && Time.time >= nextattack */)
        //{
        //    animator.SetBool("Primary", true);
        //    animator.SetBool("Secondary", false);
        //    animator.SetFloat("P", context.ReadValue<float>());
        //    nextattack = Time.time + 1f / attack_Delay;
        //    Debug.Log("pressed");
        //    StartCoroutine(ExampleCoroutine(context));
        //}
        //if (context.canceled == true)
        //{
        //    animator.SetFloat("P", context.ReadValue<float>());
        //    animator.SetBool("Primary", false);
        //}

        //Debug.Log("Started: " + context.started.ToString() + "  iteration :" + f);
        //Debug.Log("Performed: " + context.performed.ToString() + "  iteration :" + f);
        //Debug.Log("Completed: " + context.canceled.ToString() + "  iteration :" + f);
        //f++;
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