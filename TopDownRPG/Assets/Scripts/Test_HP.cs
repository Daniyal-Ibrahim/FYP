using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_HP : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public Animator animator;
    public Image image;
    public GameObject ragdoll;
    // Update is called once per frame

    private void Awake()
    {
        curHealth = maxHealth;
        image.fillAmount = 1f;
        //Slider.maxValue = maxHealth;
    }

    public void Damaged(int value)
    {
        curHealth -= value;
        Debug.Log("new hp = " + (value / maxHealth));
        image.fillAmount = (curHealth / maxHealth);
        //Slider.value -= value;
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
        Destroy(this.GetComponent<BoxCollider>());
        Instantiate(ragdoll, this.transform.localPosition, this.transform.localRotation);
        Destroy(this.gameObject);
    }


}
