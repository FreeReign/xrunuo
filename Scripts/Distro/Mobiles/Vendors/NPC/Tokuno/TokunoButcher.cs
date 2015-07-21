using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class TokunoButcher : Butcher
	{
		[Constructable]
		public TokunoButcher()
		{
		}

		public override VendorShoeType ShoeType { get { return VendorShoeType.NinjaTabi; } }

		public override void InitOutfit()
		{
			AddItem( new Cleaver() );
			AddItem( new LeatherNinjaPants() );
			AddItem( new HalfApron() );
			AddItem( new HakamaShita() );
		}

		public TokunoButcher( Serial serial )
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

			/*int version = */reader.ReadInt();
		}
	}
}