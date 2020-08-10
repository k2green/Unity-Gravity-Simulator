using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChunk {

	(List<Vector3>, List<int>) GetMeshData();

	int GenerateNode(int vertexOffset);

	Vector3 LocalUp { get; }
	Vector3 AxisA { get; }
	Vector3 AxisB { get; }
	Vector3 Position { get; }
	float ScaleFactor { get; }
	int DetailLevel { get; }
}
