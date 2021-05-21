using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Xml;

[System.Serializable]
public class Save
{
    // player information
    public float playerX; // X CORDINATE
    public float playerY; // HEIGHT
    public float playerZ; // Z CORDINATE

    // Items near player
    public List<float> itemX = new List<float>();
    public List<float> itemY = new List<float>();
    public List<float> itemZ = new List<float>();
    public List<bool> pickedup = new List<bool>();
    public int TotalItems;

    // enemy information
    public List<float> enemyX = new List<float>();
    public List<float> enemyY = new List<float>();
    public List<float> enemyZ = new List<float>();
    public List<bool> isDead = new List<bool>();

    // Current lvl
    public string currentlvl;
}

public class GameManager : MonoBehaviour
{ 
    public GameObject player;
    public Inventory_Master inv;
    public Inventory_Master equ;
    public List<GameObject> worldItems = new List<GameObject>();
    public List<EnemyAi> MeleeEnemy = new List<EnemyAi>();
    public EnemyAi MeleeEnemyPrefab;
    public GameObject dummyItem;
    public static GameManager instance;
    public bool gotData = false;
    
    public bool newGame = false;
    public bool loadGame = false;
    public bool lvlLoaded = false;
    public bool test;
    public bool gotItems, gotEnemies;

    public void setNewGame()
    {
        newGame = true;
    }
    public void unsetNewGame()
    {
        newGame = false;
    }
    public void setloadGame()
    {
        loadGame = true;
    }
    public void unsetloadGame()
    {
        loadGame = false;
    }

    private void Awake()
    {
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

        DontDestroyOnLoad(gameObject);
        //InitialSave();
        /*
        if (save== true)
        {
            inv.Clear();
            equ.Clear();
        }
        else 
        {
            LoadGame();
        }
        //inv.Load();
        //equ.Load();
        */

    }

    public void Test()
    {
        player = GameObject.Find("Player");
        player.GetComponent<Inventroy_Manager>().UpdateEquiptment();
    }
    public void GetEnemyList()
    {

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            EnemyAi AI = obj.GetComponent<EnemyAi>();
            MeleeEnemy.Add(AI);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Item"))
        {
            worldItems.Add(obj);
        }

    }

    public bool initialSave = false;
    public void InitialSave()
    {
        /*
        player = GameObject.Find("Player");
        Save save = new Save();
        // saving player location
        save.playerX = player.gameObject.transform.position.x;
        save.playerY = player.transform.position.y;
        save.playerZ = player.transform.position.z;
        // saving the current lvl
        save.currentlvl = SceneManager.GetActiveScene().name;
        // get world items
        foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("Test"))
        {
            worldItems.Add(Obj);
            save.itemX.Add(Obj.transform.position.x);
            save.itemY.Add(Obj.transform.position.y);
            save.itemZ.Add(Obj.transform.position.z);
            //bool x = Obj.GetComponent<Item_PickUp>().pickedup;
            save.pickedup.Add(false);
            save.TotalItems++;
        }

        string JsonString = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Data.save");
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("Saved");
        initialSave = false;
        */
    }

    public Save createSaveGameObj()
    {
        // things to be saved 
        //player = GameObject.Find("Player");
        player.GetComponent<Inventroy_Manager>().UpdateEquiptment();
        Save save = new Save();
        // saving player location
        save.playerX = player.transform.position.x;
        save.playerY = player.transform.position.y;
        save.playerZ = player.transform.position.z;
        // saving the current lvl
        save.currentlvl = SceneManager.GetActiveScene().name;
        // get world items
        
        foreach (GameObject Obj in instance.worldItems)
        {

            save.TotalItems++;
            //worldItems.Add(Obj);
            save.itemX.Add(Obj.transform.position.x);
            save.itemY.Add(Obj.transform.position.y);
            save.itemZ.Add(Obj.transform.position.z);
            bool x = Obj.GetComponent<Item_PickUp>().pickedup;
            save.pickedup.Add(x);
        }

        foreach (EnemyAi ai in MeleeEnemy)
        {
            save.isDead.Add(ai.isDead);
            save.enemyX.Add(ai.AIPositionX);
            save.enemyY.Add(ai.AIPositionY);
            save.enemyZ.Add(ai.AIPositionZ);
        }



        // save items
        //int x = worldItems.Count;
        //Debug.Log("items detected = " + x);
        //for (int i = 1; i <= 6; i++)
        //{
        //save.itemX[i] = worldItems[i].transform.position.x;
        //save.itemY = worldItems[i].transform.position.y;
        //save.itemZ = worldItems[i].transform.position.z;
        //}
        return save;

    }
    public void SaveGame()
    {
        

        Save save = createSaveGameObj();
        XmlDocument xmlDocument = new XmlDocument();

        #region Create XMLDocument Elements

        XmlElement root = xmlDocument.CreateElement("Save");
        root.SetAttribute("FileName", "Binary was easier");
        /*
        XmlElement coinNumElement = xmlDocument.CreateElement("CoinNum");
        //MARKER Gets or sets the concatenated 串联值 values of the node and all its child nodes.
        coinNumElement.InnerText = save.coinsNum.ToString();//Return string type
        root.AppendChild(coinNumElement);
        */
        XmlElement savedLevelElement = xmlDocument.CreateElement("SavedLevel");
        savedLevelElement.InnerText = save.currentlvl.ToString();
        root.AppendChild(savedLevelElement);

        XmlElement playerPosXElement = xmlDocument.CreateElement("PlayerPositionX");
        playerPosXElement.InnerText = save.playerX.ToString();
        root.AppendChild(playerPosXElement);

        XmlElement playerPosYElement = xmlDocument.CreateElement("PlayerPositionY");
        playerPosYElement.InnerText = save.playerY.ToString();
        root.AppendChild(playerPosYElement);

        XmlElement playerPosZElement = xmlDocument.CreateElement("PlayerPositionZ");
        playerPosZElement.InnerText = save.playerZ.ToString();
        root.AppendChild(playerPosZElement);

        //OPTIONAL ADVANCED
        XmlElement item, itemX, itemY, itemZ, itemPicked;
        for (int i = 0; i < save.TotalItems; i++)
        {
            item = xmlDocument.CreateElement("Item");
            itemX = xmlDocument.CreateElement("ItemPositionX");
            itemY = xmlDocument.CreateElement("ItemPositionY");
            itemZ = xmlDocument.CreateElement("ItemPositionZ");
            itemPicked = xmlDocument.CreateElement("ItemPicked");

            itemX.InnerText = save.itemX[i].ToString();
            itemY.InnerText = save.itemY[i].ToString();
            itemZ.InnerText = save.itemZ[i].ToString();
            itemPicked.InnerText = save.pickedup[i].ToString();

            item.AppendChild(itemX);
            item.AppendChild(itemY);
            item.AppendChild(itemZ);
            item.AppendChild(itemPicked);

            root.AppendChild(item);
        }

        XmlElement enemyMelee, EMX, EMY, EMZ, EMD;
        for (int i = 0; i < save.enemyX.Count; i++)
        {
            enemyMelee = xmlDocument.CreateElement("EnemyMelee");
            EMX = xmlDocument.CreateElement("EnemyPositionX");
            EMY = xmlDocument.CreateElement("EnemyPositionY");
            EMZ = xmlDocument.CreateElement("EnemyPositionZ");
            EMD = xmlDocument.CreateElement("EnemyDead");

            EMX.InnerText = save.enemyX[i].ToString();
            EMY.InnerText = save.enemyY[i].ToString();
            EMZ.InnerText = save.enemyZ[i].ToString();
            EMD.InnerText = save.isDead[i].ToString();

            enemyMelee.AppendChild(EMX);
            enemyMelee.AppendChild(EMY);
            enemyMelee.AppendChild(EMZ);
            enemyMelee.AppendChild(EMD);

            root.AppendChild(enemyMelee);
        }

        #endregion

        xmlDocument.AppendChild(root);

        xmlDocument.Save(Application.dataPath + "/Data.save");
        if (File.Exists(Application.dataPath + "/Data.save"))
        {
            Debug.Log("File Saved");
        }
        player.GetComponent<Inventroy_Manager>().UpdateEquiptment();

        newGame = false;
    }

    public void LoadGame()
    {
        //loadGame = false;
        string filePath = Application.dataPath + "/Data.save";
        if (File.Exists(filePath))
        {

            Save save = new Save();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);


            #region Get Save Data from File



            XmlNodeList playerX = xmlDocument.GetElementsByTagName("PlayerPositionX");
            float playerPosXNum = float.Parse(playerX[0].InnerText);
            save.playerX = playerPosXNum;

            XmlNodeList playerY = xmlDocument.GetElementsByTagName("PlayerPositionY");
            float playerPosYNum = float.Parse(playerY[0].InnerText);
            save.playerY = playerPosYNum;

            XmlNodeList playerZ = xmlDocument.GetElementsByTagName("PlayerPositionZ");
            float playerPosZNum = float.Parse(playerZ[0].InnerText);
            save.playerZ = playerPosZNum;


            XmlNodeList items = xmlDocument.GetElementsByTagName("Item");

            // OPTIONAL 

            if (items.Count != 0)
            {
                //foreach xmlNode item in items
                for (int i = 0; i < items.Count; i++)
                {
                    XmlNodeList itemX = xmlDocument.GetElementsByTagName("ItemPositionX");
                    float itemPositionX = float.Parse(itemX[i].InnerText);
                    save.itemX.Add(itemPositionX);

                    XmlNodeList itemY = xmlDocument.GetElementsByTagName("ItemPositionY");
                    float itemPositionY = float.Parse(itemY[i].InnerText);
                    save.itemY.Add(itemPositionY);

                    XmlNodeList itemZ = xmlDocument.GetElementsByTagName("ItemPositionZ");
                    float itemPositionZ = float.Parse(itemZ[i].InnerText);
                    save.itemZ.Add(itemPositionZ);

                    XmlNodeList itemPicked = xmlDocument.GetElementsByTagName("ItemPicked");
                    bool itemIsPicked = bool.Parse(itemPicked[i].InnerText);
                    save.pickedup.Add(itemIsPicked);

                }
            }

            XmlNodeList enemyMelee = xmlDocument.GetElementsByTagName("EnemyMelee");
           
            Debug.Log(enemyMelee.Count + " Melee Enemies present");
            if (enemyMelee.Count != 0)
            {
                //foreach xmlNode item in items
                for (int i = 0; i < enemyMelee.Count; i++)
                {
                    XmlNodeList EMX = xmlDocument.GetElementsByTagName("EnemyPositionX");
                    float EnemyX = float.Parse(EMX[i].InnerText);
                    save.enemyX.Add(EnemyX);

                    XmlNodeList EMY = xmlDocument.GetElementsByTagName("EnemyPositionY");
                    float EnemyY = float.Parse(EMY[i].InnerText);
                    save.enemyY.Add(EnemyY);

                    XmlNodeList EMZ = xmlDocument.GetElementsByTagName("EnemyPositionZ");
                    float EnemyZ = float.Parse(EMZ[i].InnerText);
                    save.enemyZ.Add(EnemyZ);

                    XmlNodeList EMD = xmlDocument.GetElementsByTagName("EnemyDead");
                    bool EnemyDead = bool.Parse(EMD[i].InnerText);
                    save.isDead.Add(EnemyDead);

                    //MeleeEnemy[i].transform.position = new Vector3(save.enemyX[i], save.enemyY[i], save.enemyZ[i]);
                }
            }

            #endregion



            // player position
            player = GameObject.Find("Player");
            player.transform.position = new Vector3(save.playerX, save.playerY, save.playerZ);


            //item posistion
            for (int i = 0; i < save.pickedup.Count; i++)
            {
                if (instance.worldItems[i] != null)
                {
                    if (save.pickedup[i])
                    {
                        float itemX = save.itemX[i];
                        float itemY = save.itemY[i];
                        float itemZ = save.itemZ[i];
                        //GameObject game = Instantiate(worldItems[i], new Vector3(itemX, itemY, itemZ), Quaternion.identity);
                        worldItems[i].transform.position = new Vector3(itemX, itemY, itemZ);
                        worldItems[i].SetActive(false);
                    }
                    else
                    {
                        float itemX = save.itemX[i];
                        float itemY = save.itemY[i];
                        float itemZ = save.itemZ[i];
                        //GameObject game = Instantiate(worldItems[i], new Vector3(itemX, itemY, itemZ), Quaternion.identity);
                        worldItems[i].transform.position = new Vector3(itemX, itemY, itemZ);
                    }
                }
            }
            if(instance.worldItems.Count > 0)
                gotItems = true;

            for (int i = 0; i < save.isDead.Count; i++)
            {
                if (instance.MeleeEnemy[i] != null)
                {
                    if (save.isDead[i])
                    {
                        float EMX = save.enemyX[i];
                        float EMY = save.enemyY[i];
                        float EMZ = save.enemyZ[i];
                        instance.MeleeEnemy[i].transform.position = new Vector3(EMX, EMY, EMZ);
                        //EnemyAi newAI = Instantiate(MeleeEnemyPrefab, new Vector3(EMX, EMY, EMZ), Quaternion.identity);
                        //instance.MeleeEnemy[i] = newAI;
                    }
                    else
                    {
                        float EMX = save.enemyX[i];
                        float EMY = save.enemyY[i];
                        float EMZ = save.enemyZ[i];
                        instance.MeleeEnemy[i].transform.position = new Vector3(EMX, EMY, EMZ);
                    }
                }
            }
            if(instance.MeleeEnemy.Count > 0)
                gotEnemies = true;
            /*
            StreamReader sr = new StreamReader(Application.dataPath + "/Data.save");
            string JsonString = sr.ReadToEnd();
            sr.Close();
            
            Save save = JsonUtility.FromJson<Save>(JsonString);
            // loading the current lvl
            //SceneManager.LoadScene(save.currentlvl);
            // loading player location
            player = GameObject.Find("Player");
            total = save.TotalItems;
            player.transform.position = new Vector3(save.playerX, save.playerY, save.playerZ);
            // loading items
            for (int i = 0; i < save.TotalItems; i++)
            {
                if (GameManager.instance.worldItems[i] == null)
                {
                    if (!save.pickedup[i])//If this enemy is died, but alive before we press the save button
                    {
                        float PosX = save.itemX[i];
                        float PosY = save.itemY[i];
                        float PosZ = save.itemZ[i];
                        GameObject newItem = Instantiate(dummyItem, new Vector3(PosX,PosY,PosZ), Quaternion.identity);
                        GameManager.instance.worldItems[i] = newItem;//Fill in the position which the Bats elemenet is null
                    }
                }
                else
                {
                    float PosX = save.itemX[i];
                    float PosY = save.itemY[i];
                    float PosZ = save.itemZ[i];
                    GameManager.instance.worldItems[i].transform.position = new Vector3(PosX,PosY,PosZ);
                }

            }
            /*
            foreach (GameObject Obj in GameObject.FindGameObjectsWithTag("Test"))
            {
                Debug.Log(Obj);
                worldItems.Add(Obj);
                //save.itemX.Add(Obj.transform.position.x);
                //save.itemY.Add(Obj.transform.position.y);
                //save.itemZ.Add(Obj.transform.position.z);
                //bool x = Obj.GetComponent<Item_PickUp>().pickedup;
                //save.pickedup.Add(x);
            }
            Debug.Log(save.pickedup.Count);
            for (int i = 0; i < save.pickedup.Count; i++)
            {
                if (save.pickedup[i] == true)
                {
                    worldItems[i].SetActive(false);
                }
                else
                {
                    worldItems[i].transform.position = new Vector3(save.itemX[i], save.itemY[i], save.itemZ[i]);
                }
            }
            Debug.Log("Got items");
            /*
            for (int i = 0; i <= worldItems.Count; i++)
            {
                worldItems[i].GetComponent<Item_PickUp>().pickedup = save.pickedup[i];
                if (save.pickedup[i] == true)
                {
                    //Destroy(worldItems[i]);
                }
                else 
                {
                    //worldItems[i].transform.position.x = save.itemX[i];
                }
            }
            */
            Debug.Log("Loaded");
            inv.Load();
            equ.Load();
            
            //loadGame = false;
        }
        else
        {
            Debug.Log("No Save file found");
        }
    }

    public void FixedUpdate()
    {
        if (gotData == true)
        {
            player = GameObject.Find("Player");
            gotData = true;
        }
        if (newGame == true && lvlLoaded )
        {
            inv.Clear();
            equ.Clear();
            gotData = true;
            initialSave = true;
            SaveGame();
            //Save saveing = new Save();
            //saveing.playerX = 57;
            //saveing.playerY = 23;
            //saveing.playerZ = 44;
            //string JsonStringSave = JsonUtility.ToJson(saveing);
            //StreamWriter sw = new StreamWriter(Application.dataPath + "/Data.save");
            //sw.Write(JsonStringSave);
            //sw.Close();
            //Debug.Log("Saved");

            //StreamReader sr = new StreamReader(Application.dataPath + "/Data.save");
            //string JsonStringLoad = sr.ReadToEnd();
            //sr.Close();

            //Save save = JsonUtility.FromJson<Save>(JsonStringLoad);
            //player = GameObject.Find("Player");
            //player.transform.position = new Vector3(save.playerX, save.playerY, save.playerZ);
        }
        
        if (loadGame == true && lvlLoaded == true && !gotItems && !gotEnemies)
        {
            LoadGame();
        }

        if (test)
        {
            Test();
        }
    }
}
