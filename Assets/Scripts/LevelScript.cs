using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {

	public Transform Planet;
	public Transform Meteor;
	public Transform Golfball;
	public Transform Background;

	private Transform planets;
	private Transform meteors;
	private Transform golfball;

	void Start () {
		planets = new GameObject ("Planets").transform;
		planets.parent = transform;
		List<Bounds> bounds = new List<Bounds>();

		for (int i = 0; i < 5; i++) {
			Transform planet = Instantiate(Planet) as Transform;
			planet.parent = planets;
			SetPlanet (planet);

			if (i == 0) {
				planet.GetComponent<PlanetScript>().hasFlag = true;
				planet.GetComponent<PlanetScript>().flagAngle = Random.value * 360;
			} else {
				for (int j = 0; j < bounds.Count; j++) {
					while (planet.collider2D.bounds.Intersects(bounds[j])) {
						SetPlanet (planet);
						j = 0;
					}
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
		golfball.transform.position = new Vector3 (6, 6, 0);

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
	}
	
	void Update () {
	
	}
}
