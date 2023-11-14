using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelSpawn : MonoBehaviour
{
    public Item shovelItem; // Atribuir a pá (shovel) a este campo no Inspector.
    public GameObject shovelPrefab; // Atribuir o prefab da pá no Inspector.
    public Vector2Int mapDimensions = new Vector2Int(500, 500);
    public LayerMask obstacleLayer; // Definir a camada que representa obstáculos.

    void Start()
    {
        SpawnShovelRandomly();
    }

    // Função para spawnar a pá (shovel) em uma posição aleatória sem colisões.
    void SpawnShovelRandomly()
    {
        Vector2 randomPosition;
        Collider2D hitCollider;

        do
        {
            randomPosition = new Vector2(Random.Range(0, mapDimensions.x), Random.Range(0, mapDimensions.y));
            hitCollider = Physics2D.OverlapCircle(randomPosition, 1.0f, obstacleLayer);
        } while (hitCollider != null);

        // Cria uma instância da pá (shovel) no local aleatório.
        GameObject shovelInstance = Instantiate(shovelPrefab, randomPosition, Quaternion.identity);

        // Obtém o componente InteractableObj da pá (shovel) para atribuir o item.
        InteractableObj interactable = shovelInstance.GetComponent<InteractableObj>();
        if (interactable != null)
        {
            interactable.item = shovelItem;
        }
    }
}

