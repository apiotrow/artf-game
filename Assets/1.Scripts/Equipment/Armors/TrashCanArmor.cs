using UnityEngine;
using System.Collections;

public class TrashCanArmor : Armor {
	// Used for setting stats for each weapon piece
	protected override void setInitValues() {
		base.setInitValues();
		//armVal, goldVal, strength, coordination, health
		stats = new ArmorStats(3, 1, 12, 12, 120);
	}
}