﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player Instance;

	void Start() {
		Instance = this;
	}
}
