using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

	private void Awake()
	{
		GameObject[] music = GameObject.FindGameObjectsWithTag("Muzika");

		if (music.Length > 1)
		{
			Destroy(this.gameObject);

		}
		DontDestroyOnLoad(this.gameObject);
	}
}
