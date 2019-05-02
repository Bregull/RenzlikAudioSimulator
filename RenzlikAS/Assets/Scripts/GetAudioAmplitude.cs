using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudioAmplitude : MonoBehaviour
{
    public GameObject audioSourceObject;
    AudioSource audioSource;
    float[] samples;
    float[] sampleData;
    public long time;
    public double currentSample;
    double lightIntensity;
    Light light;

    void Start()
    {
        light = GetComponent<Light>();
        time = 0;

        audioSource = audioSourceObject.GetComponent<AudioSource>();
        samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        sampleData = new float[audioSource.clip.samples * audioSource.clip.channels / 1470];

        audioSource.clip.GetData(samples, 0);

        for (int i = 0; i < sampleData.Length; i++)
        {
            sampleData[i] = samples[i * 1470];
            if (sampleData[i] < 0)
                sampleData[i] = sampleData[i] * -1;
            sampleData[i] = sampleData[i] * 20 + 1;
        }
        Debug.Log(sampleData.Length);
    }

    void Update()
    {
        lightIntensity = sampleData[time];
        //Debug.Log(sampleData[time] + "   " + time);
        light.range = (float)lightIntensity;

        time++;
        if (time >= sampleData.Length)
            time = 0;
        
    }
}
