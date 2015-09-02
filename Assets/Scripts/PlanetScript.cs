using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {
	public Transform grass;
	public Transform flag;

	public bool isSand = false;
	public bool hasFlag = false;
	public float flagAngle = 0;

	void Start () {
		float rad = GetComponent<CircleCollider2D>().radius * transform.localScale.x + 0.02f;
		for (float i = 0; i < Mathf.PI*2*rad; i += 0.24f) {
			float x = transform.position.x + rad*Mathf.Cos(i/rad);
			float y = transform.position.y + rad*Mathf.Sin(i/rad);
			Transform g = Instantiate(grass, new Vector3 (x, y, 0), Quaternion.identity) as Transform;
			g.parent = transform;
			g.localEulerAngles = new Vector3(0, 0, i/rad*180/Mathf.PI - 90);
		}

		if (hasFlag) {
			float x = transform.position.x + rad*Mathf.Cos(flagAngle/180*Mathf.PI);
			float y = transform.position.y + rad*Mathf.Sin(flagAngle/180*Mathf.PI);
			Transform f = Instantiate(flag, new Vector3 (x, y, 0), Quaternion.identity) as Transform;
			f.parent = transform;
			f.localEulerAngles = new Vector3(0, 0, flagAngle - 90);
		}
	}
}
