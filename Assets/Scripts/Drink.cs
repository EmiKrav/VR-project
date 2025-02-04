using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.Playables;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.ParticleSystem;

public class Drink : MonoBehaviour
{
	[SerializeField] private AudioClip drinksound;
	private bool drinkable = true;
	private bool drinking = false;
	bool refiling = false;
	public Mouth mouth;
	public PlayableDirector director;
	public AlembicStreamPlayer streamPlayerScript;
	public GameObject coffemachine;
	float timetostop;
	[SerializeField] private AudioClip poursound;

	GameObject[] particles;
	// Start is called before the first frame update
	void Start()
    {
		director.time = streamPlayerScript.CurrentTime;
		timetostop = streamPlayerScript.CurrentTime;
		particles = GameObject.FindGameObjectsWithTag("LiquidParticle");
	}

    // Update is called once per frame
    void Update()
    {
		var distance = Vector3.Distance(mouth.transform.position, transform.position);

		if (distance <= mouth.radius)
		{
			if (!drinking)
			{
				Slurp();
				drinking = true;
			}
			
		}
		else
		{
			if (drinking == true)
			{
				SoundManager.instance.destroyEffects();
			}
			drinking = false;
			director.Pause();
			
		}
		if (refiling)
		{
			director.Play();
			drinkable = true;
			if (director.time >= timetostop - 0.04)
			{
				streamPlayerScript.CurrentTime = streamPlayerScript.EndTime;
				director.Pause();
				StopRefil();
			}
		}

	}
	public void Slurp()
	{
		if (drinkable)
		{
			director.timeUpdateMode = DirectorUpdateMode.Manual;
			director.Play();
			director.time -= (Time.deltaTime + 0.5);
			streamPlayerScript.CurrentTime = (float)director.time;
			if (director.time <= 0) {
				foreach (GameObject par in particles)
				{
					par.SetActive(false);
				}
				drinkable = false;
				streamPlayerScript.CurrentTime = 0;

			}
			SoundManager.instance.playEfektus(drinksound, transform);
		}
	}
	public void Refill()
	{

		
		var distance = Vector3.Distance(coffemachine.transform.position, transform.position);
		if (distance <= 0.5)
		{
			SoundManager.instance.playEfektus(poursound, transform);
			director.timeUpdateMode = DirectorUpdateMode.GameTime;
			transform.position = coffemachine.transform.position;
			transform.rotation = coffemachine.transform.rotation;
			foreach (GameObject par in particles)
			{
				par.SetActive(true);
			}
			refiling = true;
			director.initialTime = streamPlayerScript.CurrentTime;
			director.time = streamPlayerScript.CurrentTime;
		}
		else {
			refiling = false;
		}
	}
	public void StopRefil()
	{
		refiling = false;
	}

}
