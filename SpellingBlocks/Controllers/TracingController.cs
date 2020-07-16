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
    class TracingController
    {
        const int COUNT = 25;

        private int index;
        public List<Texture2D> TraceCardList { get; set; }

        public Drawing Drawable { get; set; }
        public SpriteBatch spriteBatch { get; set; }

        public TracingController(SpriteBatch spriteBatch, GameContent gameContent)
        {
            index = 5;
            Drawable = new Drawing(spriteBatch, gameContent);
            this.spriteBatch = spriteBatch;
            TraceCardList = new List<Texture2D>();
            Fill(gameContent);
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

    }
}