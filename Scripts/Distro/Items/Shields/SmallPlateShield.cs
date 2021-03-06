using System;
using Server;

namespace Server.Items
{
	public class SmallPlateShield : BaseShield
	{
		public override int BasePhysicalResistance { get { return 0; } }
		public override int BaseFireResistance { get { return 0; } }
		public override int BaseColdResistance { get { return 1; } }
		public override int BasePoisonResistance { get { return 0; } }
		public override int BaseEnergyResistance { get { return 0; } }

		public override int InitMinHits { get { return 25; } }
		public override int InitMaxHits { get { return 35; } }

		public override int StrengthReq { get { return 20; } }

		public override Race RequiredRace { get { return Race.Gargoyle; } }

		[Constructable]
		public SmallPlateShield()
			: base( 0x4202 )
		{
			Weight = 6.0;
		}

		public SmallPlateShield( Serial serial )
			: base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			/*int version = */
			reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); //version
		}
	}
}