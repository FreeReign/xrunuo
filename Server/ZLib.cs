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
using System.Runtime.InteropServices;

namespace Server
{
	public enum ZLibError : int
	{
		Z_VERSION_ERROR = -6,
		Z_BUF_ERROR = -5,
		Z_MEM_ERROR = -4,
		Z_DATA_ERROR = -3,
		Z_STREAM_ERROR = -2,
		Z_ERRNO = -1,
		Z_OK = 0,
		Z_STREAM_END = 1,
		Z_NEED_DICT = 2
	}

	public enum ZLibCompressionLevel : int
	{
		Z_DEFAULT_COMPRESSION = -1,
		Z_NO_COMPRESSION = 0,
		Z_BEST_SPEED = 1,
		Z_BEST_COMPRESSION = 9
	}

	public class ZLibWin32
	{
		[DllImport( "zlib32" )]
		public static extern string zlibVersion();
		[DllImport( "zlib32" )]
		public static extern ZLibError compress( byte[] dest, ref int destLength, byte[] source, int sourceLength );
		[DllImport( "zlib32" )]
		public static extern ZLibError compress2( byte[] dest, ref int destLength, byte[] source, int sourceLength, ZLibCompressionLevel level );
		[DllImport( "zlib32" )]
		public static extern ZLibError uncompress( byte[] dest, ref int destLen, byte[] source, int sourceLen );
	}

	public class ZLibWin64
	{
		[DllImport( "zlib64" )]
		public static extern string zlibVersion();
		[DllImport( "zlib64" )]
		public static extern ZLibError compress( byte[] dest, ref int destLength, byte[] source, int sourceLength );
		[DllImport( "zlib64" )]
		public static extern ZLibError compress2( byte[] dest, ref int destLength, byte[] source, int sourceLength, ZLibCompressionLevel level );
		[DllImport( "zlib64" )]
		public static extern ZLibError uncompress( byte[] dest, ref int destLen, byte[] source, int sourceLen );
	}

	public class ZLibUnix
	{
		[DllImport( "libz.so" )]
		public static extern string zlibVersion();
		[DllImport( "libz.so" )]
		public static extern ZLibError compress( byte[] dest, ref long destLength, byte[] source, long sourceLength );
		[DllImport( "libz.so" )]
		public static extern ZLibError compress2( byte[] dest, ref long destLength, byte[] source, long sourceLength, ZLibCompressionLevel level );
		[DllImport( "libz.so" )]
		public static extern ZLibError uncompress( byte[] dest, ref long destLen, byte[] source, long sourceLen );
	}

	public class ZLib
	{
		public static string zlibVersion()
		{
			if ( Environment.Unix )
				return ZLibUnix.zlibVersion();
			else if ( Environment.Is64Bit )
				return ZLibWin64.zlibVersion();
			else
				return ZLibWin32.zlibVersion();
		}

		public static ZLibError compress( byte[] dest, ref int destLength, byte[] source, int sourceLength )
		{
			if ( Environment.Unix )
			{
				long dl2 = destLength;
				ZLibError ret = ZLibUnix.compress( dest, ref dl2, source, sourceLength );
				destLength = (int) dl2;
				return ret;
			}
			else if ( Environment.Is64Bit )
			{
				return ZLibWin64.compress( dest, ref destLength, source, sourceLength );
			}
			else
			{
				return ZLibWin32.compress( dest, ref destLength, source, sourceLength );
			}
		}

		public static ZLibError compress2( byte[] dest, ref int destLength, byte[] source, int sourceLength, ZLibCompressionLevel level )
		{
			if ( Environment.Unix )
			{
				long dl2 = destLength;
				ZLibError ret = ZLibUnix.compress2( dest, ref dl2, source, sourceLength, level );
				destLength = (int) dl2;
				return ret;
			}
			else if ( Environment.Is64Bit )
			{
				return ZLibWin64.compress2( dest, ref destLength, source, sourceLength, level );
			}
			else
			{
				return ZLibWin32.compress2( dest, ref destLength, source, sourceLength, level );
			}
		}

		public static ZLibError uncompress( byte[] dest, ref int destLen, byte[] source, int sourceLen )
		{
			if ( Environment.Unix )
			{
				long dl2 = destLen;
				ZLibError ret = ZLibUnix.uncompress( dest, ref dl2, source, sourceLen );
				destLen = (int) dl2;
				return ret;
			}
			else if ( Environment.Is64Bit )
			{
				return ZLibWin64.uncompress( dest, ref destLen, source, sourceLen );
			}
			else
			{
				return ZLibWin32.uncompress( dest, ref destLen, source, sourceLen );
			}
		}
	}
}