using UnityEngine;
using System.Collections;

public class AuraScript : MonoBehaviour {

	private ParticleSystem particles;

	void Start () {
		particles = GetComponent<ParticleSystem> ();
	}
	
	void Update () {
		particles.startSize = transform.localScale.x + Random.value;
	}
}
