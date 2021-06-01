using Sandbox;
using System;

partial class DMWeapon : BaseWeapon
{
	public float Damage = 100.0f;

	static SoundEvent HitSound1 = new( "sounds/downmatch/misc/hit_1.vsnd" ) {Volume = 0.6f,DistanceMax = 500.0f};
	static SoundEvent HitSound2 = new( "sounds/downmatch/misc/hit_2.vsnd" ) {Volume = 0.6f,DistanceMax = 500.0f};

	public override void AttackPrimary()
	{

	}

	public void Shoot(TraceResult tr)
	{
		using (Prediction.Off())
		{
			var dmg = DamageInfo.FromBullet(tr.EndPos, tr.Normal, Damage).
				UsingTraceResult(tr).
				WithAttacker(Owner).
				WithWeapon(this);

			tr.Entity.TakeDamage(dmg);

			if (IsServer && tr.Entity is Player)
			{
				PlaySound("DMWeapon.HitSound" + new Random().Next(1, 2));
			}
		}
	}

	public override void CreateHudElements()
	{
		if ( Local.Hud == null ) return;

		CrosshairPanel = new CrosshairPanel();
		CrosshairPanel.Parent = Local.Hud;
	}
}
