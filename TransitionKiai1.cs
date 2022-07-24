using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class TransitionKiai1 : StoryboardObjectGenerator
    {
        
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;
        
        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int FastTime = 0;

        [Configurable]
        public int KiaiStartTime = 0;

        [Configurable]
        public int TrocaStartTime = 0;

        [Configurable]
        public int EndStartTime = 60011;


        [Configurable]
        public int BuildupStartTime = 57011;


        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string VigBG = "sb/vig.png";

        [Configurable]
        public string TriangleFinal = "sb/triangle-128.png";

        [Configurable]
        public string TriangleTile = "sb/osu!triangles-tile.png";

        [Configurable]
        public string LogoCirclePath = "sb/logo.png";


        [Configurable]
        public string TrapezioPath = "sb/trapezio.png";

        [Configurable]
        public string LogoTextPath = "sb/logo.png";
        
        [Configurable]
        public string LogoTrianglePath = "sb/osu-logo-triangulo.png";


        [Configurable]
        public float SCALE = 0.88f;


        [Configurable]
        public float SCALE_AFTER_X = 0.88f;


        [Configurable]
        public float SCALE_BEFORE_X = 0.88f;

        public override void Generate()
        {
            var Layer = GetLayer("TransKiai");

            var BackgroundTile1 = Layer.CreateSprite(TriangleTile, OsbOrigin.Centre);
            var BackgroundTile2 = Layer.CreateSprite(TriangleTile, OsbOrigin.Centre);
            var BackgroundVinheta = Layer.CreateSprite(VigBG, OsbOrigin.Centre);

            var Vigbitmap = GetMapsetBitmap(VigBG);

            var LogoCircle = Layer.CreateSprite(LogoCirclePath, OsbOrigin.Centre); 
            var LogoText = Layer.CreateSprite(LogoTextPath, OsbOrigin.Centre); // (delta x =  0; delta y + 40)
            var LogoTriangle = Layer.CreateSprite(LogoTrianglePath, OsbOrigin.Centre, new Vector2(320, 200));
            var Trapezio1 = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, new Vector2(220, 185)); // (delta x = -100; delta y = -15) 11
            var Trapezio2 = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, new Vector2(420, 185)); // (delta x = +100; delta y = 15) -15
            var Trapezio3 = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, new Vector2(320, 357)); // (delta x = 0; delta y = 157)
            var FlashBG = Layer.CreateSprite(Flash, OsbOrigin.Centre);
            var vig = Layer.CreateSprite(VigBG,OsbOrigin.Centre);

            Vector2 CenterPosition = new Vector2(320, 240);


            BackgroundTile1.Fade(OsbEasing.InOutExpo, TrocaStartTime, TrocaStartTime + 300, 0, 1);
            BackgroundTile2.Fade(OsbEasing.InOutExpo, TrocaStartTime, TrocaStartTime + 300, 0, 1);
            BackgroundTile1.Color(TrocaStartTime, Color4.White);

            BackgroundTile1.Additive(TrocaStartTime);
            BackgroundTile2.Additive(TrocaStartTime);

            BackgroundTile2.ScaleVec(TrocaStartTime, new Vector2(2.0f, -2.0f));
            
            BackgroundTile1.MoveY(TrocaStartTime, EndTime, 80, 300);

            BackgroundTile2.MoveY(TrocaStartTime, EndTime, 400, 200);

            vig.Scale(StartTime, 540.0f / Vigbitmap.Height);
            vig.Fade(StartTime, 0.4);
            vig.Fade(EndTime, 0);

            float START_SCALE = 0.2f;

            LogoCircle.Fade(StartTime, 1);
            LogoText.Fade(StartTime, 1);

            double BeatTime = tick(StartTime, 1);

            for (double i = StartTime; i <= TrocaStartTime; i += BeatTime) {
                if (i == TrocaStartTime)
                    LogoText.Scale(OsbEasing.InOutExpo, i, i + (BeatTime *3), 0.25f, 0.15f);
                else
                    LogoText.Scale(OsbEasing.In, i, i + BeatTime, 0.19f, 0.18f);
            }

            LogoCircle.Scale(StartTime, START_SCALE);
            LogoText.Scale(StartTime, START_SCALE);

            LogoCircle.MoveY(OsbEasing.OutSine, StartTime, TrocaStartTime, -100, 240);
            LogoText.Move(OsbEasing.OutSine, StartTime, TrocaStartTime, new Vector2(320, -100),new Vector2(320, 240));

            LogoCircle.Scale(OsbEasing.In, TrocaStartTime, TrocaStartTime + 300, START_SCALE, 0.6f);
            LogoCircle.Fade(OsbEasing.InOutExpo, TrocaStartTime, TrocaStartTime + 300, 1, 0);

            LogoTriangle.Fade(37886, 37886 + 150, 0, 1);

            LogoTriangle.Scale(OsbEasing.InOutElastic, 37886, 37886 + 150, 0.35f, 0.3f);

            Trapezio1.Rotate(38261, DegToRad(120));
            Trapezio1.ScaleVec(OsbEasing.InOutExpo, 38261, 38261 + 130, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
            Trapezio1.Fade(38261, 1);
            Trapezio1.Color(38261, Color4.White);


            Trapezio2.Rotate(38636, DegToRad(240));
            Trapezio2.Fade(38636, 1);            
            Trapezio2.ScaleVec(OsbEasing.InOutExpo, 38636, 38636 + 130, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
            Trapezio2.Color(38636, Color4.White);

            Trapezio3.Fade(39011, 1);            
            Trapezio3.ScaleVec(OsbEasing.InOutExpo, 38824, 38824 + 130, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
            Trapezio3.Color(39011, Color4.White);        
              
            FlashBG.Scale(39011, 100);
            FlashBG.Fade(OsbEasing.InQuad, 39011, 39011 + 100, 0.6, 0);
            FlashBG.Fade(OsbEasing.InQuad, 45011, 45011 + 100, 0.6, 0);
            FlashBG.Fade(OsbEasing.InQuad, 57011, 57011 + 100, 0.6, 0);
            
            double MeasureTime = (BeatTime * 0.75);

            Vector2 NewRandomPosition = new Vector2();

            int Count = 0;

            var ToIncrease = MeasureTime;

            // MOVIMENTAÇÃO
            for (double i = KiaiStartTime + (BeatTime/2); i< BuildupStartTime;) { 
                if (Count == 4 ) {
                    ToIncrease = BeatTime;
                } else {
                    ToIncrease = MeasureTime;
                }

                NewRandomPosition =  GetRandomPositionFromRadius(LogoTriangle.PositionAt(i), 80);

                LogoTriangle.Move((OsbEasing) 7, i, i + ToIncrease, LogoTriangle.PositionAt(i), NewRandomPosition);
                LogoText.Move((OsbEasing) 7, i, i + ToIncrease, LogoText.PositionAt(i), new Vector2(NewRandomPosition.X, NewRandomPosition.Y + 40));
                    
                Trapezio1.Move(OsbEasing.InOutSine, i, i + ToIncrease, Trapezio1.PositionAt(i), new Vector2(NewRandomPosition.X -100, NewRandomPosition.Y -15 ));
                Trapezio2.Move(OsbEasing.InOutSine, i, i + ToIncrease, Trapezio2.PositionAt(i), new Vector2(NewRandomPosition.X + 100, NewRandomPosition.Y -15 ));
                Trapezio3.Move(OsbEasing.InOutSine, i, i + ToIncrease, Trapezio3.PositionAt(i), new Vector2(NewRandomPosition.X, NewRandomPosition.Y + 157));
                

                i+= ToIncrease;

                if (Count == 4) {
                    Count = 0;
                } else {
                    Count++;
                }
            }
            // MOVIMENTO PREBUILDUP
            Count = 0;
            for (double i = BuildupStartTime + (BeatTime/2); i< EndStartTime;) { 
                if (Count == 1 ) {
                    ToIncrease = BeatTime * 1.25;
                } else {
                    ToIncrease = MeasureTime;
                }

                NewRandomPosition = GetRandomPositionFromRadius(LogoTriangle.PositionAt(i), 80);
                
                LogoTriangle.Move((OsbEasing) 7, i, i + ToIncrease, LogoTriangle.PositionAt(i), NewRandomPosition);
                LogoText.Move((OsbEasing) 7, i, i + ToIncrease, LogoText.PositionAt(i), new Vector2(NewRandomPosition.X, NewRandomPosition.Y + 40));
                    
                Trapezio1.Move(OsbEasing.InOutSine, i, i + ToIncrease, Trapezio1.PositionAt(i), new Vector2(NewRandomPosition.X -100, NewRandomPosition.Y -15 ));
                Trapezio2.Move(OsbEasing.InOutSine, i, i + ToIncrease, Trapezio2.PositionAt(i), new Vector2(NewRandomPosition.X + 100, NewRandomPosition.Y -15 ));
                Trapezio3.Move(OsbEasing.InOutSine, i, i + ToIncrease, Trapezio3.PositionAt(i), new Vector2(NewRandomPosition.X, NewRandomPosition.Y + 157));
                

                i+= ToIncrease;

                if (Count == 1) {
                    Count = 0;
                } else {
                    Count++;
                }

            }
            
            Vector2 MoveToPosition = new Vector2((CenterPosition.X - NewRandomPosition.X ) /8,(CenterPosition.Y -  NewRandomPosition.Y)/ 8);

            // MOVIMENTO BUILDUP
            Count = 1;
            for (double i = EndStartTime + (BeatTime/2); i< EndTime;) { 

                ToIncrease = BeatTime;

                Vector2 SendToPosition = NewRandomPosition + (MoveToPosition *Count);
                        
                LogoTriangle.Move((OsbEasing) 7, i, i + ToIncrease, LogoTriangle.PositionAt(i), SendToPosition);
                LogoText.Move((OsbEasing) 7, i, i + ToIncrease, LogoText.PositionAt(i), new Vector2(SendToPosition.X, SendToPosition.Y+ 40));
                    
                Trapezio1.Move((OsbEasing) 6, i, i + ToIncrease, Trapezio1.PositionAt(i), new Vector2(SendToPosition.X -100, SendToPosition.Y -15 ));
                Trapezio2.Move((OsbEasing) 6, i, i + ToIncrease, Trapezio2.PositionAt(i), new Vector2(SendToPosition.X + 100, SendToPosition.Y -15 ));
                Trapezio3.Move((OsbEasing) 6, i, i + ToIncrease, Trapezio3.PositionAt(i), new Vector2(SendToPosition.X, SendToPosition.Y + 157));
                
                Count++;
                i+= ToIncrease;
            }

            /*
            var CenterPosition = new Vector2(320, 240);

            LogoTriangle.Move(OsbEasing.None, EndStartTime, EndStartTime + BeatTime, LogoTriangle.PositionAt(EndStartTime), CenterPosition);
            LogoText.Move(OsbEasing.None, EndStartTime, EndStartTime + BeatTime, LogoText.PositionAt(EndStartTime), new Vector2(CenterPosition.X, (CenterPosition.Y + (40 * 0.9f) ))); 
                    
            Trapezio1.Move(OsbEasing.None, EndStartTime, EndStartTime + BeatTime, Trapezio1.PositionAt(EndStartTime), new Vector2(CenterPosition.X -100, CenterPosition.Y -15 ));
            Trapezio2.Move(OsbEasing.None, EndStartTime, EndStartTime + BeatTime, Trapezio2.PositionAt(EndStartTime), new Vector2(CenterPosition.X + 100, CenterPosition.Y -15 ));
            Trapezio3.Move(OsbEasing.None, EndStartTime, EndStartTime + BeatTime, Trapezio3.PositionAt(EndStartTime), new Vector2(CenterPosition.X, CenterPosition.Y + 157));
            */
            int TrapezioToClone = 1;
 
            // KICK SNARE EXPAND TOP 10
            for (double i = KiaiStartTime; i< EndStartTime; i+=BeatTime) {
                Trapezio1.ScaleVec(OsbEasing.InOutExpo, i, i + BeatTime, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
                Trapezio2.ScaleVec(OsbEasing.InOutExpo, i, i + BeatTime, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
                Trapezio3.ScaleVec(OsbEasing.InOutExpo, i, i + BeatTime, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
                
                LogoText.Scale(OsbEasing.In, i, i + BeatTime, 0.19f, 0.17f);
                LogoTriangle.Scale(OsbEasing.InOutSine, i, i + BeatTime, 0.31f, 0.3f);

                OsbSprite TrapClone;
                Color4 ProjectileColor;
                var AngleToFace = 90;
                switch (TrapezioToClone) {
                    case 1:
                        TrapClone = Trapezio1;
                        Trapezio1.Color(i, i+BeatTime, Color4.Red, Color4.White);
                        ProjectileColor = Color4.Red;
                        Trapezio1.Additive(i, i + BeatTime);
                        AngleToFace = -150;
                        break;
                    case 2:
                        TrapClone = Trapezio2;
                        Trapezio2.Color(i, i+BeatTime, Color4.Green, Color4.White);
                        ProjectileColor = Color4.Green;
                        Trapezio2.Additive(i, i + BeatTime);
                        AngleToFace = -30;
                        break;
                    case 3:
                        AngleToFace = 90;
                        Trapezio3.Color(i, i+BeatTime, Color4.Blue, Color4.White);
                        ProjectileColor = Color4.Blue;
                        Trapezio3.Additive(i, i + BeatTime);
                        TrapClone = Trapezio3;
                        break;
                    default:  
                        TrapClone = Trapezio1;
                        ProjectileColor = Color4.White;
                        break;
                }   

                var CloneTrapezio = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, TrapClone.PositionAt(i)); // (delta x = -100; delta y = -12)
                CloneTrapezio.Rotate(i, TrapClone.RotationAt(i));
                CloneTrapezio.ScaleVec((OsbEasing) 0, i, i + BeatTime * 3, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE * 3.5f, SCALE_AFTER_X * 3.5f));
                CloneTrapezio.Color(i, ProjectileColor);
                BackgroundTile1.Color(i, ProjectileColor);
                CloneTrapezio.Additive(i);
                CloneTrapezio.Move((OsbEasing) 0, i, i + BeatTime * 3, TrapClone.PositionAt(i), new Vector2(TrapClone.PositionAt(i).X + 620 * Math.Cos(DegToRad(AngleToFace)),TrapClone.PositionAt(i).Y + 620 * Math.Sin(DegToRad(AngleToFace))));
                
                if (TrapezioToClone >= 3)
                    TrapezioToClone = 1;
                else
                    TrapezioToClone++;
            }

            float ScaleMultipl = 0.9f;

            // FINALZIN
            for (double i = EndStartTime; i< EndTime; i+=BeatTime) {
                Trapezio1.ScaleVec(OsbEasing.InOutExpo, i, i + BeatTime, new Vector2(SCALE, SCALE_BEFORE_X ), new Vector2(SCALE, SCALE_AFTER_X));
                Trapezio2.ScaleVec(OsbEasing.InOutExpo, i, i + BeatTime, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
                Trapezio3.ScaleVec(OsbEasing.InOutExpo, i, i + BeatTime, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE, SCALE_AFTER_X));
                
                LogoText.Scale(OsbEasing.In, i, i + BeatTime, 0.19f, 0.17f);
                LogoTriangle.Scale(OsbEasing.InOutSine, i, i + BeatTime, 0.31f, 0.3f);

                var TrapClone = Trapezio1;
                var AngleToFace = 150;
                var CloneTrapezio = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, TrapClone.PositionAt(i)); // (delta x = -100; delta y = -12)
                CloneTrapezio.Rotate(i, TrapClone.RotationAt(i));
                CloneTrapezio.ScaleVec((OsbEasing) 0, i, i + BeatTime * 3, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE *3, SCALE_AFTER_X *3));
                CloneTrapezio.Move((OsbEasing) 0, i, i + BeatTime * 3, TrapClone.PositionAt(i), new Vector2(TrapClone.PositionAt(i).X + 620 * Math.Cos(DegToRad(AngleToFace)),TrapClone.PositionAt(i).Y + 620 * Math.Sin(DegToRad(-AngleToFace))));
                
                TrapClone = Trapezio2;
                AngleToFace = 30;
                var CloneTrapezio1 = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, TrapClone.PositionAt(i)); // (delta x = -100; delta y = -12)
                CloneTrapezio1.Rotate(i, TrapClone.RotationAt(i));
                CloneTrapezio1.ScaleVec((OsbEasing) 0, i, i + BeatTime * 3, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE *3, SCALE_AFTER_X *3));
                CloneTrapezio1.Move((OsbEasing) 0, i, i + BeatTime * 3, TrapClone.PositionAt(i), new Vector2(TrapClone.PositionAt(i).X + 620 * Math.Cos(DegToRad(AngleToFace)),TrapClone.PositionAt(i).Y + 620 * Math.Sin(DegToRad(-AngleToFace))));
                
                AngleToFace = -90;
                TrapClone = Trapezio3;
                var CloneTrapezio2 = Layer.CreateSprite(TrapezioPath, OsbOrigin.Centre, TrapClone.PositionAt(i)); // (delta x = -100; delta y = -12)
                CloneTrapezio2.Rotate(i, TrapClone.RotationAt(i));
                CloneTrapezio2.ScaleVec((OsbEasing) 0, i, i + BeatTime * 3, new Vector2(SCALE, SCALE_BEFORE_X), new Vector2(SCALE *3, SCALE_AFTER_X *3));
                CloneTrapezio2.Move((OsbEasing) 0, i, i + BeatTime * 3, TrapClone.PositionAt(i), new Vector2(TrapClone.PositionAt(i).X + 620 * Math.Cos(DegToRad(AngleToFace)),TrapClone.PositionAt(i).Y + 620 * Math.Sin(DegToRad(-AngleToFace))));
                
                FlashBG.Fade(OsbEasing.InQuad, i,i + BeatTime, FlashBG.OpacityAt(i), FlashBG.OpacityAt(i)+ 0.12 );

               ScaleMultipl -= 0.05f ;

            }

            Trapezio1.Fade(EndTime, 0);
            Trapezio2.Fade(EndTime, 0);
            Trapezio3.Fade(EndTime, 0);
            LogoText.Fade(EndTime, 0);
            BackgroundTile2.Fade(EndTime, 0);
            LogoTriangle.Fade(EndTime, 0);
        }

        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }

        Vector2 GetRandomPositionFromRadius(Vector2 StartPosition, int Radius) {
            float x,y;
            do {
                double RandomAngle = MathHelper.DegreesToRadians(Random(0,360));
                x = (float)(Radius * Math.Cos(RandomAngle) + StartPosition.X);
                y = (float)(Radius * Math.Sin(RandomAngle) + StartPosition.Y);
            } while ((x < 75 || x > 580) || ((y<140) || y> 340));
            return new Vector2(x, y);
        }
        static double DegToRad(double degrees)
        {
             return ((Math.PI / 180) * degrees);
        }
    }
}
