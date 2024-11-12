using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLog : MonoBehaviour, IInteractable
{
    public GameObject dialoguePrefab;
    private bool logUsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (!logUsed)
        {
            logUsed = true;
            dialoguePrefab.SetActive(true);
        }
        
    }
}
