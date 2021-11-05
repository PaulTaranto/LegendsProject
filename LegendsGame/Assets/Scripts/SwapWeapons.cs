using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapons : MonoBehaviour
{
    public GameObject[] Equiped;
    public int Selected;

    public GameObject[] Weapons;
    public int WeaponNumber;

    public Inventory inventory;

    bool spawned = false;

    // Update is called once per frame
    void Update()
    {
        if(inventory == null)
        {
            inventory = GetComponent<Inventory>();
        }

        //if (Selected % 2 == 0)
        //{
        //    Equiped[0].SetActive(true);
        //    Equiped[1].SetActive(false);
        //}
        //if (Selected % 2 == 1)
        //{
        //    Equiped[0].SetActive(false);
        //    Equiped[1].SetActive(true);
        //}

        //When Tab it put down
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Selected += 1;
        //}

        if(Input.GetMouseButtonDown(1))
        {
            spawned = false;
        }

        try
        {
            if (!spawned)// == Equiped[Selected % 2])
            {
                if (inventory.currentWand.name == "Fire_wand(Clone)")
                {
                    Destroy(GameObject.FindGameObjectWithTag("Wandy Boy"));
                    Instantiate(Weapons[0]);
                    spawned = true;
                    //WeaponNumber = Selected % 2;
                    ////your wand creation logic goes here
                    //Equiped[Selected % 2] = Instantiate(Weapons[WeaponNumber], transform.position, transform.rotation);
                    //Equiped[Selected % 2].transform.SetParent(transform);
                }
                else if (inventory.currentWand.name == "Crystal_wand(Clone)")
                {
                    Destroy(GameObject.FindGameObjectWithTag("Wandy Boy"));
                    Instantiate(Weapons[1]);
                    spawned = true;
                }
            }
        }
        catch
        {
//            Debug.Log("Nothing in inventory");
        }
    }
    
    public void Change()
    {
        Equiped[Selected % 2] = Weapons[WeaponNumber];

        if (Equiped[0] != Weapons[WeaponNumber] || Equiped[Equiped.Length] != Weapons[WeaponNumber])
        {
            Equiped[Selected % 2] = Instantiate(Weapons[WeaponNumber], transform.position, transform.rotation);
            Equiped[Selected % 2].transform.SetParent(transform);
        }
        
    }
}
