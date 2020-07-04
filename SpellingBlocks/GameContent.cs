﻿using System;
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

        public List<Texture2D> SpriteList { get; set; }

        public GameContent (ContentManager Content)
        {
            SpriteList = new List<Texture2D>();
            spriteA = Content.Load<Texture2D>("A-Sprite");
            SpriteList.Add(spriteA);
            spriteB = Content.Load<Texture2D>("B-Sprite");
            SpriteList.Add(spriteB);
            spriteC = Content.Load<Texture2D>("C-Sprite");
            SpriteList.Add(spriteC);
            spriteD = Content.Load<Texture2D>("D-Sprite");
            SpriteList.Add(spriteD);
            spriteE = Content.Load<Texture2D>("E-Sprite");
            SpriteList.Add(spriteE);
            spriteF = Content.Load<Texture2D>("F-Sprite");
            SpriteList.Add(spriteF);
            spriteG = Content.Load<Texture2D>("G-Sprite");
            SpriteList.Add(spriteG);
            spriteH = Content.Load<Texture2D>("H-Sprite");
            SpriteList.Add(spriteH);
            spriteI = Content.Load<Texture2D>("I-Sprite");
            SpriteList.Add(spriteI);
            spriteJ = Content.Load<Texture2D>("J-Sprite");
            SpriteList.Add(spriteJ);
            spriteK = Content.Load<Texture2D>("K-Sprite");
            SpriteList.Add(spriteK);
            spriteL = Content.Load<Texture2D>("L-Sprite");
            SpriteList.Add(spriteL);
            spriteM = Content.Load<Texture2D>("M-Sprite");
            SpriteList.Add(spriteM);
            spriteN = Content.Load<Texture2D>("N-Sprite");
            SpriteList.Add(spriteN);
            spriteO = Content.Load<Texture2D>("O-Sprite");
            SpriteList.Add(spriteO);
            spriteP = Content.Load<Texture2D>("P-Sprite");
            SpriteList.Add(spriteP);
            spriteQ = Content.Load<Texture2D>("Q-Sprite");
            SpriteList.Add(spriteQ);
            spriteR = Content.Load<Texture2D>("R-Sprite");
            SpriteList.Add(spriteR);
            spriteS = Content.Load<Texture2D>("S-Sprite");
            SpriteList.Add(spriteS);
            spriteT = Content.Load<Texture2D>("T-Sprite");
            SpriteList.Add(spriteT);
            spriteU = Content.Load<Texture2D>("U-Sprite");
            SpriteList.Add(spriteU);
            spriteV = Content.Load<Texture2D>("V-Sprite");
            SpriteList.Add(spriteV);
            spriteW = Content.Load<Texture2D>("W-Sprite");
            SpriteList.Add(spriteW);
            spriteX = Content.Load<Texture2D>("X-Sprite");
            SpriteList.Add(spriteX);
            spriteY = Content.Load<Texture2D>("Y-Sprite");
            SpriteList.Add(spriteY);
            spriteZ = Content.Load<Texture2D>("Z-Sprite");
            SpriteList.Add(spriteZ);



        }
    }
}