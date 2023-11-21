using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public float interactionDistance = 3f; // A distância máxima para interagir com a porta.
    public float openAngleLimit = 90f; // Ângulo máximo de abertura da porta.
    
    private bool interacting = false;
    private GameObject targetDoor;
    private float initialAngle;
    private float interactTimer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !interacting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    interacting = true;
                    targetDoor = hit.collider.gameObject;
                    initialAngle = targetDoor.transform.eulerAngles.z;
                }
            }
        }

        if (interacting)
        {
            interactTimer += Time.deltaTime;

            // Mova a porta ao longo do eixo X enquanto a tecla "E" estiver pressionada.
            float moveAmount = Time.deltaTime * 30f; // Ajuste a velocidade de abertura/fechamento conforme necessário.
            float newAngle = targetDoor.transform.eulerAngles.z + moveAmount;

            // Limita o ângulo de abertura da porta.
            newAngle = Mathf.Clamp(newAngle, initialAngle, initialAngle + openAngleLimit);

            targetDoor.transform.eulerAngles = new Vector3(newAngle, 0f, 0f);

            if (interactTimer >= 5f)
            {
                // O jogador segurou "E" por 5 segundos, conclua a abertura/fechamento da porta.
                interacting = false;
                interactTimer = 0f;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // O jogador apertou "E" novamente, inverta a direção (fechar a porta).
                interacting = false;
                interactTimer = 0f;
            }
        }
    }
}
