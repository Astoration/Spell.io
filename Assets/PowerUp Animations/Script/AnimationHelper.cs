using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour {
	public bool INFINITE_LOOP = true; //For non-fixed duration use this. If true, then duration is ignored
	public float duration = 5.5f; //Fixed duration of the vfx
	public bool DESTROY_ON_END = true;
	public GameObject[] main_particles;
	private float start_time = -100.0f;
	private bool GO = true;


	void Start () 
	{
		start_time = Time.time;	
	}


	void Update () 
	{
		if (!INFINITE_LOOP && duration + start_time <= Time.time && GO)
			STOP_VFX();
	}


	public void STOP_VFX()
	{
		GO = false;
		for(int i = 0; i < main_particles.Length; i++)
		{
			foreach (Transform child in main_particles[i].transform)
		    {
		        child.GetComponent<ParticleSystem>().Stop();
		    }
		}		
		if (DESTROY_ON_END) 
			Destroy (gameObject, 0.33f); //A little time before destroying, to let remaining particles to die first. It looks better.
	}


	public void PLAY_VFX()
	{
		for(int i = 0; i < main_particles.Length; i++)
		{
			foreach (Transform child in main_particles[i].transform)
		    {
		        child.GetComponent<ParticleSystem>().Play();
		    }
		}
		start_time = Time.time;
		GO = true;
	}

	
}