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
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwapSlots();
        }

        currentWand = slots[0].transform.GetChild(0).gameObject;
    }

    void SwapSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log(slots[i]);
        }
    }

    /*//Adds item to list when picked up
    public void AddItem(Item item) {
        itemList.Add(item);
    }*/

    public void AddItem(string s) {
        collectibles.Add(s);
    }
}
