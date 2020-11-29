using Microsoft.Xna.Framework.Graphics;
using Nez.Textures;
using System;

namespace Nez.Sprites
{
	public class TextureSpriteAtlas : IDisposable
	{
		public string[] Names;
		public Sprite[] Sprites;

		public int Columns { get; protected set; }
		public int Lines { get; protected set; }

		public int Width { get; protected set; }
		public int Height { get; protected set; }

		public readonly Texture2D Atlas;

		public TextureSpriteAtlas(Texture2D atlas, int columns, int lines)
		{
			Atlas = atlas;
			Columns = columns;
			Lines = lines;

			Width = Atlas.Width / columns;
			Height = Atlas.Height / lines;

			int size = Columns * Lines;
			Sprites = new Sprite[size];
			Names = new string[size];
		}

		public Sprite GetSprite(string name)
		{
			var index = Array.IndexOf(Names, name);
			return GetSprite(index);
		}

		public Sprite GetSprite(int index)
		{
			Sprite sprite = Sprites[index];
			if (sprite == null)
			{
				int line = index / Columns;
				int column = index % Columns;

				sprite = Sprites[index] = new Sprite(Atlas, column * Width, line * Height, Width, Height);
			}
			return sprite;
		}

		public Sprite GetSprite(int x, int y)
		{
			int index = y * Lines + x;
			Sprite sprite = Sprites[index];
			if (sprite == null)
			{
				sprite = Sprites[index] = new Sprite(Atlas, x * Width, y * Height, Width, Height);
			}
			return sprite;
		}

		void IDisposable.Dispose()
		{
			Atlas.Dispose();
		}

		public Sprite this[int index] => GetSprite(index);
		public Sprite this[string name] => GetSprite(name);
		public Sprite this[int x, int y] => GetSprite(x, y);
	}
}
