using Sandbox;
using System;

public class PlayerController : WalkController
{
	public PlayerController()
	{
		SprintSpeed = 600.0f;
		WalkSpeed = 400.0f;
		DefaultSpeed = 190.0f;
		Acceleration = 100.0f;
		AirAcceleration = 1000.0f;
		FallSoundZ = -30.0f;
		GroundFriction = 10.0f;
		StopSpeed = 100.0f;
		Size = 20.0f;
		DistEpsilon = 0.03125f;
		GroundAngle = 46.0f;
		Bounce = 0.0f;
		MoveFriction = 1.0f;
		StepSize = 18.0f;
		MaxNonJumpVelocity = 140.0f;
		BodyGirth = 32.0f;
		BodyHeight = 72.0f;
		EyeHeight = 64.0f;
		Gravity = 400.0f;
		AirControl = 100.0f;
		Swimming = false;
		AutoJump = true;
	}
}
