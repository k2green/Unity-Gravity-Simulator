using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LODSettings {

	public static IDictionary<int, float> DetailLevels = new Dictionary<int, float>() {
		{0, Mathf.Infinity},
		{1, 60f},
		{2, 25f},
		/*{3, 10f},
		{4, 4f},
		{5, 1.5f},
		{6, .7f},
		{7, .3f},
		{8, .1f}*/
	};
}
