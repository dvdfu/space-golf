using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

	public Transform Planet;
	public Transform Meteor;
	public Transform Golfball;

	private Transform planets;
	private Transform meteors;
	private Transform golfball;

	void Start () {
		planets = new GameObject ("Planets").transform;
		planets.parent = transform;
		for (int i = 0; i < 5; i++) {
			Transform planet = Instantiate(Planet) as Transform;
			planet.parent = planets;
			Vector3 pos = Random.insideUnitCircle * 4;
			pos.z = 1;
			planet.position = pos;
			planet.GetComponent<PlanetScript>().SetRadius(Random.value * 3 + 1);

			if (i == 0) {
				planet.GetComponent<PlanetScript>().hasFlag = true;
				planet.GetComponent<PlanetScript>().flagAngle = Random.value * 360;
			}
		}

		meteors = new GameObject ("Meteors").transform;
		meteors.parent = transform;
		for (int i = 0; i < 3; i++) {
			Transform meteor = Instantiate (Meteor) as Transform;
			meteor.parent = meteors;

			Vector3 pos = Random.insideUnitCircle * 4;
			meteor.position = pos;
			float size = Random.value * 2 + 1;
			meteor.localScale = new Vector3(size, size, 1);
		}

		golfball = Instantiate (Golfball) as Transform;
		golfball.parent = transform;
		golfball.GetComponent<GravityScript> ().planets = planets;
		golfball.transform.position = new Vector3 (6, 6, 0);
	}
	
	void Update () {
	
	}
}
