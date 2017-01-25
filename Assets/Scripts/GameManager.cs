using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    public AudioSpawnManager chosenSong;

    public UIShow chooseText;
    public ButtonChoose[] buttons;
    public UIShow clickPlayer;
    public UIShow endButton;

    public ParallaxSystem parallax;

    private float timer;
    private bool timing;

	// Use this for initialization
	void Start () {
	
	}

    public void startTimer(float time)
    {
        timer = time;
        timing = true;
    }

    public void songSelected()
    {
        foreach(ButtonChoose button in buttons)
        {
            button.goAway();
        }
        chooseText.hide();
        clickPlayer.show();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().chosen = true;

    }

    private void songFinished()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.chosen = false;
        player.started = false;
        player.pointArea.gate = false;
        player.transform.DOMove(new Vector3(0, -2, 0), 0.5f).SetEase(Ease.InSine);
        Cursor.visible = true;
        foreach (ButtonChoose button in buttons)
        {
            button.comeBack();
        }
        parallax.changeSpeed(0.01f);
        chooseText.show();
        endButton.show();
        chooseText.GetComponent<Text>().text = "You got " + player.getScore() + "! You've made your parents proud. Why don't you play another?";
        clickPlayer.hide();
        player.clearScore();
    }
	
	// Update is called once per frame
	void Update () {

        if (timing)
        {
            if(timer <= 0)
            {
                chosenSong.end();
                songFinished();
                chosenSong = null;
                timing = false;
            } else
            {
                timer -= Time.deltaTime;
            }
            
        }
	
	}
}
