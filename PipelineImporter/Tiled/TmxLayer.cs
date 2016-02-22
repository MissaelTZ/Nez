using System.Collections.Generic;
using System.Xml.Serialization;


namespace Nez.TiledMaps
{
	[XmlInclude( typeof( TmxTileLayer ) )]
	[XmlInclude( typeof( TmxImageLayer ) )]
	public abstract class TmxLayer
	{
		[XmlAttribute( AttributeName = "name" )]
		public string name;

		[XmlAttribute( AttributeName = "opacity" )]
		public float opacity = 1f;

		[XmlAttribute( AttributeName = "visible" )]
		public bool visible = true;

		[XmlArray( "properties" )]
		[XmlArrayItem( "property" )]
		public List<TmxProperty> properties = new List<TmxProperty>();


		public override string ToString()
		{
			return name;
		}
	}
}