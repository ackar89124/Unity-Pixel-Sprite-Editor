using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSelection : MonoBehaviour
{
    public Vector2 offset;
    public GameObject SB;
    public PixelController SBcode;
    public int size;
    public float distance;
    public Vector2Int pos;
    //int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(SB.transform.position.x + offset.x, 
            SB.transform.position.y + offset.y, -1);
        SBcode = SB.GetComponent<PixelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement(int i)
    {
        switch (i)
        {
            case 1:     //right
                {
                    if (pos.x < size - 1)
                    {
                        pos.x++;
                        transform.position = new Vector3(transform.position.x
                            + distance, transform.position.y, -1);
                    }
                    break;
                }
            case 2:     //left
                {
                    if (pos.x > 0)
                    {
                        pos.x--;
                        transform.position = new Vector3(transform.position.x
                            - distance, transform.position.y, -1);
                    }
                    break;
                }
            case 3:     //up
                {
                    if (pos.y < size - 1)
                    {
                        pos.y++;
                        transform.position = new Vector3(transform.position.x,
                            transform.position.y + distance, -1);
                    }
                    break;
                }
            case 4:     //down
                {
                    if (pos.y > 0)
                    {
                        pos.y--;
                        transform.position = new Vector3(transform.position.x,
                            transform.position.y - distance, -1);
                    }
                    break;
                }
        }
    }
}
