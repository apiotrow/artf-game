// Chainsaw class, put into the head for now, could possibly expand this into a special type weapon os similarity to flamethrower

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chainsaw : MeleeWeapons {

	public float lastDmgTime, curDuration, maxDuration;
	private bool dealDamage;
	private float slowPercent;

	private List<Character> chained;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

	}
	
	// Used for setting sword stats for each equipment piece
	protected override void setInitValues() {
		base.setInitValues();

		// User dagger vars for now until we have chainsaw animations
		stats.weapType = 2;
		stats.weapTypeName = "chainsaw";

		stats.atkSpeed = 3.0f;
		stats.damage = (int)(1 + 0.1f * user.GetComponent<Character>().stats.strength);
		
		stats.maxChgTime = 3.0f;
		
		stats.chgLevels = 0.5f;

		lastDmgTime = 0.0f;
		maxDuration = 5.0f;
		curDuration = 0.0f;
		slowPercent = 0.9f;
		chained = new List<Character> ();
	}

	// Move to a Coroutine during our great weapon pur-refactor
	protected override void FixedUpdate() {
		base.FixedUpdate ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();


	}
	
	public override void initAttack() {
		base.initAttack();
	}

	protected override IEnumerator bgnAttack() {
		return base.bgnAttack();
	}

	// Chainsaw attacks in a unique way
	protected override IEnumerator bgnCharge() {
		if (user.animator.GetBool("Charging")) particles.Play();
		while (user.animator.GetBool("Charging") && stats.curChgDuration < 0.5f) {
			stats.curChgDuration = Mathf.Clamp(stats.curChgDuration + Time.deltaTime, 0.0f, stats.maxChgTime);
			stats.chgDamage = (int) (stats.curChgDuration/stats.chgLevels);
			particles.startSpeed = stats.chgDamage;
			yield return null;
		}
		attack ();
	}

	protected override void attack() {
		base.attack ();
	}

	// Basic attack, a normal swing/stab/fire
	protected override void basicAttack() {
		print("Charged Attack; Power level:" + stats.chgDamage);
		user.animator.SetBool("ChargedAttack", false);
		this.GetComponent<Collider>().enabled = true;
		StartCoroutine(atkFinish());
	}
	
	// Charged attack, something unique to the weapon type
	protected override void chargedAttack() {
		print("Charged Attack; Power level:" + stats.chgDamage);
		user.animator.SetBool("ChargedAttack", true);
		this.GetComponent<Collider>().enabled = true;
		StartCoroutine(dismember());
		StartCoroutine(atkFinish());
	}

	// Logic for when we have our chainsaw out and sawing some bitch ass plants
	protected virtual IEnumerator dismember() {
		curDuration = maxDuration;
		lastDmgTime = Time.time;
		while(user.animator.GetBool("Charging") && curDuration > 0) {
			stats.curChgDuration = Mathf.Clamp(stats.curChgDuration + Time.deltaTime, 0.0f, stats.maxChgTime);
			stats.chgDamage = (int) (stats.curChgDuration/stats.chgLevels);
			particles.startSpeed = stats.chgDamage;
			curDuration -= Time.deltaTime;

			if (Time.time - lastDmgTime >= 0.6f/stats.curChgDuration) {
				lastDmgTime = Time.time;
				foreach(Character meat in chained) {
					meat.damage(stats.damage, user);
				}
			}

			yield return null;
		}
		user.animator.SetBool("ChargedAttack", false);
	}
	
	// When our attack swing finishes, remove colliders, particles, and other stuff
	//     * Consider one more co routine after to check for when our animation is completely done
	protected virtual IEnumerator atkFinish() {
		while (user.animSteInfo.nameHash != user.atkHashEnd) {
			yield return null;
		}
		
		particles.Stop();
		this.GetComponent<Collider>().enabled = false;
		if (chained.Count > 0) {
			foreach(Character meat in chained) {
				meat.removeSlow(slowPercent);
			}
			chained.Clear();
			user.removeSlow(slowPercent);
		}

		user.animator.speed = 1.0f;
	}

	void OnTriggerEnter(Collider other) {
		Character enemy = other.GetComponent<Character>();
		if (enemy != null && !chained.Contains(enemy)) {

			if (user.animator.GetBool("Charging")) {
				if (chained.Count == 0) {
					user.slow (slowPercent);
				}
				chained.Add(enemy);
				enemy.slow (slowPercent);
			} else {
				enemy.damage(stats.damage * 2, user);
			}

		} 
	}
	
	void OnTriggerExit(Collider other) {
		Character enemy = other.GetComponent<Character>();
		if (enemy != null) {
			if (chained.Contains(enemy)) {
				chained.Remove(enemy);
				enemy.removeSlow (slowPercent);
			}
			
			if (chained.Count == 0 && user.animator.GetBool("Charging")) {
				user.removeSlow (slowPercent);
			}
		} 
	}
}