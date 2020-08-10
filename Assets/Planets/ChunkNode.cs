using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkNode : IChunk {

	private Transform faceTransform;
	private IChunk[] children;
	private Vector3[] childPositions;

	public ChunkNode(Transform faceTransform, Vector3 localUp, Vector3 axisA, Vector3 axisB, Vector3 position, float scaleFactor, int detailLevel) {
		this.faceTransform = faceTransform;

		LocalUp = localUp;
		AxisA = axisA;
		AxisB = axisB;
		Position = position;
		ScaleFactor = scaleFactor;
		DetailLevel = detailLevel;

		var halfFactor = scaleFactor / 2;

		childPositions = new Vector3[] {
			position - axisA * halfFactor - axisB * halfFactor,
			position + axisA * halfFactor - axisB * halfFactor,
			position - axisA * halfFactor + axisB * halfFactor,
			position + axisA * halfFactor + axisB * halfFactor
		};
	}

	public Vector3 LocalUp { get; }
	public Vector3 AxisA { get; }
	public Vector3 AxisB { get; }
	public Vector3 Position { get; }
	public float ScaleFactor { get; }
	public int DetailLevel { get; }

	public int GenerateNode(int vertexOffset) {
		int localOffset = 0;
		children = new IChunk[4];

		for (int i = 0; i < 4; i++) {
			children[i] = BuildChunk(childPositions[i], ScaleFactor / 2, DetailLevel + 1);
			localOffset += children[i].GenerateNode(vertexOffset + localOffset);
		}

		return localOffset;
	}

	private IChunk BuildChunk(Vector3 childPosition, float scaleFactor, int detailLevel) {
		if (LODSettings.DetailLevels.ContainsKey(detailLevel) && DistanceCheck(childPosition, detailLevel))
			return new ChunkNode(faceTransform, LocalUp, AxisA, AxisB, childPosition, scaleFactor, detailLevel);

		else
			return new PlanetChunk(LocalUp, AxisA, AxisB, childPosition, scaleFactor, detailLevel);
	}

	private bool DistanceCheck(Vector3 childPosition, int detailLevel) {
		var condition = LODSettings.DetailLevels[DetailLevel];
		var playerPos = Player.Instance.transform.position;
		var childWorldPos = faceTransform.InverseTransformPoint(childPosition);

		return Vector3.Distance(playerPos, childWorldPos) <= condition;
	}

	public (List<Vector3>, List<int>) GetMeshData() {
		var vertices = new List<Vector3>();
		var triangles = new List<int>();

		foreach (var chunk in children) {
			var (childVertices, childTriangles) = chunk.GetMeshData();

			vertices.AddRange(childVertices);
			triangles.AddRange(childTriangles);
		}

		return (vertices, triangles);
	}
}
