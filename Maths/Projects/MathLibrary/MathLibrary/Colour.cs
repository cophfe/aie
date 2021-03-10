using System;
using System.Collections.Generic;
using System.Text;

namespace Mlib
{
	public struct Colour
	{
		public uint colour;
		
		public Colour(byte r, byte g, byte b, byte a)
		{
			colour = (uint)(r << 24| g << 16 | b << 8 | a);
		}

		public byte GetRed()
		{
			return (byte)((colour & 0xFF000000) >> 24);

		}

		public byte GetGreen()
		{
			return (byte)((colour & 0x00FF0000) >> 16);
		}

		public byte GetBlue()
		{
			return (byte)((colour & 0x0000FF00) >> 8);
		}

		public byte GetAlpha()
		{
			return (byte)(colour & 0xFF);
		}

		public void SetRed(byte r)
		{
			colour = ((colour & 0x00FFFFFF) | (uint)(r << 24));
		}

		public void SetGreen(byte g)
		{
			colour = ((colour & 0xFF00FFFF) | (uint)(g << 16));
		}

		public void SetBlue(byte b)
		{
			colour = ((colour & 0xFFFF00FF) | (uint)(b << 8));
		}

		public void SetAlpha(byte a)
		{
			colour = (colour & 0xFFFFFF00) | a;
		}
	}
}
