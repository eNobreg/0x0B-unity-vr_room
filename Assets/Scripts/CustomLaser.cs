using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLaser : MonoBehaviour
{
    // Start is called before the first frame update
	GameObject holder;
	public bool activeParticles = false;

	public float rayLength = 2f;
	public Animator door;
	public LineRenderer line;
	public GameObject particles;

	public bool buttonsPressed = false;

	public GameObject[] buttons;

    void Start()
    {
		line.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray raycast = new Ray(this.transform.position, this.transform.forward);
		RaycastHit hit;
		
		bool bHit  = Physics.Raycast(raycast, out hit, rayLength);

		if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) <= 0.01f)
		{
			
			if (bHit)
			{	line.enabled = true;
				line.SetPosition(0, this.transform.position);
				line.SetPosition(1, hit.point);

				if (OVRInput.Get(OVRInput.Button.One) && hit.collider.CompareTag("Handle") == true)
				{
					checkPressed();
					if (buttonsPressed == true)
					{
						door.SetBool("character_nearby", true);
						StartCoroutine("door_timer");
					}
					
				}
				if (OVRInput.GetDown(OVRInput.Button.One) && hit.collider.CompareTag("Projector") == true)
				{
					if (activeParticles == false)
					{
						Debug.Log("First");
						particles.SetActive(true);
						activeParticles = true;
					}
					else if (activeParticles == true)
					{
						Debug.Log("Second");
						particles.SetActive(false);
						activeParticles = false;
					}
				}
			}
			else
				line.enabled = false;
		}
		else
			line.enabled = false;

    }
	
	IEnumerator door_timer()
	{
		yield return new WaitForSeconds(5f);
		door.SetBool("character_nearby", false);
	}

	public void checkPressed()
	{
		bool flag = true;
		foreach (GameObject entry in buttons)
		{
			if (entry.GetComponent<CustomButton>().pressed == false)
			{
				flag = false;
			}
		}
		if (flag != false)
		{
			buttonsPressed = true;
		}
	}
}
