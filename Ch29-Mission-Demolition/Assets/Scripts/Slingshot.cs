﻿using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	static public Slingshot S;


	public GameObject prefabProjectile;
	public float velocityMult = 4f;
	public bool _______________________;
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;

	void Awake(){

		S = this;

		Transform launchPointTrans = transform.Find("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter(){
		print("Slingshot:OnMouseEnter()");
		launchPoint.SetActive(true);
	}

	void OnMouseExit(){

		print("Slingshot:OnMouseExit()");
		launchPoint.SetActive(false);
	}

	void OnMouseDown(){

		aimingMode = true;


		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}

	void Update(){
		if (!aimingMode) return;

		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);

		Vector3 mouseDelta = mousePos3D - launchPos;
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;

		if(mouseDelta.magnitude > maxMagnitude){
			mouseDelta.Normalize ();
			mouseDelta *=maxMagnitude;
		}

		if (Input.GetMouseButtonUp (0)){
			aimingMode = false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
			FollowCam.S.poi = projectile;
			projectile = null;
		}

	}

}
