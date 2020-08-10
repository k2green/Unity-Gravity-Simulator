using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetChunk : IChunk {

	public const int Resolution = 2;

	public PlanetChunk(Vector3 localUp, Vector3 axisA, Vector3 axisB, Vector3 position, float scaleFactor, int depth) {
		LocalUp = localUp;
		Position = position;
		ScaleFactor = scaleFactor;
		DetailLevel = depth;

		AxisA = axisA;
		AxisB = axisB;
	}

	public (List<Vector3>, List<int>) GetMeshData() {
		return (Vertices, Triangles);
	}

	public int GenerateNode(int vertexOffset) {
		int vertexCount = CreateVertices(vertexOffset);
		CreateTriangles();

		return vertexCount;
	}

	private int CreateVertices(int vertexOffset) {
		Vertices = new List<Vector3>();
		VertexIndices = new int[Resolution, Resolution];

		for (int index = 0, y = 0; y < Resolution; y++) {
			for (int x = 0; x < Resolution; x++, index++) {
				var globalIndex = vertexOffset + index;

				var percent = new Vector2(x, y) / (Resolution - 1);
				var rescale = percent * 2 - Vector2.one;

				var pointOnCube = Position + rescale.x * ScaleFactor * AxisA + rescale.y * ScaleFactor * AxisB;
				var pointOnSphere = pointOnCube.normalized;

				Vertices.Add(pointOnSphere);
				VertexIndices[x, y] = globalIndex;
			}
		}

		return Vertices.Count;
	}

	private void AddTriangle(int a, int b, int c) {
		Triangles.Add(a);
		Triangles.Add(b);
		Triangles.Add(c);
	}

	private void CreateTriangles() {
		Triangles = new List<int>();

		for (int index = 0, y = 0; y < Resolution - 1; y++) {
			for (int x = 0; x < Resolution - 1; x++, index += 6) {
				var a = VertexIndices[x, y];
				var b = VertexIndices[x + 1, y];
				var c = VertexIndices[x + 1, y + 1];
				var d = VertexIndices[x, y + 1];

				AddTriangle(a, b, c);
				AddTriangle(a, c, d);
			}
		}
	}

	public List<Vector3> Vertices { get; private set; }
	public List<int> Triangles { get; private set; }
	public int[,] VertexIndices { get; private set; }

	public Vector3 LocalUp { get; }
	public Vector3 AxisA { get; }
	public Vector3 AxisB { get; }
	public Vector3 Position { get; }
	public float ScaleFactor { get; }
	public int DetailLevel { get; }
}
