using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Inventory inventory;
    public GameObject itemPickup;
    private bool canPickupWand;

    // Start is called before the first frame update
    /*private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }*/


    private void Update()
    {
        if(inventory == null)
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        //if button pressed pickup itemPickup (currently set to Z)
        if (Input.GetKeyDown(KeyCode.E)) {
            PickupWand();
        }
    }
    
    /*
    //Runs through the inventory and checks if there is any free slots if collision with item occurs
    //Item is added to Wand slot if free
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Collision");
            for (int i = 0; i < inventory.slots.Length; i++) {
                if (inventory.isFull[i] == false) {
                    inventory.isFull[i] = true;
                    //Check if item is going into second slot.
                    //Changes size of item in slot to match the box.
                    if (i == 1) {
                        itemPickup = (GameObject)Instantiate(itemPickup, inventory.slots[i].transform, false);
                        itemPickup.transform.localScale = new Vector2(0.6f, 0.6f);
                        Destroy(gameObject);
                        break;
                        
                    }
                    else {
                        itemPickup = (GameObject)Instantiate(itemPickup, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                    
                }
            }
        }
    }*/

    //Allow pickup when on object
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //Debug.Log("Collison Active");
            if (this.CompareTag("Wand")) {
                //Debug.Log("Wand");
                canPickupWand = true;
            }
            else {
//                Debug.Log("yeet");
                //string s = itemPickup.tag;
                //Debug.Log(s);
                if(!inventory.collectibles.Contains(this.tag))
                {
                    inventory.AddItem(this.tag);
                }
                //Debug.Log(inventory.collectibles.Count);
                Destroy(gameObject);
                //break;
                for (int i = 0; i < inventory.collectibles.Count; i++) {
                    Debug.Log(inventory.collectibles[i]);
                }
                
            }
            
        }
    }

    //Turn off pickup when not on object
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //Debug.Log("Collision Not Active");
            if (this.CompareTag("Wand")) {
                //Debug.Log("Wand Gone");
                canPickupWand = false;
            }
            
        }
    }

    //Runs through the inventory and checks if there is any free slots if collision with item occurs
    //Item is added to Wand slot if free
    void PickupWand()
    {
        if (canPickupWand)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    //Check if item is going into second slot.
                    //Changes size of item in slot to match the box.
                    if (i == 1)
                    {
                        itemPickup = (GameObject)Instantiate(itemPickup, inventory.slots[i].transform, false);
                        itemPickup.transform.localScale = new Vector2(0.6f, 0.6f);
                        Destroy(gameObject);
                        break;

                    }
                    else
                    {
                        itemPickup = (GameObject)Instantiate(itemPickup, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }

                }
            }
        }
    }
}
