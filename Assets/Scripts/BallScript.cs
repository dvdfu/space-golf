using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	public Transform Golfer;
	public Transform Line;
	public Transform UI;
	public Transform flag;
	public Transform Explosion;
	public float hitForce = 2;
	public float distToFlag;
	public Transform ground;

	private Transform player;
	private Transform line;
	private Transform explosion;
	private Vector2 anchor;
	private Vector2 dist;
	private float respawnAngle;

	void Start () {
		player = Instantiate (Golfer) as Transform;
		player.parent = transform;
		line = Instantiate (Line) as Transform;
		line.parent = transform;
		line.position = transform.position;
		explosion = Instantiate (Explosion) as Transform;
	}

	void Update () {
		distToFlag = ((Vector2)(flag.position - transform.position)).magnitude * 10;
		UI.GetComponent<UIScript> ().SetDistance (distToFlag);

		if (transform.position.magnitude > 15) {
			Reset();
		}
		
		if (GetComponent<Rigidbody2D>().velocity.magnitude < 2) {
			if (distToFlag < 1) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
		SetLineLength(0);
		if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.1) {
			if (player != null) {
				player.GetComponent<RotateScript> ().planet = ground;
				player.GetComponent<RotateScript> ().angle = ground.GetComponent<PlanetScript>().GetAngle(transform.position) + 10;
			}
			if (anchor != Vector2.zero) {
				Vector2 mouse = Input.mousePosition;
				dist = mouse - anchor;
				if (Input.GetMouseButtonUp (0)) {
					if (UI != null) {
						player.GetComponent<PlayerScript>().Swing();
						UI.GetComponent<UIScript>().Stroke();
					}
					GetComponent<Rigidbody2D>().AddForce(-(dist*hitForce + dist.normalized*100));
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
			respawnAngle = col.gameObject.GetComponent<PlanetScript>().GetAngle (transform.position);
			ground = col.gameObject.transform;
		} else if (col.gameObject.name == "Sun" || col.gameObject.name == "Sun(Clone)") {
			explosion.position = transform.position;
			explosion.GetComponent<ParticleSystem>().Emit(60);
			GetComponent<AudioSource>().Play ();
			Reset();
		}
	}

	void SetLineLength(float length) {
		Vector3 lineScale = line.localScale;
		lineScale.x = length / 16;
		line.localScale = lineScale;
	}

	void Reset() {
		transform.position = ground.GetComponent<PlanetScript>().GetPoint(respawnAngle);
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
}
