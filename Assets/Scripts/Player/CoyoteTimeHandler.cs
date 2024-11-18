using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteTimeHandler : MonoBehaviour
{
    [SerializeField] private float coyoteDuration;
    [SerializeField] private float lastOnGroundTime;
    public bool CoyoteTime {  get; set; }

    private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!player.IsGrounded())
        {
            lastOnGroundTime += Time.deltaTime;
            if(lastOnGroundTime > coyoteDuration)
            {
                CoyoteTime = false;
            }
        }
        else
        {
            CoyoteTime = true;
            lastOnGroundTime = 0;
        }
    }
}
