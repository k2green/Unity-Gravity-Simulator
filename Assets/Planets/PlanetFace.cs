using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFace {
	private readonly Vector3 localUp;
	private readonly Vector3 axisA;
	private readonly Vector3 axisB;
	private int currentVertex = 0;

	private PlanetChunk chunk;

	public PlanetFace (Vector3 localUp) {
		this.localUp = localUp;

		axisA = new Vector3(localUp.y, localUp.z, localUp.x);
		axisB = Vector3.Cross(localUp, axisA);
		
		chunk = new PlanetChunk(localUp, axisA, axisB, localUp, 1, 0, currentVertex);
	}

	public (List<Vector3>, List<int>) CreateMeshData () => chunk.CreateMeshData();
}
