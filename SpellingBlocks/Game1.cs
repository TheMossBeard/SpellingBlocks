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
        SpellingBlocksNature,
        SpellingBlocksAnimals,
        SpellingBlocksMachines
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
        CategoryController category;
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

            blocks = new BlockController(spriteBatch, gameContent, state);
            winnerBlocks = new Winner(spriteBatch, gameContent);
            winner = false;
            menu = new MenuController(spriteBatch, gameContent);
            category = new CategoryController(spriteBatch, gameContent);


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
                    UpdateCategory(gameTime);
                    break;
                case GameState.SpellingBlocksNature:
                  
                    UpdateSpellingBlock(gameTime);
                    break;
                case GameState.SpellingBlocksAnimals:
                    UpdateSpellingBlock(gameTime);
                    break;
                case GameState.SpellingBlocksMachines:
                    UpdateSpellingBlock(gameTime);
                    break;
            }

        }

        public void UpdateCategory(GameTime gameTime)
        {
            var touchPanelState = TouchPanel.GetState();
            foreach (var touch in touchPanelState)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    Vector2 Screen = resolution.ScreenToGameCoord(touch.Position);
                    touchBox = new Rectangle((int)Screen.X, (int)Screen.Y, 2, 2);

                    state = category.Update(touchBox, gameContent, blocks, state);
                }
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
                    blocks.ArrowButton(touchBox, state);
                    state = blocks.HomeButtonUpdate(touchBox, state);
                    blocks.MoveHighlightedBlock(touchBox);
                    winner = blocks.CheckWin();
                    if (blocks.Skip)
                        blocks.CreateGame(spriteBatch, gameContent, state);
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
                    DrawCategory(gameTime);
                    break;
                case GameState.SpellingBlocksNature:
                    DrawSpellingBlocks(gameTime);
                    break;
                case GameState.SpellingBlocksAnimals:
                    DrawSpellingBlocks(gameTime);
                    break;
                case GameState.SpellingBlocksMachines:
                    DrawSpellingBlocks(gameTime);
                    break;
            }
        }

        public void DrawCategory(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,
                      BlendState.AlphaBlend,
                      null, null, null, null,
                      Resolution.TransformationMatrix());
            category.Draw();
            spriteBatch.End();
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
