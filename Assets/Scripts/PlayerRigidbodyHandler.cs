using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigidbodyHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeedDefault;
    [SerializeField] private float jumpForceDefault;
    [SerializeField] private float gravityScaleDefault;
    [SerializeField] private float linearDragDefault;
    [SerializeField] private float angularDragDefault;

    [SerializeField] private float antiGravityMoveForce;
    [SerializeField] private float antiGravityJumpForce;
    [SerializeField] private float antiGravityScale;
    [SerializeField] private float antiGravityLinearDrag;
    [SerializeField] private float antiGravityAngularDrag;

    private PlayerMovement player;
    private Rigidbody2D rb;

    private bool antiGravityOn = false;
    public bool AntiGravityOn { get { return antiGravityOn; } set { antiGravityOn = value; } }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapGravity(bool antiGravity)
    {
        antiGravityOn = antiGravity;
        if(antiGravity)
        {
            rb.gravityScale = antiGravityScale;
            rb.drag = antiGravityLinearDrag;
            rb.angularDrag = antiGravityAngularDrag;
            player.MoveSpeed = antiGravityMoveForce;
            player.JumpForce = antiGravityJumpForce;
        }
        else
        {
            rb.gravityScale = gravityScaleDefault;
            rb.drag = linearDragDefault;
            rb.angularDrag = angularDragDefault;
            player.MoveSpeed = moveSpeedDefault;
            player.JumpForce = jumpForceDefault;
        }
    }
}
