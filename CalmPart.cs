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
    public class CalmPart : StoryboardObjectGenerator{

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string Triangle1 = "sb/triangle-128.png";

        [Configurable]
        public string Triangle2 = "sb/triangulo128.png";

        [Configurable]
        public string Background = "sb/bg4.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string Ponto = "sb/dot3.png";

        [Configurable]
        public int triangleCount = 16;

        [Configurable]
        public Color4 Color = Color4.White;

        [Configurable]
        public float ColorVariance = 0.6f;
        public override void Generate(){
		    
            var layer = GetLayer("CalmPart");
            var MiddleTime = (StartTime+EndTime)/2;
            
            var bg = layer.CreateSprite(Background, OsbOrigin.Centre);
            var bitmap = GetMapsetBitmap(Background);
            bg.Scale(StartTime, 480.0f / bitmap.Height);
            bg.Fade(StartTime, StartTime, 0, 0.5);
            bg.Fade(EndTime, EndTime + 500, 0.5, 0);

            var flashBG = layer.CreateSprite(Flash, OsbOrigin.Centre);
            flashBG.Scale(StartTime, 50);
            flashBG.Fade(StartTime, EndTime, 0.95, 0.7);

            var triangleCentre = new Vector2(320, 240);

            int run = 0;
            for(double tempo = StartTime-tick(0,(double)1/(double)16); tempo < EndTime; tempo+=tick(0,6)){

                var color2 = Color;
                if (ColorVariance > 0){
                    ColorVariance = MathHelper.Clamp(ColorVariance, 0, 1);

                    var hsba = Color4.ToHsl(color2);
                    var sMin = Math.Max(0, hsba.Y - ColorVariance * 0.5f);
                    var sMax = Math.Min(sMin + ColorVariance, 1);
                    var vMin = Math.Max(0, hsba.Z - ColorVariance * 0.5f);
                    var vMax = Math.Min(vMin + ColorVariance, 1);

                    color2 = Color4.FromHsl(new Vector4(
                        hsba.X,
                        (float)Random(sMin, sMax),
                        (float)Random(vMin, vMax),
                        hsba.W));
                }
                var triangle2 = layer.CreateSprite(Triangle1, OsbOrigin.Centre);
                if(run%2==0){
                    triangle2 = layer.CreateSprite(Triangle1, OsbOrigin.Centre);
                }else{
                    triangle2 = layer.CreateSprite(Triangle2, OsbOrigin.Centre);
                }
                
                if (color2.R != 1 || color2.G != 1 || color2.B != 1)
                    triangle2.Color(StartTime, color2);
                    triangle2.Scale(StartTime, Random(0.1,0.25));

                var RandomBorder2 = Random(1,5);
                var newPositiontriangle2 = new Vector2(320,240);
                switch(RandomBorder2){
                    case 1:
                        newPositiontriangle2 = new Vector2(-130, Random(-30, 500));
                    break;
                    case 2:
                        newPositiontriangle2 = new Vector2(770, Random(-30, 500));
                    break;
                    case 3:
                        newPositiontriangle2 = new Vector2(Random(-130, 770), -30);
                    break;
                    case 4:
                        newPositiontriangle2 = new Vector2(Random(-130, 770), 500);
                    break; 
                }
                
                var RandomFade2 = Random(0.6, 1);
                triangle2.Move(tempo, Random(tempo+tick(0,(double)1/(double)15),tempo+tick(0,(double)1/(double)32)), triangleCentre, newPositiontriangle2);
                triangle2.Fade(StartTime-tick(0,(double)1/(double)16), 0);
                if(tempo<StartTime){
                    triangle2.Fade(StartTime, EndTime, RandomFade2, RandomFade2);
                }else{
                    triangle2.Fade(tempo, EndTime, RandomFade2, RandomFade2);
                }
                
                triangle2.Rotate(tempo, Random(tempo+tick(0,(double)1/(double)15),tempo+tick(0,(double)1/(double)32)), MathHelper.DegreesToRadians(Random(-150,150)), MathHelper.DegreesToRadians(Random(-150,150)));
                triangle2.Fade(EndTime, 0);
                run+=1;
            }

            var brilho = layer.CreateSprite(Ponto, OsbOrigin.Centre);
            brilho.Scale(StartTime, 30);
            brilho.Fade(StartTime, 1);
            brilho.Fade(EndTime, EndTime + 500, 1, 0);
            brilho.Color(StartTime, new Color4(255,130,180,255));
            
            flashBG.Additive(StartTime);

            var flashBG2 = layer.CreateSprite(Flash, OsbOrigin.Centre);
            flashBG2.Scale(StartTime, 50);
            flashBG2.Fade((OsbEasing)7, StartTime, MiddleTime, 0.95, 0);
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
