//
//  X-RunUO - Ultima Online Server Emulator
//  Copyright (C) 2015 Pedro Pardal
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Network;

namespace Server
{
	public class RegionRect : IComparable
	{
		private Region m_Region;
		private Rectangle3D m_Rect;

		public Region Region { get { return m_Region; } }
		public Rectangle3D Rect { get { return m_Rect; } }

		public RegionRect( Region region, Rectangle3D rect )
		{
			m_Region = region;
			m_Rect = rect;
		}

		public bool Contains( Point3D loc )
		{
			return m_Rect.Contains( loc );
		}

		int IComparable.CompareTo( object obj )
		{
			if ( obj == null )
				return 1;

			RegionRect regRect = obj as RegionRect;

			if ( regRect == null )
				throw new ArgumentException( "obj is not a RegionRect", "obj" );

			return ( (IComparable) m_Region ).CompareTo( regRect.m_Region );
		}
	}


	public class Sector
	{
		private int m_X, m_Y;
		private Map m_Owner;
		private List<Mobile> m_Mobiles;
		private List<Mobile> m_Players;
		private List<Item> m_Items;
		private List<GameClient> m_Clients;
		private List<BaseMulti> m_Multis;
		private List<RegionRect> m_RegionRects;
		private bool m_Active;

		// TODO: Can we avoid this?
		private static List<Mobile> m_DefaultMobileList = new List<Mobile>();
		private static List<Item> m_DefaultItemList = new List<Item>();
		private static List<GameClient> m_DefaultClientList = new List<GameClient>();
		private static List<BaseMulti> m_DefaultMultiList = new List<BaseMulti>();
		private static List<RegionRect> m_DefaultRectList = new List<RegionRect>();

		public Sector( int x, int y, Map owner )
		{
			m_X = x;
			m_Y = y;
			m_Owner = owner;
			m_Active = false;
		}

		private void Add<T>( ref List<T> list, T value )
		{
			if ( list == null )
			{
				list = new List<T>();
			}

			list.Add( value );
		}

		private void Remove<T>( ref List<T> list, T value )
		{
			if ( list != null )
			{
				list.Remove( value );

				if ( list.Count == 0 )
				{
					list = null;
				}
			}
		}

		private void Replace<T>( ref List<T> list, T oldValue, T newValue )
		{
			if ( oldValue != null && newValue != null )
			{
				int index = ( list != null ? list.IndexOf( oldValue ) : -1 );

				if ( index >= 0 )
				{
					list[index] = newValue;
				}
				else
				{
					Add( ref list, newValue );
				}
			}
			else if ( oldValue != null )
			{
				Remove( ref list, oldValue );
			}
			else if ( newValue != null )
			{
				Add( ref list, newValue );
			}
		}

		public void OnClientChange( GameClient oldState, GameClient newState )
		{
			Replace( ref m_Clients, oldState, newState );
		}

		public void OnEnter( Item item )
		{
			Add( ref m_Items, item );
		}

		public void OnLeave( Item item )
		{
			Remove( ref m_Items, item );
		}

		public void OnEnter( Mobile mob )
		{
			Add( ref m_Mobiles, mob );

			if ( mob.Client != null )
			{
				Add( ref m_Clients, mob.Client );
			}

			if ( mob.IsPlayer )
			{
				if ( m_Players == null )
				{
					m_Owner.ActivateSectors( m_X, m_Y );
				}

				Add( ref m_Players, mob );
			}
		}

		public void OnLeave( Mobile mob )
		{
			Remove( ref m_Mobiles, mob );

			if ( mob.Client != null )
			{
				Remove( ref m_Clients, mob.Client );
			}

			if ( mob.IsPlayer && m_Players != null )
			{
				Remove( ref m_Players, mob );

				if ( m_Players == null )
				{
					m_Owner.DeactivateSectors( m_X, m_Y );
				}
			}
		}

		public void OnEnter( Region region, Rectangle3D rect )
		{
			Add( ref m_RegionRects, new RegionRect( region, rect ) );

			m_RegionRects.Sort();

			UpdateMobileRegions();
		}

		public void OnLeave( Region region )
		{
			if ( m_RegionRects != null )
			{
				for ( int i = m_RegionRects.Count - 1; i >= 0; i-- )
				{
					RegionRect regRect = m_RegionRects[i];

					if ( regRect.Region == region )
					{
						m_RegionRects.RemoveAt( i );
					}
				}

				if ( m_RegionRects.Count == 0 )
				{
					m_RegionRects = null;
				}
			}

			UpdateMobileRegions();
		}

		private void UpdateMobileRegions()
		{
			if ( m_Mobiles != null )
			{
				List<Mobile> sandbox = new List<Mobile>( m_Mobiles );

				foreach ( Mobile mob in sandbox )
				{
					mob.UpdateRegion();
				}
			}
		}

		public void OnMultiEnter( BaseMulti multi )
		{
			Add( ref m_Multis, multi );
		}

		public void OnMultiLeave( BaseMulti multi )
		{
			Remove( ref m_Multis, multi );
		}

		public void Activate()
		{
			if ( !Active && m_Owner != Map.Internal )
			{
				if ( m_Items != null )
				{
					foreach ( Item item in m_Items )
					{
						item.OnSectorActivate();
					}
				}

				if ( m_Mobiles != null )
				{
					foreach ( Mobile mob in m_Mobiles )
					{
						mob.OnSectorActivate();
					}
				}

				m_Active = true;
			}
		}

		public void Deactivate()
		{
			if ( Active )
			{
				if ( m_Items != null )
				{
					foreach ( Item item in m_Items )
					{
						item.OnSectorDeactivate();
					}
				}

				if ( m_Mobiles != null )
				{
					foreach ( Mobile mob in m_Mobiles )
					{
						mob.OnSectorDeactivate();
					}
				}

				m_Active = false;
			}
		}

		public List<RegionRect> RegionRects
		{
			get
			{
				if ( m_RegionRects == null )
					return m_DefaultRectList;

				return m_RegionRects;
			}
		}

		public List<BaseMulti> Multis
		{
			get
			{
				if ( m_Multis == null )
					return m_DefaultMultiList;

				return m_Multis;
			}
		}

		public List<Mobile> Mobiles
		{
			get
			{
				if ( m_Mobiles == null )
					return m_DefaultMobileList;

				return m_Mobiles;
			}
		}

		public List<Item> Items
		{
			get
			{
				if ( m_Items == null )
					return m_DefaultItemList;

				return m_Items;
			}
		}

		public List<GameClient> Clients
		{
			get
			{
				if ( m_Clients == null )
					return m_DefaultClientList;

				return m_Clients;
			}
		}

		public List<Mobile> Players
		{
			get
			{
				if ( m_Players == null )
					return m_DefaultMobileList;

				return m_Players;
			}
		}

		public bool Active
		{
			get
			{
				return ( m_Active && m_Owner != Map.Internal );
			}
		}

		public Map Owner
		{
			get
			{
				return m_Owner;
			}
		}

		public int X
		{
			get
			{
				return m_X;
			}
		}

		public int Y
		{
			get
			{
				return m_Y;
			}
		}
	}
}