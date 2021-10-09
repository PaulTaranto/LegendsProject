using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Inventory inventory;
    public GameObject itemB;
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

        //if button pressed pickup wand (currently set to Z)
        if (Input.GetKeyDown(KeyCode.Z)) {
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
                        itemB = (GameObject)Instantiate(itemB, inventory.slots[i].transform, false);
                        itemB.transform.localScale = new Vector2(0.6f, 0.6f);
                        Destroy(gameObject);
                        break;
                        
                    }
                    else {
                        itemB = (GameObject)Instantiate(itemB, inventory.slots[i].transform, false);
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
            Debug.Log("Collison Active");
            canPickupWand = true;
        }
    }

    //Turn off pickup when not on object
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Collision Not Active");
            canPickupWand = false;
        }
    }
    
    //Runs through the inventory and checks if there is any free slots if collision with item occurs
    //Item is added to Wand slot if free
    void PickupWand() {
        if (canPickupWand) {
            for (int i = 0; i < inventory.slots.Length; i++) {
                if (inventory.isFull[i] == false) {
                    inventory.isFull[i] = true;
                    //Check if item is going into second slot.
                    //Changes size of item in slot to match the box.
                    if (i == 1) {
                        itemB = (GameObject)Instantiate(itemB, inventory.slots[i].transform, false);
                        itemB.transform.localScale = new Vector2(0.6f, 0.6f);
                        Destroy(gameObject);
                        break;
                        
                    }
                    else {
                        itemB = (GameObject)Instantiate(itemB, inventory.slots[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                    
                }
            }
        }
        
    }

}
