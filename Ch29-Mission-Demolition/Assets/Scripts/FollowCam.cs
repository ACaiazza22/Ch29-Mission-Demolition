using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	static public FollowCam S;
	public float easing = 0.05f;
	public Vector2 minXY;

	public bool ______________________;

	public GameObject poi;
	public float camZ;
	void FixedUpdate(){
		Vector3 destination;
		Vector3 origin = new Vector3(0,0,-10);

		if (poi == null){

			destination = Vector3.zero;
		}
		else{
			destination = poi.transform.position;
			if (poi.tag == "Projectile"){
				if(poi.GetComponent<Rigidbody>().IsSleeping()){

					poi = null;

					Camera.main.transform.position = origin;
					

					return;
				}
			}
		}
		

	}

	// Use this for initialization
	void Awake () {
		S= this;
		camZ = this.transform.position.z;
	
	}
	// Update is called once per frame
	void Update () {
	
		if(poi == null ) return;

		Vector3 destination = poi.transform.position;
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);
		destination = Vector3.Lerp(transform.position, destination, easing);
		destination.z = camZ;
		transform.position = destination;
		this.GetComponent<Camera>().orthographicSize= destination.y +10;
	}


	
	


}