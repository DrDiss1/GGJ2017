using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Projectile : MonoBehaviour {

    public int framesTillDeath;
    private int ticker = 0;
    public int vertForceMax;
    public int vertForceMin;

	// Use this for initialization
	void Awake () {
        // transform.DOMove(transform.position + new Vector3(-30, 0, 0), 5f).OnComplete(killOff);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000, Random.Range(vertForceMin,vertForceMax)));

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "DeadProjectile")
        {
            //transform.DOKill();
            GetComponent<Renderer>().material.color = Color.red;
            gameObject.tag = "DeadProjectile";
        }
    }

    private void killOff()
    {
        transform.DOScale(0, 0.5f).OnComplete(dead);
    }

    private void killOff(bool fail)
    {
        if (fail)
        {
            transform.DOScale(0, 0.5f).OnComplete(dead);
        }
    }

    private void dead()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {

        if(ticker >= framesTillDeath)
        {
            killOff();
        } else
        {
            ticker++;
        }
	
	}
}
