using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EpicLight : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    void Start()
    {
        AudioProcessor processor = GameObject.Find("GameManager").GetComponent<AudioProcessor>();
        processor.addAudioCallback(this);
        GetComponent<Light>().color = Random.ColorHSV();
    }


    void Update()
    {

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
            GetComponent<Light>().intensity = highNote * 10;
        }
    }
}
