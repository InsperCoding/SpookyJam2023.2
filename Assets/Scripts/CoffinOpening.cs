using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinOpening : MonoBehaviour
{
    public float interactionDistance = 3f; // A distância máxima para interagir com o caixão.
    private float interactTimer = 0f;
    private bool interacting = false;
    private GameObject targetCoffin;

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !interacting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Coffin"))
                {
                    interacting = true;
                    targetCoffin = hit.collider.gameObject;
                }
            }
        }

        if (interacting)
        {
            interactTimer += Time.deltaTime;

            // Mova o caixão ao longo do eixo X enquanto a tecla "E" estiver pressionada.
            float moveAmount = Time.deltaTime * 1f; // Ajuste a velocidade de abertura do caixão conforme necessário.
            Vector3 newPosition = targetCoffin.transform.position;
            newPosition.x += moveAmount/2;
            targetCoffin.transform.position = newPosition;

            if (interactTimer >= 5f)
            {
                // O jogador segurou "E" por 5 segundos, conclua a abertura do caixão.
                interacting = false;
                interactTimer = 0f;
            }

            if (!Input.GetKey(KeyCode.E))
            {
                // O jogador parou de pressionar "E", pare a interação.
                interacting = false;
                interactTimer = 0f;
            }
        }
    }
}
