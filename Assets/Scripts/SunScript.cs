using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour {

	private ParticleSystem particles;

	void Start () {
		particles = GetComponent<ParticleSystem> ();
	}
	
	void Update () {
		particles.startSize = transform.localScale.x + Random.value * 0.5f;
	}
}
