using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunfire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject gunFire;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            gunFire.SetActive(true);
        }
        else
        {
            gunFire.SetActive(false);
        }
    }

    void Shoot()
    {

    }
}
