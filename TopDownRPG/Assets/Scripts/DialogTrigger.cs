using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public GameObject DialogBox;
    public bool talking;
    public bool isNPC;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<DialogManager>().ShowDialogeTrigger();
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<DialogManager>().HideDialogeTrigger();
        FindObjectOfType<DialogManager>().EndDialog();
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
        if (isNPC)
        {
            transform.LookAt(player.transform);
        }
    }

    public void TriggerNextLine()
    {
        FindObjectOfType<DialogManager>().DisplayNextLine();
    }


}
