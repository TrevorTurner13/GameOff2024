using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePrefab; //To be set in inspector
    private bool dialogueTriggered = false;

    public Animator animator;
    public bool isTransmitting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!dialogueTriggered)
            {
                dialogueTriggered = true;
                dialoguePrefab.SetActive(true);
                if (animator != null)
                {
                    animator.SetBool("isTransmitting", true);
                }
                isTransmitting = true;
            }
        }
    }
}
