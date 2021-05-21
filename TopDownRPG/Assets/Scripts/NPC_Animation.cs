using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    public Animator animator;
    public float TalkAnimtion = 0f;
    public GameObject player;
    public DialogTrigger dialogTrigger;
    public bool talking;
    private void Awake()
    {
        player = GameObject.Find("Plauer");
    }

    

    private void Update()
    {



        animator.SetFloat("Talk Animation", TalkAnimtion);
    }
}
