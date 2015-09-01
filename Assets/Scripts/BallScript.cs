using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public float hitForce = 2;
	
	private Transform line;
	private bool halted = false;
	private bool grounded = false;
	private Vector2 anchor;
	private Vector2 dist;

	void Start () {
		line = transform.Find ("line");
	}

	void Update () {
		if (grounded) {
			if (rigidbody2D.velocity.magnitude < 0.1) {
				Halt ();
			}
		}
		
		SetLineLength(0)	;
		if (halted) {
			if (anchor != Vector2.zero) {
				Vector2 mouse = Input.mousePosition;
				dist = mouse - anchor;
				if (Input.GetMouseButtonUp (0)) {
					Hit ();
					rigidbody2D.AddForce(-dist*hitForce);
					anchor = Vector2.zero;
				}

				SetLineLength(dist.magnitude);
				line.localEulerAngles = new Vector3(0, 0, Mathf.Atan2(-dist.y, -dist.x) * 180 / Mathf.PI);

			} else if (Input.GetMouseButtonDown (0)) {
				anchor = Input.mousePosition;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Planet") {
			grounded = true;
		}
	}
	
	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.name == "Planet") {
			grounded = false;
		}
	}

	void SetLineLength(float length) {
		Vector3 lineScale = line.localScale;
		lineScale.x = length / 16;
		line.localScale = lineScale;
	}

	void Halt() {
		halted = true;
//		GetComponent<GravityScript> ().enabled = false;
	}

	void Hit() {
		halted = false;
//		GetComponent<GravityScript> ().enabled = true;
		grounded = false;
	}
}
