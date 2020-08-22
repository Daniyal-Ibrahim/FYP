using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    public Animator animator;
    public float TalkAnimtion = 0f;

    private void Update()
    {
        animator.SetFloat("Talk Animation", TalkAnimtion);
    }
}
