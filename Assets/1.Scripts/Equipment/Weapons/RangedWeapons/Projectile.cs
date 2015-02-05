﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public int damage;
	public float speed;
	public Character player;
	public ParticleSystem particles;
	public Transform target;
	// Use this for initialization
	protected virtual void Start() {
		setInitValues();
	}
	protected virtual void setInitValues() {
		particles.Play();
	}

	// Update is called once per frame
	protected virtual void Update() {
		transform.position = Vector3.MoveTowards (transform.position, target.position, speed);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Wall") {
			particles.Stop();
			Destroy(this.transform.parent.gameObject);
		}

		IDamageable<int, Character> component = (IDamageable<int, Character>) other.GetComponent( typeof(IDamageable<int, Character>) );
		Enemy enemy = other.GetComponent<Enemy>();
		if( component != null && enemy != null) {
			enemy.damage(damage, player);
			particles.Stop();
			Destroy(this.transform.parent.gameObject);
		}
	}
}