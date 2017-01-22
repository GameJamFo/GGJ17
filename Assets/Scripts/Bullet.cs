﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Bullet : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    void Start()
    {
        AudioProcessor processor = GetComponent<AudioProcessor>();
        processor.addAudioCallback(this);
    }

    public void onOnbeatDetected()
    {
        Debug.Log("Beat!!!");
    }

    public void onSpectrum(float[] spectrum)
    {
        for (int i = 0; i < spectrum.Length; ++i)
        {
            float highNote = spectrum.Sum(x => x);
            GetComponentInChildren<Light>().intensity = highNote * 40;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<Enemy>().Hit();
        }
    }
}