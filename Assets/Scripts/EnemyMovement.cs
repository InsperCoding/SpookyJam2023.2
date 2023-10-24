using UnityEngine;
using UnityEngine.AI;

public class PerseguirJogador : MonoBehaviour
{
    public Transform jogador;
    private NavMeshAgent agente;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (jogador == null)
        {
            Debug.LogWarning("Jogador não está disponível para perseguição.");
            return;
        }

        agente.SetDestination(jogador.position);
    } 
}
