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
    public class Kiai2 : StoryboardObjectGenerator{

        [Configurable]
        public string Trapezio = "sb/trapezio.png";

        [Configurable]
        public string Triangle1 = "sb/triangle-128.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public Color4 Color = Color4.White;

        public override void Generate(){

            var layer = GetLayer("Kiai2");

            var triangleR = layer.CreateSprite(Triangle1, OsbOrigin.Centre);
            var triangleG = layer.CreateSprite(Triangle1, OsbOrigin.Centre);
            var triangleB = layer.CreateSprite(Triangle1, OsbOrigin.Centre);

            triangleR.Fade(85511, 1);
            triangleG.Fade(85511, 1);
            triangleB.Fade(85511, 1);
            triangleR.Fade(111011, 0);
            triangleG.Fade(111011, 0);
            triangleB.Fade(111011, 0);
            triangleR.Additive(111011);
            triangleG.Additive(111011);
            triangleB.Additive(111011);
            triangleR.Scale(85511, 1);
            triangleG.Scale(85511, 1);
            triangleB.Scale(85511, 1);

            triangleR.Color(StartTime, new Color4(255, 0, 0, 255));
            triangleG.Color(StartTime, new Color4(0, 255, 0, 255));
            triangleB.Color(StartTime, new Color4(0, 0, 255, 255));

            triangleR.Move((OsbEasing)5, StartTime, StartTime+tick(0, (double)1/(double)4), new Vector2(320, 170), new Vector2(120, 170));
            triangleG.Move((OsbEasing)8, StartTime, StartTime+tick(0, (double)1/(double)4), new Vector2(320, 170), new Vector2(120, 170));
            triangleB.Move((OsbEasing)11, StartTime, StartTime+tick(0, (double)1/(double)4), new Vector2(320, 170), new Vector2(120, 170));

            int run = 0;
            int trocaX = 0;
            for(double tempo = StartTime+tick(0, (double)1/(double)4); tempo<=EndTime; tempo+=tick(0, (double)1/(double)8)){
                trocaX = (run%2==0)? 200 : -200;
                triangleR.Move((OsbEasing)5, tempo, tempo+tick(0, (double)1/(double)8), new Vector2(320-trocaX, 170), new Vector2(320+trocaX, 170));
                triangleG.Move((OsbEasing)8, tempo, tempo+tick(0, (double)1/(double)8), new Vector2(320-trocaX, 170), new Vector2(320+trocaX, 170));
                triangleB.Move((OsbEasing)11, tempo, tempo+tick(0, (double)1/(double)8), new Vector2(320-trocaX, 170), new Vector2(320+trocaX, 170));
                run+=1;
            }
            for(double tempo = StartTime; tempo<=EndTime; tempo+=tick(0, 1)){
                triangleR.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 1.2, 1);
                triangleG.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 1.2, 1);
                triangleB.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 1.2, 1);
            }

            float Y_trapezio = 0;
            for(float x = 0; x<= 11; x++){
                if((new [] {2,6,7}).Contains((int)x)) continue; // 0,1,3,4,5,8,9
                var trapezio = layer.CreateSprite(Trapezio, OsbOrigin.Centre);

                Y_trapezio = 255.0f+(x*15.9f);
                trapezio.Move((OsbEasing)7, 75011, 84011, new Vector2(320, Y_trapezio+250), new Vector2(320, Y_trapezio));
                trapezio.Scale(75011, 0.4+(x*0.047f));
                trapezio.Fade(75011, 1);
                trapezio.Fade(86261, 0);

                if((int)x==0 || (int)x==1){
                    trapezio.Color((OsbEasing)12, 75011, 76886, Color, Color4.White);
                    trapezio.Color((OsbEasing)12, 77261, 77636, Color, Color4.White);

                    trapezio.Color((OsbEasing)12, 78011, 78574, Color, Color4.White);
                    trapezio.Color((OsbEasing)12, 80824, 81199, Color, Color4.White);
                }
                if((int)x==3 || (int)x==4 || (int)x==5){
                    trapezio.Color(75011, Color4.White);
                    trapezio.Color((OsbEasing)12, 78574, 79136, Color, Color4.White);
                    trapezio.Color((OsbEasing)12, 80636, 81011, Color, Color4.White);

                    trapezio.Color((OsbEasing)12, 83636, 84011, Color, Color4.White);
   
                }
                if((int)x>=8){
                    trapezio.Color(75011, Color4.White);
                    trapezio.Color((OsbEasing)12, 79136, 80449, Color, Color4.White);
                    trapezio.Color((OsbEasing)12, 81011, 82886, Color, Color4.White);
                    trapezio.Color((OsbEasing)12, 84011, 85886, Color, Color4.White);
   
                }
            }

            for(float x = 0; x<= 11; x++){
                if((new [] {2,6,7}).Contains((int)x)) continue; // 0,1,3,4,5,8,9
                var trapezioR = layer.CreateSprite(Trapezio, OsbOrigin.Centre);
                var trapezioG = layer.CreateSprite(Trapezio, OsbOrigin.Centre);
                var trapezioB = layer.CreateSprite(Trapezio, OsbOrigin.Centre);

                Y_trapezio = 255.0f+(x*15.9f);

                trapezioR.Move(StartTime-tick(0,1)-tick(0,2), new Vector2(320, Y_trapezio));
                trapezioG.Move(StartTime-tick(0,1)-tick(0,2), new Vector2(320, Y_trapezio));
                trapezioB.Move(StartTime-tick(0,1)-tick(0,2), new Vector2(320, Y_trapezio));

                trapezioR.Move((OsbEasing)Random(2,8), StartTime-tick(0,1)-tick(0,2), StartTime-tick(0,1), new Vector2(320, Y_trapezio), new Vector2(320+Random(-50,50), Y_trapezio));
                trapezioG.Move((OsbEasing)Random(2,8), StartTime-tick(0,1)-tick(0,2), StartTime-tick(0,1), new Vector2(320, Y_trapezio), new Vector2(320+Random(-50,50), Y_trapezio));
                trapezioB.Move((OsbEasing)Random(2,8), StartTime-tick(0,1)-tick(0,2), StartTime-tick(0,1), new Vector2(320, Y_trapezio), new Vector2(320+Random(-50,50), Y_trapezio));

                var bug_glitch = ((new [] {3,4,5}).Contains((int)x))? 80 : -80;
                trapezioR.Move((OsbEasing)Random(2,8), StartTime-tick(0,1), StartTime-tick(0,2), new Vector2(320+Random(-50,50)+bug_glitch, Y_trapezio), new Vector2(320+Random(-50,50)+bug_glitch, Y_trapezio));
                trapezioG.Move((OsbEasing)Random(2,8), StartTime-tick(0,1), StartTime-tick(0,2), new Vector2(320+Random(-50,50)+bug_glitch, Y_trapezio), new Vector2(320+Random(-50,50)+bug_glitch, Y_trapezio));
                trapezioB.Move((OsbEasing)Random(2,8), StartTime-tick(0,1), StartTime-tick(0,2), new Vector2(320+Random(-50,50)+bug_glitch, Y_trapezio), new Vector2(320+Random(-50,50)+bug_glitch, Y_trapezio));


                trapezioR.Move((OsbEasing)Random(2,14), StartTime, StartTime+tick(0, (double)1/(double)4), new Vector2(320+Random(-50,50), Y_trapezio), new Vector2(120+Random(-50,50), Y_trapezio));
                trapezioG.Move((OsbEasing)Random(2,14), StartTime, StartTime+tick(0, (double)1/(double)4), new Vector2(320+Random(-50,50), Y_trapezio), new Vector2(120+Random(-50,50), Y_trapezio));
                trapezioB.Move((OsbEasing)Random(2,14), StartTime, StartTime+tick(0, (double)1/(double)4), new Vector2(320+Random(-50,50), Y_trapezio), new Vector2(120+Random(-50,50), Y_trapezio));

                trapezioR.Move((OsbEasing)Random(2,8), StartTime-tick(0,2), StartTime, new Vector2(320+Random(-50,50)-bug_glitch, Y_trapezio), new Vector2(320-Random(-50,50)-bug_glitch, Y_trapezio));
                trapezioG.Move((OsbEasing)Random(2,8), StartTime-tick(0,2), StartTime, new Vector2(320+Random(-50,50)-bug_glitch, Y_trapezio), new Vector2(320-Random(-50,50)-bug_glitch, Y_trapezio));
                trapezioB.Move((OsbEasing)Random(2,8), StartTime-tick(0,2), StartTime, new Vector2(320+Random(-50,50)-bug_glitch, Y_trapezio), new Vector2(320-Random(-50,50)-bug_glitch, Y_trapezio));

                trapezioR.Scale((OsbEasing)Random(2,8), StartTime-tick(0,1), StartTime-tick(0,2), Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                trapezioG.Scale((OsbEasing)Random(2,8), StartTime-tick(0,1), StartTime-tick(0,2), Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                trapezioB.Scale((OsbEasing)Random(2,8), StartTime-tick(0,1), StartTime-tick(0,2), Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));

                trapezioR.Scale((OsbEasing)Random(2,8), StartTime-tick(0,2), StartTime, Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                trapezioG.Scale((OsbEasing)Random(2,8), StartTime-tick(0,2), StartTime, Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                trapezioB.Scale((OsbEasing)Random(2,8), StartTime-tick(0,2), StartTime, Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));

                trapezioR.Additive(86261);
                trapezioG.Additive(86261);
                trapezioB.Additive(86261);
                trapezioR.Fade(86261, 1);
                trapezioG.Fade(86261, 1);
                trapezioB.Fade(86261, 1);
                trapezioR.Fade(EndTime, 0);
                trapezioG.Fade(EndTime, 0);
                trapezioB.Fade(EndTime, 0);
                trapezioR.Scale(86261, 0.4+(x*0.047f));
                trapezioG.Scale(86261, 0.4+(x*0.047f));
                trapezioB.Scale(86261, 0.4+(x*0.047f));
                trapezioR.Color(86261, new Color4(255, 0, 0, 255));
                trapezioG.Color(86261, new Color4(0, 255, 0, 255));
                trapezioB.Color(86261, new Color4(0, 0, 255, 255));
                for(double tempo = StartTime; tempo<=EndTime; tempo+=tick(0, 1)){
                    trapezioR.Scale((OsbEasing)7, tempo, tempo+tick(0,1), Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                    trapezioG.Scale((OsbEasing)7, tempo, tempo+tick(0,1), Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                    trapezioB.Scale((OsbEasing)7, tempo, tempo+tick(0,1), Random(0.45f, 0.7f)+(x*0.047f), 0.4+(x*0.047f));
                }
                
                for(double tempo = StartTime+tick(0, (double)1/(double)4); tempo<EndTime; tempo+=tick(0, (double)1/(double)8)){
                    trocaX = (run%2==0)? 200 : -200;
                    trapezioR.Move((OsbEasing)Random(2,14), tempo, tempo+tick(0, (double)1/(double)4), new Vector2(320-trocaX, Y_trapezio), new Vector2(320, Y_trapezio));
                    trapezioG.Move((OsbEasing)Random(2,14), tempo, tempo+tick(0, (double)1/(double)4), new Vector2(320-trocaX, Y_trapezio), new Vector2(320, Y_trapezio));
                    trapezioB.Move((OsbEasing)Random(2,14), tempo, tempo+tick(0, (double)1/(double)4), new Vector2(320-trocaX, Y_trapezio), new Vector2(320, Y_trapezio));
                    trapezioR.Move((OsbEasing)Random(2,14), tempo+tick(0, (double)1/(double)4), tempo+tick(0, (double)1/(double)8), new Vector2(320, Y_trapezio), new Vector2(320+trocaX, Y_trapezio));
                    trapezioG.Move((OsbEasing)Random(2,14), tempo+tick(0, (double)1/(double)4), tempo+tick(0, (double)1/(double)8), new Vector2(320, Y_trapezio), new Vector2(320+trocaX, Y_trapezio));
                    trapezioB.Move((OsbEasing)Random(2,14), tempo+tick(0, (double)1/(double)4), tempo+tick(0, (double)1/(double)8), new Vector2(320, Y_trapezio), new Vector2(320+trocaX, Y_trapezio));
                    run+=1;
                }
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
