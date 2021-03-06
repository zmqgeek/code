			var s = "843370923007003347112437570992242323";
			var result = new List<byte>();
			result.Add( 0 );
			foreach ( char c in s )
			{
				int val = (int)( c - '0' );
				for ( int i = 0 ; i < result.Count ; i++ )
				{
					int digit = result[i] * 10 + val;
					result[i] = (byte)( digit & 0x0F );
					val = digit >> 4;
				}
				if ( val != 0 )
					result.Add( (byte)val );
			}
			var hex = "";
			foreach ( byte b in result )
				hex = "0123456789ABCDEF"[ b ] + hex;
