using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudioAmplitude : MonoBehaviour
{
    public GameObject audioSourceObject; // obiekt który będzie analizowany
    public AudioSource audioSource; // źródło dźwięku przy obiekcie audioSourceObject
    float[] samples; // tablica sampli w audioClipie
    float[] sampleData; // tablica która będzie informacją dla źródła światła
    new Light light; //  komponent światła obiektu

    void Start()
    {
        light = GetComponent<Light>(); // komponent Light
        audioSource = audioSourceObject.GetComponent<AudioSource>(); // audioSource podpięty do naszego obiektu
        samples = new float[audioSource.clip.samples * audioSource.clip.channels]; // tablica o długości równej ilości próbek nagrania audio dla każdego kanału
        sampleData = new float[audioSource.clip.samples]; // tablica równa ilości sampli dla jednego kanału
        audioSource.clip.GetData(samples, 0); // pobiera informacje o audioClipie i przypisuje je do tablicy samples

        for (int i = 0; i < sampleData.Length; i++) // wykonuje się tyle razy ile mniejsza z tablic -> dla każdego sampla jednego kanału
        {
            sampleData[i] = samples[audioSource.clip.channels * i]; // przypisuje tablicy dla jednego kanału wartość każdego sampla tego kanału (stąd przemnożenie)
        }
    }

    void Update()
    {
        light.range = System.Math.Abs(sampleData[audioSource.timeSamples] * 7) + 2; // przypisuje zasięgowi światła wartość bezwzględną sampla w danej chwili czasu. stałe są kwestią estetyki
                                                                                    // timeSamples -> znajduje numer sampla ścieżki w zależności od danej chwili w czasie
    }
}