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
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public Color4 Color = Color4.White;

        public override void Generate(){

            var layer = GetLayer("Kiai2");

            float Y_trapezio = 0;
            for(float x = 0; x<= 11; x++){
                if((new [] {2,6,7}).Contains((int)x)) continue; // 0,1,3,4,5,8,9
                var trapezio = layer.CreateSprite(Trapezio, OsbOrigin.Centre);

                Y_trapezio = 255.0f+(x*15.9f);
                trapezio.Move((OsbEasing)7, 75011, 84011, new Vector2(320, Y_trapezio+250), new Vector2(320, Y_trapezio));
                trapezio.Scale(75011, 0.4+(x*0.047f));
                trapezio.Fade(75011, 1);
                trapezio.Fade(EndTime, 0);

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

		    
            
        }
    }
}
