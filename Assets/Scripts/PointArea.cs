using UnityEngine;
using System.Collections;

public class PointArea : MonoBehaviour {

    public bool gate = true;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Projectile" && gate)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().changeScore(2);
        }
        if (coll.gameObject.tag == "DeadProjectile" && gate)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().changeScore(1);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
