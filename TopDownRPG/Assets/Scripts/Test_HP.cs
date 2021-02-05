using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_HP : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Animator animator;
    public Slider Slider;
    // Update is called once per frame

    private void Awake()
    {
        curHealth = maxHealth;
        Slider.maxValue = maxHealth;
    }

    public void Damaged(int value)
    {
        curHealth -= value;
        Slider.value -= value;
        if (curHealth <= 0)
        {
            Death();
        }
        else
        {
            animator.SetTrigger("Hit");
            Debug.Log("Damaged");
        }
    }
    public void Death()
    {
        Debug.Log("Dead");
        animator.SetTrigger("Death");
        //Destroy(this.gameObject);
        Destroy(animator, 3);
        Destroy(this.gameObject, 5);
    }


}
