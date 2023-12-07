using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsSpawn : MonoBehaviour
{
    public Item bpItem;
    public GameObject BodyPartsPrefab;
    public List<Vector3> BodyPositions = new List<Vector3>(); 
    int contador = 0; // Comece com 0 para garantir que 6 posições sejam escolhidas
    List<Vector3> BodyPositionsUsed = new List<Vector3>();

    void Start()
    {
        int B = BodiesPresent();
        if (B == 6)
        {
            SpawnPositionsUpdate();
            SpawnBodyPart();
        }
    }

    int BodiesPresent()
    {
        GameObject[] existingBodies = GameObject.FindGameObjectsWithTag("BodiesTag");
        return existingBodies.Length;
    }

    void SpawnPositionsUpdate()
    {
        BodyPositions.Add(new Vector3(-70f, -27.02431f, 35.6f));
        BodyPositions.Add(new Vector3(153.92f, 1.4f, -0.5f));
        BodyPositions.Add(new Vector3(165.94f, 1.4f, -0.5f));
        BodyPositions.Add(new Vector3(76.625f, 0.7f, 7.7f));
        BodyPositions.Add(new Vector3(14.02f, -1.4f, -5.42f));
        BodyPositions.Add(new Vector3(67f, -1.4f, -24.2f));
    }

    void SpawnBodyPart()
    {
        for (int i = 0; i < 6; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, BodyPositions.Count);
            Vector3 position = BodyPositions[randomNumber];

            // Verifica se a posição já foi usada
            if (!BodyPositionsUsed.Contains(position))
            {
                BodyPositionsUsed.Add(position);
                GameObject bpInstance = Instantiate(BodyPartsPrefab, position, Quaternion.identity);
                contador++;
            }
            else
            {
                i--; // Se a posição já foi usada, tenta novamente na próxima iteração
            }
        }
    }
}
