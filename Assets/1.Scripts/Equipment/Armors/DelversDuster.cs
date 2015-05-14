using UnityEngine;
using System.Collections;

public class DelversDuster : Armor {
	// Used for setting stats for each weapon piece
	protected override void setInitValues() {
		base.setInitValues();
		//armVal, goldVal, strength, coordination, health
		stats = new ArmorStats(8, 1, 18, 35, 220);
	}
}