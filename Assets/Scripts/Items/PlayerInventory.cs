using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerInventory : MonoBehaviour
{

    Dictionary<string, int> itemCount;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = new Dictionary<string, int>();
        itemCount["GRASS"] = 0;
        itemCount["WOOD"] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickup(string id)
    {
        itemCount[id] += 1;
        Debug.Log("PICKED UP: " + id);
    }


    // Check if wee can pay for buildiing a specific structure based on its cost
    public bool CanExpend(Dictionary<string, int> costs)
    {
        foreach (string item in costs.Keys)
        {
            if(itemCount[item] < costs[item])
            {
                return false;
            }
        }

        return true;
    }

    public void Expend(Dictionary<string, int> costs)
    {
        foreach(string item in costs.Keys)
        {
            itemCount[item] -= costs[item];
        }
    }


    public int GetItemCount(string id)
    {
        return itemCount[id];
    }
}
