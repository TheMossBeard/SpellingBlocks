using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SpellingBlocks.Objects;

namespace SpellingBlocks.Controllers
{
    class MenuController
    {
        public MenuButton ButtonSpelling { get; set; }
        public MenuButton ButtonWordSearch { get; set; }
        public MenuButton ButtonMatching { get; set; }
        public MenuButton ButtonTracing { get; set; }
        public Texture2D Background { get; set; }
        public SpriteBatch spriteBatch { get; set; }
        public List<MenuButton> MenuButtonList { get; set; }

        public MenuController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.spriteBatch = spriteBatch;
            Background = gameContent.menuBackground;
            MenuButtonList = new List<MenuButton>();

            ButtonSpelling = new MenuButton(new Vector2(112, 144), "ButtonSpelling", gameContent.menu01, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonSpelling);
            ButtonWordSearch = new MenuButton(new Vector2(528, 144), "ButtonWordSearch", gameContent.menu02, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonWordSearch);
            ButtonTracing = new MenuButton(new Vector2(112, 304), "ButtonTracing", gameContent.menu03, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonTracing);
            ButtonMatching = new MenuButton(new Vector2(528, 304), "ButtonMatching", gameContent.menu04, spriteBatch, gameContent);
            MenuButtonList.Add(ButtonMatching);
        }

        public GameState Update(Rectangle touchBox, GameState state)
        {
            if (HitTest(ButtonSpelling.HitBox, touchBox))
            {
                state = GameState.CategoryMenu;
            }
            else if (HitTest(ButtonTracing.HitBox, touchBox))
            {
                state = GameState.Draw;
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
                new Vector2(0, 0), 4f, SpriteEffects.None, 0);

            foreach (MenuButton button in MenuButtonList)
            {
                button.Draw();
            }
        }
    }
}