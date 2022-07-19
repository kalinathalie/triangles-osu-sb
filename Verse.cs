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
    public class Verse : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public string Flash = "sb/flash.png";

        [Configurable]
        public string VigBG = "sb/vig.png";

        [Configurable]
        public string Head = "sb/head.png";

        [Configurable]
        public string CircleHead = "sb/c2.png";

        [Configurable]
        public string TriangleFinal = "sb/triangle-128.png";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        public override void Generate(){
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            var layer = GetLayer("Verse");

            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = layer.CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 1250.0f / bitmap.Height);
            bg.Fade(StartTime, StartTime, 0, Opacity);
            bg.Fade(EndTime+tick(0,1), 0);

            var MiddleTime = tick(0,(double)1/(double)4)+(StartTime+EndTime)/2;
            bg.Move(StartTime, MiddleTime, 320, 1460, 320, -100);
            bg.Move(MiddleTime, EndTime, 320, -100, 320, 1460);
            bg.Rotate((OsbEasing)6, StartTime, MiddleTime, MathHelper.DegreesToRadians(90), MathHelper.DegreesToRadians(180));
            bg.Rotate((OsbEasing)7, MiddleTime, EndTime, MathHelper.DegreesToRadians(180), MathHelper.DegreesToRadians(270));

            var flashBG = layer.CreateSprite(Flash, OsbOrigin.Centre);
            var Flashbitmap = GetMapsetBitmap(Flash);

            flashBG.Fade(OsbEasing.InCubic, StartTime, StartTime+tick(0,(double)1/(double)2), 0.8, 0);

            flashBG.Fade(EndTime+tick(0,1), 1);
            flashBG.Fade(EndTime+tick(0,(double)1/(double)7), EndTime+tick(0,(double)1/(double)8), 1, 0.2);
            flashBG.Color(EndTime+tick(0,1), new Color4(200,200,200,255));
            flashBG.ScaleVec((OsbEasing)9,EndTime+tick(0,(double)1/(double)6), EndTime+tick(0,(double)1/(double)8), 60, 50, 60, 10);
            flashBG.ScaleVec((OsbEasing)7, 28886, 28886+tick(0,1), 60, 10, 60, 0);
            flashBG.ScaleVec((OsbEasing)19, 29261, 29636, 0, 60, 5, 60);
            flashBG.ScaleVec((OsbEasing)18, 29636, 30011, 5, 60, 60, 60);
            flashBG.Fade((OsbEasing)6, 30011, 33011, 0.2, 0.8);

            for(int x = -98; x<= 740; x+=128){
                for(int y = 20; y<= 480; y+=110){
                    var triangulos = layer.CreateSprite(TriangleFinal, OsbOrigin.Centre, new Vector2(x, y));
                    var triangulos2 = layer.CreateSprite(TriangleFinal, OsbOrigin.Centre, new Vector2(x+64, y));
                    triangulos.Fade(EndTime, 1);
                    triangulos.Scale((OsbEasing)1,EndTime,EndTime+tick(0,1), 0, 1.0);
                    triangulos.Color(EndTime, new Color4(255,255,255,255));

                    triangulos.Scale(EndTime+tick(0,1), 0.8);

                    triangulos2.Fade(EndTime, 1);
                    triangulos2.Rotate(EndTime, MathHelper.DegreesToRadians(180));
                    triangulos2.Scale((OsbEasing)1,EndTime,EndTime+tick(0,1), 0, 1.0);
                    triangulos2.Color(EndTime, new Color4(255,255,255,255));

                    triangulos2.Scale(EndTime+tick(0,1), 0.8);
                    

                    if(y>=200 && y<=280){
                        triangulos.Fade(EndTime+tick(0,1),1);
                        triangulos2.Fade(EndTime+tick(0,1),1);
                        triangulos2.Color(EndTime+tick(0,1), new Color4(255,255,255,255));
                        triangulos.Color(EndTime+tick(0,1), new Color4(255,255,255,255));
                    }
                    if((y>=120 && y<=160) || (y>=320 && y<=380) ){
                        triangulos.Fade(EndTime+tick(0,1),0);
                        triangulos2.Fade(EndTime+tick(0,1),0);
                        triangulos.Fade(EndTime+tick(0,1)+tick(0,2),1);
                        triangulos2.Fade(EndTime+tick(0,1)+tick(0,2),1);

                        triangulos2.Color(EndTime+tick(0,1)+tick(0,2), new Color4(255,130,180,255));
                        triangulos.Color(EndTime+tick(0,1)+tick(0,2), new Color4(255,130,180,255));
                    }
                    if((y>=0 && y<=40) || (y>=430 && y<=480) ){
                        triangulos.Fade(EndTime+tick(0,1),0);
                        triangulos2.Fade(EndTime+tick(0,1),0);
                        triangulos.Fade(EndTime+tick(0,1)+tick(0,1),1);
                        triangulos2.Fade(EndTime+tick(0,1)+tick(0,1),1);

                        triangulos2.Color(EndTime+tick(0,1)+tick(0,1), new Color4(92,200,250,255));
                        triangulos.Color(EndTime+tick(0,1)+tick(0,1), new Color4(92,200,250,255));
                    }
                    
                    if(x <= -85 && x >= -107){
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)3), EndTime+tick(0,(double)1/(double)4), 0.8, 0);
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)3)+tick(0,2), EndTime+tick(0,(double)1/(double)3)+tick(0,2)+tick(0,1), 0.8, 0);
                    }
                    if(x>=650 && x<= 745){
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)3), EndTime+tick(0,(double)1/(double)4), 0.8, 0);
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)3)+tick(0,2), EndTime+tick(0,(double)1/(double)3)+tick(0,2)+tick(0,1), 0.8, 0);
                    }
                    if(x >= 20 && x <= 70){
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)4), EndTime+tick(0,(double)1/(double)5), 0.8, 0);
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)4)+tick(0,2), EndTime+tick(0,(double)1/(double)4)+tick(0,2)+tick(0,1), 0.8, 0);
                    }
                    if(x>=533 && x<= 560){
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)4), EndTime+tick(0,(double)1/(double)5), 0.8, 0);
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)4)+tick(0,2), EndTime+tick(0,(double)1/(double)4)+tick(0,2)+tick(0,1), 0.8, 0);
                    }
                    if(x >= 150 && x <= 170){
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)5), EndTime+tick(0,(double)1/(double)6), 0.8, 0);
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)5)+tick(0,2), EndTime+tick(0,(double)1/(double)5)+tick(0,2)+tick(0,1), 0.8, 0);
                    }
                    if(x>=400 && x<= 440){
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)5), EndTime+tick(0,(double)1/(double)6), 0.8, 0);
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)5)+tick(0,2), EndTime+tick(0,(double)1/(double)5)+tick(0,2)+tick(0,1), 0.8, 0);
                    }
                    if(x >= 280 && x <= 300){
                        triangulos.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)6), EndTime+tick(0,(double)1/(double)7), 0.8, 0);
                        triangulos2.Scale((OsbEasing)7, EndTime+tick(0,(double)1/(double)6), EndTime+tick(0,(double)1/(double)7), 0.8, 0);
                    }
                }
            }

            var headSpin = layer.CreateSprite(Head,OsbOrigin.Centre);
            headSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 0, 1);
            headSpin.Fade(StartTime+tick(0,1), EndTime-tick(0,(double)1/(double)2), 1, 1);
            headSpin.Scale(StartTime, 0.33);
            headSpin.Fade(EndTime, EndTime+tick(0,1), 1, 0);
            
            headSpin.Rotate(StartTime, 15011, MathHelper.DegreesToRadians(0),  MathHelper.DegreesToRadians(180));

            var rotateInit = 180;
            var rotateIncrease = 0;
            for(double spinInit = 15011+tick(0,2); spinInit<=EndTime;spinInit+=tick(0,1)){
                headSpin.Rotate((OsbEasing)8,spinInit, spinInit+tick(0,2), MathHelper.DegreesToRadians(rotateInit),  MathHelper.DegreesToRadians(rotateInit-3));
                //Log($"{rotateInit-3}");
                rotateInit-= 3+rotateIncrease;
                rotateIncrease+=2;
                
            }
            

            var circleSpin = layer.CreateSprite(CircleHead,OsbOrigin.Centre);
            circleSpin.Fade(OsbEasing.OutCirc, StartTime, StartTime+tick(0,1), 0, 1);
            circleSpin.Fade(StartTime+tick(0,1), EndTime, 1, 1);
            circleSpin.Scale(StartTime, 0.45);

            for(double circlePump = StartTime+tick(0,1); circlePump <= 20626; circlePump+=tick(0,(double)1/(double)2)){
                headSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.33, 0.3);
                circleSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,1), 0.45, 0.45*1.1);

                circleSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.45*1.1, 0.45);
                headSpin.Scale(circlePump+tick(0,1), circlePump+tick(0,(double)1/(double)2), 0.3, 0.33);
            }
            for(double circlePump = 20636; circlePump <= EndTime-20; circlePump+=tick(0,1)){
                headSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,2), 0.33, 0.3);
                circleSpin.Scale(OsbEasing.OutCirc, circlePump, circlePump+tick(0,2), 0.45, 0.45*1.1);

                if(circlePump==23636){
                    circleSpin.Scale(circlePump+tick(0,2), circlePump+tick(0,1), 0.45*1.1, 0.45);
                    headSpin.Scale(circlePump+tick(0,2), circlePump+tick(0,1), 0.3, 0.33);
                }else{
                    circleSpin.Scale(circlePump+tick(0,2), circlePump+tick(0,1), 0.45*1.1, 0.45);
                    headSpin.Scale(circlePump+tick(0,2), circlePump+tick(0,1), 0.3, 0.33);
                }
            }
            circleSpin.Scale((OsbEasing)4, EndTime, EndTime+tick(0,1), 0.45, 2);
            headSpin.Scale((OsbEasing)4, EndTime, EndTime+tick(0,1), 0.33, 1.6);
            headSpin.Rotate((OsbEasing)4, EndTime, EndTime+tick(0,1), MathHelper.DegreesToRadians(-45), MathHelper.DegreesToRadians(-300));

            var vig = layer.CreateSprite(VigBG,OsbOrigin.Centre);
            var Vigbitmap = GetMapsetBitmap(VigBG);
            vig.Fade(StartTime, StartTime+tick(0,(double)1/(double)8), 0,0.65);
            vig.Fade(StartTime+tick(0,(double)1/(double)8), EndTime, 0.65,0.65);
            vig.Fade(EndTime, 0.65);
            vig.Fade(EndTime+tick(0,(double)1/(double)7), 0);
            vig.Scale(StartTime, 540.0f / Vigbitmap.Height);
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
        int mod(int x, int m) {
            return (x%m + m)%m;
        }
    }
}
