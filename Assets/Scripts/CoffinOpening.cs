using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinOpening : MonoBehaviour
{
    public float interactionDistance = 3f;
    private float interactTimer = 0f;
    private bool interacting = false;
    private bool isOpen = false; // Adicionada variável para rastrear o estado do caixão.
    private GameObject targetCoffin;

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !interacting && !isOpen)
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
            float moveAmount = Time.deltaTime * 1f;
            Vector3 newPosition = targetCoffin.transform.position;
            newPosition.x += moveAmount / 2;
            targetCoffin.transform.position = newPosition;

            if (interactTimer >= 5f)
            {
                isOpen = true; // Define o estado do caixão como aberto.
                interacting = false;
                interactTimer = 0f;
            }

            if (!Input.GetKey(KeyCode.E))
            {
                interacting = false;

                if (isOpen)
                {
                    // Se o caixão estiver aberto e o jogador parou de pressionar "E",
                    // comece a fechar o caixão.
                    interactTimer = 0f;
                    isOpen = false;
                }
            }
        }
    }
}