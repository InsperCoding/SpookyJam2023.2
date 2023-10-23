using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    [Header("UI")]
    public Image image;
    
    [HideInInspector] public Item item;

    public void InitializeItem(Item newItem) {
        item = newItem;
        image.sprite = newItem.image;
    }
}