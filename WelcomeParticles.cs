using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StorybrewScripts
{
    public class WelcomeParticles : StoryboardObjectGenerator
    {
        [Configurable]
        public string Path = "sb/particle.png";

        [Configurable]
        public string Path2 = "sb/particle.png";

        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public int ParticleCount = 32;

        [Configurable]
        public Vector2 Scale = new Vector2(1, 1);

        [Configurable]
        public OsbOrigin Origin = OsbOrigin.Centre;

        [Configurable]
        public Color4 Color = Color4.White;

        [Configurable]
        public float ColorVariance = 0.6f;

        [Configurable]
        public bool Additive = false;

        [Configurable]
        public Vector2 SpawnOrigin = new Vector2(420, 0);

        [Configurable]
        public float SpawnSpread = 360;

        [Configurable]
        public float Angle = 110;

        [Configurable]
        public float AngleSpread = 60;

        [Configurable]
        public float Lifetime = 1000;

        [Configurable]
        public OsbEasing Easing = OsbEasing.None;

        public override void Generate()
        {
            if (StartTime == EndTime && Beatmap.HitObjects.FirstOrDefault() != null)
            {
                StartTime = (int)Beatmap.HitObjects.First().StartTime;
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;
            }
            EndTime = Math.Min(EndTime, (int)AudioDuration);
            StartTime = Math.Min(StartTime, EndTime);

            var bitmap = GetMapsetBitmap(Path);

            var duration = (double)(EndTime - StartTime);

            var layer = GetLayer("");
            for (var i = 0; i < ParticleCount; i++)
            {
                var particleStartTime = StartTime+(i*50);
                var spawnAngle = Random(Math.PI * 2);
                var spawnDistance = (float)(SpawnSpread * Math.Sqrt(Random(1f)));

                var moveAngle = MathHelper.DegreesToRadians(Angle + Random(-AngleSpread, AngleSpread) * 0.5f);

                var spriteRotation = moveAngle + MathHelper.DegreesToRadians(0);

                var startPosition = SpawnOrigin + new Vector2((float)Random(-200, 200), (float)Random(-50, 50));
                var endPosition = startPosition;

                var color = Color;
                if (ColorVariance > 0)
                {
                    ColorVariance = MathHelper.Clamp(ColorVariance, 0, 1);

                    var hsba = Color4.ToHsl(color);
                    var sMin = Math.Max(0, hsba.Y - ColorVariance * 0.5f);
                    var sMax = Math.Min(sMin + ColorVariance, 1);
                    var vMin = Math.Max(0, hsba.Z - ColorVariance * 0.5f);
                    var vMax = Math.Min(vMin + ColorVariance, 1);

                    color = Color4.FromHsl(new Vector4(
                        hsba.X,
                        (float)Random(sMin, sMax),
                        (float)Random(vMin, vMax),
                        hsba.W));
                }
                var changeTriangle = new List<string>();
                changeTriangle.Add(Path);
                changeTriangle.Add(Path2);
                var particle = layer.CreateSprite(changeTriangle[i%2], Origin);
                if (color.R != 1 || color.G != 1 || color.B != 1)
                    particle.Color(particleStartTime, color);
                particle.Scale(particleStartTime, Random(0.2,0.4));
                if (Additive)
                    particle.Additive(particleStartTime, EndTime);

                particle.Fade(OsbEasing.Out, particleStartTime, EndTime, 0.8, 0);
                particle.Move(particleStartTime, EndTime, startPosition, endPosition);
                particle.Fade(EndTime, EndTime+1, 0, 0);
            }
        }
    }
}
