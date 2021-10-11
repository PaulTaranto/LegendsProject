using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Item item;
    private List<Item> itemList;
    public bool[] isFull;
    public GameObject[] slots;
    private Inventory inventory;
    

    public List<string> collectibles = new List<string>();

    //public ItemType itemType;

    private void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("Wand Slot");
        inventory = new Inventory();
    }

    void swapWandSlot() {

    }


    public Inventory() {
        itemList = new List<Item>();

        //AddItem(new Item { itemType = Item.ItemType.Apple});
        Debug.Log(itemList.Count);
        Debug.Log("Inventory");
    }

    /*//Adds item to list when picked up
    public void AddItem(Item item) {
        itemList.Add(item);
    }*/

    public void AddItem(string s) {
        collectibles.Add(s);
    }
}
