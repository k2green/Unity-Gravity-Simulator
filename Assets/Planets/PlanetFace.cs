using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFace {
	private readonly Vector3 localUp;
	private readonly Transform faceTransform;
	private readonly Vector3 axisA;
	private readonly Vector3 axisB;

	private IChunk chunk;

	public PlanetFace(Vector3 localUp, Transform faceTransform) {
		this.localUp = localUp;
		this.faceTransform = faceTransform;
		axisA = new Vector3(localUp.y, localUp.z, localUp.x);
		axisB = Vector3.Cross(localUp, axisA);

		chunk = new ChunkNode(faceTransform, localUp, axisA, axisB, localUp, 1, 0);
		chunk.GenerateNode(0);
	}

	public (List<Vector3>, List<int>) GetMeshData() => chunk.GetMeshData();
}
