using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Sprite IdleSprite;
	public Sprite SwingSprite;

	private float swingTimer;

	void Start () {
		swingTimer = 0;
	}
	
	void Update () {
		if (swingTimer > 0) {
			swingTimer -= Time.deltaTime;
		} else {
			swingTimer = 0;
			GetComponent<SpriteRenderer>().sprite = IdleSprite;
		}
		if (swingTimer < 0.45f) {
			GetComponentsInChildren<SpriteRenderer> ()[1].enabled = false;
		}
	}

	public void Swing() {
		GetComponent<AudioSource> ().Play ();
		swingTimer = 0.5f;
		GetComponentsInChildren<SpriteRenderer> ()[1].enabled = true;
		GetComponent<SpriteRenderer>().sprite = SwingSprite;
	}
}
