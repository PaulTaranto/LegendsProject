using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private Inventory inventory;
    public GameObject itemB;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    //Runs through the inventory and checks if there is any free slots if collision with item occurs
    //Item is added to Wand slot if free
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Collision");
            for (int i = 0; i < inventory.slots.Length; i++) {
                if (inventory.isFull[i] == false) {
                    inventory.isFull[i] = true;
                    Instantiate(itemB, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

}
