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

    [SerializeField] private float fallGravity;
    [SerializeField] private float fallAntiGravity;
    [SerializeField] private float fallTimer = 0f;
    [SerializeField] private float fallDuration = 1f;

    private PlayerMovement player;
    private Rigidbody2D rb;

    private bool antiGravityOn = false;
    public bool AntiGravityOn { get { return antiGravityOn; } set { antiGravityOn = value; } }
    public bool HasJetPack { get; private set; }

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

    public void FallGravity()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer < fallDuration)
        {
            if (!antiGravityOn)
            {
                rb.gravityScale = Mathf.Lerp(gravityScaleDefault, fallGravity, (fallTimer/fallDuration));
            }
            else
            {
                rb.gravityScale = Mathf.Lerp(antiGravityScale, fallAntiGravity, (fallTimer / fallDuration));
            }
        }
    }

    public void ResetGravity()
    {
        if (antiGravityOn)
        {
            rb.gravityScale = antiGravityScale;
        }
        else
        {
            rb.gravityScale = gravityScaleDefault;
        }   
        fallTimer = 0;
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
