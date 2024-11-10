using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Renderer rend;
    [SerializeField] private PlayerMovement player;
    public enum ScrollState
    {
        Scrolling,
        FollowPlayer
    }

    [SerializeField] ScrollState currentState = ScrollState.Scrolling;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == ScrollState.Scrolling)
        {
            float offset = Time.time * scrollSpeed;
            rend.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0)); //Horizontal Scroll for URP
        }
        else if(currentState == ScrollState.FollowPlayer && player != null)
        {
            
            Vector2 currentOffset = rend.material.mainTextureOffset;
            
            if (player.GetComponent<Rigidbody2D>().velocity.x > 0) 
            {
                float offset = currentOffset.x + (scrollSpeed * player.GetComponent<Rigidbody2D>().velocity.x);
                rend.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0)); 
            }
            else if (player.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                float offset = currentOffset.x + (scrollSpeed * player.GetComponent<Rigidbody2D>().velocity.x);
                rend.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0)); 
            }
        }
    }
}
