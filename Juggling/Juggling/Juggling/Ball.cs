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
	class Ball
	{
		public int x;
		public int y;
		public Vector2 vel;
		Texture2D ballTex;
		public bool Left;
		float accel = .1f;

		public Ball(int x, int y, ContentManager Content, Vector2 vel)
		{
			this.x = x;
			this.y = y;
			this.vel = vel;
			ballTex = Content.Load<Texture2D>("ball");
			Left = false;
		}

		public void draw(SpriteBatch sb)
		{
			sb.Begin();
			sb.Draw(ballTex, new Vector2(x, y), (Left ? Color.Red : Color.Blue));
			sb.End();
		}

		public void update()
		{
			vel = new Vector2(vel.X, vel.Y + accel);
			
			
			x += (int)vel.X;
			y += (int)vel.Y;

			if (x < 0) 
			{
				vel.X = -vel.X;
			}
			if (x > 1240)
			{
				vel.X = -vel.X;
			}
		}

		public Rectangle getRect()
		{
			return new Rectangle(x, y, ballTex.Width, ballTex.Height);
		}
	}
}
