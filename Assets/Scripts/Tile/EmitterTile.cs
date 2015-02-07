﻿using UnityEngine;
using System.Collections;

public class EmitterTile : Tile {

    public override Direction ResultingDirection(Direction incoming) {
        // solid wall
        Direction result;
        result.x = -incoming.x;
        result.y = -incoming.y;
        return result;
    }

	// Use this for initialization
	public override void Start () {
	   base.Start();

	}
	
	// Update is called once per frame
    public override void Update () {
       base.Update();
    }
}
