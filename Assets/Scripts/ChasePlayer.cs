using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Use a tag do jogador para identificá-lo
        {
            // Quando o perseguidor encosta no jogador, mude de cena
            SceneManager.LoadScene("Game Over"); // Substitua "SuaCenaDeDestino" pelo nome da cena que deseja carregar
        }
    }

}

