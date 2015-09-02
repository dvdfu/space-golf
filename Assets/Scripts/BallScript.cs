using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public Transform Golfer;
	public Transform Line;
	public Transform UI;
	public float hitForce = 2;

	private Transform player;
	private Transform line;
	private bool halted = false;
	private bool grounded = false;
	private Vector2 anchor;
	private Vector2 dist;
	private float airTime = 0;
	private Vector3 respawn;

	void Start () {
		player = Instantiate (Golfer) as Transform;
		player.parent = transform;
		line = Instantiate (Line) as Transform;
		line.parent = transform;
		line.position = transform.position;
	}

	void Update () {
		if (grounded) {
			if (rigidbody2D.velocity.magnitude < 0.1) {
				Halt ();
			}
		} else {
			airTime += Time.deltaTime;
			if (airTime > 5) {
				// reset TODO
			}
		}

		if (transform.position.x * transform.position.x + transform.position.x * transform.position.x > 100) {
			transform.position = respawn;
		}
		
		SetLineLength(0);
		if (halted) {
			if (anchor != Vector2.zero) {
				Vector2 mouse = Input.mousePosition;
				dist = mouse - anchor;
				if (Input.GetMouseButtonUp (0)) {
					Hit ();
					if (UI != null) {
						UI.GetComponent<UIScript>().Stroke();
					}
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
		if (col.gameObject.name == "Planet" || col.gameObject.name == "Planet(Clone)") {
			respawn = transform.position;
			player.GetComponent<RotateScript> ().planet = col.gameObject.transform;
			float angle = Mathf.Atan2 (transform.position.y - col.gameObject.transform.position.y, transform.position.x - col.gameObject.transform.position.x);
			player.GetComponent<RotateScript> ().angle = angle * 180 / Mathf.PI + 5;
			grounded = true;
		} else if (col.gameObject.name == "Sun") {
			transform.position = respawn;
		}
	}
	
	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.name == "Planet" || col.gameObject.name == "Planet(Clone)") {
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
		// GetComponent<GravityScript> ().enabled = false;
	}

	void Hit() {
		halted = false;
		// GetComponent<GravityScript> ().enabled = true;
		grounded = false;
	}
}
