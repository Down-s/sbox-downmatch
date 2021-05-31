using Sandbox;

[Library("dm_lasergun", Title="Laser Gun")]
class LaserGun : DMWeapon
{
	private Particles Beam;
	private Particles HitEffect;
	private float BeamTime = 0.0f;

	static SoundEvent ShootSound = new( "sounds/weapons/lasergun/shoot.vsnd" )
	{
		Volume = 1,
		DistanceMax = 2048.0f
	};

	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
	}

	public override void Simulate(Client pl)
	{
		base.Simulate(pl);

		if (Beam != null && Time.Now > BeamTime + 0.075f)
		{
			HitEffect.Destroy(true);

			Beam.Destroy(true);
			Beam = null;
		}
	}

	public override void AttackPrimary()
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
	}
}
