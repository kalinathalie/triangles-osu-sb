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
            bg.Fade(MiddleTime, MiddleTime+tick(0,1), 0.5, 0.1);
            bg.Fade((OsbEasing)6, 85511, 86261, 0.1, 0);

            var flashBG = layer.CreateSprite(Flash, OsbOrigin.Centre);
            flashBG.Fade(StartTime, MiddleTime, 0.92, 0.7);
            flashBG.Fade((OsbEasing)6, MiddleTime, MiddleTime+tick(0,1), 0.7, 0.2);
            flashBG.Fade(85511, 86074, 0.2, 0.1);
            flashBG.Fade(86074, 86449, 0.1, 0.7);
            flashBG.ScaleVec((OsbEasing)7, 86636, 86824, 60, 50, 60, 0);

            flashBG.Additive(StartTime);
            flashBG.Color(StartTime, new Color4(255,255,255,255));
            flashBG.Color(86074, new Color4(80,80,80,255));

            var triangleCentre = new Vector2(320, 240);

            int run = 0;
            for(double tempo = StartTime-tick(0,(double)1/(double)16); tempo < 73136; tempo+=tick(0,6)){
                
                if((tempo>=69761 && tempo<=71261) || (tempo>=63761 && tempo<=64886)) continue;
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
                
                var randomScale = Random(0.1,0.25);
                if (color2.R != 1 || color2.G != 1 || color2.B != 1)
                    triangle2.Color(StartTime, color2);
                    triangle2.Scale(StartTime, randomScale);

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
                var RandomEndTime = Random(tempo+tick(0,(double)1/(double)15),tempo+tick(0,(double)1/(double)32));
                
                triangle2.Move(tempo, RandomEndTime, triangleCentre, newPositiontriangle2);
                triangle2.Fade(StartTime-tick(0,(double)1/(double)16), 0);
                if(tempo<StartTime){
                    triangle2.Fade(StartTime, EndTime, RandomFade2, RandomFade2);
                }else{
                    triangle2.Fade(tempo, EndTime, RandomFade2, RandomFade2);
                }

                if(RandomEndTime >= 73886){
                    triangle2.Scale((OsbEasing)4, 73886, 74261, randomScale, randomScale*1.5);
                    triangle2.Scale((OsbEasing)4, 74261, 75011, randomScale*1.5, 0);
                }
                
                triangle2.Rotate(tempo, RandomEndTime, MathHelper.DegreesToRadians(Random(-150,150)), MathHelper.DegreesToRadians(Random(-150,150)));
                triangle2.Fade(EndTime, 0);
                run+=1;
            }

            var brilho = layer.CreateSprite(Ponto, OsbOrigin.Centre);
            brilho.Scale(StartTime, 30);
            brilho.Fade(StartTime, 1);
            brilho.Fade((OsbEasing)7, 73886, 74636, 1, 0);
            brilho.Color(StartTime, new Color4(255,130,180,255));

            var triangulao = layer.CreateSprite(Triangle1, OsbOrigin.Centre, new Vector2(320, 240));
            triangulao.Fade(StartTime, 1);
            triangulao.Fade(85511, 0);
            triangulao.Color(StartTime, new Color4(200, 20, 120, 255));
            
            triangulao.Rotate((OsbEasing)1, StartTime, 64136, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(60));
            triangulao.Rotate((OsbEasing)1, 65261, 65636, MathHelper.DegreesToRadians(60), MathHelper.DegreesToRadians(40));
            triangulao.Rotate((OsbEasing)1, 65636, 66011, MathHelper.DegreesToRadians(40), MathHelper.DegreesToRadians(60));
            triangulao.Rotate((OsbEasing)1, 66011, 66574, MathHelper.DegreesToRadians(60), MathHelper.DegreesToRadians(30));
            triangulao.Rotate((OsbEasing)1, 66574, 67136, MathHelper.DegreesToRadians(30), MathHelper.DegreesToRadians(60));
            triangulao.Rotate((OsbEasing)1, 67136, 68261, MathHelper.DegreesToRadians(60), MathHelper.DegreesToRadians(0));

            triangulao.Rotate((OsbEasing)1, 68636, 68824, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-10));
            triangulao.Rotate((OsbEasing)1, 68824, 69011, MathHelper.DegreesToRadians(-10), MathHelper.DegreesToRadians(0));
            triangulao.Rotate((OsbEasing)1, 69011, 70136, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-60));

            triangulao.Rotate((OsbEasing)1, 71636, 71824, MathHelper.DegreesToRadians(-60), MathHelper.DegreesToRadians(-50));
            triangulao.Rotate((OsbEasing)1, 71824, 72011, MathHelper.DegreesToRadians(-50), MathHelper.DegreesToRadians(-60));
            triangulao.Rotate((OsbEasing)1, 72011, 73511, MathHelper.DegreesToRadians(-60), MathHelper.DegreesToRadians(20));

            triangulao.Scale((OsbEasing)1, StartTime, 64136, 1*1.2, 1.2*0.91);
            triangulao.Scale((OsbEasing)1, 65261, 65636, 1.2*0.91, 1.2*0.88);
            triangulao.Scale((OsbEasing)1, 65636, 66011, 1.2*0.88, 1.2*0.85);
            triangulao.Scale((OsbEasing)1, 66011, 66574, 1.2*0.85, 1.2*0.805);
            triangulao.Scale((OsbEasing)1, 66574, 67136, 1.2*0.805, 1.2*0.76);
            triangulao.Scale((OsbEasing)1, 67136, 68261, 1.2*0.76, 1.2*0.65);

            triangulao.Scale((OsbEasing)1, 68636, 68824, 1.2*0.65, 1.2*0.635);
            triangulao.Scale((OsbEasing)1, 68824, 69011, 1.2*0.635, 1.2*0.62);
            triangulao.Scale((OsbEasing)1, 69011, 70136, 1.2*0.62, 1.2*0.51);

            triangulao.Scale((OsbEasing)1, 71636, 71824, 1.2*0.51, 1.2*0.495);
            triangulao.Scale((OsbEasing)1, 71824, 72011, 1.2*0.495, 1.2*0.48);
            triangulao.Scale((OsbEasing)1, 72011, 73511, 1.2*0.48, 1.2*0.37);

            triangulao.Rotate((OsbEasing)4, 73886, 74261, MathHelper.DegreesToRadians(20), MathHelper.DegreesToRadians(0));
            triangulao.Rotate((OsbEasing)4, 74261, 75011, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(360));

            triangulao.Scale((OsbEasing)4, 73886, 74261, 0.37, 0.15);
            triangulao.Scale((OsbEasing)4, 74261, 75011, 0.15, 1);

            triangulao.Move((OsbEasing)4, 73886, 74261, new Vector2(320, 240), new Vector2(320, 200));
            triangulao.Move((OsbEasing)4, 74261, 75011, new Vector2(320, 200), new Vector2(320, 420));

            triangulao.Move((OsbEasing)7, 75011, 84011, new Vector2(320, 420), new Vector2(320, 170));
            triangulao.Color((OsbEasing)4, 74636, 75386, new Color4(200, 20, 120, 255), new Color4(255, 255, 255, 255));

            triangulao.Color((OsbEasing)12, 77636, 78011, Color, Color4.White);
            triangulao.Color((OsbEasing)12, 83824, 84199, Color, Color4.White);
            
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
