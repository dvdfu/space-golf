using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {
	public Sprite SandPlanet;
	public Sprite GrassPlanet;
	public Transform SandTerrain;
	public Transform GrassTerrain;
	public PhysicsMaterial2D SandMaterial;
	public PhysicsMaterial2D GrassMaterial;
	public string type;

	private Transform terrain;

	void Start () {
		if (type == "Grass") {
			terrain = GrassTerrain;
			GetComponent<SpriteRenderer>().sprite = GrassPlanet;
			GetComponent<CircleCollider2D>().sharedMaterial = GrassMaterial;
		} else if (type == "Sand") {
			terrain = SandTerrain;
			GetComponent<SpriteRenderer>().sprite = SandPlanet;
			GetComponent<CircleCollider2D>().sharedMaterial = SandMaterial;
		}
		GetComponent<CircleCollider2D> ().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = true;
		float rad = Radius ();
		if (terrain != null) {
			for (float i = 0; i < Mathf.PI*2*rad; i += 0.24f) {
				Transform g = Instantiate(terrain) as Transform;
				g.parent = transform;
				g.GetComponent<RotateScript>().planet = transform;
				g.GetComponent<RotateScript>().angle = i/rad*180/Mathf.PI;
			}
		}
	}

	public float Radius() {
		return 0.32f * transform.localScale.x + 0.025f;
	}

	public void SetRadius(float r) {
		transform.localScale = new Vector3 (r, r, 1);
	}
}
