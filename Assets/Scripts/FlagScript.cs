using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	}

	public bool Visible() {
		return GetComponent<Renderer> ().isVisible;
	}
}
