using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class Canir : MondainQuester
	{
		public override Type[] Quests
		{
			get
			{
				return new Type[] 
			{ 
				typeof( TroglodytesQuest ),
				typeof( TrogAndHisDogQuest )
			};
			}
		}

		[Constructable]
		public Canir()
			: base( "Canir", "the thaumaturgist" )
		{
			SetSkill( SkillName.Focus, 60.0, 83.0 );
		}

		public Canir( Serial serial )
			: base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );

			Female = true;
			CantWalk = true;
			Race = Race.Elf;

			Hue = 0x876C;
			HairItemID = 0x2FD0;
			HairHue = 0x33;
		}

		public override void InitOutfit()
		{
			AddItem( new Sandals( 0x1BB ) );
			AddItem( new ElvenRobe( 0x5A5 ) );
			AddItem( new GemmedCirclet() );
			AddItem( RandomWand.CreateWand() );
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