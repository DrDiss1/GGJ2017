using UnityEngine;
using System.Collections;

public class ParallaxSystem : MonoBehaviour {

    public Transform[] sprites;
    public float warpStartX;
    public float warpEndX;
    public float speed;

    private static float tweenSpeed = 0.005f;

	// Use this for initialization
	void Start () {
	
	}

    public void changeSpeed(float newSpeed)
    {
        StartCoroutine(speedTween(newSpeed));
    }

    private IEnumerator speedTween(float newSpeed)
    {
        bool gate = true;
        bool direction = false;
        if(speed < newSpeed)
        {
            direction = true;
        } else if (speed >= newSpeed)
        {
            direction = false;
        }
        while (gate)
        {
            if(speed <= newSpeed + tweenSpeed && speed >= newSpeed - tweenSpeed)
            {
                speed = newSpeed;
                gate = false;
            } else
            {
                if (direction)
                {
                    speed += tweenSpeed;
                } else
                {
                    speed -= tweenSpeed;
                }
            }
            yield return 0;
        }
    }
	
	// Update is called once per frame
	void Update () {

        foreach(Transform sprite in sprites)
        {
            if(sprite.position.x <= warpEndX)
            {
                sprite.position = new Vector3(warpStartX - speed - (warpEndX - sprite.position.x),sprite.position.y,sprite.position.z);
            } else
            {
                sprite.position = new Vector3(sprite.position.x - speed, sprite.position.y, sprite.position.z);
            }
        }
	
	}
}
