using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_cursor : MonoBehaviour
{
    public Vector2 mouse;
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        mouse = Mouse.current.position.ReadValue();
        transform.position = mouse;
    }
}
