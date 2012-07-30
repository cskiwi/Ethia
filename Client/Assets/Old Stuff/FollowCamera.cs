/// <summary>
/// Auther: Glenn Latomme
/// Creation Date: 7/6/2012
/// Project: Ethia
/// 
/// Discription:
/// Following the tharget, while its moving around
/// - when pressing the middle mouse it will zoom out
/// - When pressing right mouse, you wil gain acces to look around
/// </summary>

using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    // variables
    private Transform target;                        // where to look at
	
    private float _distance = 1.5f;			        // Distance between player and camera
	private float _height = 1.0f;			        // Height above player
    
    private float _xPos;				            // Current Xpos for camera
    private float _yPos;			                // Current Ypos for camera

    // constants
    private const float MINHEIGHT = -2.0f;		    // Min height the camera can be in freemode
    private const float MAXHEIGHT = 2.0f;		    // Max height the camera can be in freemode
	
    private const float HEIGHTDAMPING = 2.0f;	    // Delay in changing height
    private const float ROTATIONDAMPING = 3.0f;	    // Delay in rotation
	
    private const float XSPEED = 250.0f;			// Modifier for mouse speed
	private const float YSPEED = 120.0f;			// Modifier for mouse speed
	
	public void Awake(){
        // temp in here
		Screen.lockCursor = true;

        // find player
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	public void Update(){
		// Check if toggle mouse button is pressed (temp)
		if(Input.GetButtonUp("Toggle Mouse"))
			if (Screen.lockCursor)
				Screen.lockCursor = false;
			else 
				Screen.lockCursor = true;
		// If mouse is enabled, and you press any button mouse will dissapear again
		if(Screen.lockCursor == false && Input.anyKey){
			Screen.lockCursor = true;
		}
	}
	public void LateUpdate () {
        // If your working with the mouse no camera calculations are needed and there is a target
		if (Screen.lockCursor == true && target != false) {		
			float wantedRotationAngle = 0.0f;
			float wantedHeight = target.position.y + _height;
				
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			#region Mouselook
			// Check mouse look
			if(Input.GetMouseButton(1)){
				_xPos += Input.GetAxis("Mouse X") * XSPEED * 0.02f;
		        float nextYpos = Input.GetAxis("Mouse Y") * YSPEED * 0.02f;
				
				//reset if out of bounds
				if (_yPos  == 0.0f)
					_yPos = _height;
				
				// add if mousemovement
				if (nextYpos != 0){
					if (_yPos + nextYpos < MAXHEIGHT && nextYpos > 0)
						_yPos += nextYpos;
					else if (_yPos + nextYpos > MINHEIGHT && nextYpos < 0)
						_yPos += nextYpos;
				}

				wantedHeight = target.position.y + _yPos;
				wantedRotationAngle = _xPos;
				
			} else {				
				// Calculate the wanted rotation angle
				wantedRotationAngle = target.eulerAngles.y;				
			}
			#endregion
			#region Damping
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, ROTATIONDAMPING * Time.deltaTime);
		
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, HEIGHTDAMPING * Time.deltaTime);
		
			// Convert the angle into a rotation
			Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * _distance;
		
			// Set the height of the camera
			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
			
			// Always look at the target
			transform.LookAt (target);
			#endregion
			#region Scrolling
			float scrollvalue = Input.GetAxis("Mouse ScrollWheel");
			//Debug.Log("scrollvalue = " + scrollvalue + ", distance = " + _distance);
			
			// positive scrolling
			if(scrollvalue > 0 && _distance + scrollvalue < 3){
				_distance += scrollvalue;
			}
			// negtive scrolling
			else if(scrollvalue < 0 && _distance + scrollvalue > 0.1){
				_distance += scrollvalue;
			}
			#endregion
		}
	}
}
