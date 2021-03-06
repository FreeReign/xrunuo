using System;
using Server.Network;
using Server.Items;
using Server.Engines.Craft;

namespace Server.Items
{
	[Alterable( typeof( DefBlacksmithy ), typeof( DreadSword ) )]
	[FlipableAttribute( 0xF5E, 0xF5F )]
	public class Broadsword : BaseSword
	{
		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ArmorIgnore; } }

		public override int StrengthReq { get { return 30; } }
		public override int MinDamage { get { return 14; } }
		public override int MaxDamage { get { return 15; } }
		public override int Speed { get { return 13; } }

		public override int HitSound { get { return 0x237; } }
		public override int MissSound { get { return 0x23A; } }

		public override int InitMinHits { get { return 31; } }
		public override int InitMaxHits { get { return 100; } }

		[Constructable]
		public Broadsword()
			: base( 0xF5E )
		{
			Weight = 6.0;
		}

		public Broadsword( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			/*int version = */
			reader.ReadInt();
		}
	}
}