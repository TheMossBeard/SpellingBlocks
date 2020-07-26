using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SpellingBlocks.Objects;

namespace SpellingBlocks.Controllers
{
    class CategoryController
    {
        public MenuButton ButtonNature { get; set; }
        public MenuButton ButtonAnimals { get; set; }
        public MenuButton ButtonMachines { get; set; }
        public Texture2D Background { get; set; }
        public SpriteBatch spriteBatch { get; set; }
        public List<MenuButton> MenuButtonList { get; set; }

        public CategoryController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            Background = gameContent.categoryBackground;
            MenuButtonList = new List<MenuButton>();

            ButtonNature = new MenuButton(new Vector2(320, 64), "ButtonNature", gameContent.NatureButton, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonNature);
            ButtonAnimals = new MenuButton(new Vector2(320, 224), "ButtonAnimals", gameContent.AnimalsButton, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonAnimals);
            ButtonMachines = new MenuButton(new Vector2(320, 384), "ButtonMachines", gameContent.MachinesButton, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonMachines);
        }

        public GameState Update(Rectangle touchBox, GameContent gameContent, BlockController blocks, GameState state)
        {
            if (HitTest(ButtonNature.HitBox, touchBox))
            {
                state = GameState.SpellingBlocksNature;
                blocks.CreateGame(spriteBatch, gameContent, state);
            }
            else if (HitTest(ButtonAnimals.HitBox, touchBox))
            {
                state = GameState.SpellingBlocksAnimals;
                blocks.CreateGame(spriteBatch, gameContent, state);
            }
            else if (HitTest(ButtonMachines.HitBox, touchBox))
            {
                state = GameState.SpellingBlocksMachines;
                blocks.CreateGame(spriteBatch, gameContent, state);
            }
            return state;
        }

        public GameState UpdateSearch(Rectangle touchBox, GameContent gameContent, WordSearchController wordSearch, GameState state)
        {
            if (HitTest(ButtonNature.HitBox, touchBox))
            {
                state = GameState.WordSearchNature;
                wordSearch.CreateWordSearch(spriteBatch, gameContent);
            }
            else if (HitTest(ButtonAnimals.HitBox, touchBox))
            {
                state = GameState.WordSearchAnimal;
                wordSearch.CreateWordSearch(spriteBatch, gameContent);
            }
            else if (HitTest(ButtonMachines.HitBox, touchBox))
            {
                state = GameState.WordSearchMachines;
                wordSearch.CreateWordSearch(spriteBatch, gameContent);
            }
            return state;
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }

        public void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);

            foreach (MenuButton button in MenuButtonList)
            {
                button.Draw();
            }
        }
    }
}