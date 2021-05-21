using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Animator animator;
    public float waittime;
    
    public void LoadingnNext()
    {
        StartCoroutine( lvl(SceneManager.GetActiveScene().buildIndex + 1 ));
    }

    public void LoadingnPrevious()
    {
        StartCoroutine(lvl(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void LoadingCurrent()
    {
        StartCoroutine(lvl(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator lvl(int lvlindex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(waittime);
        SceneManager.LoadScene(lvlindex);
    }

}
