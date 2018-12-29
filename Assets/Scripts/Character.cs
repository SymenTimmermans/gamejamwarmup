using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject playerCamera;	
	public GameObject playerGraphics;
    public float speed;
    
	private Rigidbody myRigidbody;	
    private Vector3 jump;
    public float jumpForce;
    private Vector3 eulerAngleVelocity;

     private AudioSource myAudioSource;
    public AudioClip jumpClip;

    private bool isDead;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        this.isDead = false;
        this.myAudioSource = this.GetComponent<AudioSource>();
        this.eulerAngleVelocity = new Vector3(0,100,0);
        this.myRigidbody = this.GetComponent<Rigidbody>();
        this.jump = new Vector3(0.0f, 2.0f, 0.0f);
        this.speed = 10f;	
        this.jumpForce = 2.5f;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {						
        // if(Input.GetKey(KeyCode.RightArrow)) {
        //     this.myRigidbody.MovePosition(transform.position + transform.right * Time.deltaTime * this.speed);
        // }

        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     this.myRigidbody.MovePosition(transform.position + -transform.right * Time.deltaTime * this.speed);
        // }

        if(this.isDead) {
            return;
        }

        if(Input.GetKey(KeyCode.RightArrow)) {
            Quaternion deltaRotation = Quaternion.Euler(this.eulerAngleVelocity * Time.deltaTime);
            this.myRigidbody.MoveRotation(this.myRigidbody.rotation * deltaRotation);
        }

        if(Input.GetKey(KeyCode.LeftArrow)) {
            Quaternion deltaRotation = Quaternion.Euler(-this.eulerAngleVelocity * Time.deltaTime);
            this.myRigidbody.MoveRotation(this.myRigidbody.rotation * deltaRotation);
        }

        if(Input.GetKey(KeyCode.UpArrow)) {
            this.myRigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * this.speed);
        }

        if(Input.GetKey(KeyCode.DownArrow)) {
            this.myRigidbody.MovePosition(transform.position + -transform.forward * Time.deltaTime * this.speed);
        }

        //JUMPEHHH
        if(Input.GetKey(KeyCode.Space)) {	
            if (!this.myAudioSource.isPlaying) {
                this.myAudioSource.Play();	 
                this.myRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);                       
            }					                    
                     
        }		
	}

    public void killYourSelf() {
        this.isDead = true;
        Destroy(this.playerGraphics);
    }
}
