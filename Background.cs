using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using OpenTK;
using OpenTK.Graphics;
using System.Linq;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        public override void Generate()
        {
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);
            
            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = GetLayer("").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            
            
            bg.Scale(StartTime, 480.0f / bitmap.Height);
            bg.Color(StartTime, Color4.Black);
            bg.Fade(StartTime, StartTime, 0, Opacity);
            bg.Fade(EndTime, EndTime + 500, Opacity, 0);
        }
    }
}
