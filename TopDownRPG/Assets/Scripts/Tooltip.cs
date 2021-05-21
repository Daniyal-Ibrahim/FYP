using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;
    [SerializeField]
    TextMeshProUGUI TMP_text;
    [SerializeField]
    GameObject tooltip;
    [SerializeField]
    Vector2 offset;
    [SerializeField]
    float padding;
    [SerializeField]
    private Canvas canvas;

    private void Awake()
    {
        //canvas = tooltip.GetComponent<Canvas>();
    }

    private void Update()
    {
        //Vector2 mouse = Mouse.current.position.ReadValue();
        //tooltip.transform.position = mouse + offset; //new Vector2(mouse.x /*+ transform.rect.width * Canvas.scaleFactor / 2 */, mouse.y /*+ transform.rect.height * Canvas.scaleFactor*/);

    }

    public void Show_Tooltip(Item item)
    {
        //Debug.Log(item.Name + "has been picked");
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=30>").Append(item.Name).Append("</size>").AppendLine();
        builder.Append("<size=15>").Append("ID").Append(" ");
        builder.Append("<size=15>").Append(item.ID).Append("</size>").AppendLine(); 
        builder.Append("<size=15>").Append(item.description).Append("</size>").AppendLine();
        builder.Append("<size=15>").Append("Stackable").Append(" ");
        builder.Append("<size=15>").Append(item.Stackable).Append("</size>").AppendLine();
        builder.Append("<size=15>").Append("ID").Append(" ");
        builder.Append("<size=15>").Append(item.Type).Append("</size>").AppendLine();
        if (item.properties.Length > 0)
        {
            for (int i = 0; i < item.properties.Length; i++)
            {
                builder.Append("<size=15>").Append(item.properties[i].property).Append(" ");
                builder.Append("<size=15>").Append(item.properties[i].value).Append("</size>").AppendLine();
            }
        }
        

        TMP_text.text = builder.ToString();
        tooltip.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void Hide_Tooltip()
    {
        tooltip.SetActive(false);
    }
}
