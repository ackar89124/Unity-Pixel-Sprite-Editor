using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EditorCode : MonoBehaviour
{
    public List<GameObject> sBs;
    public List<PixelController> sBcs = new List<PixelController>();
    public GameObject pS;
    private PixelSelection pSc;
    public int selected = 0;
    public SaveData saveData = new SaveData();
    public string saveName = "save";
    public GameObject spriteBuilderTemp;
    public int spriteLimit;
    public int activeSL = 5;
    public int colours = 4;
    public int selectedColours = 0;

    // Change in PC colour change to editor colour change

    /* 
     * on start it loads a sprite builder with each of the saved textures and 
     * puts them in a line.
     * the editor can select on of the SB and move it to the camera positon
     * when moving it resets all SBs and moves the target one after
     * move selector control from PS to editor
     * selected space can be reset when SB is changed
     * changing sprite should ask if you want to save or reset
     * saving should be done in the editor and save all when modified
     */

    // Start is called before the first frame update
    void Start()
    {
        Creation();
        LoadData();
        pSc = pS.GetComponent<PixelSelection>();
        pSc.SB = sBs[0];
        SwapSB(0);
        //LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            pSc.Movement(3);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pSc.Movement(2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pSc.Movement(4);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pSc.Movement(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sBcs[selected].ChangePixel(pSc.pos, true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            sBcs[selected].ChangePixel(pSc.pos, false);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveAllData();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            LoadData();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwapSB(false);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SwapSB(true);
        }
    }

    public void SwapSB(int s)
    {
        if (true)
        {
            selected = s;
            for (int i = 0; i < sBs.Count; i++)
            {
                sBs[i].transform.position = new Vector2(-1 + i, 1);
            }
            sBs[s].transform.position = Vector3.zero;
        }
    }
    public void SwapSB(bool c)
    {
        if (c)
        {
            if(selected >= activeSL - 1)
            {
                SwapSB(0);
            }
            else
            {
                SwapSB(selected + 1);
            }
        }
        else
        {
            if (selected <= 0)
            {
                SwapSB(activeSL - 1);
            }
            else
            {
                SwapSB(selected - 1);
            }
        }
    }

    public void LoadData()
    {
        if (!File.Exists(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + saveName + ".json")))
        {
            string load = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + saveName + ".json");
            saveData = JsonUtility.FromJson<SaveData>(load);
            /*foreach(GameObject temp in sBs)
            {
                Destroy(temp);
            }*/
            for (int i = 0; i < saveData.sprites.Count; i++)
            {
                if (saveData.sprites[i] == null || saveData.sprites[i] == "")
                {
                    saveData.sprites[i] = "111111111111111111111111111" +
                        "1111111111111111111111111111111111111";
                }
                else
                {
                    sBcs[i].LoadFromFile(saveData.sprites[i]);
                }
            }
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    public void SaveAllData()
    {
        foreach(PixelController controller in sBcs)
        {
            Debug.Log("Saveing: " + controller.gameObject.name + " " + controller.SaveToFile(saveData));
            
        }

        string st = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/"+ saveName + ".json", st);
        Debug.Log("Saved to: " + Application.persistentDataPath);
    }

    public void Creation()
    {
        sBs = new List<GameObject>();
        sBcs = new List<PixelController>();

        Debug.Log("1");
        for (int i = 0; i < spriteLimit; i++)
        {
            Debug.Log("2");
            GameObject temp = Instantiate<GameObject>(spriteBuilderTemp, transform);
            temp.name = "S" + i;
            sBs.Add(temp);
            sBcs.Add(sBs[i].GetComponent<PixelController>());
            sBcs[i].spriteType = i;
            }
    }

    public void ChangeColour(Vector2Int pos, bool up, int i)
    {
        string bank = "0123456789abcdefghijklmnopqrstuvwxyz";
        int temp1 = bank.IndexOf(sBcs[i].data[pos.x][pos.y]);
        if (up)
        {
            if (temp1 > colours)
            {
                temp1 = 0;
            }
            else
            {
                temp1++;
            }
        }
        else
        {
            if (temp1 < 1)
            {
                temp1 = colours + 1;
            }
            else
            {
                temp1--;
            }
        }
        sBcs[i].ChangePixel(pos, temp1);
    }
    public void ChangeColour(Vector2Int pos, int i, char colour)
    {
        string bank = "0123456789abcdefghijklmnopqrstuvwxyz";
        int temp1 = bank.IndexOf(colour);
        sBcs[i].ChangePixel(pos, temp1);
    }
    public void ChangeColour(Vector2Int pos, int i, int col)
    {
        sBcs[i].ChangePixel(pos, col);
    }

    public void ChangeSelectCol(int col)
    {
        selectedColours = col;
    }
    public void ChangeSelectCol(bool up)
    {
        if (up)
        {
            if (selectedColours > colours)
            {
                selectedColours = 0;
            }
            else
            {
                selectedColours++;
            }
        }
        else
        {
            if (selectedColours < 1)
            {
                selectedColours = colours + 1;
            }
            else
            {
                selectedColours--;
            }
        }
    }
}
