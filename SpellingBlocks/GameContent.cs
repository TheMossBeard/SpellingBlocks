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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellingBlocks
{
    class GameContent
    {
        public Texture2D spriteA, spriteB, spriteC, spriteD, spriteE, spriteF, spriteG, spriteH, spriteI, spriteJ, spriteK,
            spriteL, spriteM, spriteN, spriteO, spriteP, spriteQ, spriteR, spriteS, spriteT, spriteU, spriteV, spriteW,
            spriteX, spriteY, spriteZ;


        public GameContent (ContentManager Content)
        {
            spriteA = Content.Load<Texture2D>("A-Sprite");
            spriteB = Content.Load<Texture2D>("B-Sprite");
            spriteC = Content.Load<Texture2D>("C-Sprite");
            spriteD = Content.Load<Texture2D>("D-Sprite");
            spriteE = Content.Load<Texture2D>("E-Sprite");
            spriteF = Content.Load<Texture2D>("F-Sprite");
            spriteG = Content.Load<Texture2D>("G-Sprite");
            spriteH = Content.Load<Texture2D>("H-Sprite");
            spriteI = Content.Load<Texture2D>("I-Sprite");
            spriteJ = Content.Load<Texture2D>("J-Sprite");
            spriteK = Content.Load<Texture2D>("K-Sprite");
            spriteL = Content.Load<Texture2D>("L-Sprite");
            spriteM = Content.Load<Texture2D>("M-Sprite");
            spriteN = Content.Load<Texture2D>("N-Sprite");
            spriteO = Content.Load<Texture2D>("O-Sprite");
            spriteP = Content.Load<Texture2D>("P-Sprite");
            spriteQ = Content.Load<Texture2D>("Q-Sprite");
            spriteR = Content.Load<Texture2D>("R-Sprite");
            spriteS = Content.Load<Texture2D>("S-Sprite");
            spriteT = Content.Load<Texture2D>("T-Sprite");
            spriteU = Content.Load<Texture2D>("U-Sprite");
            spriteV = Content.Load<Texture2D>("V-Sprite");
            spriteW = Content.Load<Texture2D>("W-Sprite");
            spriteX = Content.Load<Texture2D>("X-Sprite");
            spriteY = Content.Load<Texture2D>("Y-Sprite");
            spriteZ = Content.Load<Texture2D>("Z-Sprite");


        }
    }
}