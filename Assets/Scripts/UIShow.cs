using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIShow : MonoBehaviour {

    public Vector2 spot;
    public bool hover;
    RectTransform rect;

	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}

    public void show()
    {
        if (hover)
        {
            rect.DOLocalMove(spot, 0.5f).SetEase(Ease.InQuad).OnComplete(hoverNow);
        } else
        {
            rect.DOLocalMove(spot, 0.5f).SetEase(Ease.InQuad);
        }
        
    }

    private void hoverNow()
    {
        rect.DOLocalMove(new Vector2(spot.x,spot.y + 5), 0.5f).SetEase(Ease.InQuad).SetLoops(-1,LoopType.Yoyo);
    }

    public void hide()
    {
        if (hover)
        {
            rect.DOKill();
        }
        rect.DOLocalMove(new Vector2(0, 400), 0.5f).SetEase(Ease.InQuad);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
