using UnityEngine;
using System.Collections;

public class AudioSpawnManager : MonoBehaviour {

    public MelodyShooter[] audioComponents;
    public BeatShooter[] beats;

	// Use this for initialization
	void Start () {

	}

    public void begin()
    {
        foreach(MelodyShooter shooter in audioComponents)
        {
            shooter.releaseGate();
            shooter.audioInput.audioSource.Play();
        }
        foreach(BeatShooter beat in beats)
        {
            beat.GetComponent<AudioSource>().Play();
        }
    }

    public void end()
    {
        foreach (MelodyShooter shooter in audioComponents)
        {
            shooter.releaseGate();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
