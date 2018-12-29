using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject enemyGraphics;
    public float speed;
    private AudioSource myAudioSource;
    public AudioClip bamClip;
    public AudioClip explosionClip;

    private bool isDangerous = true;
    public float timeTillDangerous;
    private float nextTime;

    // Start is called before the first frame update
    void Start()
    {
        this.myAudioSource = GetComponent<AudioSource>();
        this.speed = 10f;
        this.timeTillDangerous = 2f;
        this.nextTime = Time.time + this.timeTillDangerous;

        this.setColor(Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {            
            this.isDangerous = !this.isDangerous;

            if (this.isDangerous)
            {
                this.setColor(Color.red);
            }
            else
            {
                this.setColor(Color.green);
            }
            this.nextTime = Time.time + this.timeTillDangerous;
        }
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            if (isDangerous)
            {
                // play dead sound and - destroy player cube     
                this.myAudioSource.clip = this.explosionClip;
                this.myAudioSource.Play();

                var character = other.gameObject.GetComponent<Character>();
                character.killYourSelf();
            }
            else
            {
                // play bam sound - and freeze enemy movement?
                this.myAudioSource.clip = this.bamClip;
                if (!this.myAudioSource.isPlaying) {
                    this.myAudioSource.Play();
                }
            }
        }
    }

    private void setColor(Color color)
    {
        this.enemyGraphics.GetComponent<MeshRenderer>().material.color = color;
    }

}
