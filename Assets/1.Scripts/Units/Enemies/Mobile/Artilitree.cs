using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Artilitree: MobileEnemy {
	
	// protected Stealth stealth;
	
	protected override void Awake () {
		base.Awake ();
	}
	
	protected override void Start() {
		base.Start ();
	}
	
	protected override void Update() {
		base.Update ();
	}
	
	protected override void setInitValues() {
		base.setInitValues();
		stats.maxHealth = 40;
		stats.health = stats.maxHealth;
		stats.armor = 0;
		stats.strength = 10;
		stats.coordination=0;
		stats.speed=3;
		stats.luck=0;
		
		this.minAtkRadius = 40.0f;
		this.maxAtkRadius = 100.0f;
	}
}
