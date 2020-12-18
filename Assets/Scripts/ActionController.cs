using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    // Start is called before the first frame update
	public GameObject customLaser;

	public GameObject lockLight;
	public Light lockPoint;
	public Material[] red;

	public Material[] green;
    void Start()
    {
        lockLight.GetComponent<Renderer>().materials = red;
		lockPoint.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
		customLaser.SendMessage("checkPressed");
        bool isPressed = customLaser.gameObject.GetComponent<CustomLaser>().buttonsPressed;
		if (isPressed)
		{
			lockLight.GetComponent<Renderer>().materials = green;
			lockPoint.color = Color.green;
		}
    }
}
