using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Juggling
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		int score=0;
		Hand blueHand;
		Hand2 redHand;
		List<Ball> balls;
		int numballs = 2;
		Random rand;
		int delayTimer = 10;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferHeight = 960;
			graphics.PreferredBackBufferWidth = 1280;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			blueHand = new Hand(800, 800, Content);
			redHand = new Hand2(400, 800, Content);
			
			base.Initialize();
			balls = new List<Ball>();
			loadBalls();
			rand = new Random();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

		}

		protected void loadBalls()
		{
			while (balls.Count > 0)
			{
				balls.RemoveAt(0);
			}
			for (int i = 0; i < numballs; i++)
			{
				balls.Add(new Ball(810, -200*i, Content, new Vector2(0, 0)));
			}
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
				this.Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.F5) && delayTimer >= 10) 
			{
				delayTimer = 0;
				numballs++;
				loadBalls();
			}
			if (delayTimer < 10)
			{
				delayTimer++;
			}

			Window.Title = "Score: " + score.ToString(); 

			blueHand.update();
			redHand.update();
			foreach (Ball b in balls)
			{
				b.update();
				
				
			}

			collide();

			base.Update(gameTime);
		}

		public void collide()
		{
			foreach (Ball b in balls)
			{
				if (b.vel.Y > 0 && b.Left)
				{
					if (b.getRect().Intersects(redHand.getRect()))
					{
						if (blueHand.x > redHand.x)
						{
							b.vel.X = (float)rand.NextDouble() * 4;
						}
						else
						{
							b.vel.X = -(float)rand.NextDouble() * 4;
						}
						if (redHand.up) b.vel.Y = -10;
						else if (redHand.down) b.vel.Y = -5;
						else b.vel.Y = -7;
						b.Left = !b.Left;
						score++;
					}
				}
				else if (b.vel.Y > 0 && !(b.Left))
				{
					if (b.getRect().Intersects(blueHand.getRect()))
					{
						if (redHand.x > blueHand.x)
						{
							b.vel.X = (float)rand.NextDouble()*4;
						}
						else
						{
							b.vel.X = -(float)rand.NextDouble()*4;
						}
						if (blueHand.up) b.vel.Y = -10;
						else if (blueHand.down) b.vel.Y = -5;
						else b.vel.Y = -7;
						score++;
						b.Left = !b.Left;
					}
				}
			}
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			blueHand.draw(spriteBatch);
			redHand.draw(spriteBatch);
			foreach (Ball b in balls)
			{
				b.draw(spriteBatch);
			}


			base.Draw(gameTime);
		}
	}
}
