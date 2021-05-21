using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_FIxShitThatsBroken : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        var game = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!game.newGame)
        {
            game.LoadGame();
        }
        else
        {
            game.SaveGame();
            game.LoadGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
