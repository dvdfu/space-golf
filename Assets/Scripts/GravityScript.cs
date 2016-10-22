using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour {
	public Transform planets;
	public float gravity = 4;

	private Vector2 gravityForce;
	
	void Update () {
		gravityForce = Vector2.zero;
		if (planets != null) {
			foreach (Transform t in planets) {
				float mass = t.localScale.x * t.localScale.y;
				Vector2 d = t.position - transform.position;
				gravityForce += gravity * mass * d.normalized / d.magnitude / d.magnitude;
			}
		}
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().AddForce (gravityForce);
	}
}
