using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decontamination : MonoBehaviour
{

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Doors leftDoor;
    [SerializeField] private Doors rightDoor;

    [SerializeField] private float airlockDuration;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _particleSystem.Play();
        leftDoor.Airlocked = true;
        rightDoor.Airlocked = true;
        StartCoroutine(AirLock());
    }

    private IEnumerator AirLock()
    {
        yield return new WaitForSeconds(airlockDuration);
        leftDoor.Airlocked = false;
        rightDoor.Airlocked = false;
    } 
}
