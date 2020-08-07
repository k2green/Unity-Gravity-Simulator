using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Face {
	private Mesh mesh;
	private int resolution;
	private Vector3 localUp;
	private Vector3 axisA;
	private Vector3 axisB;

	public Dictionary<Vector3, int> BorderVertices { get; private set; }

	public Face (Mesh mesh, int resolution, Vector3 localUp) {
		this.mesh = mesh;
		this.resolution = resolution;
		this.localUp = localUp;

		axisA = new Vector3(localUp.y, localUp.z, localUp.x);
		axisB = Vector3.Cross(localUp, axisA);
	}

	public void ConstructMesh () {
		BorderVertices = new Dictionary<Vector3, int>();
		var vertices = new Vector3[resolution * resolution];
		var triangles = new int[(resolution - 1) * (resolution - 1) * 6];

		for (int i = 0, triIndex = 0, y = 0; y < resolution; y++) {
			for (int x = 0; x < resolution; x++, i++) {
				Vector2 percent = new Vector2(x, y) / (resolution - 1);
				Vector3 pointOnSphere = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;
				vertices[i] = pointOnSphere.normalized;

				if (x == 0 || y == 0 || x == resolution - 1 || y == resolution - 1)
					BorderVertices.Add(vertices[i], i);

				if (x != resolution - 1 && y != resolution - 1) {
					triangles[triIndex] = i;
					triangles[triIndex + 1] = i + resolution + 1;
					triangles[triIndex + 2] = i + resolution;

					triangles[triIndex + 3] = i;
					triangles[triIndex + 4] = i + 1;
					triangles[triIndex + 5] = i + resolution + 1;

					triIndex += 6;
				}
			}
		}

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
	}
}
