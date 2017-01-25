using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Spine.Unity;

public class Player : MonoBehaviour {

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    private int score;
    private SkeletonAnimation skele;

    public float limitUp;
    public float limitDown;
    public float limitLeft;
    public float limitRight;

    private string[] hurtAnimNames =
    {
        "Hurt1",
        "Hurt2",
        "Hurt3"
    };

    public PointArea pointArea;

    [HideInInspector]
    public bool chosen;
    [HideInInspector]
    public bool started = false;

    public Text text;

    // Use this for initialization
    void Start () {
        skele = GetComponent<SkeletonAnimation>();
        skele.state.SetAnimation(0, "Idle", true);
	}

    public void changeScore(int amount)
    {
        score += amount;
        text.text = score.ToString();
    }

    public int getScore()
    {
        return score;
    }

    public void clearScore()
    {
        score = 0;
        text.text = score.ToString();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0) && chosen)
        {
            GameManager manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            manager.clickPlayer.hide();
            manager.chosenSong.begin();
            manager.endButton.hide();
            manager.parallax.changeSpeed(0.1f);
            manager.startTimer(manager.chosenSong.audioComponents[0].GetComponent<AudioSource>().clip.length);
            chosen = false;
            started = true;
            pointArea.gate = true;
            Cursor.visible = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Projectile" || coll.gameObject.tag == "DeadProjectile")
        {
            skele.state.SetAnimation(0,hurtAnimNames[Random.Range(0,hurtAnimNames.Length -1)], false);
            skele.state.AddAnimation(0, "Idle", true, 0f);

            float force = 1000f;
            // Calculate Angle Between the collision point and the player
            Vector2 dir = coll.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
    }

    // Update is called once per frame
    void Update () {

        

        if (started)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            mousePosition = new Vector3(Mathf.Clamp(mousePosition.x,limitLeft,limitRight), Mathf.Clamp(mousePosition.y, limitDown, limitUp),mousePosition.z);

            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }
}
