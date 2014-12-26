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

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldState;
        MouseState mouseStateCurrent, mouseStatePrevious;
        Rectangle r1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
            oldState = Keyboard.GetState();
        }

        // This is a texture we can render.
        Texture2D myTexture;
        Texture2D myRectangle;

        // Set the coordinates to draw the sprite at.
        Vector2 spritePosition = Vector2.Zero;
        Vector2 rectanglePosition = Vector2.Zero;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myTexture = Content.Load<Texture2D>("character");
            myRectangle = new Texture2D(graphics.GraphicsDevice, 30, 30);
            Color[] data = new Color[30 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            myRectangle.SetData(data);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        Vector2 spriteSpeed = new Vector2(100, 100);
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - myTexture.Height;
            int MinY = 0;

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }

            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }

            if (checkKeyDown(Keys.Up))
            {
                if (spriteSpeed.Y > 0)
                {
                    spriteSpeed.Y *= -1;
                }
            }

            if (checkKeyDown(Keys.Down))
            {
                if (spriteSpeed.Y < 0)
                {
                    spriteSpeed.Y *= -1;
                }
            }

            if (checkKeyDown(Keys.Right))
            {
                if (spriteSpeed.X < 0)
                {
                    spriteSpeed.X *= -1;
                }
            }

            if (checkKeyDown(Keys.Left))
            {
                if (spriteSpeed.X > 0)
                {
                    spriteSpeed.X *= -1;
                }
            }

            // Check for mouse click
            mouseStateCurrent = Mouse.GetState();
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed)
            {
                rectanglePosition.X = mouseStateCurrent.X;
                rectanglePosition.Y = mouseStateCurrent.Y;
            }
            mouseStatePrevious = mouseStateCurrent;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Draw the sprite.
            spriteBatch.Begin();
            spriteBatch.Draw(myTexture, spritePosition, Color.White);
            spriteBatch.Draw(myRectangle, rectanglePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected bool checkKeyDown(Keys key)
        {
            KeyboardState newState = Keyboard.GetState();
            return  newState.IsKeyDown(key) && !oldState.IsKeyDown(key);
        }
    }
}
