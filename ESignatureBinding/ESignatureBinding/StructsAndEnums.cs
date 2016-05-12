using System;
using System.Runtime.InteropServices;

namespace KAS.Esignature
{
	[StructLayout (LayoutKind.Sequential)]
	public struct SPUserResizableViewAnchorPoint
	{
		public nfloat adjustsX;

		public nfloat adjustsY;

		public nfloat adjustsH;

		public nfloat adjustsW;
	}
}
