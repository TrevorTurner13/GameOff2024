using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject topDoor;
    [SerializeField] private GameObject bottomDoor;

    [SerializeField] private Vector2 topClosePos;
    [SerializeField] private Vector2 bottomClosePos;
    [SerializeField] private Vector2 topOpenPos;
    [SerializeField] private Vector2 bottomOpenPos;

    [SerializeField] private float duration;
    [SerializeField] private float timer;

    private bool isOpen;
    private bool isLocked;
    private bool open;
    private bool close;

    public bool Airlocked {  get; set; }

    // Start is called before the first frame update
    void Start()
    {
        topClosePos = topDoor.transform.position;
        bottomClosePos = bottomDoor.transform.position;
        topOpenPos = topClosePos + new Vector2(0, 1f);
        bottomOpenPos = bottomClosePos + new Vector2(0, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Airlocked)
        {
            if (open && !isOpen)
            {
                OpenDoor();
            }
            else if (close && isOpen)
            {
                CloseDoor();
            }
        }
    }

    public void Interact()
    {
       
    }

    public void OpenDoor()
    {
        timer += Time.deltaTime;
        if(timer < duration)
        {
            topDoor.transform.position = Vector2.Lerp(topClosePos, topOpenPos, (timer/duration));
            bottomDoor.transform.position = Vector2.Lerp(bottomClosePos, bottomOpenPos, (timer/duration));
        }
        else
        {
            topDoor.transform.position = topOpenPos;
            bottomDoor.transform.position = bottomOpenPos;
            timer = 0;
            isOpen = true;
            open = false;
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            timer += Time.deltaTime;
            if (timer < duration)
            {
                topDoor.transform.position = Vector2.Lerp(topOpenPos, topClosePos, timer / duration);
                bottomDoor.transform.position = Vector2.Lerp(bottomOpenPos, bottomClosePos, timer / duration);
            }
            else
            {
                topDoor.transform.position = topClosePos;
                bottomDoor.transform.position = bottomClosePos;
                timer = 0;
                isOpen = false;
                close = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            open = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            close = true;
        }
    }
}

