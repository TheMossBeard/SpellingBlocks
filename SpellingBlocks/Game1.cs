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
     public enum GameState
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
        MenuController menu;
        IResolution resolution;
        GameState state;
        bool winner;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            resolution = new ResolutionComponent(this, graphics, new Point(1024, 576), 
                new Point(1024, 576), false, true);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            state = new GameState();
            state = GameState.MainMenu;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);

            blocks = new BlockController(spriteBatch, gameContent);
            winnerBlocks = new Winner(spriteBatch, gameContent);
            winner = false;
            menu = new MenuController(spriteBatch, gameContent);



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
                    UpdateMenu(gameTime);
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
                    Vector2 Screen = resolution.ScreenToGameCoord(touch.Position);
                    touchBox = new Rectangle((int)Screen.X, (int)Screen.Y, 2, 2);
                    
                    blocks.MoveHighlightedBlock(touchBox);
                    winner = blocks.CheckWin();
                }
            }
        }

        public void UpdateMenu(GameTime gameTime)
        {
            var touchPanelState = TouchPanel.GetState();
            foreach (var touch in touchPanelState)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    Vector2 Screen = resolution.ScreenToGameCoord(touch.Position);
                    touchBox = new Rectangle((int)Screen.X, (int)Screen.Y, 2, 2);

                    state = menu.Update(touchBox, state);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Black);
            switch (state)
            {
                case GameState.MainMenu:
                    DrawMenu(gameTime);
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

        public void DrawMenu(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,
                      BlendState.AlphaBlend,
                      null, null, null, null,
                      Resolution.TransformationMatrix());
            menu.Draw();
            spriteBatch.End();
        }
    }
}
