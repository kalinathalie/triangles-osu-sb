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
    public class AspiraFinal : StoryboardObjectGenerator
    {

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int TrocaStartTime = 0;

        [Configurable]
        public string Triangle1 = "sb/triangle-128.png";

        [Configurable]
        public string Triangle2 = "sb/triangulo128.png";


        [Configurable]
        public float START_SCALE = 0.88f;

        [Configurable]
        public float SCALE_AFTER_X = 0.88f;


        [Configurable]
        public float SCALE_BEFORE_X = 0.88f;

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
        public override void Generate(){


        var Layer = GetLayer("AspiraFinal");

        var LogoCircle = Layer.CreateSprite(LogoCirclePath, OsbOrigin.Centre); 
        var LogoText = Layer.CreateSprite(LogoTextPath, OsbOrigin.Centre); 
        var LogoTriangle = Layer.CreateSprite(LogoTrianglePath, OsbOrigin.Centre, new Vector2(320, 200));


        LogoCircle.Fade(StartTime, 1);
        LogoText.Fade(StartTime, 1);

        double BeatTime = tick(StartTime, 1);

        for (double i = StartTime; i < EndTime; i += BeatTime) {
                if (i == TrocaStartTime)
                    LogoText.Scale(OsbEasing.InOutExpo, i, i + (BeatTime *3), 0.25f, 0.15f);
                else
                    if (TrocaStartTime > i){ 
                        LogoText.Scale(OsbEasing.In, i, i + BeatTime, 0.19f, 0.18f);
                    }
            
        }


            LogoCircle.Scale(StartTime, START_SCALE);
            LogoText.Scale(StartTime, START_SCALE);

            LogoCircle.MoveY(OsbEasing.OutSine, StartTime, TrocaStartTime, -100, 240);
            LogoText.Move(OsbEasing.OutSine, StartTime, TrocaStartTime, new Vector2(320, -100),new Vector2(320, 240));

            LogoCircle.Scale(OsbEasing.None, TrocaStartTime, TrocaStartTime + 300, START_SCALE, START_SCALE*1.3);
            LogoCircle.Fade(OsbEasing.InOutExpo, TrocaStartTime, TrocaStartTime + 500, 1, 0);
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
