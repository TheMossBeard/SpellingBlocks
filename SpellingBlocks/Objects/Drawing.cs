using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2Extensions;

namespace SpellingBlocks.Objects
{
    class Drawing
    {
        public Texture2D Sprite { get; set; }
        public Vector2 Position { get; set; }
        public SpriteBatch spriteBatch { get; set; }
        public List<Vector2> PositionList { get; set; }

        public Texture2D Background { get; set; }
        Vector2 pointA;
        Vector2 pointB;
        Vector2 deltaVector;
        float distance;
        Vector2 direction;
        Vector2 newPoint;
        int index = 0;


        public Drawing(SpriteBatch spriteBatch, GameContent gameContent)
        {
            Background = gameContent.traceA;
            PositionList = new List<Vector2>();
            Position = new Vector2(0, 0);
            Sprite = gameContent.spriteA;
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, Color.White, 0,
                new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            for (int ii = 0; ii < PositionList.Count - 1; ii++)
            {
                pointA = PositionList[ii];
                pointB = PositionList[ii + 1];
                deltaVector = pointB - pointA;
                distance = deltaVector.Length();
                direction = deltaVector / distance;
                for (float z = 1; z < distance; z++)
                {
                    newPoint = pointA + direction * (distance * (z / distance));
                    spriteBatch.Draw(Sprite, new Rectangle((int)newPoint.X - 4, (int)newPoint.Y - 4, 16, 16), Color.Black);
                }
            }
        }

        public void DrawUpdate(Rectangle touchBox, GameContent gameContent, SpriteBatch spriteBatch)
        {
            Position = new Vector2(touchBox.X, touchBox.Y);
            PositionList.Add(Position);
            if (PositionList.Count > 300)
                PositionList.Clear();
        }

        public void Clear()
        {
            PositionList.Clear();
        }


    }


}