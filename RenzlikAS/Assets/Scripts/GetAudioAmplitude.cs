using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//##### SKRYPT W PIERWSZEJ FAZIE -> KONIECZNE POPRAWKI, BO LAGI SIĘ POJAWIĄJĄ :(


public class GetAudioAmplitude : MonoBehaviour
{
    public GameObject audioSourceObject; // obiekt który będzie analizowany
    AudioSource audioSource; // źródło dźwięku przy obiekcie audioSourceObject
    float[] samples; // tablica sampli w audioClipie
    float[] sampleData; // tablica która będzie informacją dla źródła światła
    public long time; // zmienna będąca licznikiem kolejnych wartości przekazywanych do źródła światła
    double lightIntensity; // zmienna kontrolująca natężenie naszego źródła światła
    Light light; //  komponent światła obiektu ----> wywala warning nie wiem dlaczego

    void Start()
    {
        light = GetComponent<Light>(); // komponent Light
        time = 0; // ustawiamy licznik na 0

        audioSource = audioSourceObject.GetComponent<AudioSource>(); // audioSource podpięty do naszego obiektu
        samples = new float[audioSource.clip.samples * audioSource.clip.channels]; // tablica o długości równej ilości próbek nagrania audio
        sampleData = new float[audioSource.clip.samples * audioSource.clip.channels / 1470]; // druga tablica, opis poniżej

        /* Nagrania audio w większości są próbkowane z częstotliwością 44100 Hz
         * częstotliwość odświeżania ekranu to 60 Hz
         * także na każdą klatkę przypada co 735 sampel z tablicy -> 44100 / 60 = 735
         * z racji że mamy dwa kanały mnożymy 735 razy dwa i otrzymujemy 1470
         * sampleData więc przyjmuje jedną wartość na każdą klatkę wyświetlaną na ekranie
         * */

        audioSource.clip.GetData(samples, 0); // pobieramy informacje o naszym źródle dźwięku

        for (int i = 0; i < sampleData.Length; i++) // pętla o tylu przebiegach, ile wartości w tablicy sampleData
        {
            sampleData[i] = samples[i * 1470]; // przypisuje sampleData wartość co 1470 sampli -> 1 wartość na klatke
            if (sampleData[i] < 0) // jeśli wartość ta jest mniejsza od 1 to zmienia znak
                sampleData[i] = sampleData[i] * -1; // zmienia znak
            sampleData[i] = sampleData[i] * 15 + 2; // przemnażamy przez stałe -> lepszy efekt wizualny
        }
    }

    void Update()
    {
        lightIntensity = sampleData[time]; // przypisujemy zmiennej lightIntensity wartość z tablicy sampleData o indeksie time
        light.range = (float)lightIntensity; // zmieniamy lightIntensity na float (bo trzeba) i taką ustawiamy siłę światła przy audioControlerze
        time++; // inkrementujemy wartośc time -> przechodzimy do kolejnej wartości z tablicy
        if (time >= sampleData.Length) // przy skończeniu się nagrania audio wracamy do początku
            time = 0;
        
    }
}
