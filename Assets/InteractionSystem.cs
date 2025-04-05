using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float interactionDistance = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if(interactable.type == Interactable.Type.StartLevel1)
                    {
                        SceneManager.LoadScene("Heist");
                    }
                    else if(interactable.type == Interactable.Type.StartLevel2)
                    {
                        SceneManager.LoadScene("Chase");
                    }
                }
            }
        }
    }
}
