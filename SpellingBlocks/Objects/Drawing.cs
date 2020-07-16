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
        public List<List<Vector2>> ToDrawList { get; set; }
        Vector2 pointA;
        Vector2 pointB;
        Vector2 deltaVector;
        float distance;
        Vector2 direction;
        Vector2 newPoint;


        public Drawing(SpriteBatch spriteBatch, GameContent gameContent)
        {
            PositionList = new List<Vector2>();
            ToDrawList = new List<List<Vector2>>();
            Position = new Vector2(0, 0);
            Sprite = gameContent.spriteA;
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
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

            if (ToDrawList.Count > 1)
            {
                for (int jj = 1; jj < ToDrawList.Count; jj++)
                {
                    for (int ii = 0; ii < ToDrawList[jj].Count - 1; ii++)
                    {
                        pointA = ToDrawList[jj][ii];
                        pointB = ToDrawList[jj][ii + 1];
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
            }
        }

        public void DrawUpdate(Rectangle touchBox, GameContent gameContent, SpriteBatch spriteBatch)
        {
            Position = new Vector2(touchBox.X, touchBox.Y);
            PositionList.Add(Position);
        }

        public void Clear()
        {
            PositionList.Clear();
        }

        public void NewDraw()
        {
            ToDrawList.Add(PositionList);
            PositionList = new List<Vector2>();
        }
    }


}