﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {

	public Transform CameraController;
	public Transform Planet;
	public Transform Meteor;
	public Transform Sun;
	public Transform Golfball;
	public Transform Background;
	public Transform Flag;
	public float numPlanets = 5;
	public float numMeteors = 3;
	public float numSuns = 1;

	private Transform cam;
	private Transform planets;
	private Transform meteors;
	private Transform golfball;

	void Start () {
		planets = new GameObject ("Planets").transform;
		planets.parent = transform;
		planets.position = new Vector3 (0, 0, 1);
		List<Bounds> bounds = new List<Bounds>();

		for (int i = 0; i < numSuns; i++) {
			Transform sun = Instantiate (Sun) as Transform;
			sun.parent = planets;
			SetSun (sun);

			for (int j = 0; j < bounds.Count; j++) {
				while (sun.GetComponent<Collider2D>().bounds.Intersects(bounds[j])) {
					SetPlanet (sun);
					j = 0;
				}
			}

			bounds.Add (sun.GetComponent<Collider2D>().bounds);
		}

		Transform flag = Instantiate (Flag) as Transform;
		golfball = Instantiate (Golfball) as Transform;

		for (int i = 0; i < numPlanets; i++) {
			Transform planet = Instantiate(Planet) as Transform;
			planet.parent = planets;
			if (Random.value < 0.5) {
				planet.GetComponent<PlanetScript>().type = "Grass";
			} else {
				planet.GetComponent<PlanetScript>().type = "Sand";
			}
			SetPlanet (planet);
			for (int j = 0; j < bounds.Count; j++) {
				while (planet.GetComponent<Collider2D>().bounds.Intersects(bounds[j])) {
					SetPlanet (planet);
					j = 0;
				}
			}

			bounds.Add(planet.GetComponent<Collider2D>().bounds);
			if (i == 0) {
				planet.GetComponent<PlanetScript>().type = "Grass";
				flag.GetComponent<RotateScript>().planet = planet;
				flag.GetComponent<RotateScript>().angle = Random.value * 360;
			} else if (i == numPlanets - 1) {
				golfball.transform.localPosition = planet.GetComponent<PlanetScript>().GetPoint(0);
				golfball.GetComponent<BallScript>().ground = planet;
			}
		}

		meteors = new GameObject ("Meteors").transform;
		meteors.parent = transform;
		for (int i = 0; i < numMeteors; i++) {
			Transform meteor = Instantiate (Meteor) as Transform;
			meteor.parent = meteors;
			SetMeteor(meteor);

			for (int j = 0; j < bounds.Count; j++) {
				while (meteor.GetComponent<Collider2D>().bounds.Intersects(bounds[j])) {
					SetMeteor (meteor);
					j = 0;
				}
			}

			bounds.Add(meteor.GetComponent<Collider2D>().bounds);
		}

		golfball.parent = transform;
		golfball.GetComponent<GravityScript> ().planets = planets;
		golfball.GetComponent<BallScript> ().UI = GameObject.Find ("Canvas").transform;
		golfball.GetComponent<BallScript> ().flag = flag;
		golfball.GetComponent<BallScript> ().UI.GetComponent<UIScript> ().flag = flag;

		cam = Instantiate (CameraController) as Transform;
		cam.GetComponent<CameraScript> ().golfball = golfball;
		Transform bg1 = Instantiate (Background) as Transform;
		bg1.GetComponent<BackgroundScript> ().type = 1;
		cam.GetComponent<CameraScript> ().background1 = bg1;
		Transform bg2 = Instantiate (Background) as Transform;
		bg2.GetComponent<BackgroundScript> ().type = 2;
		cam.GetComponent<CameraScript> ().background2 = bg2;

		golfball.GetComponent<BallScript> ().UI.GetComponent<UIScript> ().cam = cam.GetComponentInChildren<Camera>();
		golfball.GetComponent<BallScript> ().UI.GetComponent<Canvas>().worldCamera = cam.GetComponentInChildren<Camera>();
	}

	private void SetPlanet(Transform planet) {
		Vector3 pos = Random.insideUnitCircle * 8;
		pos.z = 1;
		planet.position = pos;
		planet.GetComponent<PlanetScript>().SetRadius(Random.Range(3, 5));
	}
	
	private void SetSun(Transform sun) {
		Vector3 pos = Random.insideUnitCircle * 8;
		pos.z = 1;
		sun.position = pos;
		sun.GetComponent<PlanetScript>().SetRadius(Random.Range(5, 8));
	}
	
	private void SetMeteor(Transform meteor) {
		meteor.position = Random.insideUnitCircle * 8;
		float size = Random.Range(1, 3);
		meteor.localScale = new Vector3(size, size, 1);
		meteor.eulerAngles = new Vector3 (0, 0, Random.value * 360);
	}
	
	void Update () {
	
	}
}
