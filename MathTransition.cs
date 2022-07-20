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
        public string Triangle = "sb/triangle-128.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public string Flash = "sb/flash.png";
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
            osuR.Color(29261, new Color4(255,0,0,255));
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




            var triangleR = layer.CreateSprite(Triangle, OsbOrigin.Centre, osuRlocation1);
            triangleR.Fade(30011, 1);
            triangleR.Fade(31042, 0);
            triangleR.Color(30011, new Color4(235,0,0,255));
            triangleR.Additive(30011);
            triangleR.Scale((OsbEasing)7, 30199, 30480, 0, 1);
            triangleR.Move((OsbEasing)7, 30480, 30667, 320, 240, 320, 185);
            triangleR.Color((OsbEasing)7, 30761, 31042, new Color4(255,0,0,255), new Color4(255,255,255,255));

            var triangleRR = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleRR.Fade(31042, 1);
            triangleRR.Fade(31699, 0);
            triangleRR.Color(31042, new Color4(255,0,0,255));
            triangleRR.Additive(31042);
            triangleRR.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleRR.Move((OsbEasing)7, 31042, 31324, 320, 185, 320, 157);

            var triangleRG = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleRG.Fade(31042, 1);
            triangleRG.Fade(31699, 0);
            triangleRG.Color(31042, new Color4(0,255,0,255));
            triangleRG.Additive(31042);
            triangleRG.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleRG.Move((OsbEasing)7, 31042, 31324, 320, 185, 351.2, 212);

            var triangleRB = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleRB.Fade(31042, 1);
            triangleRB.Fade(31699, 0);
            triangleRB.Color(31042, new Color4(0,0,255,255));
            triangleRB.Additive(31042);
            triangleRB.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleRB.Move((OsbEasing)7, 31042, 31324, 320, 185, 287.8, 212);




            var triangleB = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            run = 84;
            for(double tempo = 30011; tempo<=30386; tempo+=tick(0,32)){
                Log($"{run}");
                triangleB.Move(tempo, tempo+tick(0,32), curveBlue[run], curveBlue[run-1], curveBlue[run-2], curveBlue[run-3]);
                run-=2;
            }
            triangleB.Fade(30011, 1);
            triangleB.Fade(31042, 0);
            triangleB.Color(30011, new Color4(0,0,255,255));
            triangleB.Additive(30011);
            triangleB.Scale((OsbEasing)7, 30199, 30480, 0, 1);
            triangleB.Move((OsbEasing)7, 30480, 30667, 320, 240, 383, 294);
            triangleB.Color((OsbEasing)7, 30761, 31042, new Color4(0,0,255,255), new Color4(255,255,255,255));

            var triangleBR = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleBR.Fade(31042, 1);
            triangleBR.Fade(31699, 0);
            triangleBR.Color(31042, new Color4(255,0,0,255));
            triangleBR.Additive(31042);
            triangleBR.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleBR.Move((OsbEasing)7, 31042, 31324, 383, 294, 320+63, 157+109);

            var triangleBG = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleBG.Fade(31042, 1);
            triangleBG.Fade(31699, 0);
            triangleBG.Color(31042, new Color4(0,255,0,255));
            triangleBG.Additive(31042);
            triangleBG.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleBG.Move((OsbEasing)7, 31042, 31324, 383, 294, 351.2+63, 212+109);

            var triangleBB = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleBB.Fade(31042, 1);
            triangleBB.Fade(31699, 0);
            triangleBB.Color(31042, new Color4(0,0,255,255));
            triangleBB.Additive(31042);
            triangleBB.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleBB.Move((OsbEasing)7, 31042, 31324, 383, 294, 287.8+63, 212+109);


            var triangleG = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            run = 84;
            for(double tempo = 30011; tempo<=30386; tempo+=tick(0,32)){
                triangleG.Move(tempo, tempo+tick(0,32), curveGreen[run], curveGreen[run-1], curveGreen[run-2], curveGreen[run-3]);
                run-=2;
            }
            triangleG.Fade(30011, 1);
            triangleG.Fade(31042, 0);
            triangleG.Color(30011, new Color4(0,255,0,255));
            triangleG.Additive(30011);
            triangleG.Scale((OsbEasing)7, 30199, 30480, 0, 1);
            triangleG.Move((OsbEasing)7, 30480, 30667, 320, 240, 257, 294);
            triangleG.Color((OsbEasing)7, 30761, 31042, new Color4(0,255,0,255), new Color4(255,255,255,255));

            var triangleGR = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleGR.Fade(31042, 1);
            triangleGR.Fade(31699, 0);
            triangleGR.Color(31042, new Color4(255,0,0,255));
            triangleGR.Additive(31042);
            triangleGR.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleGR.Move((OsbEasing)7, 31042, 31324, 257, 294, 320-63, 157+109);

            var triangleGG = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleGG.Fade(31042, 1);
            triangleGG.Fade(31699, 0);
            triangleGG.Color(31042, new Color4(0,255,0,255));
            triangleGG.Additive(31042);
            triangleGG.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleGG.Move((OsbEasing)7, 31042, 31324, 257, 294, 351.2-63, 212+109);

            var triangleGB = layer.CreateSprite(Triangle, OsbOrigin.Centre);
            triangleGB.Fade(31042, 1);
            triangleGB.Fade(31699, 0);
            triangleGB.Color(31042, new Color4(0,0,255,255));
            triangleGB.Additive(31042);
            triangleGB.Scale((OsbEasing)7, 31042, 31324, 1, 0.5);
            triangleGB.Move((OsbEasing)7, 31042, 31324, 257, 294, 287.8-63, 212+109);
            
            triangleRR.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleRG.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleRB.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleGR.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleGG.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleGB.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleBR.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleBG.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);
            triangleBB.Scale((OsbEasing)7, 31324, 31699, 0.5, 0);

            
            int runY = 0;
            
            for(int y = 50; y<= 480; y+=55){
                int runX = 0;
                for(int x = -162; x<= 740; x+=32){     
                    
                    var triangulos = layer.CreateSprite(Triangle, OsbOrigin.Centre, new Vector2(x, y));
                    if(runY%2==0){
                        if(runX%2!=0){
                            triangulos = layer.CreateSprite(Triangle, OsbOrigin.Centre, new Vector2(x+32, y-5));
                            triangulos.Rotate(31511, MathHelper.DegreesToRadians(180));
                        }else{
                            triangulos = layer.CreateSprite(Triangle, OsbOrigin.Centre, new Vector2(x+32, y));
                        }
                    }else{
                        if(runX%2!=0){
                            triangulos = layer.CreateSprite(Triangle, OsbOrigin.Centre, new Vector2(x, y-5));
                            triangulos.Rotate(31511, MathHelper.DegreesToRadians(180));
                        }
                    }
                    
                    
                    triangulos.Color(31511, new Color4(0,0,0,255));

                    if(((new [] {13,14,15}).Contains(runX) && runY==4) || (runX==15 && runY==5)){
                        triangulos.Fade(31699, 1);
                        triangulos.Scale((OsbEasing)7,31699,31699+tick(0,1.5), 0, 0.3);
                        if(runX==14 && runY==4){
                            triangulos.Color(31699, new Color4(255,255,255,255));
                            triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                            triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));

                        }else{
                            triangulos.Color(31699, new Color4(255,130,180,255));
                            triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                            triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                            triangulos.Color(33761, new Color4(255,255,255,255));
    
                            triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                        }
                    }else if(((new [] {14,15,16}).Contains(runX) && runY==3) || (runX==14 && runY==2)){
                        triangulos.Fade(31980, 1);
                        triangulos.Scale((OsbEasing)7,31980,31980+tick(0,1.5), 0, 0.3);
                        if(runX==15 && runY==3){
                            triangulos.Color(31980, new Color4(255,130,180,255));
                            triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                            triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                            triangulos.Color(33761, new Color4(255,255,255,255));
    
                            triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                        }else{   
                            triangulos.Color(31980, new Color4(255,255,255,255));
                            triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                            triangulos.Color(34042, new Color4(255, 130, 180, 255));
                            triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                            triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                        }
                    }else if(((new [] {16,17,18}).Contains(runX) && runY==5) || (runX==16 && runY==4)){
                        triangulos.Fade(31980, 1);
                        triangulos.Scale((OsbEasing)7,31980,31980+tick(0,1.5), 0, 0.3);
                        if(runX==17 && runY==5){
                            triangulos.Color(31980, new Color4(255,130,180,255));
                            triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                            triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                            triangulos.Color(33761, new Color4(255,255,255,255));
    
                            triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                        }else{   
                            triangulos.Color(31980, new Color4(255,255,255,255));
                            triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                            triangulos.Color(34042, new Color4(255, 130, 180, 255));
                            triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                            triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                        }
                    }else if(((new [] {12,13,14}).Contains(runX) && runY==5) || (runX==12 && runY==4)){
                        triangulos.Fade(31980, 1);
                        triangulos.Scale((OsbEasing)7,31980,31980+tick(0,1.5), 0, 0.3);
                        if(runX==13 && runY==5){
                            triangulos.Color(31980, new Color4(255,130,180,255));
                            triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                            triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                            triangulos.Color(33761, new Color4(255,255,255,255));
    
                            triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                        }else{   
                            triangulos.Color(31980, new Color4(255,255,255,255));
                            triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                            triangulos.Color(34042, new Color4(255, 130, 180, 255));
                            triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                            triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                        }
                    }else if(((new [] {8,9,10,11,12,13,14,15,16,17,18,19,20}).Contains(runX) && runY==6)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {10,11,19,20}).Contains(runX) && runY==5)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {10,11,17,18}).Contains(runX) && runY==4)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {12,13,17,18}).Contains(runX) && runY==3)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {12,13,15,16}).Contains(runX) && runY==2)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {14,15,16}).Contains(runX) && runY==1)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {14}).Contains(runX) && runY==0)){
                        triangulos.Fade(32261, 1);
                        triangulos.Scale((OsbEasing)7,32261,32261+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32261, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {5,23}).Contains(runX) && runY==0)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,255,255,255));
                        triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                        triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                    }else if(((new [] {3,4,6,7,21,22,24,25}).Contains(runX) && runY==0)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {5,6,7,23,24,25}).Contains(runX) && runY==1)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {5,23}).Contains(runX) && runY==2)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {1,2,8,9,19,20,26,27}).Contains(runX) && runY==0)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,255,255,255));
                        triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                        triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                    }else if(((new [] {3,4,8,9,21,22,26,27}).Contains(runX) && runY==1)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,255,255,255));
                        triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                        triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                    }else if(((new [] {3,4,6,7,21,22,24,25}).Contains(runX) && runY==2)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,255,255,255));
                        triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                        triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                    }else if(((new [] {5,6,7,23,24,25}).Contains(runX) && runY==3)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,255,255,255));
                        triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                        triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                    }else if(((new [] {5,23}).Contains(runX) && runY==4)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,255,255,255));
                        triangulos.Scale((OsbEasing)7, 33480, 33761, 0.3, 0);
                        triangulos.Color(34042, new Color4(255, 130, 180, 255));
                        triangulos.Scale((OsbEasing)7, 34042, 34324, 0, 0.3);
                        triangulos.Color((OsbEasing)6, 34511, 36386, new Color4(255, 130, 180, 255), new Color4(255, 255, 255, 255));
                    }else if(((new [] {10,11,17,18}).Contains(runX) && runY==0)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {2,10,11,19,20,28}).Contains(runX) && runY==1)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {1,2,8,9,19,20,26,27}).Contains(runX) && runY==2)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {3,4,8,9,21,22,26,27}).Contains(runX) && runY==3)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {3,4,6,7,21,22,24,25}).Contains(runX) && runY==4)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {5,6,7,23,24,25}).Contains(runX) && runY==5)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else if(((new [] {5,23}).Contains(runX) && runY==6)){
                        triangulos.Fade(32542, 1);
                        triangulos.Scale((OsbEasing)7,32542,32542+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32542, new Color4(255,130,180,255));
                        triangulos.Scale((OsbEasing)7,33199, 33480, 0.3, 0);
                        triangulos.Scale((OsbEasing)7, 33761, 34042, 0,0.3);
                        triangulos.Color(33761, new Color4(255,255,255,255));
                        triangulos.Color(34324, 34511, new Color4(255,255,255,255), new Color4(0,0,0,255));
                    }else{
                        triangulos.Fade(32824, 1);
                        triangulos.Scale((OsbEasing)7,32824,32824+tick(0,1.5), 0, 0.3);
                        triangulos.Color(32824, new Color4(0,0,0,255));
                        var colorRandom1 = Random(0.0f,1.0f);
                        var colorRandom2 = Random(0.0f,1.0f);
                        triangulos.Color(34511, 35261, new Color4(0,0,0,255), new Color4(colorRandom1,colorRandom1,colorRandom1,255));
                        triangulos.Color(35261, 36386, new Color4(colorRandom1,colorRandom1,colorRandom1,255), new Color4(colorRandom2,colorRandom2,colorRandom2,255));
                    }

                    triangulos.Fade(36386, 0);
                
                    runX+=1;
                }
                runY+=1;
            }
            var flashBG = layer.CreateSprite(Flash, OsbOrigin.Centre);
            flashBG.Color(36011, new Color4(0,0,0,255));
            flashBG.ScaleVec((OsbEasing)6, 35824, 36386, 0, 60, 60, 60);
            
            
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
