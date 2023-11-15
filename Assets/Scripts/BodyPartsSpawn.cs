using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelSpawn : MonoBehaviour
{
    public Item shovelItem; // Atribuir a pá (shovel) a este campo no Inspector.
    public GameObject shovelPrefab; // Atribuir o prefab da pá no Inspector.
    public List<Vector3> BodyPositions = new List<Vector3>(); // Lista de posições definidas para spawn da pá.

    // Implementar uma parte do código que impeça a repetição de certas partes do corpo spawnadas
    // Fazer testes com "bolinhas" enquanto estiver sem os assets dos corpos
    void Start()
    {
        if (BodiesPresent == 6){
            SpawnPositionsUpdate();
            SpawnBodyPart();
        }
    }

    int BodiesPresent()
    {
        // Verifica quantos corpos estão presentes no momento
        GameObject[] existingBodies = GameObject.FindGameObjectsWithTag("BodiesTag"); // Tag para identificar as partes de corpo

        // Não há pás no mapa
        return existingBodies.Length;
    }

    void SpawnPositionsUpdate()
    {
        // Define lista de posições fixas das partes de corpo (4 enterrados e 4 caixões)
        // Corpos dentro dos caixões: coord. y = 0.7f
        // Corpos enterrados dos caixões: coord. y = -1.5f
        BodyPositions.Add(new Vector3(143.385f, 0.7f, -34.384f));
        BodyPositions.Add(new Vector3(133.6f, -1.5f, 53.4f));
        BodyPositions.Add(new Vector3(37.39f, -1.5f, 54.47f));
        BodyPositions.Add(new Vector3(76.625f, 0.7f, -34.36f));
        BodyPositions.Add(new Vector3(14.02f, -1.5f, -5.42f));
        BodyPositions.Add(new Vector3(67f, -1.5f, -24.2f));
        BodyPositions.Add(new Vector3(-48.92f, 25.6f, -22.18f));
        BodyPositions.Add(new Vector3(???, ???, ???));
    }

    // Função para spawnar os corpos em uma posições definidas previamente.
    void SpawnBodyPart()
    {
        // Aqui você precisará instanciar o objeto Prefab??? na posição escolhida
        GameObject shovelInstance = Instantiate(Prefab???, posição, Quaternion.identity);
    }
}