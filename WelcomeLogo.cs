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
    public class WelcomeLogo : StoryboardObjectGenerator
    {
        [Configurable]
        public string LogoPath = "";

        [Configurable]
        public string LogoLightPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        public override void Generate()
        {
            var logo = GetLayer("").CreateSprite(LogoPath, OsbOrigin.Centre);
            var logolight = GetLayer("").CreateSprite(LogoLightPath, OsbOrigin.Centre);

            logo.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            logo.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0.1, 1);

            logolight.Additive(StartTime, EndTime);
            logo.Additive(StartTime, EndTime);
        
            logolight.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            
            logolight.Fade((OsbEasing)3, StartTime, EndTime, 0, 1);

        }
    }
}
