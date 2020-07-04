using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using OpenTK.Graphics.ES20;
using SpellingBlocks.Objects;
using SpellingBlocks.Controllers;

namespace SpellingBlocks
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;
        BlockController blocks;
        Rectangle touchBox;
        int screenWidth = 0;
        int screenHeight = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);

            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            float blockPosx = screenWidth / 10;
            float blockPosy = screenHeight / 10;
            blocks = new BlockController(spriteBatch, gameContent);
  
        }

        protected override void UnloadContent()
        {

        }

        TouchCollection tc;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            tc = TouchPanel.GetState();
            foreach (TouchLocation tl in tc)
            {
                
                if (TouchLocationState.Pressed == tl.State)
                {
                    touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 64, 64);
                    blocks.Update(touchBox);
                    //Vector2 tmp = blockA.Position * 2;
                    //Rectangle hitBox = new Rectangle((int)blockA.Position.X, (int)blockA.Position.Y, 64,64);
                    //Rectangle touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 64, 64);
                    // if (HitTest( touchBox, hitBox)) //touch offset is fucky
                    //{
                    //blockA.Position = tmp;

                    //}
                }
            }

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
        
            blocks.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
            
        }
    }
}
