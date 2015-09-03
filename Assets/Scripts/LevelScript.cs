using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {

	public Transform Planet;
	public Transform Meteor;
	public Transform Sun;
	public Transform Golfball;
	public Transform Background;
	public Transform Flag;

	private Transform planets;
	private Transform meteors;
	private Transform golfball;

	void Start () {
		planets = new GameObject ("Planets").transform;
		planets.parent = transform;
		planets.position = new Vector3 (0, 0, 1);
		List<Bounds> bounds = new List<Bounds>();

		
		Transform sun = Instantiate (Sun) as Transform;
		sun.parent = planets;
		SetPlanet (sun);
		bounds.Add (sun.collider2D.bounds);
		Transform flag = Instantiate (Flag) as Transform;

		for (int i = 0; i < 5; i++) {
			Transform planet = Instantiate(Planet) as Transform;
			planet.parent = planets;
			SetPlanet (planet);

			if (i == 0) {
				flag.GetComponent<RotateScript>().planet = planet;
				flag.GetComponent<RotateScript>().angle = Random.value * 360;
			}
			for (int j = 0; j < bounds.Count; j++) {
				while (planet.collider2D.bounds.Intersects(bounds[j])) {
					SetPlanet (planet);
					j = 0;
				}
			}

			bounds.Add(planet.collider2D.bounds);
		}

		meteors = new GameObject ("Meteors").transform;
		meteors.parent = transform;
		for (int i = 0; i < 3; i++) {
			Transform meteor = Instantiate (Meteor) as Transform;
			meteor.parent = meteors;
			SetMeteor(meteor);

			for (int j = 0; j < bounds.Count; j++) {
				while (meteor.collider2D.bounds.Intersects(bounds[j])) {
					SetMeteor (meteor);
					j = 0;
				}
			}

			bounds.Add(meteor.collider2D.bounds);
		}

		golfball = Instantiate (Golfball) as Transform;
		golfball.parent = transform;
		golfball.GetComponent<GravityScript> ().planets = planets;
		golfball.GetComponent<BallScript> ().UI = GameObject.Find ("Canvas").transform;
		golfball.transform.localPosition = new Vector3 (3, 3, 0);
		golfball.GetComponent<BallScript> ().flag = flag;

		Instantiate (Background);
	}

	private void SetPlanet(Transform planet) {
		Vector3 pos = Random.insideUnitCircle * 4;
		pos.z = 1;
		planet.position = pos;
		planet.GetComponent<PlanetScript>().SetRadius(Random.value * 3 + 1);
	}
	
	private void SetMeteor(Transform meteor) {
		meteor.position = Random.insideUnitCircle * 4;
		float size = Random.value * 2 + 1;
		meteor.localScale = new Vector3(size, size, 1);
		meteor.eulerAngles = new Vector3 (0, 0, Random.value * 360);
	}
	
	void Update () {
	
	}
}
