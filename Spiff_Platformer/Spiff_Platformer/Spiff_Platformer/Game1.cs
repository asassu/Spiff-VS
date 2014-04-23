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

namespace Spiff_Platformer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Globals
        const int WINDOW_WIDTH = 700;
        const int WINDOW_HEIGHT = 500;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldState;

        playerSprite Spiff;
        solidGround Long1;

        bool gamePaused = true;
        bool gameStartScreen = true;

        Texture2D MyBackground;
        Vector2 MyBGPosition = Vector2.Zero;

        bool jumping = false; //Is the character jumping?
        float startY, jumpspeed = 0; //startY to tell us //where it lands, jumpspeed to see how fast it jumps
        bool canFall = false;

        float worldGround = WINDOW_HEIGHT - 150;

        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // changing the back buffer size changes the window size (when in windowed mode) 
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
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

            base.Initialize();
            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Spiff = new playerSprite(Content.Load<Texture2D>("fightingCat"), new Vector2(200, WINDOW_HEIGHT - 150), new
                Vector2(64f, 64f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            Long1 = new solidGround(Content.Load<Texture2D>("Long1"), new Vector2(400, WINDOW_HEIGHT - 180), new
                Vector2(248f, 60f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            MyBackground = Content.Load<Texture2D>("cloudBG");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Spiff.texture.Dispose();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //Updating keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            //HEY LISTEN: Replace me with a function?
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                if (Spiff.position.Y - 5 > 0)
                {
                    Spiff.position += new Vector2(-5, 0);
                }
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                if (Spiff.position.Y - 5 > 0)
                {
                    Spiff.position += new Vector2(5, 0);
                }
            }

            if(!Spiff.Collides1(Long1) && canFall)
            {
                jumping = true;
                jumpspeed = 0;
                Spiff.position += new Vector2(0, jumpspeed);//Making it go up
                jumpspeed += 1;//Some math

                if (Spiff.position.Y >= worldGround)
                //If it's farther than ground
                {
                    float currentX = Spiff.position.X;
                    Spiff.position = new Vector2(currentX, worldGround);//Then set it on
                    jumping = false;
                    canFall = false;
                }
            }

            if (jumping)
            {
                Spiff.position += new Vector2(0, jumpspeed);//Making it go up
                jumpspeed += 1;//Some math
                if(Spiff.Collides1(Long1))
                {
                    float currentX = Spiff.position.X;
                    float currentY = Spiff.position.Y;
                    Spiff.position = new Vector2(currentX, currentY);//Then set it on
                    jumping = false;
                    canFall = true;
                }
                if (Spiff.position.Y >= startY)
                //If it's farther than ground
                {
                    float currentX = Spiff.position.X;
                    Spiff.position = new Vector2(currentX, startY);//Then set it on
                    jumping = false;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    startY = Spiff.position.Y;
                    jumping = true;
                    jumpspeed = -14;//Give it upward thrust
                }
            }

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

            spriteBatch.Begin();
            spriteBatch.Draw(MyBackground, MyBGPosition, Color.White);

            Long1.Draw(spriteBatch);

            Spiff.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
