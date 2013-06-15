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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
     
        //Sprite object
        Sprite mSprite;
        
        float gravity;
        bool pressed;

        Vector2 spritePosition1;
        Vector2 spriteSpeed1 = new Vector2(50.0f, 50.0f);

        Texture2D mSpriteTexture;

        Sprite mBackgroundOne;
        Sprite mBackgroundTwo;
        Sprite mBackgroundThree;
        Sprite mBackgroundFour;
        Sprite mBackgroundFive;

        Sprite mSkyscraper;

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
            // Windows 8 Touch Gestures for 
            gravity = 0F;
            pressed = false;

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

            mSkyscraper = new Sprite();
            mSkyscraper.Scale = 2.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
 
            spritePosition1.X = 0;
            spritePosition1.Y = 150;

            loadSprites();
 
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

            mSkyscraper.LoadContent(this.Content, "skyscraper");
            mSkyscraper.Position = new Vector2(mBackgroundFour.Position.X + mBackgroundFour.Size.Width, 100);
        }

        protected override void Update(GameTime gameTime)
        {
            checkBackButton();

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
            mSkyscraper.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

            if (mSkyscraper.Position.X < -mBackgroundFour.Size.Width)
            {
                mSkyscraper.Position.X = mBackgroundFour.Position.X + mSkyscraper.Size.Width;
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
            if (((spritePosition.X <= (mSkyscraper.Position.X) + 5) && (spritePosition.X >= (mSkyscraper.Position.X) - 5)) && spritePosition.Y >= 50)
            {
                System.Diagnostics.Debug.WriteLine("Hit the skyscraper");
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            mBackgroundOne.Draw(this.spriteBatch);
            mBackgroundTwo.Draw(this.spriteBatch);
            mBackgroundThree.Draw(this.spriteBatch);
            mBackgroundFour.Draw(this.spriteBatch);
            mBackgroundFive.Draw(this.spriteBatch);
            mSkyscraper.Draw(this.spriteBatch);

            spriteBatch.Draw(mSpriteTexture, spritePosition1, Color.White);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
