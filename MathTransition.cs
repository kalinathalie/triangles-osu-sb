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
    public class MathTransition : StoryboardObjectGenerator
    {
        [Configurable]
        public string OsuLogo = "sb/logo.png";

        [Configurable]
        public string Taiko = "sb/f/0044.png";

        [Configurable]
        public string Mania = "sb/f/0043.png";

        [Configurable]
        public string Ctb = "sb/f/0042.png";

        [Configurable]
        public string Std = "sb/f/0041.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;
        public override void Generate(){

            var layer = GetLayer("Math");

		    var osu = layer.CreateSprite(OsuLogo, OsbOrigin.Centre, new Vector2(320, 240));
            var std = layer.CreateSprite(Std, OsbOrigin.Centre, new Vector2(100, 240));
            var taiko = layer.CreateSprite(Taiko, OsbOrigin.Centre, new Vector2(200, 240));
            var mania = layer.CreateSprite(Mania, OsbOrigin.Centre, new Vector2(440, 240));
            var ctb = layer.CreateSprite(Ctb, OsbOrigin.Centre, new Vector2(540, 240));

            osu.Fade((OsbEasing)12, StartTime, StartTime+tick(0,2), 0, 1);
            osu.Fade(29261, 0);
            osu.Scale(StartTime, 0.09);
            osu.Color(StartTime, new Color4(235,120,170,255));
            osu.Color(28886, 28886+tick(0,1), new Color4(235,120,170,255), Color4.White);

            std.Fade((OsbEasing)7, 27386, 27386+tick(0,4), 0, 1);
            taiko.Fade((OsbEasing)7, 27667, 27667+tick(0,4), 0, 1);
            mania.Fade((OsbEasing)7, 27949, 27949+tick(0,4), 0, 1);
            ctb.Fade((OsbEasing)7, 28230, 28230+tick(0,4), 0, 1);

            std.Scale((OsbEasing)7, 28886, 28886+tick(0,1), 1, 0);
            taiko.Scale((OsbEasing)7, 28886, 28886+tick(0,1), 1, 0);
            mania.Scale((OsbEasing)7, 28886, 28886+tick(0,1), 1, 0);
            ctb.Scale((OsbEasing)7, 28886, 28886+tick(0,1), 1, 0);

            var osuRlocation1 = new Vector2(320, 240);
            
            var osuR = layer.CreateSprite(OsuLogo, OsbOrigin.Centre, osuRlocation1);
            osuR.Fade(29261, 1);
            osuR.Fade(EndTime, 0);
            osuR.Scale(29261, 0.09);
            osuR.Color(29261, new Color4(235,0,0,255));
            osuR.Additive(29261);
            osuR.Scale((OsbEasing)7, 29261, 29542, 0.09, 0.15);
            osuR.Scale((OsbEasing)6, 29542, 30011, 0.15, 0);
            
            var osuG = layer.CreateSprite(OsuLogo, OsbOrigin.Centre);
            List<double> curveGreen = CalculateCurve1(310, 240, 10);
            int run = 0;
            for(double tempo = 29261; tempo<=30011; tempo+=tick(0,32)){
                osuG.Move(tempo, tempo+tick(0,32), curveGreen[run], curveGreen[run+1], curveGreen[run+2], curveGreen[run+3]);
                run+=2;
            }
            osuG.Fade(29261, 1);
            osuG.Fade(EndTime, 0);
            osuG.Scale(29261, 0.09);
            osuG.Color(29261, new Color4(0,255,0,255));
            osuG.Additive(29261);
            osuG.Scale((OsbEasing)7, 29261, 29542, 0.09, 0.15);
            osuG.Scale((OsbEasing)6, 29542, 30011, 0.15, 0);
            
            var osuB = layer.CreateSprite(OsuLogo, OsbOrigin.Centre);
            List<double> curveBlue = CalculateCurve2(330, 240, 10);
            run = 0;
            for(double tempo = 29261; tempo<=30011; tempo+=tick(0,32)){
                osuB.Move(tempo, tempo+tick(0,32), curveBlue[run], curveBlue[run+1], curveBlue[run+2], curveBlue[run+3]);
                run+=2;
            }
            osuB.Fade(29261, 1);
            osuB.Fade(EndTime, 0);
            osuB.Scale(29261, 0.09);
            osuB.Color(29261, new Color4(0,0,255,255));
            osuB.Additive(29261);
            osuB.Scale((OsbEasing)7, 29261, 29542, 0.09, 0.15);
            osuB.Scale((OsbEasing)6, 29542, 30011, 0.15, 0);
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
        List<double> CalculateCurve1(double x, double y, double radius){
            List<double> curve = new List<double>();
            for(double a = 0; a<=180; a++){
                curve.Add(x+radius*Math.Cos((a)*(Math.PI/90)));
                curve.Add(y+radius*Math.Sin((a)*(Math.PI/90)));
            }
            return curve;
        }
        List<double> CalculateCurve2(double x, double y, double radius){
            List<double> curve = new List<double>();
            for(double a = 270; a<=360; a++){
                curve.Add(x+radius*Math.Cos((a)*(Math.PI/90)));
                curve.Add(y+radius*Math.Sin((a)*(Math.PI/90)));
            }
            return curve;
        }
    }
}
