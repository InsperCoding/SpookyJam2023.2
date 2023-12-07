using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelSpawn : MonoBehaviour
{
    public Item shovelItem; // Atribuir a pá (shovel) a este campo no Inspector.
    public GameObject shovelPrefab; // Atribuir o prefab da pá no Inspector.
    public List<Vector3> SpawnPositions = new List<Vector3>(); // Lista de posições definidas para spawn da pá.

    void Start()
    {
        if (NoShovelsPresent()){
            SpawnPositionsUpdate();
            SpawnShovel();
        }
    }

    bool NoShovelsPresent()
    {
        // Verifica se não há pás presentes no momento
        GameObject[] existingShovels = GameObject.FindGameObjectsWithTag("ShovelTag"); // Tag para identificar a pá

        // Não há pás no mapa
        return existingShovels.Length == 0;
    }

    void SpawnPositionsUpdate()
    {
        // Define lista de posições fixas da pá
        SpawnPositions.Add(new Vector3(28.82f, 1.2f, -31.66f));
        SpawnPositions.Add(new Vector3(37.37f, 1.2f, 46.76f));
        SpawnPositions.Add(new Vector3(149.74f, 1.2f, -31.75f));
        SpawnPositions.Add(new Vector3(169.51f, 1.2f, -0.41f));
        SpawnPositions.Add(new Vector3(135.52f, 1.2f, 44.63f));
        SpawnPositions.Add(new Vector3(89.57f, 1.2f, -48.17f));
        SpawnPositions.Add(new Vector3(82.8f, 1.2f, 2.78f));
        SpawnPositions.Add(new Vector3(102.28f, 1.2f, 36.93f));
        SpawnPositions.Add(new Vector3(133.56f, 1.2f, 46.08f));
        SpawnPositions.Add(new Vector3(38.53f, 1.2f, 47.28f));
        SpawnPositions.Add(new Vector3(2.18f, 1.2f, 55.78f));
        SpawnPositions.Add(new Vector3(-8.95f, 1.2f, 5.14f));
    }

    // Função para spawnar a pá (shovel) em uma posição definida previamente.
    void SpawnShovel()
    {
        int randomNumber = UnityEngine.Random.Range(0, SpawnPositions.Count);
        Vector3 posição = SpawnPositions[randomNumber];
        
        // Aqui você precisará instanciar o objeto shovelPrefab na posição escolhida
        GameObject shovelInstance = Instantiate(shovelPrefab, posição, Quaternion.identity);

        InteractableObj interactable = shovelInstance.GetComponent<InteractableObj>();
        if (interactable != null)
        {
            interactable.item = shovelItem;
        }
    }
}

