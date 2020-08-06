using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GravityTreeOutput {

	private const string SPACING = "    ";
	private const string NEXT_PREFIX = "  ├─";
	private const string CONT_PREFIX = "  │ ";
	private const string LAST_PREFIX = "  └─";

	public static void Output (TextWriter writer, GravityNode node, string prefix1, string prefix2) {
		if (node == null)
			return;

		Output(writer, node, node, prefix1, prefix2);
	}

	private static void Output (TextWriter writer, GravityNode node, GravityNode rootNode, string prefix1, string prefix2) {
		if (node == null)
			return;

		if (node.IsLeaf) {
			OutputLeaf(writer, (LeafNode) node, rootNode, prefix1, prefix2);
		} else {
			OutputParent(writer, (ParentNode) node, rootNode, prefix1, prefix2);
		}
	}

	private static void OutputParent (TextWriter writer, ParentNode node, GravityNode rootNode, string prefix1, string prefix2) {
		writer.Write(prefix1);
		writer.Write("Total Mass: ");
		writer.Write(node.Mass);
		writer.Write("\tCentre of mass: ");
		writer.Write(node.CentreOfMass);
		writer.Write("\tCell Size: ");
		writer.WriteLine(node.Size);

		int range = 7;
		while (range > 0 && node.SubNodes[range] == null)
			range--;

		for (int index = 0; index <= range; index++) {
			if (node.SubNodes[index] != null) {
				if (index == range) {
					Output(writer, node.SubNodes[index], rootNode, prefix2 + LAST_PREFIX, prefix2 + SPACING);
				} else {
					Output(writer, node.SubNodes[index], rootNode, prefix2 + NEXT_PREFIX, prefix2 + CONT_PREFIX);
				}
			}
		}
	}


	private static void OutputLeaf (TextWriter writer, LeafNode node, GravityNode rootNode, string prefix1, string prefix2) {
		writer.Write(prefix1);
		writer.Write("Mass: ");
		writer.Write(node.Mass);
		writer.Write("\tCentre of mass: ");
		writer.Write(node.CentreOfMass);
		writer.Write("\tForce: ");
		writer.WriteLine(GravityManager.CalculateTreeForce(node, rootNode));
	}
}
