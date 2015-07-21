﻿//
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

namespace Server
{
	public struct Serial : IComparable
	{
		private int m_Serial;

		public static readonly Serial MinusOne = new Serial( -1 );
		public static readonly Serial Zero = new Serial( 0 );

		public Serial( int serial )
		{
			m_Serial = serial;
		}

		public int Value
		{
			get
			{
				return m_Serial;
			}
		}

		public bool IsMobile
		{
			get
			{
				return ( m_Serial > 0 && m_Serial < 0x40000000 );
			}
		}

		public bool IsItem
		{
			get
			{
				return ( m_Serial >= 0x40000000 && m_Serial <= 0x7FFFFFFF );
			}
		}

		public bool IsValid
		{
			get
			{
				return ( m_Serial > 0 );
			}
		}

		public override int GetHashCode()
		{
			return m_Serial;
		}

		public int CompareTo( object o )
		{
			if ( o == null )
				return 1;
			else if ( !( o is Serial ) )
				throw new ArgumentException();

			int ser = ( (Serial) o ).m_Serial;

			if ( m_Serial > ser )
				return 1;
			else if ( m_Serial < ser )
				return -1;
			else
				return 0;
		}

		public override bool Equals( object o )
		{
			if ( o == null || !( o is Serial ) )
				return false;

			return ( (Serial) o ).m_Serial == m_Serial;
		}

		public static bool operator ==( Serial l, Serial r )
		{
			return l.m_Serial == r.m_Serial;
		}

		public static bool operator !=( Serial l, Serial r )
		{
			return l.m_Serial != r.m_Serial;
		}

		public static bool operator >( Serial l, Serial r )
		{
			return l.m_Serial > r.m_Serial;
		}

		public static bool operator <( Serial l, Serial r )
		{
			return l.m_Serial < r.m_Serial;
		}

		public static bool operator >=( Serial l, Serial r )
		{
			return l.m_Serial >= r.m_Serial;
		}

		public static bool operator <=( Serial l, Serial r )
		{
			return l.m_Serial <= r.m_Serial;
		}

		public override string ToString()
		{
			return String.Format( "0x{0:X8}", m_Serial );
		}

		public static implicit operator int( Serial a )
		{
			return a.m_Serial;
		}

		public static implicit operator Serial( int a )
		{
			return new Serial( a );
		}
	}
}