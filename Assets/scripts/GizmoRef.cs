using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRef : MonoBehaviour {

	enum GizmoShape
	{
		CUBE, WIRE_CUBE, SPHERE, WIRE_SPHERE
	}

	[SerializeField]GizmoShape gizmoShape;
	[SerializeField]Color gizmoColor = Color.gray;
	[SerializeField]float size = 0.5f;

	void OnDrawGizmos() {
		Gizmos.color = gizmoColor;

		if (gizmoShape == GizmoShape.CUBE) {
			Gizmos.DrawCube (transform.position, new Vector3 (size, size, size));
		} else if (gizmoShape == GizmoShape.WIRE_CUBE) {
			Gizmos.DrawWireCube (transform.position, new Vector3 (size, size, size));
		} else if (gizmoShape == GizmoShape.SPHERE) {
			Gizmos.DrawSphere (transform.position, size);
		} else if (gizmoShape == GizmoShape.WIRE_SPHERE) {
			Gizmos.DrawWireSphere (transform.position, size);
		}
	}
}
