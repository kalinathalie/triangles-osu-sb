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
            var logolight = GetLayer("").CreateSprite(LogoPath, OsbOrigin.Centre);
            var logolight2 = GetLayer("").CreateSprite(LogoPath, OsbOrigin.Centre);
            var logolight3 = GetLayer("").CreateSprite(LogoPath, OsbOrigin.Centre);
            var logolight4 = GetLayer("").CreateSprite(LogoPath, OsbOrigin.Centre);
            var logolight5 = GetLayer("").CreateSprite(LogoPath, OsbOrigin.Centre);

            logo.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            logo.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0.1, 1);

            logo.Color(StartTime, Color4.Gray);
            logolight.Color(StartTime, Color4.White);
            logolight2.Color(StartTime, Color4.White);
            logolight3.Color(StartTime, Color4.White);
            logolight4.Color(StartTime, Color4.White);
            logolight5.Color(StartTime, Color4.White);

            logolight.Additive(StartTime, EndTime);
            logolight2.Additive(StartTime, EndTime);
            logolight3.Additive(StartTime, EndTime);
            logolight4.Additive(StartTime, EndTime);
            logolight5.Additive(StartTime, EndTime);
            logo.Additive(StartTime, EndTime);
        
            logolight.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            logolight2.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            logolight3.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            logolight4.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            logolight5.Scale(OsbEasing.InQuart, StartTime, EndTime, 0.8, 0);
            
            logolight.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0, 1);
            logolight2.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0, 1);
            logolight3.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0, 1);
            logolight4.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0, 1);
            logolight5.Fade(OsbEasing.OutCubic, StartTime, EndTime, 0, 1);

        }
    }
}
