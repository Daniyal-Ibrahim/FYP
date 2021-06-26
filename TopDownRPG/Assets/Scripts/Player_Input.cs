using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player_Input : MonoBehaviour 
{

    // Input Controls
    public InputMaster controls;
    // Movement
    private Vector2 inputVector = new Vector2(0, 0);
    private Rigidbody rb;
    public float Move_Speed;
    private float range = 100f;
    public Transform lookat;

    Vector3 X, Z;
    public float speed;
    public float runingSpeed;
    float currentSpeed;
    public float curr_rotation;
    private Vector3 targetVector;
    private Quaternion rotation;

    [SerializeField]
    private Camera Camera;

    // Animations
    public Animator animator;
    public float input_Delay = 1f;
    private bool Cast_Start = false;
    private bool Cast_Loop = false;
    private bool Cast_End = false;
    public float nextinput = 0f;
    public bool isTalking;
    public bool stillTalking;
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

    // dialogue control
    // option 1 : use mouse data to seee who to talk to 

    // option 2 : using a trigger collider
    public BoxCollider dialogCollider;
    public DialogTrigger dialog;
    // zoom control
    public CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        X = Camera.main.transform.forward;
        X.y = 0;
        X = Vector3.Normalize(X);
        Z = Quaternion.Euler(new Vector3(0,90,0))* X;

    }

    public AdvancedAudioManager audioManager;

    public void FootStep()
    {
        audioManager = GameObject.Find("Advanced Audio Manager").GetComponent<AdvancedAudioManager>();
        audioManager.PlaySound("FootStep");
    }
    //public void Inventory(InputAction.CallbackContext context)
    //{ // logic moved to inventory manager
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
        // player input split into X and Y axis
        inputVector = context.ReadValue<Vector2>();

        //Debug.Log("X: " + inputVector.x.ToString() + " Y: " + inputVector.y.ToString());

        //Vector3 direction = new Vector3(inputVector.y, 0, inputVector.x);
        Vector3 ZAxis = Z * Move_Speed * Time.deltaTime * inputVector.x;
        Vector3 YAxis = X * Move_Speed * Time.deltaTime * inputVector.y;

        // Vrotation also rotates just put this in the update rather then the Quaternion rotation
        //Vector3 Vrotation = Vector3.Normalize(ZAxis + YAxis);
        //transform.forward = Vrotation;

        transform.position += ZAxis;
        transform.position += YAxis;

        // used to get values for rotation
        targetVector = new Vector3(inputVector.x, 0, inputVector.y);
        targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;

    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            animator.SetBool("Sprint", true);
            Move_Speed = 6;
        }

        if (context.canceled == true)
        {
            animator.SetBool("Sprint", false);
            Move_Speed = 4;
        }
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
        if (pauseMenu.activeInHierarchy == true)
        {
            pauseMenu.SetActive(false);
        }
        else
            pauseMenu.SetActive(true);
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dialogCollider.gameObject.SetActive(true);
        }

        if (context.performed == true && stillTalking)
        {
            dialog.TriggerNextLine();
        }

        else if (context.performed == true && isTalking && !stillTalking)
        {
            Debug.Log("Interact");
            dialog.talking = true;
            dialog.TriggerDialog();
            stillTalking = true;
            //canRotate = false;
        }



        if (context.canceled)
        {
            dialogCollider.gameObject.SetActive(false);
        }
    }
    public void Zoom(InputAction.CallbackContext context)
    {
        float z = context.ReadValue<float>();
        /*
        if (z > 0 && virtualCamera.m_Lens.OrthographicSize > 1)
        {
            virtualCamera.m_Lens.OrthographicSize -= 1;
            Debug.Log("Scroll UP");
        }
        else if (z < 0 && virtualCamera.m_Lens.OrthographicSize < 20)
        {
            virtualCamera.m_Lens.OrthographicSize += 1;
            Debug.Log("Scroll DOWN");
        }
        */

        if (z > 0 && virtualCamera.m_Lens.FieldOfView > 5)
        {
            virtualCamera.m_Lens.FieldOfView -= 5;
            Debug.Log("Scroll UP");
        }
        else if (z < 0 && virtualCamera.m_Lens.FieldOfView < 60)
        {
            virtualCamera.m_Lens.FieldOfView += 5;
            Debug.Log("Scroll DOWN");
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
        PlayerAnimation();

        // rotate the player as long as player is moving 
        // move this into its own function for clearner code
        if (inputVector.x > 0 || inputVector.x < 0 || inputVector.y > 0 || inputVector.y < 0)
        {
            rotation = Quaternion.LookRotation(targetVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 20);
        }

    }

    private void PlayerAnimation()
    {
        animator.SetFloat("Vertical", inputVector.y);
        animator.SetFloat("Horizontal", inputVector.x);
    }
}