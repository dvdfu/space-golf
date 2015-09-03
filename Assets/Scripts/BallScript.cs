using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public Transform Golfer;
	public Transform Line;
	public Transform UI;
	public Transform flag;
	public Transform Explosion;
	public float hitForce = 2;

	private Transform player;
	private Transform line;
	private Transform explosion;
	private Vector2 anchor;
	private Vector2 dist;
	private Vector3 respawn;

	void Start () {
		player = Instantiate (Golfer) as Transform;
		player.parent = transform;
		line = Instantiate (Line) as Transform;
		line.parent = transform;
		line.position = transform.position;
		explosion = Instantiate (Explosion) as Transform;
	}

	void Update () {
		float distance = ((Vector2)(flag.position - transform.position)).magnitude * 10;
		UI.GetComponent<UIScript> ().SetDistance (distance);

		if (transform.position.magnitude > 10) {
			Reset();
		}

		
		if (rigidbody2D.velocity.magnitude < 5) {
			if (distance < 1) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
		SetLineLength(0);
		if (rigidbody2D.velocity.magnitude < 0.1) {
			if (anchor != Vector2.zero) {
				Vector2 mouse = Input.mousePosition;
				dist = mouse - anchor;
				if (Input.GetMouseButtonUp (0)) {
					if (UI != null) {
						UI.GetComponent<UIScript>().Stroke();
					}
					rigidbody2D.AddForce(-(dist*hitForce + dist.normalized*100));
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
			if (player != null) {
				player.GetComponent<RotateScript> ().planet = col.gameObject.transform;
			}
			float angle = Mathf.Atan2 (transform.position.y - col.gameObject.transform.position.y, transform.position.x - col.gameObject.transform.position.x);
			player.GetComponent<RotateScript> ().angle = angle * 180 / Mathf.PI + 5;
		} else if (col.gameObject.name == "Sun" || col.gameObject.name == "Sun(Clone)") {
//			Transform e = Instantiate(Explosion) as Transform;
//			e.position = transform.position;
			// TODO
			explosion.position = transform.position;
			explosion.particleSystem.Emit(30);
			Reset();
		}
	}

	void SetLineLength(float length) {
		Vector3 lineScale = line.localScale;
		lineScale.x = length / 16;
		line.localScale = lineScale;
	}

	void Reset() {
		transform.position = respawn;
		rigidbody2D.velocity = Vector2.zero;
	}
}
