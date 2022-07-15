using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class Verse : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string VigBG = "sb/vig.png";

        [Configurable]
        public string Head = "sb/head.png";

        [Configurable]
        public string CircleHead = "sb/c2.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        public override void Generate(){
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            var layer = GetLayer("Verse");

            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = layer.CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 1250.0f / bitmap.Height);
            bg.Fade(StartTime, StartTime, 0, Opacity);
            bg.Fade(EndTime, EndTime + 500, Opacity, 0);

            var MiddleTime = (StartTime+EndTime)/2;
            bg.Move(StartTime, MiddleTime, 320, 1460, 320, -100);
            bg.Move(MiddleTime, EndTime, 320, -100, 320, 1460);
            bg.Rotate((OsbEasing)6, StartTime, MiddleTime, MathHelper.DegreesToRadians(90), MathHelper.DegreesToRadians(180));
            bg.Rotate((OsbEasing)7, MiddleTime, EndTime, MathHelper.DegreesToRadians(180), MathHelper.DegreesToRadians(270));

            var flashBG = layer.CreateSprite(Flash, OsbOrigin.Centre);
            var Flashbitmap = GetMapsetBitmap(Flash);

            flashBG.Fade(OsbEasing.InCubic, StartTime, StartTime+tick(0,(double)1/(double)2), 0.8, 0);
            flashBG.Scale(StartTime, 510.0f / Flashbitmap.Height);

            var headSpin = layer.CreateSprite(Head,OsbOrigin.Centre);
            headSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 0, 1);
            headSpin.Fade(StartTime+tick(0,1), EndTime-tick(0,(double)1/(double)2), 1, 1);
            headSpin.Scale(StartTime, 0.33);
            headSpin.Fade(EndTime, EndTime+tick(0,1), 1, 0);
            
            headSpin.Rotate(StartTime, 15011, MathHelper.DegreesToRadians(0),  MathHelper.DegreesToRadians(180));

            var rotateInit = 180;
            var rotateIncrease = 0;
            for(double spinInit = 15011+tick(0,2); spinInit<=EndTime;spinInit+=tick(0,1)){
                headSpin.Rotate((OsbEasing)8,spinInit, spinInit+tick(0,2), MathHelper.DegreesToRadians(rotateInit),  MathHelper.DegreesToRadians(rotateInit-3));
                Log($"{rotateInit-3}");
                rotateInit-= 3+rotateIncrease;
                rotateIncrease+=2;
                
            }
            

            var circleSpin = layer.CreateSprite(CircleHead,OsbOrigin.Centre);
            circleSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 0, 1);
            circleSpin.Fade(StartTime+tick(0,1), EndTime, 1, 1);
            circleSpin.Scale(StartTime, 0.45);

            for(double circlePump = StartTime+tick(0,1); circlePump <= EndTime; circlePump+=tick(0,(double)1/(double)2)){
                headSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.33, 0.3);
                circleSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.45, 0.45*1.1);

                if(circlePump==23636){
                    circleSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,1), 0.45*1.1, 0.45);
                    headSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,1), 0.3, 0.33);
                }else{
                    circleSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.45*1.1, 0.45);
                    headSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.3, 0.33);
                }
            }
            circleSpin.Scale((OsbEasing)4, EndTime, EndTime+tick(0,1), 0.45, 2);
            headSpin.Scale((OsbEasing)4, EndTime, EndTime+tick(0,1), 0.68, 1.6);
            headSpin.Rotate((OsbEasing)4, EndTime, EndTime+tick(0,1), MathHelper.DegreesToRadians(45), MathHelper.DegreesToRadians(-300));

            var vig = layer.CreateSprite(VigBG,OsbOrigin.Centre);
            var Vigbitmap = GetMapsetBitmap(VigBG);
            vig.Fade(StartTime, StartTime+tick(0,(double)1/(double)8), 0,0.65);
            vig.Fade(StartTime+tick(0,(double)1/(double)8), EndTime, 0.65,0.65);
            vig.Scale(StartTime, 540.0f / Vigbitmap.Height);
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
