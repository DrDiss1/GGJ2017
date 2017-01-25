using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonChoose : MonoBehaviour {

    public AudioSpawnManager audioSpawn;
    public Vector3 spot;

	// Use this for initialization
	void Start () {
	
	}

    public void chosen()
    {
        GameManager manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        manager.songSelected();
        manager.chosenSong = audioSpawn;
    }

    public void goAway()
    {
        GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 400, 0), 0.5f).SetEase(Ease.InQuad).OnComplete(disable);
    }

    public void comeBack()
    {
        GetComponent<RectTransform>().DOLocalMove(spot, 0.5f).SetEase(Ease.InQuad).OnComplete(enable);
    }

    private void disable()
    {
        GetComponent<Button>().interactable = false;
    }

    private void enable()
    {
        GetComponent<Button>().interactable = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
