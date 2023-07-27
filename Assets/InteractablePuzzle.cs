using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractablePuzzle : MonoBehaviour
{
    public GameObject interactionIcon;
    public UnityEvent interactionEvent;

    [SerializeField] private bool playerInRange = false;
    private void Start()
    {
        if (interactionIcon != null)
        {
            interactionIcon.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionIcon != null)
            {
                interactionIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionIcon != null)
            {
                interactionIcon.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInRange == true && Input.GetMouseButtonDown(0))
        {
            interactionEvent.Invoke();
        }
    }
}
