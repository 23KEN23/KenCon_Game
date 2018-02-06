using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.UIElements.StyleEnums;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpHeight;
	public Rigidbody2D rb2d;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;

	private float moveVelocity;
	private bool doubleJumped;
	private Animator anim;

	public Transform firePoint;
	public GameObject ninjaStar;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}



	//FixedUpdate is called a certain number of times per second
	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

	}



	// Update is called once per frame
	void Update () {
		anim.SetBool("Grounded", grounded);
//		moveVelocity = 0f;

		if(grounded){
			doubleJumped = false;
		}

		if( Input.GetButtonDown ("Jump") && grounded){
			Jump();
		}

		if( Input.GetButtonDown ("Jump") && !doubleJumped && ! grounded){
			Jump();
			doubleJumped = true;
		}
			
		moveVelocity = moveSpeed * Input.GetAxisRaw ("Horizontal");

		GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
		anim.SetFloat ("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

		if (GetComponent<Rigidbody2D> ().velocity.x > 0) {
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} 
		else if (GetComponent<Rigidbody2D> ().velocity.x < 0) {
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}

		if (Input.GetKeyDown (KeyCode.Return)) {
			Instantiate (ninjaStar, firePoint.position, firePoint.rotation);
		}
			
	}

	public void Jump(){
		rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
	}
}
