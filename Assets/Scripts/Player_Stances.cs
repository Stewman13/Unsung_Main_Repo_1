using UnityEngine;
using System.Collections;

public class Player_Stances : MonoBehaviour 
{

	private PlayerStances currentStance;

	public bool playerWalking = false;
	public bool playerRunning = false;
	public bool playerSneaking = false;

	public int ap = 10;




	public enum PlayerStances 
	{
		Walk,
		Run,
		Sneak

	}

	void Start()
	{


		currentStance = PlayerStances.Walk;

	}

	void Update ()
	{
		Debug.Log (currentStance);

			switch (currentStance) 
			{
				case (PlayerStances.Walk):
					playerWalking = true;
					playerRunning = false;
					playerSneaking = false;
					
						break;

				case (PlayerStances.Run):
					playerWalking = false;
					playerRunning = true;
					playerSneaking = false;
					
						break;

				case (PlayerStances.Sneak):
					playerWalking = false;
					playerRunning = false;
					playerSneaking = true;
			
						break;

			}
	}

	void OnGUI()
	{
		if (GUILayout.Button ("Walk Stance")) 
		{
			if(currentStance == PlayerStances.Sneak)
			{
				currentStance = PlayerStances.Walk;
			}
			else if(currentStance == PlayerStances.Run)
			{
				currentStance = PlayerStances.Walk;
			}
		}

		if (GUILayout.Button ("Run Stance") && ap >= 5) 
		{
			if(currentStance == PlayerStances.Sneak)
			{
				currentStance = PlayerStances.Run;
				ap -= 5;
			}
			else if(currentStance == PlayerStances.Walk)
			{
				currentStance = PlayerStances.Run;
				ap -= 5;
			}
		}

		if (GUILayout.Button ("Sneak Stance") && ap >= 5) 
		{
			if(currentStance == PlayerStances.Walk)
			{
				currentStance = PlayerStances.Sneak;
				ap -= 5;
			}
			else if(currentStance == PlayerStances.Run)
			{
				currentStance = PlayerStances.Sneak;
				ap -= 5;
			}
		}
	}









}
