using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	public Transform planet;
	public float angle;
	
	void Update () {
		if (planet != null) {
			Vector3 pos = planet.GetComponent<PlanetScript>().GetPoint(angle);
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);
			transform.localEulerAngles = new Vector3 (0, 0, angle - 90);
		}
	}
}
