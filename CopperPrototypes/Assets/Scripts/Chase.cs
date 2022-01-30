/*
	Created by: Lech Szymanski
				lechszym@cs.otago.ac.nz
				Dec 29, 2015			
*/

using UnityEngine;
using System.Collections;

/* This is an example script using A* pathfinding to chase a
 * target game object*/

public class Chase : MonoBehaviour {

	// Target of the chase
	// (initialise via the Inspector Panel)
	public GameObject target = null;

	// Chaser's speed
	// (initialise via the Inspector Panel)
	public float speed;

	public bool enabled = true;

	public Rigidbody2D rb;

	public GameObject roomMaster = null;

	// Chasing game object must have a AStarPathfinder component - 
	// this is a reference to that component, which will get initialised
	// in the Start() method
	private AStarPathfinder pathfinder = null; 

	// Use this for initialization
	void Start () {
		//Get the reference to object's AStarPathfinder component
		pathfinder = transform.GetComponent<AStarPathfinder> ();

		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (roomMaster == null)
        {
			Debug.Log("RoomMaster has not been set! AI may not function properly");
			return;
        }

		enabled = roomMaster.GetComponent<RoomMaster>().aiEnabled;

		if (enabled && rb.isKinematic == true && pathfinder != null) {
			//Travel towards the target object at certain speed.
			pathfinder.GoTowards(target, speed);
		}
	}
}
