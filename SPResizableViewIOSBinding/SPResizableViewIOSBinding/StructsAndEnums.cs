using System;
using System.Runtime.InteropServices;

namespace KAS.SPResizableView
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
