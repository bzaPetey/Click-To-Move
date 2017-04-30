﻿using UnityEngine;
using System.Collections;


[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour {
	[SerializeField][Range(1,20)]
	private float speed = 10;					//how fast the player moves.

	private Vector3 targetPosition;				//where we want to travel too.
	private bool isMoving;						//toggle to check track if we are moving or not.

	const int LEFT_MOUSE_BUTTON = 0;			//a more visual description of what the left mouse button code is.



	/// <summary>
	/// Use this for initialization.
	/// </summary>
	void Start ()
	{
		targetPosition = transform.position;		//set the target postion to where we are at the start
		isMoving = false;							//set  out move toggle to false.
	}



	/// <summary>
	/// Detect the player input every frame
	/// </summary>
	void Update ()
	{
		//if the player clicked on the screen, find out where
		if(Input.GetMouseButton(LEFT_MOUSE_BUTTON))
			SetTargetPosition();

		//if we are still moving, then move the player
		if(isMoving)
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
		
		//set the ball to move
		isMoving = true;
	}



	/// <summary>
	/// Moves the player in the right direction and also rotates them to look at the target position.
	/// When the player gets to the target position, stop them from moving.
	/// </summary>
	void MovePlayer()
	{
		transform.LookAt(targetPosition);
		transform.position = Vector3.MoveTowards( transform.position, targetPosition, speed * Time.deltaTime);
		
		//if we are at the target position, then stop moving
		if(transform.position == targetPosition)
			isMoving = false;

		Debug.DrawLine(transform.position, targetPosition, Color.red);
	}
}
