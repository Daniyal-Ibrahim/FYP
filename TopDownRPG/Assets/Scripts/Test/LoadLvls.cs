using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvls : MonoBehaviour
{
    public GameObject game;
    public GameObject audioManager;
    public Inventory_Master inv;
    public Inventory_Master equ;
    public static LoadLvls instance;
    IEnumerator DisableUI()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        //game.GetComponent<GameManager>().InitialSave();
        //UI.SetActive(false);
    }

    IEnumerator EnemyList()
    {
        yield return new WaitForSecondsRealtime(0.005f);
        //game.GetComponent<GameManager>().InitialSave();
        game.GetComponent<GameManager>().GetEnemyList();
        //if (!game.GetComponent<GameManager>().newGame)
        //{
        //    game.GetComponent<GameManager>().LoadGame();
        //}
    }
    private void Awake()
    {

        audioManager = GameObject.Find("Advanced Audio Manager");
        game = GameObject.Find("GameManager");
        //UI.SetActive(false); 
        //StartCoroutine("DisableUI");
        StartCoroutine("EnemyList");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        inv.Save();
        equ.Save();

        //UI.SetActive(true);
        game.GetComponent<GameManager>().SaveGame();
        game.GetComponent<GameManager>().lvlLoaded = false;
        game.GetComponent<GameManager>().MeleeEnemy.Clear();
        game.GetComponent<GameManager>().worldItems.Clear();
        audioManager.GetComponent<AdvancedAudioManager>().StopSound("Ambience");


        //SceneManager.LoadScene(0);
    }

    public void SaveGameUI()
    {
        game.GetComponent<GameManager>().SaveGame();
    }

    public void LoadGameUI()
    {
        game.GetComponent<GameManager>().LoadGame();
    }
}   
