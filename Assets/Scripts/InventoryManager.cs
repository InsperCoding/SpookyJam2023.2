using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    
    public GameObject inventoryItemPrefab;

    void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject newItemGabeObject = Instantiate(inventoryItemPrefab, slot.transform);
    }
}
