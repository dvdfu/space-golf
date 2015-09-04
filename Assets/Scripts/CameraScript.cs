using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform golfball;
	public Transform background1;
	public Transform background2;
	private Vector3 oldPos;

	void Start () {
		oldPos = transform.position;
	}
	
	void Update () {
		Vector3 diff = golfball.position - oldPos;
		diff /= 20;
		diff.z = 0;
		transform.Translate (diff);

		float d = golfball.GetComponent<BallScript>().distToFlag;
		GetComponentInChildren<Camera> ().orthographicSize = d / 30 + 2;

		background1.Translate (diff / 2);
		background2.Translate (diff / 4);
		oldPos = transform.position;
	}
}
