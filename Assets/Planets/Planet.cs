using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	[Range(2, 256)]
	public int resolution=4;

	private MeshFilter[] meshFilters;
	private Face[] faces;

	private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

	private void OnValidate () {
		Initialise();
		GenerateMesh();
	}

	private void Initialise () {
		if (meshFilters == null || meshFilters.Length == 0)
			meshFilters = new MeshFilter[6];

		faces = new Face[6];

		for (int i = 0; i < 6; i++) {
			if (meshFilters[i] == null) {
				GameObject faceObject = new GameObject($"Face {i}");
				faceObject.transform.parent = transform;

				faceObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("HDRP/Lit"));
				meshFilters[i] = faceObject.AddComponent<MeshFilter>();
				meshFilters[i].sharedMesh = new Mesh();
			}

			faces[i] = new Face(meshFilters[i].sharedMesh, resolution, directions[i]);
		}
	}

	private void GenerateMesh () {
		foreach (var face in faces) {
			face.ConstructMesh();
		}
	}
}
