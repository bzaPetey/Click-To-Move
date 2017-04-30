using UnityEngine;
using System.Collections;


[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovementNavMesh : MonoBehaviour {
	private Vector3 targetPosition;				//where we want to travel too.

	const int LEFT_MOUSE_BUTTON = 0;			//a more visual description of what the left mouse button code is.

	NavMeshAgent agent;



	/// <summary>
	/// Cache all of our components that need it.
	/// </summary>
	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}



	/// <summary>
	/// Use this for initialization.
	/// </summary>
	void Start ()
	{
		targetPosition = transform.position;		//set the target postion to where we are at the start
	}



	/// <summary>
	/// Detect the player input every frame
	/// </summary>
	void Update ()
	{
		//if the player clicked on the screen, find out where
		if(Input.GetMouseButton(LEFT_MOUSE_BUTTON))
			SetTargetPosition();

		MovePlayer();
	}



	/// <summary>
	/// Sets the target position we will travel too.
	/// </summary>
	void SetTargetPosition()
	{
		Plane plane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float point = 0f;
		
		if(plane.Raycast (ray, out point))
			targetPosition = ray.GetPoint(point);
	}



	/// <summary>
	/// Moves the player in the right direction and also rotates them to look at the target position.
	/// When the player gets to the target position, stop them from moving.
	/// </summary>
	void MovePlayer()
	{
		agent.SetDestination(targetPosition);

		Debug.DrawLine(transform.position, targetPosition, Color.red);
	}
}
