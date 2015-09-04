using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Transform Sky1;
	public Transform Sky2;
	public int type;

	private Transform sky;

	void Start () {
		int n = 10;
		float size = 2.56f;
		if (type == 1) {
			sky = Sky1;
			transform.position = new Vector3 (0, 0, 10);
		} else {
			sky = Sky2;
			transform.position = new Vector3 (0, 0, 5);
			size = 5.12f;
		}
		for (int x = -n; x < n; x++) {
			for (int y = -n; y < n; y++) {
				Transform s = Instantiate(sky) as Transform;
				s.parent = transform;
				s.localPosition = new Vector3(x*size, y*size, 0);
			}
		}
	}
}
