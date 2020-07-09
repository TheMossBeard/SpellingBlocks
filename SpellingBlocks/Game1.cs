using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using OpenTK.Graphics.ES20;
using SpellingBlocks.Objects;
using SpellingBlocks.Controllers;
using Android.Text.Method;

namespace SpellingBlocks
{
    enum GameState
    {
        MainMenu,
        CategoryMenu,
        SpellingBlocks
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;
        BlockController blocks;
        Rectangle touchBox;
        Winner winnerBlocks;
        GameState state;
        bool winner;
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
            
            state = new GameState();
            state = GameState.SpellingBlocks;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);

            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        
            blocks = new BlockController(spriteBatch, gameContent);
            winnerBlocks = new Winner(spriteBatch, gameContent);
            winner = false;


        }

        protected override void UnloadContent()
        {

        }
        TouchCollection tc;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            base.Update(gameTime);
            switch (state)
            {
                case GameState.MainMenu:
                    //call function here
                    break;
                case GameState.CategoryMenu:
                    break;
                case GameState.SpellingBlocks:
                    UpdateSpellingBlock(gameTime);
                    break;
            }

        }

        public void UpdateSpellingBlock(GameTime gameTime)
        {
            tc = TouchPanel.GetState();
            foreach (TouchLocation tl in tc)
            {
                if (TouchLocationState.Pressed == tl.State)
                {
                    touchBox = new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 2, 2);
                    blocks.MoveHighlightedBlock(tl);
                    winner = blocks.CheckWin();
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (state)
            {
                case GameState.MainMenu:
                    //call function here
                    break;
                case GameState.CategoryMenu:
                    break;
                case GameState.SpellingBlocks:
                    DrawSpellingBlocks(gameTime);
                    break;
            }
        }

        public void DrawSpellingBlocks(GameTime gameTime)
        {

            spriteBatch.Begin();
            blocks.Draw();
            if (winner)
                winnerBlocks.Draw();
            spriteBatch.End();
        }
    }
}
