using Microsoft.Xna.Framework;

namespace Nez
{
	public class VirtualAxis2D
	{
		public VirtualAxis X, Y;

		public Vector2 Value
		{
			get
			{
				var val = new Vector2(X.Value, Y.Value);
				if (ShouldNormalize)
					return Vector2Ext.Normalize(val);
				return val;
			}
		}

		public bool ShouldNormalize = false;

		public VirtualAxis2D(VirtualAxis horizontal, VirtualAxis vertical)
		{
			X = horizontal;
			Y = vertical;
		}
	}
}
