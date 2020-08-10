using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	private PlanetFace[] faces;
	private MeshFilter[] filters;

	private void Update () {
		Initialise();
		GenerateMeshes();
	}

	private void CreateFaceGameObject (Face face) {
		var faceObject = new GameObject($"{face} Face");
		faceObject.transform.parent = transform;

		faceObject.transform.localPosition = Vector3.zero;
		faceObject.transform.localScale = Vector3.one;
		faceObject.transform.localEulerAngles = Vector3.zero;

		faceObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("HDRP/Lit"));
		filters[(int) face] = faceObject.AddComponent<MeshFilter>();

		if (filters[(int) face].sharedMesh == null)
			filters[(int) face].sharedMesh = new Mesh();
	}

	private void Initialise () {
		if (faces != null && filters != null && faces.Length == 6 && faces.Length == 6) return;

		faces = new PlanetFace[6];
		filters = new MeshFilter[6];

		foreach (Face face in Enum.GetValues(typeof(Face))) {
			CreateFaceGameObject(face);

			faces[(int) face] = new PlanetFace(face.GetUpVector(), filters[(int) face].transform);
		}
	}

	private void GenerateMeshes () {
		for (int i = 0; i < 6; i++) {
			var mesh = filters[i].sharedMesh;
			mesh.Clear();

			var (vertices, triangles) = faces[i].GetMeshData();
			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();

			mesh.RecalculateNormals();
		}
	}
}
