using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gunfire : MonoBehaviour
{
    public Animator animator;

    public Transform firePoint;
 
    public GameObject bullet;

    public DialogueTrigger dialogueTrigger;

    [SerializeField] private AudioClip gunfireSFX;

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!dialogueTrigger.isTransmitting)
            {
                SFXManager.instance.PlaySFXClip(gunfireSFX, transform, 1f);

                animator.SetTrigger("IsShooting");

                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }

        }
    }
}
