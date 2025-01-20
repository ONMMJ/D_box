using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Never_destory : MonoBehaviour
{
	public static Never_destory instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	void Awake()
	{
		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{

	}
}
