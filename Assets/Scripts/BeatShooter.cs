using UnityEngine;
using System.Collections;
using System;

public class BeatShooter : MonoBehaviour, AudioProcessor.AudioCallbacks {

    public GameObject projectile;

    public void onOnbeatDetected()
    {
        Debug.Log("Beat detected");
        Instantiate(projectile, transform.position + new Vector3(0, UnityEngine.Random.Range(-5,5), 0), Quaternion.identity);
    }

    public void onSpectrum(float[] spectrum)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
