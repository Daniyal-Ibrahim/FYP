using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_cursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        transform.position = mouse;
    }
}
