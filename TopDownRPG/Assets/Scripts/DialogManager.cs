using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

[System.Serializable]
public class Dialog
{
    public string npcName;

    [TextArea(3,5)]
    public string[] sentences;


}

public class DialogManager : MonoBehaviour
{

    public GameObject player;

    private Queue<string> diaogLine;

    public TextMeshProUGUI Name_text;
    public TextMeshProUGUI Dialog_text;

    public Animator pannelInteract;

    public Animator animator;
    void Start()
    {
        diaogLine = new Queue<string>();
        diaogLine.Clear();
    }


    public void ShowDialogeTrigger()
    {
        pannelInteract.SetBool("isOpen", true);
    }

    public void HideDialogeTrigger()
    {
        pannelInteract.SetBool("isOpen", false);
    }


    public void StartDialog(Dialog dialog)
    {
        player.GetComponent<Player_Input>().isTalking = true;
        animator.SetBool("isOpen", true);
        Debug.Log("Starting Dialog with " +dialog.npcName+ " ");
        Name_text.text = dialog.npcName;

        foreach (string sentence in dialog.sentences)
        {
            diaogLine.Enqueue(sentence);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (diaogLine.Count == 0)
        {
            EndDialog();
            //gameObject.SetActive(false);
            return;
        }

        string sentence = diaogLine.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));
        //Dialog_text.text = sentence;
        Debug.Log(sentence);
    }

    IEnumerator TypeSentece(string sentemce)
    {
        Dialog_text.text = "";
        foreach (char letter in sentemce.ToCharArray())
        {
            Dialog_text.text += letter;
            yield return null;
        }
    }

    public void EndDialog()
    {
        diaogLine.Clear();
        animator.SetBool("isOpen", false);
        player.GetComponent<Player_Input>().isTalking = false;
        player.GetComponent<Player_Input>().stillTalking = false;
        // Diable the Dialog Panel
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
