using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    public Item item;  // Atribuir a pá (shovel) a este campo no Inspector.

    public void InteractWith()
    {
        Debug.Log("Interagiu!");
    }

    private void CollectShovel(){
        
    }
}
