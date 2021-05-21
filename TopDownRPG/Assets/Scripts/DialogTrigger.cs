using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public GameObject DialogBox;
    public bool talking;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    public void TriggerDialog()
    {
        //DialogBox.SetActive(true);
        transform.LookAt(player.transform);
        FindObjectOfType<DialogManager>().StartDialog(dialog);

    }


}
