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
        public int TrocaStartTime = 0;

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string VigBG = "sb/vig.png";

        [Configurable]
        public string TriangleFinal = "sb/triangle-128.png";

        [Configurable]
        public string LogoPath = "sb/logo.png";
        
        [Configurable]
        public string TriangleLogoPath = "sb/osu-logo-triangulo.png";

        public override void Generate()
        {
            var layer = GetLayer("Kiai");

            var logo = layer.CreateSprite(LogoPath, OsbOrigin.Centre);
            logo.Move(OsbEasing.InOutQuint, StartTime, TrocaStartTime, new Vector2(320, -200), new Vector2(320, 245));
            logo.Fade(StartTime, StartTime, 0, 1);
            logo.Scale(OsbEasing.OutSine, StartTime,TrocaStartTime, 0.15f, 0.22f);

            
            var trilegal = layer.CreateSprite(TriangleLogoPath, OsbOrigin.Centre);
            trilegal.Move(TrocaStartTime + 200, new Vector2(320, 200));


            var troca = false;
            var timeDifferenceBetweenBeats = tick(StartTime, 1);



            for (double i = StartTime; i < EndTime; i += timeDifferenceBetweenBeats)
            {
                
                if (i >= FastTime) {
                    timeDifferenceBetweenBeats = tick(StartTime, 2);
                }

                if (i > TrocaStartTime) {
                    if (troca) {
                        logo.Fade(i, i, 1, 0);
                        trilegal.Fade(i, i, 0, 1);
                        trilegal.Scale(OsbEasing.OutSine, i, i+130, 0.3f, 0.35f);
                    } else {
                        logo.Fade(i, i, 0, 1);
                        trilegal.Fade(i, i, 1, 0);
                        //logo.Scale(OsbEasing.OutSine, i, i+130, 0.15f, 0.22f);
                    }
                    troca = !troca;
                }
            }

        }

        void GenerateTriangleBars() {   
            
        }


        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
        List<double> CalculateCurve(double x, double y, double radius){
            List<double> curve = new List<double>();
            for(double a = 0; a<=180; a++){
                curve.Add(x+radius*Math.Cos((a)*(Math.PI/90)));
                curve.Add(y+radius*Math.Sin((a)*(Math.PI/90)));
            }
            return curve;
        }
        int mod(int x, int m) {
            return (x%m + m)%m;
        }
    }
}
