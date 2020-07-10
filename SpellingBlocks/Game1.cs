using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using OpenTK.Graphics.ES20;
using SpellingBlocks.Objects;
using SpellingBlocks.Controllers;
using Android.Text.Method;
using ResolutionBuddy;
using Plugin.DeviceInfo;
using Plugin.DeviceInfo.Abstractions;

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
        IResolution resolution;
        GameState state;
        bool winner;

        const int BLOCK_SIZE_OFFSET = 64;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;



            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            resolution = new ResolutionComponent(this, graphics, new Point(1024, 576), new Point(1024, 576), true, false);
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

            blocks = new BlockController(spriteBatch, gameContent);
            winnerBlocks = new Winner(spriteBatch, gameContent);
            winner = false;



        }

        protected override void UnloadContent()
        {

        }
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
            var touchPanelState = TouchPanel.GetState();
            foreach (var touch in touchPanelState)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    float ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 1024f;
                    float ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 576f;
                    Vector2 rawTouch = new Vector2(touch.Position.X, touch.Position.Y);
                    Vector2 scaledTouch;
                    var matrix = Matrix.CreateScale(ScreenWidth, ScreenHeight, 1f);

                    scaledTouch = Vector2.Transform(rawTouch, Matrix.Invert(matrix));
                    touchBox = new Rectangle((int)scaledTouch.X, (int)scaledTouch.Y - BLOCK_SIZE_OFFSET, 15, 15);

                    blocks.MoveHighlightedBlock(touchBox);
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

            spriteBatch.Begin(SpriteSortMode.Immediate,
                      BlendState.AlphaBlend,
                      null, null, null, null,
                      Resolution.TransformationMatrix());
            blocks.Draw();
            if (winner)
                winnerBlocks.Draw();
            spriteBatch.End();
        }
    }
}
