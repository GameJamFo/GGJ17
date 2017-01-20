using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EpicLight : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    void Start()
    {
        //Select the instance of AudioProcessor and pass a reference
        //to this object
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.addAudioCallback(this);
    }


    void Update()
    {

    }

    //this event will be called every time a beat is detected.
    //Change the threshold parameter in the inspector
    //to adjust the sensitivity
    public void onOnbeatDetected()
    {
        Debug.Log("Beat!!!");
    }

    //This event will be called every frame while music is playing
    public void onSpectrum(float[] spectrum)
    {
        //The spectrum is logarithmically averaged
        //to 12 bands

        for (int i = 0; i < spectrum.Length; ++i)
        {
            //Vector3 start = new Vector3(i, 0, 0);
            //Vector3 end = new Vector3(i, spectrum[i], 0);
            //Debug.DrawLine(start, end);
            float highNote = spectrum.OrderByDescending(x => x).FirstOrDefault();
            //Debug.Log(highNote);
            GetComponent<Light>().intensity = highNote * 100;
        }
    }
}
