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
    public class RazerLazer2 : StoryboardObjectGenerator{

        [Configurable]
        public string Triangle1 = "sb/triangle-128.png";

        [Configurable]
        public string Triangle2 = "sb/triangulo128.png";

        [Configurable]
        public string OsuTriangle = "sb/osu-logo-triangulo.png";

        [Configurable]
        public int triangleCount = 16;

        [Configurable]
        public Color4 Color = Color4.White;

        [Configurable]
        public float ColorVariance = 0.6f;

        public override void Generate(){

            var layer = GetLayer("RazerLazer");

            int random_angle = 0;
            double head_x = 0;
            double head_y = 0;
            double tail_x = 0;
            double tail_y = 0;

            List<int> run_rhythm = new List<int>(new int[] {
                87011, 87574, 88511, 89074, 90199, 90386, 90761, 91136, 91324, 92074, 93011, 93574, 94511, 95074, 96011, 96574, 97324
            } );

            var actual_x = 320;
            for(int run = 0; run < run_rhythm.Count; run++){
                if(run%2==0){
                    actual_x = 320;
                    if(Random(0,2)%2==0){
                        random_angle = (run_rhythm[run]%2==0) ? Random(0, 15) : Random(165, 180);
                    }else{
                        random_angle = (run_rhythm[run]%2==0) ? Random(345, 360) : Random(180, 195);
                    }
                }else{
                    actual_x = Random(320-250, 320+250);
                    if(Random(0,2)%2==0){
                        random_angle = (run_rhythm[run]%2==0) ? Random(75, 90) : Random(270, 285);
                    }else{
                        random_angle = (run_rhythm[run]%2==0) ? Random(90, 105) : Random(255, 270);
                    }
                }
            
                head_x = actual_x + 430*Math.Cos(MathHelper.DegreesToRadians(random_angle));
                int random_head_y = Random(240-150, 240+150);
                head_y = random_head_y + 430*Math.Sin(MathHelper.DegreesToRadians(random_angle));
                tail_x = 2*actual_x - head_x;
                tail_y = 2*random_head_y - head_y;
                                

                int randomTriangle = 0;
                double randomFade = 0;
                
                for(int x = 1; x<=35; x++){
                    var mini_triangle = layer.CreateSprite(Triangle1, OsbOrigin.Centre);
                    if(randomTriangle%2==0){
                        mini_triangle = layer.CreateSprite(Triangle1, OsbOrigin.Centre);
                    }else{
                        mini_triangle = layer.CreateSprite(Triangle2, OsbOrigin.Centre);
                    }
                    
                    var mini_triangle_position = new Vector2((float)(head_x-((head_x-tail_x)/35)*x), (float)(head_y-((head_y-tail_y)/35)*x));
                    //mini_triangle.FlipH(brush_start, EndTime);
                    mini_triangle.Rotate(run_rhythm[run], MathHelper.DegreesToRadians(Random(0,120)));
                    if(actual_x==320){
                         mini_triangle.Move((OsbEasing)7, run_rhythm[run]+(x*9.7), run_rhythm[run]+(x*9.7)+tick(0,1), mini_triangle_position, new Vector2(mini_triangle_position.X, mini_triangle_position.Y+ Random(-60,60)));
                    }else{
                         mini_triangle.Move((OsbEasing)7, run_rhythm[run]+(x*9.7), run_rhythm[run]+(x*9.7)+tick(0,1), mini_triangle_position, new Vector2(mini_triangle_position.X+ Random(-60,60), mini_triangle_position.Y));
                    }
                    mini_triangle.Fade(run_rhythm[run], 0);
                    randomFade = Random(0.6, 1);
                    mini_triangle.Fade(run_rhythm[run]+(x*9.7), randomFade);
                    if(run_rhythm[run] != 85324){
                        mini_triangle.Scale((OsbEasing)6, run_rhythm[run]+tick(0, (double)1/(double)1), run_rhythm[run]+(x*9.7)+tick(0, (double)1/(double)1.5), 0.3, 0);
                    }else{
                        mini_triangle.Scale((OsbEasing)6, run_rhythm[run]+tick(0, (double)1/(double)1), 86636, 0.3, 0);
                    }
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
                    mini_triangle.Color(run_rhythm[run], color2);
                    mini_triangle.Scale(run_rhythm[run], 0.3);
                    mini_triangle.Additive(run_rhythm[run]);
                    randomTriangle+=1;
                }
            
            }            
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
