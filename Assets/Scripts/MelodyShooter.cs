using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MelodyShooter : MonoBehaviour {

    public AudioInput audioInput;
    public Ease ease;

    private int[] heldFrequencies;
    private int ticker = 0;
    private static int frames = 60;
    public int bpm;
    public NoteType noteType;
    private float divider;
    private float initialY;

    private bool first = true;

    private bool gate = false;

    public int maxFrequency;
    public int minFrequency;

    public GameObject projectile;

    public bool debug;

    // Use this for initialization
    void Start()
    {
        switch (noteType)
        {
            case NoteType.Breve:
                divider = 480f;
                break;
            case NoteType.Semibreve:
                divider = 240f;
                break;
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
    }

    public void releaseGate()
    {
        gate = !gate;
    }

    // Update is called once per frame
    void Update()
    {
        if (gate)
        {
            if (first)
            {
                int total = (int)audioInput.frequency;
                if (total <= maxFrequency && total >= minFrequency)
                {
                    //shoot a thing!
                    Instantiate(projectile, transform.position + new Vector3(0, total / (maxFrequency / 5f), 0), Quaternion.identity);
                }
                first = false;
            }
            else
            {
                if (ticker >= (int)((divider / bpm) * frames))
                {
                    int total = (int)audioInput.frequency;
                    if (total <= maxFrequency && total >= minFrequency)
                    {
                        if (debug)
                        {
                            Debug.Log(name + ": F = " + total);
                        }
                        
                        //shoot a thing!
                        Instantiate(projectile, transform.position + new Vector3(0, total / (maxFrequency / 5f), 0), Quaternion.identity);
                    }
                    ticker = 0;
                }
                else
                {
                    ticker++;
                }
            }
        }
        
    }
}