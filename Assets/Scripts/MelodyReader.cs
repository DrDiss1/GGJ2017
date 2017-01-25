using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public enum NoteType
{
    Breve, Semibreve, Minim, Crotchet, Quaver, Semiquaver
}

public class MelodyReader : MonoBehaviour {

    public AudioInput audioInput;
    public Text text;
    public Ease ease;

    private int[] heldFrequencies;
    private int ticker = 0;
    private static int frames = 60;
    public int bpm;
    public NoteType noteType;
    private float divider;
    private float initialY;

    private bool first = true;

    public int maxFrequency;
    public int minFrequency;

    // Use this for initialization
    void Start()
    {
        initialY = transform.position.y;
        
        switch (noteType)
        {
            case NoteType.Minim:
                divider = 120f;
                break;
            case NoteType.Crotchet:
                divider = 60f;
                break;
            case NoteType.Quaver:
                divider = 30f;
                break;
            case NoteType.Semiquaver:
                divider = 15f;
                break;
        }
        Debug.Log("Frames = " + (int)((divider / bpm) * frames));
        heldFrequencies = new int[(int)((divider / bpm) * frames)];
        audioInput.audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            int total = (int)audioInput.frequency;
            if (total <= maxFrequency && total >= minFrequency)
            {
                transform.DOMoveY(initialY + (total / (maxFrequency / 10f)), divider / bpm).SetEase(ease);
            }
            first = false;
        } else
        {
            if (ticker >= (int)((divider / bpm) * frames))
            {
                int total = 0;
                foreach (int i in heldFrequencies)
                {
                    total += i;
                }
                total = total / (int)((divider / bpm) * frames);
                Debug.Log("F = " + total);
                if (total <= maxFrequency && total >= minFrequency)
                {
                    transform.DOMoveY(initialY + (total / (maxFrequency / 10f)), divider / bpm).SetEase(ease);
                }
                ticker = 0;
            }
            else
            {
                heldFrequencies[ticker] = (int)audioInput.frequency;
                ticker++;
            }
        }
    }
}