using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
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
    class TracingController
    {
        const int COUNT = 26;

        private int index;
        public List<Texture2D> TraceCardList { get; set; }
        public Drawing Drawable { get; set; }
        public SpriteBatch spriteBatch { get; set; }
        private MenuButton ArrowRight { get; set; }
        private MenuButton HomeButton { get; set; }

        public TracingController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            index = 0;
            Drawable = new Drawing(spriteBatch, gameContent);
            this.spriteBatch = spriteBatch;
            TraceCardList = new List<Texture2D>();
            Fill(gameContent);

            HomeButton = new MenuButton(new Vector2(16, 16), "HomeButton", gameContent.home, spriteBatch, gameContent);
            HomeButton.HitBox = new Rectangle((int)HomeButton.Position.X, (int)HomeButton.Position.Y, 64, 64);
            ArrowRight = new MenuButton(new Vector2(944, 16), "ArrowRight", gameContent.arrorRight, spriteBatch, gameContent);
            ArrowRight.HitBox = new Rectangle((int)ArrowRight.Position.X, (int)ArrowRight.Position.Y, 64, 64);
        }

        public void Fill(GameContent gameContent)
        {
            for(int ii = 0; ii < COUNT; ii++)
            {
                TraceCardList.Add(gameContent.TraceList[ii]);
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(TraceCardList[index], new Vector2(0, 0), null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            HomeButton.Draw();
            ArrowRight.Draw();
            Drawable.Draw();
        }

        public void Touch(Rectangle touchBox,GameContent gameContent,SpriteBatch spriteBatch)
        {
            Drawable.DrawUpdate(touchBox, gameContent, spriteBatch);
        }

        public void Release()
        {
            Drawable.NewDraw();
        }

        public GameState HomeButtonUpdate(Rectangle touchBox, GameState state)
        {
            if (HitTest(HomeButton.HitBox, touchBox))
            { 
                index = 0;
                Drawable.Clear();
                state = GameState.MainMenu;
            }
            return state;
        }

        public void ArrowButton(Rectangle touchBox)
        {
            if (HitTest(ArrowRight.HitBox, touchBox))
            {
                if (index < 25)
                    index++;
                else
                    index = 0;

                Drawable.Clear();
            }
        }

        public bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }
    }
}