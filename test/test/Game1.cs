using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum GameState { 
            MainMenu, 
            Options, 
            Playing,
        }

        GameState CurrentGameState = GameState.MainMenu;

        int screenWidth = 800, screenHeight = 600;

        cButton btnPlay;

        SpriteFont Font1;
        SpriteFont Font2;
        Vector2 FontPos;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
     
        //Sprite object
        Sprite mSprite;

        Vector2 spritePosition1;
        Vector2 spriteSpeed1 = new Vector2(50.0f, 50.0f);

        Texture2D mSpriteTexture;

        Sprite mBackgroundOne;
        Sprite mBackgroundTwo;
        Sprite mBackgroundThree;
        Sprite mBackgroundFour;
        Sprite mBackgroundFive;

        Sprite obstacle1;
        Sprite obstacle1a;
        Sprite obstacle2;
        Sprite obstacle2a;
        Sprite obstacle3;
        Sprite obstacle4;

        bool paused = false;
        
        long score = 0L;

        string gameOver = "Game Over!";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        protected override void Initialize()
        {
            //Enable the FreeDrag gesture
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            initSprites();
        }

        private void initSprites()
        {
            // TODO: Add your initialization logic here
            mSprite = new Sprite();

            mBackgroundOne = new Sprite();
            mBackgroundOne.Scale = 2.0f;

            mBackgroundTwo = new Sprite();
            mBackgroundTwo.Scale = 2.0f;

            mBackgroundThree = new Sprite();
            mBackgroundThree.Scale = 2.0f;

            mBackgroundFour = new Sprite();
            mBackgroundFour.Scale = 2.0f;

            mBackgroundFive = new Sprite();
            mBackgroundFive.Scale = 2.0f;

            obstacle1 = new Sprite();
            obstacle1.Scale = 2.0f;

            obstacle1a = new Sprite();
            obstacle1a.Scale = 2.0f;

            obstacle2 = new Sprite();
            obstacle2.Scale = 2.0f;

            obstacle2a = new Sprite();
            obstacle2a.Scale = 2.0f;

            obstacle3 = new Sprite();
            obstacle3.Scale = 2.0f;

            obstacle4 = new Sprite();
            obstacle4.Scale = 2.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            spritePosition1.X = 0;
            spritePosition1.Y = 150;


            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            Font1 = Content.Load<SpriteFont>("SpriteFont1");
            Font2 = Content.Load<SpriteFont>("SpriteFont1");

            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width - 70, 15);

            loadSprites();


            btnPlay = new cButton(Content.Load<Texture2D>("ironman"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 300));

            graphics.ApplyChanges();
            IsMouseVisible = true;
 
        }

        private void loadSprites()
        {
            // TODO: use this.Content to load your game content here
            mSpriteTexture = mSprite.LoadContent(this.Content, "ironman");

            mBackgroundOne.LoadContent(this.Content, "Background01");
            mBackgroundOne.Position = new Vector2(0, 0);

            mBackgroundTwo.LoadContent(this.Content, "Background02");
            mBackgroundTwo.Position = new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 0);

            mBackgroundThree.LoadContent(this.Content, "Background03");
            mBackgroundThree.Position = new Vector2(mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width, 0);

            mBackgroundFour.LoadContent(this.Content, "Background04");
            mBackgroundFour.Position = new Vector2(mBackgroundThree.Position.X + mBackgroundThree.Size.Width, 0);

            mBackgroundFive.LoadContent(this.Content, "Background05");
            mBackgroundFive.Position = new Vector2(mBackgroundFour.Position.X + mBackgroundFour.Size.Width, 0);

            obstacle1.LoadContent(this.Content, "obstacle");
            obstacle1.Position = new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 120);

            obstacle1a.LoadContent(this.Content, "obstacle");
            obstacle1a.Position = new Vector2(mBackgroundOne.Position.X + mBackgroundOne.Size.Width, 20);

            obstacle2.LoadContent(this.Content, "obstacle");
            obstacle2.Position = new Vector2(mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width, 80);

            obstacle2a.LoadContent(this.Content, "obstacle");
            obstacle2a.Position = new Vector2(mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width, 250);

            obstacle3.LoadContent(this.Content, "obstacle");
            obstacle3.Position = new Vector2(mBackgroundThree.Position.X + mBackgroundThree.Size.Width, 50);

            obstacle4.LoadContent(this.Content, "obstacle");
            obstacle4.Position = new Vector2(mBackgroundFour.Position.X + mBackgroundFour.Size.Width, 250);
        }

        protected override void Update(GameTime gameTime)
        {
            checkBackButton();

            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState) { 
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    break;

                case GameState.Playing:
                    break;
            }
            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                Vector2 pos;
                TouchLocationState state;

                TouchCollection collection = TouchPanel.GetState();

                if (collection.Count != 0)
                {
                    pos = collection[0].Position;
                    int id = collection[0].Id;
                    state = collection[0].State;

                    if (state == TouchLocationState.Pressed)
                    {
                        spritePosition1.Y = spritePosition1.Y - 10;

                    }
                    else if (state != TouchLocationState.Pressed)
                    {
                        spritePosition1.Y = spritePosition1.Y - 8;
                    }
                }
            }
            else
            {
                spritePosition1.Y = spritePosition1.Y + 6;
            }
            
            checkCollision(ref spritePosition1, ref spriteSpeed1);

            base.Draw(gameTime);

            resortBackgroundSprites();
            adjustBackgroundSpritePositions(gameTime);

            base.Update(gameTime);
        }

        private void adjustBackgroundSpritePositions(GameTime gameTime)
        {
            Vector2 aDirection = new Vector2(-3, 0);
            Vector2 aSpeed = new Vector2(160, 0);

            mBackgroundOne.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundTwo.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundThree.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundFour.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            mBackgroundFive.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            obstacle1.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            obstacle1a.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            obstacle2.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            obstacle2a.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            obstacle3.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            obstacle4.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void resortBackgroundSprites()
        {
            if (mBackgroundOne.Position.X < -mBackgroundOne.Size.Width)
            {
                mBackgroundOne.Position.X = mBackgroundFive.Position.X + mBackgroundFive.Size.Width;
            }

            if (mBackgroundTwo.Position.X < -mBackgroundTwo.Size.Width)
            {
                mBackgroundTwo.Position.X = mBackgroundOne.Position.X + mBackgroundOne.Size.Width;
            }

            if (mBackgroundThree.Position.X < -mBackgroundThree.Size.Width)
            {
                mBackgroundThree.Position.X = mBackgroundTwo.Position.X + mBackgroundTwo.Size.Width;
            }

            if (mBackgroundFour.Position.X < -mBackgroundFour.Size.Width)
            {
                mBackgroundFour.Position.X = mBackgroundThree.Position.X + mBackgroundThree.Size.Width;
            }

            if (mBackgroundFive.Position.X < -mBackgroundFive.Size.Width)
            {
                mBackgroundFive.Position.X = mBackgroundFour.Position.X + mBackgroundFour.Size.Width;
            }

            if (obstacle1.Position.X < -mBackgroundOne.Size.Width)
            {
                obstacle1.Position.X = mBackgroundOne.Position.X + obstacle1.Size.Width;
            }

            if (obstacle1a.Position.X < -mBackgroundOne.Size.Width)
            {
                obstacle1a.Position.X = mBackgroundOne.Position.X + obstacle1a.Size.Width;
            }

            if (obstacle2.Position.X < -mBackgroundTwo.Size.Width)
            {
                obstacle2.Position.X = mBackgroundTwo.Position.X + obstacle2.Size.Width;
            }

            if (obstacle2a.Position.X < -mBackgroundTwo.Size.Width)
            {
                obstacle2a.Position.X = mBackgroundTwo.Position.X + obstacle2a.Size.Width;
            }

            if (obstacle3.Position.X < -mBackgroundThree.Size.Width)
            {
                obstacle3.Position.X = mBackgroundThree.Position.X + obstacle3.Size.Width;
            }

            if (obstacle4.Position.X < -mBackgroundFour.Size.Width)
            {
                obstacle4.Position.X = mBackgroundFour.Position.X + obstacle4.Size.Width;
            }
        }

        private void checkBackButton()
        {
            // Allow the game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed)
                this.Exit();
        }

        private void checkCollision(ref Vector2 spritePosition, ref Vector2 spriteSpeed1)
        {

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - mSpriteTexture.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - mSpriteTexture.Height;
            int MinY = 0;

            //Check if the sprite has hit the skyscraper, allow a margin of error of 5, and hard-coded height 
            if (((spritePosition.X + 100 >= obstacle1.Position.X - 100) && (spritePosition.X <= obstacle1.Position.X - 95)) && (spritePosition.Y >= obstacle1.Position.Y-30) && (spritePosition.Y <= obstacle1.Position.Y + 65))
            {
                paused = true;
            }
            if (((spritePosition.X + 100 >= obstacle1a.Position.X - 100) && (spritePosition.X <= obstacle1a.Position.X - 95)) && (spritePosition.Y >= obstacle1a.Position.Y - 30) && (spritePosition.Y <= obstacle1a.Position.Y + 65))
            {
                paused = true;
            }
            if (((spritePosition.X + 100 >= obstacle2.Position.X - 100) && (spritePosition.X <= obstacle2.Position.X - 95)) && (spritePosition.Y >= obstacle2.Position.Y - 30) && (spritePosition.Y <= obstacle2.Position.Y + 65))
            {
                paused = true;
            }
            if (((spritePosition.X + 100 >= obstacle2a.Position.X - 100) && (spritePosition.X <= obstacle2a.Position.X - 95)) && (spritePosition.Y >= obstacle2a.Position.Y - 30) && (spritePosition.Y <= obstacle2a.Position.Y + 65))
            {
                paused = true;
            }
            if (((spritePosition.X + 100 >= obstacle3.Position.X - 100) && (spritePosition.X <= obstacle3.Position.X - 95)) && (spritePosition.Y >= obstacle3.Position.Y - 30) && (spritePosition.Y <= obstacle3.Position.Y + 65))
            {
                paused = true;
            }
            if (((spritePosition.X + 100 >= obstacle4.Position.X - 100) && (spritePosition.X <= obstacle4.Position.X - 95)) && (spritePosition.Y >= obstacle4.Position.Y - 30) && (spritePosition.Y <= obstacle4.Position.Y + 65))
            {
                paused = true;
            }

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                spriteSpeed1.X *= -1;
                spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed1.X *= -1;
                spritePosition.X = MinX;
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed1.Y *= -1;
                spritePosition.Y = MaxY;
            }

            else if (spritePosition.Y < MinY)
            {
                spriteSpeed1.Y *= -1;
                spritePosition.Y = MinY;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            string output;

            // Update ticks (score) if the user hasn't hit an obstacle, otherwise just use the old value for score 
            spriteBatch.Begin();

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("ironman"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    //spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    break;
                case GameState.Playing:

                    if (!paused)
                    {
                        score = gameTime.TotalGameTime.Ticks / 100000;
                        output = "Score: " + score;
                    }
                    else
                    {
                        output = "Score: " + score;
                    }
                    // Find the center of the string
                    Vector2 FontOrigin = Font1.MeasureString(output) / 2;

                    Vector2 fontOrign = Font2.MeasureString(gameOver) / 2;
                    Vector2 gameOverPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

                    mBackgroundOne.Draw(this.spriteBatch);
                    mBackgroundTwo.Draw(this.spriteBatch);
                    mBackgroundThree.Draw(this.spriteBatch);
                    mBackgroundFour.Draw(this.spriteBatch);
                    mBackgroundFive.Draw(this.spriteBatch);
                    obstacle1.Draw(this.spriteBatch);
                    obstacle1a.Draw(this.spriteBatch);
                    obstacle2.Draw(this.spriteBatch);
                    obstacle2a.Draw(this.spriteBatch);
                    obstacle3.Draw(this.spriteBatch);
                    obstacle4.Draw(this.spriteBatch);

                    spriteBatch.Draw(mSpriteTexture, spritePosition1, Color.White);

                    spriteBatch.DrawString(Font1, output, FontPos, Color.LightGreen,
                        0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    if (paused)
                    {
                        spriteBatch.DrawString(Font2, gameOver + "\n" + output, gameOverPos, Color.Red,
                            0, fontOrign, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
