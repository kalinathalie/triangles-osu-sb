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
        public string Background = "sb/bg1.png";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string Line = "sb/dot3.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public Color4 Color = Color4.White;

        public override void Generate(){

            var layer = GetLayer("Kiai2");

            var bg = layer.CreateSprite(Background, OsbOrigin.Centre);

            var MiddleTime = (StartTime+EndTime)/2;

            bg.Fade(86824, 1);
            bg.Fade(EndTime, 0);
            bg.Scale(StartTime, 0.7);
            bg.Rotate(StartTime, MathHelper.DegreesToRadians(7));
            bg.Rotate(MiddleTime, MathHelper.DegreesToRadians(0));
            bg.Move(StartTime, MiddleTime, 400, 260, 240, 230);
            bg.Move(MiddleTime, 105386, 550, 100, 550, 250);
            bg.Move(105386, EndTime, 150, 380, 150, 230);


            int local_linha = -60;
            for(double tempo = 98261;tempo<MiddleTime;tempo+=tick(0,4)){
                var hSprite2 = layer.CreateSprite(Line, OsbOrigin.Centre);
                hSprite2.MoveX(tempo, local_linha);
                hSprite2.ScaleVec((OsbEasing)7, tempo, tempo+tick(0,4), 0, 854/2, 55, 854/2);
                hSprite2.Fade((OsbEasing)7, tempo, tempo+tick(0,4), 0, 1);
                hSprite2.Fade(MiddleTime, 0);
                hSprite2.Color(tempo, new Color4(0,0,0,255));
                local_linha+=110;
            }

            local_linha = 710;
            for(double tempo = 110261;tempo<EndTime;tempo+=tick(0,4)){
                var hSprite2 = layer.CreateSprite(Line, OsbOrigin.Centre);
                hSprite2.MoveX(tempo, local_linha);
                hSprite2.ScaleVec((OsbEasing)7, tempo, tempo+tick(0,4), 0, 854/2, 55, 854/2);
                hSprite2.Fade((OsbEasing)7, tempo, tempo+tick(0,4), 0, 1);
                hSprite2.Fade(EndTime, 0);
                hSprite2.Color(tempo, new Color4(0,0,0,255));
                local_linha-=110;
            }
            

            var flashBG3 = layer.CreateSprite(Flash, OsbOrigin.Centre);

            flashBG3.Color(86824, new Color4(0,0,0,255));
            flashBG3.Fade(86824, 1);
            flashBG3.ScaleVec((OsbEasing)7, 86824, 87011, 60, 50, 0, 50);

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
            triangleR.Color(99011, new Color4(255, 0, 0, 255));
            triangleG.Color(99011, new Color4(0, 255, 0, 255));
            triangleB.Color(99011, new Color4(0, 0, 255, 255));
            triangleR.Color(105386, new Color4(255, 0, 0, 255));
            triangleG.Color(105386, new Color4(0, 255, 0, 255));
            triangleB.Color(105386, new Color4(0, 0, 255, 255));

            float run_jump = 32;
            bool run_bool = true;
            int run_seno = 0;
            for(double tempo = StartTime; tempo < EndTime; tempo+=tick(0,8)){
                var actual_y = (float)Math.Sin(MathHelper.DegreesToRadians(200/2)+0.04*run_seno)*41;
                var after_y = (float)Math.Sin(MathHelper.DegreesToRadians(200/2)+0.04*run_seno+0.04)*41;
                run_seno+=5;
                if(run_bool){
                    triangleR.Move(tempo, tempo+tick(0,8), new Vector2(120f+(run_jump*6.25f), 130+actual_y), new Vector2(120f+((run_jump-1f)*6.25f), 130+after_y));
                    triangleG.Move(tempo+tick(0,4), tempo+tick(0,8)+tick(0,4), new Vector2(120f+(run_jump*6.25f), 130+actual_y), new Vector2(120f+((run_jump-1f)*6.25f), 130+after_y));
                    triangleB.Move(tempo+tick(0,2), tempo+tick(0,8)+tick(0,2), new Vector2(120f+(run_jump*6.25f), 130+actual_y), new Vector2(120f+((run_jump-1f)*6.25f), 130+after_y));
                    
                    if(run_jump!=1){
                        run_jump-=1;
                    }else{
                        run_bool=false;
                    }       
                }else{
                   
                    triangleR.Move(tempo, tempo+tick(0,16), new Vector2(120f+((run_jump-1f)*6.25f), 130+actual_y), new Vector2(120f+(run_jump*6.25f), 130+after_y));
                    triangleG.Move(tempo+tick(0,4), tempo+tick(0,8)+tick(0,4), new Vector2(120f+((run_jump-1f)*6.25f), 130+actual_y), new Vector2(120f+(run_jump*6.25f), 130+after_y));
                    triangleB.Move(tempo+tick(0,2), tempo+tick(0,8)+tick(0,2), new Vector2(120f+((run_jump-1f)*6.25f), 130+actual_y), new Vector2(120f+(run_jump*6.25f), 130+after_y));
                    if(run_jump!=64){
                        run_jump+=1;
                    }else{
                        run_bool=true;
                    } 
                }
            }

            int run = 0;
            int trocaX = 0;

            triangleR.Additive(98261, 0);
            triangleG.Additive(98261, 0);
            triangleB.Additive(98261, 0);
            triangleR.Additive(110261, 0);
            triangleG.Additive(110261, 0);
            triangleB.Additive(110261, 0);
            triangleR.Additive(99011);
            triangleG.Additive(99011);
            triangleB.Additive(99011);
            
            int run_rotate = 0;
            for(double tempo = StartTime; tempo<EndTime; tempo+=tick(0, 1)){
                bg.Fade((OsbEasing)7,tempo, tempo+tick(0,1), 0.7, 0.55);
                if(tempo==105011){
                    triangleR.Color(tempo, new Color4(255, 130, 170, 255));
                    triangleR.Color(tempo+tick(0,4), new Color4(50, 50, 50, 255));
                    triangleG.Color(tempo+tick(0,4), new Color4(255, 130, 170, 255));
                    triangleG.Color(tempo+tick(0,2), new Color4(50, 50, 50, 255));
                    triangleB.Color(tempo+tick(0,2), new Color4(255, 130, 170, 255));
                    triangleB.Color(tempo+tick(0,2), new Color4(255, 255, 255, 255));  

                    triangleR.Color(104824, new Color4(50, 50, 50, 255));
                    triangleG.Color(104824, new Color4(50, 50, 50, 255));
                    triangleB.Color(104824, new Color4(50, 50, 50, 255));            
                    continue;
                }
                if(tempo==98261 || tempo==98636 || tempo==104824 || tempo==110261 || tempo==110636){
                    triangleR.Color(tempo, new Color4(255, 130, 170, 255));
                    triangleR.Color(tempo+tick(0,4), new Color4(255, 255, 255, 255));
                    triangleG.Color(tempo, new Color4(255, 130, 170, 255));
                    triangleG.Color(tempo+tick(0,4), new Color4(255, 255, 255, 255));
                    triangleB.Color(tempo, new Color4(255, 130, 170, 255));
                    triangleB.Color(tempo+tick(0,4), new Color4(255, 255, 255, 255));
                    continue;
                }
                triangleR.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 1.2, 1);
                triangleG.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 1.2, 1);
                triangleB.Scale((OsbEasing)7, tempo, tempo+tick(0,1), 1.2, 1);

                triangleR.Rotate((OsbEasing)4, tempo, tempo+tick(0,1), MathHelper.DegreesToRadians(run_rotate), MathHelper.DegreesToRadians(run_rotate+30));
                triangleG.Rotate((OsbEasing)4, tempo, tempo+tick(0,1), MathHelper.DegreesToRadians(run_rotate), MathHelper.DegreesToRadians(run_rotate+30));
                triangleB.Rotate((OsbEasing)4, tempo, tempo+tick(0,1), MathHelper.DegreesToRadians(run_rotate), MathHelper.DegreesToRadians(run_rotate+30));
                run_rotate+=45;
            }

            triangleR.Scale((OsbEasing)7, StartTime-tick(0,1), StartTime-tick(0,2), 1.2, 1);
            triangleG.Scale((OsbEasing)7, StartTime-tick(0,1), StartTime-tick(0,2), 1.2, 1);
            triangleB.Scale((OsbEasing)7, StartTime-tick(0,1), StartTime-tick(0,2), 1.2, 1);

            triangleR.Scale((OsbEasing)7, StartTime-tick(0,2), StartTime, 1.2, 1);
            triangleG.Scale((OsbEasing)7, StartTime-tick(0,2), StartTime, 1.2, 1);
            triangleB.Scale((OsbEasing)7, StartTime-tick(0,2), StartTime, 1.2, 1);

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
                trapezioR.Additive(99011);
                trapezioG.Additive(99011);
                trapezioB.Additive(99011);
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
                trapezioR.Color(99011, new Color4(255, 0, 0, 255));
                trapezioG.Color(99011, new Color4(0, 255, 0, 255));
                trapezioB.Color(99011, new Color4(0, 0, 255, 255));
                for(double tempo = StartTime; tempo<=EndTime; tempo+=tick(0, 1)){
                    if(tempo==98261 || tempo==98636 || tempo==110261 || tempo==110636){
                        if(x==0 || x==1){
                            trapezioR.Additive(tempo+tick(0,4), 0);
                            trapezioG.Additive(tempo+tick(0,4), 0);
                            trapezioB.Additive(tempo+tick(0,4), 0);
                            trapezioR.Color(tempo+tick(0,4), new Color4(255, 130, 170, 255));
                            trapezioR.Color(tempo+tick(0,2), new Color4(255, 255, 255, 255));
                            trapezioG.Color(tempo+tick(0,4), new Color4(255, 130, 170, 255));
                            trapezioG.Color(tempo+tick(0,2), new Color4(255, 255, 255, 255));
                            trapezioB.Color(tempo+tick(0,4), new Color4(255, 130, 170, 255));
                            trapezioB.Color(tempo+tick(0,2), new Color4(255, 255, 255, 255));
                        }else if(x==3 || x==4 || x==5){
                            trapezioR.Additive(tempo+tick(0,2), 0);
                            trapezioG.Additive(tempo+tick(0,2), 0);
                            trapezioB.Additive(tempo+tick(0,2), 0);
                            trapezioR.Color(tempo+tick(0,2), new Color4(255, 130, 170, 255));
                            trapezioR.Color(tempo+tick(0,2)+tick(0,4), new Color4(255, 255, 255, 255));
                            trapezioG.Color(tempo+tick(0,2), new Color4(255, 130, 170, 255));
                            trapezioG.Color(tempo+tick(0,2)+tick(0,4), new Color4(255, 255, 255, 255));
                            trapezioB.Color(tempo+tick(0,2), new Color4(255, 130, 170, 255));
                            trapezioB.Color(tempo+tick(0,2)+tick(0,4), new Color4(255, 255, 255, 255));
                        }else{
                            trapezioR.Additive(tempo+tick(0,2)+tick(0,4), 0);
                            trapezioG.Additive(tempo+tick(0,2)+tick(0,4), 0);
                            trapezioB.Additive(tempo+tick(0,2)+tick(0,4), 0);
                            trapezioR.Color(tempo+tick(0,2)+tick(0,4), new Color4(255, 130, 170, 255));
                            trapezioR.Color(tempo+tick(0,1), new Color4(255, 255, 255, 255));
                            trapezioG.Color(tempo+tick(0,2)+tick(0,4), new Color4(255, 130, 170, 255));
                            trapezioG.Color(tempo+tick(0,1), new Color4(255, 255, 255, 255));
                            trapezioB.Color(tempo+tick(0,2)+tick(0,4), new Color4(255, 130, 170, 255));
                            trapezioB.Color(tempo+tick(0,1), new Color4(255, 255, 255, 255));
                        }
                        continue;
                    }
                    if(tempo==105011) continue;
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
