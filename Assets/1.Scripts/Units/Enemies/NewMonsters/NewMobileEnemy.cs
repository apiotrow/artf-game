// Enemies that can move

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewMobileEnemy : NewEnemy {
	
	public Vector3 resetpos;
	public Vector3 targetDir;
	public CoroutineController searchController, searchStateController;

	//-------------------//
	// Primary Functions //
	//-------------------//
	
	// Get players, navmesh and all colliders
	protected override void Awake () {
		base.Awake ();
		resetpos = transform.position;
	}
	
	protected override void Start() {
		base.Start ();

		foreach(EnemyBehaviour behaviour in this.animator.GetBehaviours<EnemyBehaviour>()) {
			behaviour.unit = this;
		}
	}
	
	protected override void Update() {
		base.Update ();

		if (this.target != null && this.target.GetComponent<Player>() != null) {
			this.targetDir = this.target.GetComponent<Player>().facing;
		}
	}
	
	protected override void setInitValues() {
		base.setInitValues();
		stats.maxHealth = 40;
		stats.health = stats.maxHealth;
		stats.armor = 0;
		stats.strength = 10;
		stats.coordination=0;
		stats.speed=9;
		stats.luck=0;
		
		this.minAtkRadius = 0.0f;
		this.maxAtkRadius = 3.0f;
	}
	
	//----------------------//
	
	//----------------------//
	// Transition Functions //
	//----------------------//
	
	public virtual bool isResting() {
		return this.lastSeenPosition == null && !this.alerted;
	}
	
	public virtual bool isApproaching() {
		// If we don't have a target currently and aren't alerted, automatically assign anyone in range that we can see as our target
		if (this.actable) {
			if (this.target == null) {// && !this.alerted) {
				if (aRange.unitsInRange.Count > 0) {
					foreach(Character tars in aRange.unitsInRange) {
						if (this.canSeePlayer(tars.gameObject) && !tars.isDead) {
							this.alerted = true;
							this.animator.SetBool("Alerted", true);
							target = tars.gameObject;
							break;
						}
					}
					
					if (target == null) {
						return false;
					}
				} else {
					return false;
				}
			}
			
			// float distance = this.distanceToPlayer(this.target);
			if (this.canSeePlayer (this.target) && !isInAtkAnimation()) {
				// agent.alerted = true;
				return true;
			}
		}
		return false;
	}
	
	protected virtual bool isAttacking() {
		if (this.target != null && !this.isInAtkAnimation() && this.actable) {
			float distance = this.distanceToPlayer(this.target);
			//float distance = Vector3.Distance(this.transform.position, this.target.transform.position);
			//&& this.canSeePlayer(this.target)
			return distance < this.maxAtkRadius && distance >= this.minAtkRadius;
		}
		return false;
	}
	
	protected virtual bool isInAtkAnimation() {
		if (this.attacking || this.animSteHash == this.atkHashChgSwing || this.animSteHash == this.atkHashCharge) {
			this.rb.velocity = Vector3.zero;
			return true;
		}
		return false;
	}
	
	protected virtual bool isSearching() {
		if (this.target == null || (this.lastSeenPosition.HasValue && !(this.canSeePlayer (this.target) && this.alerted) && !this.isInAtkAnimation()) && this.actable) {
			return true;
		}
		return false;
	}
	
	
	protected virtual bool isRetreating() {
		return this.target == null && !this.alerted;
	}
	
	protected virtual bool isCreatingSpace() {
		if (this.target != null && !this.isInAtkAnimation()) {
			float distance = this.distanceToPlayer(this.target);
			return distance < this.minAtkRadius && this.canSeePlayer(this.target);
		}
		return false;
	}
	
	protected virtual bool isFar () {
		if (this.target != null && this.actable) {
			float distance = this.distanceToPlayer(this.target);
			return distance > this.minAtkRadius;
		}
		if (this.target == null) {
			return true;
		}
		return false;
	}
	
	//---------------------//
	
	
	//------------------//
	// Action Functions //
	//------------------//
	
	protected virtual void Approach() {
		this.getFacingTowardsTarget();
		this.rb.velocity = this.facing * stats.speed * stats.spdManip.speedPercent;
	}
	
	protected virtual void EnterAttack() {
		if (this.actable && !attacking){
			this.getFacingTowardsTarget();
			this.transform.localRotation = Quaternion.LookRotation(facing);
			this.gear.weapon.initAttack();
		}
	}
	
	protected virtual void Attack() {
		//print ("Attack()");
	}
	
	// We can have some logic here, but it's mostly so our unit is still during and attack animation
	protected virtual void AtkAnimation() {
		this.rb.velocity = Vector3.zero;
		this.isApproaching();
	}
	
	protected virtual void Search() {
		target = null;
		if (this.lastSeenPosition.HasValue) {
			this.facing = this.lastSeenPosition.Value - this.transform.position;
			this.facing.y = 0.0f;
			this.StartCoroutineEx(searchForEnemy(this.lastSeenPosition.Value), out this.searchController);
			// StartCoroutine("searchForEnemy", this.lastSeenPosition.Value);
			this.lastSeenPosition = null;
			this.animator.SetBool ("HasLastSeenPosition", false);
		}
	}
	
	public virtual void StopSearch() {
		this.searchController.Stop ();
		this.searchStateController.Stop ();
		/*
		this.StopCoroutine("searchForEnemy");
		this.StopCoroutine("moveToPosition");
		this.StopCoroutine("moveToExpectedArea");
		this.StopCoroutine("randomSearch");
		*/
	}
	
	//Improve retreat AI
	protected virtual void Retreat() {
		// agent.nav.destination = agent.retreatPos;
		this.facing = this.resetpos - this.transform.position;
		StartCoroutine(moveToPosition(this.resetpos));
	}
	
	protected virtual void Spacing() {
		this.facing = (this.lastSeenPosition.Value - this.transform.position) * -1;
		this.facing.y = 0.0f;
		this.rb.velocity = this.facing.normalized * stats.speed * stats.spdManip.speedPercent;
	}
	
	protected virtual void Far () {
		this.facing = this.facing * -1;
	}
	
	//------------------//
	
	
	
	//-----------------------------//
	// Coroutines for timing stuff //
	//-----------------------------//
	
	protected IEnumerator moveToPosition(Vector3 position) {
		print("MovingToPos");
		while (!this.isApproaching() && this.distanceToVector3(position) > 0.25f && !this.isInAtkAnimation() && this.target == null) {
			this.rb.velocity = this.facing.normalized * stats.speed * stats.spdManip.speedPercent;
			yield return null;
		}
	}
	
	protected IEnumerator moveToExpectedArea() {
		print("MovingToExpected");
		this.facing = this.targetDir;
		float moveToTime = 0.5f;
		while (!this.isApproaching() && this.distanceToVector3(this.targetDir) > 0.1f && !this.isInAtkAnimation() && this.target == null && moveToTime > 0.0f) {
			this.rb.velocity = this.facing.normalized * stats.speed * stats.spdManip.speedPercent;
			moveToTime -= Time.deltaTime;
			yield return null;
		}
	}
	
	protected IEnumerator randomSearch() {
		print("RandomSearch");
		float resetTimer = aggroTimer;
		while(!this.isApproaching() && resetTimer > 0.0f && !this.isInAtkAnimation() && this.target == null) {
			this.facing = new Vector3(Random.Range (-1.0f, 1.0f), 0.0f, Random.Range (-1.0f, 1.0f)).normalized;
			this.rb.velocity = this.facing.normalized * stats.speed * stats.spdManip.speedPercent;
			yield return new WaitForSeconds (0.5f);
			resetTimer -= 0.5f;
		}
	}
	
	public virtual IEnumerator searchForEnemy(Vector3 lsp) {
		yield return this.StartCoroutineEx(moveToPosition(lsp), out this.searchStateController);
		yield return this.StartCoroutineEx(moveToExpectedArea(), out this.searchStateController);
		yield return this.StartCoroutineEx(randomSearch(), out this.searchStateController);


		/*
		yield return StartCoroutine("moveToPosition", lsp);
		
		yield return StartCoroutine ("moveToExpectedArea");
		
		// float resetTimer = aggroTimer;
		
		yield return StartCoroutine("randomSearch");
		*/
		
		if (this.target == null) {
			alerted = false;
			this.animator.SetBool("Alerted", false);
		}
	}
	
	//-----------------------------//
	
	
	//-----------------------//
	// Calculation Functions //
	//-----------------------//
	
	protected float distanceToVector3(Vector3 position) {
		Vector3 distance = position - this.transform.position;
		return distance.sqrMagnitude;
	}
	
	//-----------------//
}
