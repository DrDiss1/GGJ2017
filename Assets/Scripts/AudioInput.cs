using UnityEngine;
using System.Collections;

public class AudioInput : MonoBehaviour {

    public float sensitivity = 100.0f;
    [HideInInspector]
    public float loudness = 0.0f;
    [HideInInspector]
    public float frequency = 0.0f;
    public int samplerate = 4096;
    public AudioSource audioSource;
    public string micName;

    void Start()
    {
    }

    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
        frequency = GetFundamentalFrequency();
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    float GetFundamentalFrequency()
    {
        float fundamentalFrequency = 0.0f;
        float[] data = new float[8192];
        audioSource.GetSpectrumData(data, 0, FFTWindow.BlackmanHarris);
        float s = 0.0f;
        int i = 0;
        for (int j = 1; j < 8192; j++)
        {
            if (s < data[j])
            {
                s = data[j];
                i = j;
            }
        }
        fundamentalFrequency = i * samplerate / 8192;
        return fundamentalFrequency;
    }
}
