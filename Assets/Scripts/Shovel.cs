using UnityEngine;

public class Shovel : MonoBehaviour
{
    public Item shovelItem; // Atribuir a pá (shovel) a este campo no Inspector.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Certificar de que o jogador tenha uma tag "Player".
        {
            CollectShovel(other.gameObject);
        }
    }

    private void CollectShovel(GameObject player)
    {
        // Adicione a pá (shovel) ao inventário do jogador.
        InventoryManager inventoryManager = player.GetComponent<InventoryManager>();
        if (inventoryManager != null)
        {
            bool addedToInventory = inventoryManager.AddItem(shovelItem);
            if (addedToInventory)
            {
                // Destrua a pá (shovel) da cena após coleta.
                Destroy(gameObject);
            }
        }
    }
}
