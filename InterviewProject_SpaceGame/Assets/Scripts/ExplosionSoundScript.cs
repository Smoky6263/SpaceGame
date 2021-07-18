using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundScript : MonoBehaviour
{
    private float timeToDestroyObj;
    private AudioSource audioSource;
    void Start()
    {
        timeToDestroyObj = 1f;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        timeToDestroyObj -= Time.deltaTime;

        if (timeToDestroyObj < 0)
            Destroy(gameObject);
    }
}
