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
using Android.Net.Wifi.Hotspot2.Pps;
using System.Reflection;

namespace SpellingBlocks
{
    class GameContent
    {
        public Texture2D spriteA, spriteB, spriteC, spriteD, spriteE, spriteF, spriteG, spriteH, spriteI, spriteJ, spriteK,
            spriteL, spriteM, spriteN, spriteO, spriteP, spriteQ, spriteR, spriteS, spriteT, spriteU, spriteV, spriteW,
            spriteX, spriteY, spriteZ, emptySprite, menu01, menu02, menu03, menu04, menuBackground, categoryBackground,
            logoSprite, arrorRight, home, MachinesButton, NatureButton, AnimalsButton, airplane, apple, boat, bulldozer, car,
            dog, elephant, flower, lion, mushroom, snake, train, tree, waterfall, zebra, spellingblocksbackground;

        public Texture2D traceA, traceB, traceC, traceD, traceE, traceF, traceG, traceH, traceI, traceJ, traceK, traceL, traceM,
            traceN, traceO, traceP, traceQ, traceR, traceS, traceT, traceU, traceV, traceW, traceX, traceY, traceZ;

        public List<Texture2D> SpriteList { get; set; }
        public List<Texture2D> TraceList { get; set; }

        public GameContent(ContentManager Content)
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

            emptySprite = Content.Load<Texture2D>("Empty-Sprite");

            logoSprite = Content.Load<Texture2D>("Logo-Sprite");

            menuBackground = Content.Load<Texture2D>("MenuBackground");

            menu01 = Content.Load<Texture2D>("Menu01-Sprite");
            menu02 = Content.Load<Texture2D>("Menu02-Sprite");
            menu03 = Content.Load<Texture2D>("Menu03-Sprite");
            menu04 = Content.Load<Texture2D>("Menu04-Sprite");

            arrorRight = Content.Load<Texture2D>("Right-Arrow-Sprite");

            home = Content.Load<Texture2D>("Home-Sprite");

            categoryBackground = Content.Load<Texture2D>("BackgroundCatergory");

            MachinesButton = Content.Load<Texture2D>("MachinesButton");
            NatureButton = Content.Load<Texture2D>("NatureButton");
            AnimalsButton = Content.Load<Texture2D>("AnimalsButton");

            spellingblocksbackground = Content.Load<Texture2D>("SpellingBlocksBackground");

            elephant = Content.Load<Texture2D>("Elephant-Image");
            airplane = Content.Load<Texture2D>("Airplane");
            apple = Content.Load<Texture2D>("Apple");
            boat = Content.Load<Texture2D>("Boat");
            bulldozer = Content.Load<Texture2D>("Bulldozer");
            car = Content.Load<Texture2D>("Car");
            dog = Content.Load<Texture2D>("Dog");
            flower = Content.Load<Texture2D>("Flower");
            lion = Content.Load<Texture2D>("Lion");
            mushroom = Content.Load<Texture2D>("Mushroom");
            snake = Content.Load<Texture2D>("Snake");
            train = Content.Load<Texture2D>("Train");
            tree = Content.Load<Texture2D>("Tree");
            waterfall = Content.Load<Texture2D>("Waterfall");
            zebra = Content.Load<Texture2D>("Zebra");

            TraceList = new List<Texture2D>();
            traceA = Content.Load<Texture2D>("TraceA");
            TraceList.Add(traceA);
            traceB = Content.Load<Texture2D>("TraceB");
            TraceList.Add(traceB);
            traceC = Content.Load<Texture2D>("TraceC");
            TraceList.Add(traceC);
            traceD = Content.Load<Texture2D>("TraceD");
            TraceList.Add(traceD);
            traceE = Content.Load<Texture2D>("TraceE");
            TraceList.Add(traceE);
            traceF = Content.Load<Texture2D>("TraceF");
            TraceList.Add(traceF);
            traceG = Content.Load<Texture2D>("TraceG");
            TraceList.Add(traceG);
            traceH = Content.Load<Texture2D>("TraceH");
            TraceList.Add(traceH);
            traceI = Content.Load<Texture2D>("TraceI");
            TraceList.Add(traceI);
            traceJ = Content.Load<Texture2D>("TraceJ");
            TraceList.Add(traceJ);
            traceK = Content.Load<Texture2D>("TraceK");
            TraceList.Add(traceK);
            traceL = Content.Load<Texture2D>("TraceL");
            TraceList.Add(traceL);
            traceM = Content.Load<Texture2D>("TraceM");
            TraceList.Add(traceM);
            traceN = Content.Load<Texture2D>("TraceN");
            TraceList.Add(traceN);
            traceO = Content.Load<Texture2D>("TraceO");
            TraceList.Add(traceO);
            traceP = Content.Load<Texture2D>("TraceP");
            TraceList.Add(traceP);
            traceQ = Content.Load<Texture2D>("TraceQ");
            TraceList.Add(traceQ);
            traceR = Content.Load<Texture2D>("TraceR");
            TraceList.Add(traceR);
            traceS = Content.Load<Texture2D>("TraceS");
            TraceList.Add(traceS);
            traceT = Content.Load<Texture2D>("TraceT");
            TraceList.Add(traceT);
            traceU = Content.Load<Texture2D>("TraceU");
            TraceList.Add(traceU);
            traceV = Content.Load<Texture2D>("TraceV");
            TraceList.Add(traceV);
            traceW = Content.Load<Texture2D>("TraceW");
            TraceList.Add(traceW);
            traceX = Content.Load<Texture2D>("TraceX");
            TraceList.Add(traceX);
            traceY = Content.Load<Texture2D>("TraceY");
            TraceList.Add(traceY);
            traceZ = Content.Load<Texture2D>("TraceZ");
            TraceList.Add(traceZ);


        }
    }
}