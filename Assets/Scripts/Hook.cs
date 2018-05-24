////////////////////////////////////////////////////
// Grapple Hook 2D ////////	Be sure to check out //
//	By Sykoo ///////////// SurvivalEngine too!////
/////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {

	public Transform Player; //the object for player
	public Transform HookHolder; //the object for hook holder
	public Transform ThisObject; //defining this object itself
	public Transform Target; //target object for detecting if the hook is not triggering the meant to trigger object
	private GameObject OtherObject; //the object that will be hooked

	public SpringJoint PlayerSpringJoint; //the SpringJoint attached to the player object

	public bool Fired = false; //checking if the hook has been fired
	public bool Hooked = false; //checking if something has been hooked

	public float MovementSpeed = 0.2f; //movement speed for the hook object to fly to its destination
	public int MD = 10; //setting a limit to maximum distance, MD stands for "Max Distance"
	public float DFH; //calculating distance from hook holder, DFH stands for "Distance From HookHolder"

	private Vector3 lastPos; //calculating the last position of the mouse click in order to sen the hook object
	private Vector3 lastPosOO; //last position for OtherObject (lastPosOO stands for "last position other object"
	private Vector3 curPosOO; //current position for OtherObject (curPosOO stands for "current position other object"
	
	public AudioClip ShootSound; //the sound effect when hook gets shot	

	public float Force;
	
	void Start()
	{

	}

	void Update()
	{
		//calculating the mouse position
		var MousePosition = Input.mousePosition; //taking input for the positioning of the mouse
		MousePosition.z = 15; //you can change this depending on your game. test it out on "15" first, it may be some sort of default value
		MousePosition = Camera.main.ScreenToWorldPoint(MousePosition); //setting the position reading to local instead of global, meaning it'll only reach across your camera's space
	
		//taking input from player (default is left mouse button, you can change this to your willings)
		if(Input.GetMouseButtonDown(0) && !Fired && !Hooked)
		{
			Fired = true; //identifying that it's been fired
			lastPos = MousePosition; //change the values of lastPos to the mouse position

			Instantiate(Target, lastPos, Quaternion.identity);
		}

		//lets now let the player to press right mouse button to replace the hook
		if(Input.GetMouseButtonDown(1))
		{
			Fired = false;
			Hooked = false;
			PlayerSpringJoint.maxDistance = 4;
		}

		//by default, the hook will be following the HookHolder object if not fired and if not hooked
		if(!Fired && !Hooked)
		{
			transform.position = Vector3.MoveTowards(transform.position, HookHolder.position, 1); 
			PlayerSpringJoint.maxDistance = 2;
		}

		//if, however, it is fired, then it will go to Shot method
		if(Fired)
		{
			GetComponent<AudioSource>().clip = ShootSound; //playing the sound when shot
			transform.position = Vector3.MoveTowards(transform.position, lastPos, MovementSpeed); //move the hook towards lastPos
			print(lastPos); //you can delete this if you want to, this is just for seeing in the dev console where the lastPos is set (debugging purposes for me)
			DFH = Vector3.Distance(ThisObject.position, HookHolder.position);

			if(!Hooked)
			{
				PlayerSpringJoint.maxDistance = 100;
			}

			if(Hooked)
			{
				PlayerSpringJoint.maxDistance = 2;
			}

		}

		//initializing the distance limit
		if(DFH > 10)
		{
			//return the hook to hook holder
			Fired = false;
			Hooked = false;
		}

		//lets now initialize what will happen when hooked
		if(Hooked)
		{
			Fired = false; //lets once again define that Fired will be false, so it does not have any chances of bugging out
			curPosOO = OtherObject.transform.position;
		
			if(lastPosOO != curPosOO)
			{
				transform.position = Vector3.MoveTowards(transform.position, OtherObject.transform.position, 1);
			}

			//we also will call the RopeMovement function
			RopeMovement();

		}

	
	}

	//here we initialize when we want the hook to be colliding
	void OnTriggerEnter(Collider other)
	{
		//you can change the name of the object which comes with the asset named "HOOK_PLACE", but you'll have to change it here too
		//or if you want to use your own object, please rename this to whatever the object you're using is
		if(other.gameObject.name == "HOOK_PLACE")
		{
			Hooked = true; //defining that the hook object is colliding with the HOOK_PLACE, meaning that the hook will stop moving
			OtherObject = other.gameObject;
			Fired = false; //setting Fired to false so that hook will stop moving and stay where it is
			PlayerSpringJoint.maxDistance = 2; //setting the maxDistance for SpringJoint that the player has to 2
			lastPosOO = OtherObject.transform.position;
			print("Hooked at a HOOK Object!");
		}

		//if the other object is TARGET, it means that hook is fired to the limit but has not reached any object to hook on, so it comes back
		if(other.gameObject.tag == "TARGET")
		{
			Hooked = false;
			Fired = false;
		}

	}

	void RopeMovement()
	{
		//first we'll initialize the rigidbody of player
		Rigidbody rig;
		rig = Player.GetComponent<Rigidbody>();

		//let's set up our movement while hooked
		if(Input.GetKey(KeyCode.A))
		{
			rig.AddForce(Vector3.left * Force);
		}

		if(Input.GetKey(KeyCode.D))
		{
			rig.AddForce(Vector3.right * Force);
		}
	}

}
