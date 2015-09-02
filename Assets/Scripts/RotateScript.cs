using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public Transform planet;
	public float angle;
	
	void Update () {
		if (planet != null) {
			float rad = planet.GetComponent<PlanetScript> ().Radius ();
			float x = planet.position.x + rad * Mathf.Cos (angle / 180 * Mathf.PI);
			float y = planet.position.y + rad * Mathf.Sin (angle / 180 * Mathf.PI);
			transform.position = new Vector3 (x, y, transform.position.z);
			transform.localEulerAngles = new Vector3 (0, 0, angle - 90);
		}
	}
}
