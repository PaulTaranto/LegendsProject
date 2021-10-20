using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private Item item;
    private List<Item> itemList;
    public bool[] isFull;
    public GameObject[] slots;

    public GameObject currentWand;

    //private Inventory inventory;
    
    public List<string> collectibles = new List<string>();

    //public ItemType itemType;

    private void Start()
    {
        itemList = new List<Item>();

        //AddItem(new Item { itemType = Item.ItemType.Apple});
        //        Debug.Log(itemList.Count);
        //        Debug.Log("Inventory");
        slots = new GameObject[2];
        slots[0] = GameObject.FindGameObjectWithTag("Wand Slot");
        slots[1] = GameObject.FindGameObjectWithTag("Wand Slot 2");
        //inventory = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapSlots();
        }

        try
        {
            if (slots[0].transform.GetChild(0) != null)
            {
                currentWand = slots[0].transform.GetChild(0).gameObject;
            }
        }
        catch
        {
            Debug.Log("Not enjoying this habit of just try catching my issues away. This shouldn't get an issue so long as the inventory functionality is preserved");
        }
    }

    void SwapSlots()
    {
        GameObject temp = Instantiate(slots[1].transform.GetChild(0).gameObject, slots[0].transform, false);
        GameObject beans = Instantiate(slots[0].transform.GetChild(0).gameObject, slots[1].transform, false);

        temp.transform.localScale = new Vector2(1f, 1f);
        beans.transform.localScale = new Vector2(0.6f, 0.6f);
        Destroy(slots[0].transform.GetChild(0).gameObject);
        Destroy(slots[1].transform.GetChild(0).gameObject);

        temp.name = temp.name.Substring(0, temp.name.Length - 7);
        beans.name = beans.name.Substring(0, beans.name.Length - 7);

        //temp = slots[1];
        //slots[1] = slots[0];
        //slots[0] = temp;        
    }

    /*//Adds item to list when picked up
    public void AddItem(Item item) {
        itemList.Add(item);
    }*/

    public void AddItem(string s) {
        collectibles.Add(s);
    }
}
