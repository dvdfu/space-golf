﻿using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {
	public Sprite SandPlanet;
	public Sprite GrassPlanet;
	public Transform terrain;
	public string type;

	void Start () {
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
		return 0.32f * transform.localScale.x + 0.02f;
	}

	public void SetRadius(float r) {
		transform.localScale = new Vector3 (r, r, 1);
	}
}
