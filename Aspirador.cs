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
    public class Aspirador : StoryboardObjectGenerator
    {

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string Triangle1 = "sb/triangle-128.png";

        [Configurable]
        public string Triangle2 = "sb/triangulo128.png";

        [Configurable]
        public int ParticleCount = 16;

        [Configurable]
        public Color4 Color = Color4.White;

        [Configurable]
        public float ColorVariance = 0.6f;
        public override void Generate(){

            var layer = GetLayer("Aspirador");

            int run = 0;
            List<int> positionAspira = new List<int>(new int[] {-15, 80,
                                130, 190,
                                -15, 300,
                                130, 406,
                                655, 80,
                                510, 190,
                                655, 300,
                                510, 406} );
		    for(double tempo = StartTime; tempo<=EndTime; tempo+=tick(0,1)){
                //Log($"sb/Hexacons/{run+1}");
                var hexagon = layer.CreateSprite($"sb/Hexacons/{(run%24)+1}.png", OsbOrigin.Centre);
                hexagon.Move((OsbEasing)9, tempo, tempo+tick(0,(double)1/(double)2), positionAspira[(run%8)*2], positionAspira[((run%8)*2) + 1], 320, 240);
                hexagon.Fade(tempo, 1);
                hexagon.Fade(tempo+tick(0,(double)1/(double)2), 0);
                hexagon.Scale((OsbEasing)7, tempo, tempo+tick(0,2) , 0, 1);
                run+=1;
            }
            List<double> triangulosLista = new List<double>(new double[] {3011, 5261, 5636, 6011, 6574, 7136, 8636, 8824, 9011, 11636, 11824, 12011});
            List<int> triangulosAngulos = new List<int>(new int[] {180, 0, 90, 180, 180, 270, 0, 180, 90, 0, 180, -90});
            List<int> triangulosPosition = new List<int>(new int[] {
                                70, 240,
                                70, 390,
                                70, 90,
                                570, 90,
                                570, 240,
                                570, 390,
                                70, 390,
                                70, 90,
                                70, 240,
                                570, 390,
                                570, 90,
                                570, 240} );
            run = 0;
            foreach(double tempo in triangulosLista){
                var trianguloA = layer.CreateSprite(Triangle2, OsbOrigin.Centre, new Vector2(triangulosPosition[run], triangulosPosition[(run)+1]));
                trianguloA.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 0, 1);
                trianguloA.Fade((OsbEasing)12, tempo+tick(0,1), tempo+tick(0,(double)1/(double)4), 1, 0);
                trianguloA.Rotate(tempo, MathHelper.DegreesToRadians(triangulosAngulos[run/2]));
                trianguloA.Color(tempo, new Color4(235,120,170,255));
                var trianguloB = layer.CreateSprite(Triangle2, OsbOrigin.Centre, new Vector2(triangulosPosition[run], triangulosPosition[(run)+1]));
                trianguloB.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 0, 0.75);
                trianguloB.Fade((OsbEasing)12, tempo+tick(0,1), tempo+tick(0,(double)1/(double)4), 1, 0);
                trianguloB.Rotate(tempo, MathHelper.DegreesToRadians(triangulosAngulos[run/2]));
                trianguloB.Color(tempo, new Color4(235,120,170,255));
                var trianguloC = layer.CreateSprite(Triangle2, OsbOrigin.Centre, new Vector2(triangulosPosition[run], triangulosPosition[(run)+1]));
                trianguloC.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 0, 0.5);
                trianguloC.Fade((OsbEasing)12, tempo+tick(0,1), tempo+tick(0,(double)1/(double)4), 1, 0);
                trianguloC.Rotate(tempo, MathHelper.DegreesToRadians(triangulosAngulos[run/2]));
                trianguloC.Color(tempo, new Color4(235,120,170,255));

                for (var i = 0; i < ParticleCount; i++){

                    var color = Color;
                    if (ColorVariance > 0){
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
                    changeTriangle.Add(Triangle2);
                    changeTriangle.Add(Triangle1);
                    var particle = layer.CreateSprite(changeTriangle[i%2], OsbOrigin.Centre);
                    if (color.R != 1 || color.G != 1 || color.B != 1)
                        particle.Color(tempo, color);
                    particle.Scale(tempo, Random(0.2,0.4));

                    var newPositionParticle = new Vector2(triangulosPosition[run]+Random(-100,100), triangulosPosition[run+1]+Random(-100,100));
                    particle.Fade(tempo, 0.7);
                    particle.Move((OsbEasing)7, tempo, tempo+tick(0,(double)1/(double)2), new Vector2(triangulosPosition[run], triangulosPosition[run+1]), newPositionParticle);
                    particle.Move((OsbEasing)9, tempo+tick(0,(double)1/(double)2), tempo+tick(0,(double)1/(double)5), newPositionParticle,  new Vector2(320, 240));
                    particle.Fade(tempo+tick(0,(double)1/(double)2), tempo+tick(0,(double)1/(double)5), 0.7, 0.7);
                }

                run+=2;
            }
            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
