using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    private AudioSource hit;
    private void Awake()
    {
        hit = gameObject.AddComponent<AudioSource>();
        hit.playOnAwake = false;
        hit.loop = false;
    }

    public void SetHitSound(AudioClip clip)
    {
        hit.clip = clip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hit.clip!=null)
            hit.Play();
    }
}
