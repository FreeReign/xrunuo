using System;
using Server;

namespace Server.Items
{
	public class SealingWaxOrder : Item
	{
		public override int LabelNumber { get { return 1073132; } } // Sealing Wax Order addressed to Petrus
		public override int QuestItemHue { get { return PrivateHue; } }

		[Constructable]
		public SealingWaxOrder()
			: base( 0xEBF )
		{
			LootType = LootType.Blessed;
			Weight = 1.0;
		}

		public SealingWaxOrder( Serial serial )
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

			int version = reader.ReadInt();
		}
	}
}