using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Juggling
{
	class Hand2
	{
		Texture2D handTex;
		public int x;
		public int y;
		public int ms = 10;
		public bool up = false, down = false;
		public Hand2(int x, int y, ContentManager Content)
		{
			this.x = x;
			this.y = y;

			handTex = Content.Load<Texture2D>("hand");
		}

		public void draw(SpriteBatch sb)
		{
			sb.Begin();
			sb.Draw(handTex, new Vector2(x, y), Color.Red);
			sb.End();
		}

		public Rectangle getRect()
		{
			return new Rectangle(x, y, handTex.Width, handTex.Height);
		}

		public void update()
		{
			KeyboardState ks = Keyboard.GetState();
			if (ks.IsKeyDown(Keys.W))
			{
				y -= ms;
				up = true;
			}
			else
			{
				up = false;
			}

			if (ks.IsKeyDown(Keys.S))
			{
				down = true;
				y += ms;
			}
			else
			{
				down = false;
			}

			if (ks.IsKeyDown(Keys.A))
			{
				x -= ms;
			}

			if (ks.IsKeyDown(Keys.D))
			{
				x += ms;
			}

			if (x > 1220) x = 1220;
			if (x < 0) x = 0;
			if (y > 940) y = 940;
			if (y < 0) y = 0;
		}
	}
}
