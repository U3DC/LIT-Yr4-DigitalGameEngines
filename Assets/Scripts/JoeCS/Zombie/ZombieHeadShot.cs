﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHeadShot : MonoBehaviour {

	//public AudioSource audio;
	public AudioClip shotFX;

	AudioSource audio1;														// Use zombies main audiosource instead

    public int zombieHealth = 5;                							// Default enemy health value set to 15

	//public ManageZombies zombieCount;
	public GameObject bloodSplatter;
	public GameObject destroyZombie;										// Needed for separate head shots

	//public Text actionText;												// Canvas action text
    
	GameObject gc;

	void Start(){
		gc = GameObject.FindWithTag ("GameController");
		audio1 = destroyZombie.GetComponent<AudioSource> ();
	}

	/*
    void Update()    {
		if (zombieHealth <= 0)                   							// If the enemies health has run out
		{
			//Destroy(gameObject);              							// Destroy the game object the script is attached to
			Destroy(destroyZombie);              							// Destroy the game object specified by destroyZombie

			//if (gameObject.tag == "Zombie")								// If the object to be destroyed is a target
				zombieCount.incrementZombies ();							// Increment the number of zombies killed
        }
    }
    */

    void DeductPoints(int DamageAmount)
    {
		if (destroyZombie.GetComponent<ZombieHealth> ().currentHealth > 0 && destroyZombie.GetComponent<ZombieHealth>().isAlive()) {
			Debug.Log("<color=green>HEADSHOT!!!</color>");

			zombieHealth -= DamageAmount;            						// Decrement enemy health
			audio1.clip = shotFX;											// Load the effect into the audio source
			audio1.Play ();													// Play the effect
			StartCoroutine (BloodSplatter ());
			destroyZombie.GetComponent<ZombieHealth> ().setHealth(0);		// Kill the Zombie
			destroyZombie.GetComponent<ZombieHealth> ().setDead();			// Kill the Zombie
		}
    }

	/*
	 	1 headshot needed to kill Zombie
 	*/
	IEnumerator BloodSplatter(){
		destroyZombie.GetComponent<Animator> ().SetTrigger ("HeadShot");

		bloodSplatter.SetActive (true);
		//yield return new WaitForSeconds (0.4f);							// Display the particle for the specified time
		//bloodSplatter.SetActive (false);									// Turn it off (no need really)
		//zombieCount.incrementZombies ();									// Increment the number of zombies killed
		//gc.GetComponent<ManageZombies>().incrementZombies();
		gc.GetComponent<ManageZombies>().incrementZombieHeadshots();
		//actionText.GetComponent<Text> ().text = "Headshot 50";			// Display headshot message
		//yield return new WaitForSeconds (2);								// Show on screen for specified time
		//actionText.GetComponent<Text> ().text = "";						// Then clear the message

		//yield return new WaitForSeconds (1);								// Show on screen for specified time
		yield return new WaitForSeconds (3);								// Show on screen for specified time

		bloodSplatter.SetActive (false);

		destroyZombie.GetComponent<SphereCollider> ().enabled = false;		// Don't get hurt after colliding

		//destroyZombie.SetActive(false)									// Leave zombies on the ground, instead of killing
		//Destroy(destroyZombie);              								// Destroy the game object specified by destroyZombie
	}
}
