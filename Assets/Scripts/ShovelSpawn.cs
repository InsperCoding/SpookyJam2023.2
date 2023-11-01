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
        SpawnPositions.Add(new Vector3(27.092f, 1.54f, -33.79f));
        SpawnPositions.Add(new Vector3(52.76f, 1.54f, 34.33f));
        SpawnPositions.Add(new Vector3(37.452f, 1.54f, 46.68f));
        SpawnPositions.Add(new Vector3(-8.4f, 1.54f, 5.72f));
        SpawnPositions.Add(new Vector3(47.34f, 1.54f, -49.3f));
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

