using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    //public Texture2D texture2D;
    /*public Sprite sprite;
    public SpriteRenderer spriteRenderer;*/
    public List<string> data;
    public int colours = 4;
    public int spriteType = 1;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTexture(data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTexture(List<string> td)
    {
        data = td;
        Texture2D texture = new Texture2D(8, 8);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 8, 8), Vector2.zero);
        GetComponent<SpriteRenderer>().sprite = sprite;
        
        int temp = 0;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++) //Goes through each pixel
            {
                temp = 0;
                temp = data[x][y];
                Color pixelColour = new Color(0, 0, 0, 0);
                if (data[x][y] == '1')
                {
                    pixelColour = new Color(0.1f, 0.1f, 0.1f, 1);
                }
                else if (data[x][y] == '2')
                {
                    pixelColour = new Color(1, 1, 1, 1);
                }
                else if (data[x][y] == '3')
                {
                    pixelColour = new Color(1, 0, 0, 1);
                }
                else if (data[x][y] == '4')
                {
                    pixelColour = new Color(0, 1, 0, 1);
                }
                else if (data[x][y] == '5')
                {
                    pixelColour = new Color(0, 0, 1, 1);
                }
                else
                {
                    pixelColour = new Color(0, 0, 0, 0);
                }
                texture.SetPixel(x, y, pixelColour);
            }
        }
        texture.Apply();
        //texture2D = texture;
    }

    public void ChangePixel(Vector2Int pos, bool up) // change up or down
    {
        string bank = "0123456789abcdefghijklmnopqrstuvwxyz";
        int temp1 = bank.IndexOf(data[pos.x][pos.y]);
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
        char[] chars = data[pos.x].ToCharArray();
        chars[pos.y] = bank[temp1];
        data[pos.x] = new string (chars);
        UpdateTexture(data);
    }
    public void ChangePixel(Vector2Int pos, char c) // change to symbol
    {
        char[] chars = data[pos.x].ToCharArray();
        chars[pos.y] = c;
        data[pos.x] = new string(chars);
        UpdateTexture(data);
    }
    public void ChangePixel(Vector2Int pos, int i) // change to number
    {
        string bank = "0123456789abcdefghijklmnopqrstuvwxyz";
        char[] chars = data[pos.x].ToCharArray();
        chars[pos.y] = bank[i];
        data[pos.x] = new string(chars);
        UpdateTexture(data);
    }

    public string SaveToFile(SaveData save)
    {
        // save data to file
        string temp = "";
        foreach (string s in data)
        {
            temp += s;
        }
        save.sprites[spriteType] = temp;
        return temp;
    }

    /*public void LoadFromFile()
    {
        // load data from file
        string load = System.IO.File.ReadAllText(Application.persistentDataPath + "/save.json");
        SaveData saveData = JsonUtility.FromJson<SaveData>(load);
        // load sprite from file
        int size = (int) Mathf.Sqrt(saveData.sprite1.Length);
        string t = "";
        List<string> list = new List<string>();
        for (int i = 0; i < saveData.sprite1.Length; i++)
        {
            if (i % (size - 0) == 0 && i > 0)
            {
                list.Add(t);
                t = "";
            }
            t += saveData.sprite1[i];
        }
        list.Add(t);
        UpdateTexture(list);
    }*/

    public void LoadFromFile(string d)
    {
        // load sprite from file
        int size = (int)Mathf.Sqrt(d.Length);
        string t = "";
        List<string> list = new List<string>();
        for (int i = 0; i < d.Length; i++)
        {
            if (i % (size - 0) == 0 && i > 0)
            {
                list.Add(t);
                t = "";
            }
            t += d[i];
        }
        list.Add(t);
        UpdateTexture(list);
    }

    public Texture2D ReTexture()
    {
        return gameObject.GetComponent<Texture2D>();
    }
}
