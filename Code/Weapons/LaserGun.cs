using Sandbox;

[Library("dm_lasergun", Title="Laser Gun")]
class LaserGun : DMWeapon
{
	private Particles Beam;
	private Particles HitEffect;
	private float NextHit = 0.0f;

	public LaserGun()
	{
		Damage = 20.0f;
	}

	static SoundEvent ShootSound = new( "sounds/downmatch/weapons/lasergun/attack.vsnd" )
	{
		Volume = 0.65f,
		DistanceMax = 2048.0f
	};

	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
	}

	void CheckLaser()
	{
		if (Owner == null || !IsActiveChild())
		{
			Beam.Destroy(true);
			Beam = null;
			return;
		}

		if (Owner.Input.Down(InputButton.Attack1))
		{
			TraceResult tr = Trace.Ray(Owner.EyePos, Owner.EyePos + (Owner.EyeRot.Forward * 8192)).Radius(5).Ignore(Owner).Run();

			if (Beam == null)
			{
				Beam = Particles.Create( "particles/lasers/laser-beam.vpcf", tr.EndPos );
				Beam.SetEntityAttachment( 0, EffectEntity, "muzzle", true );
			}

			Beam.SetPos( 1, tr.EndPos );

			if (Time.Now > NextHit)
			{
				if (IsClient)
				{
					var CrPanel = CrosshairPanel as CrosshairPanel;
					CrPanel.Crosshair.OnEvent("onattack");
				}

				PlaySound(ShootSound.Name);
				Shoot(tr);
				NextHit = Time.Now + 0.1f;
			}
		}
		else
		{
			if (Beam != null)
			{
				Beam.Destroy(true);
				Beam = null;
			}
		}
	}

	public override void OnCarryDrop( Entity dropper )
	{
		base.OnCarryDrop(dropper);
	}

	public override void Simulate(Client pl)
	{
		base.Simulate(pl);

		CheckLaser();

		/*if (Beam != null && Time.Now > BeamTime + 0.075f)
		{
			HitEffect.Destroy(true);

			Beam.Destroy(true);
			Beam = null;
		}*/
	}

	/*public override void AttackPrimary()
	{
		base.AttackPrimary();

		TraceResult tr = Trace.Ray(Owner.EyePos, Owner.EyePos + (Owner.EyeRot.Forward * 4096)).Radius(5).Ignore(Owner).Run();
		
		HitEffect = Particles.Create( "particles/lasers/laser-start.vpcf" );
		HitEffect.SetPos( 0, tr.EndPos );

		if (Beam == null)
		{
			Beam = Particles.Create( "particles/lasers/laser-beam.vpcf", tr.EndPos );
			Beam.SetEntityAttachment( 0, EffectEntity, "muzzle", true );
			Beam.SetPos( 1, tr.EndPos );
			BeamTime = Time.Now;
		}

		if ( IsLocalPawn )
		{
			new Sandbox.ScreenShake.Perlin( 0.85f, 1.5f, 4.5f);
		}

		Shoot(tr);

		ViewModelEntity?.SetAnimBool("fire", true);
		PlaySound(ShootSound.Name);
	}*/
}
