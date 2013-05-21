var guiObject : GUITexture;
var fadeTime = 2.0;
var LoadLevel = "Main";
enum Fade {In, Out}
     
function Start () {
    yield FadeGUITexture(guiObject, fadeTime, Fade.In);
    yield WaitForSeconds(3.0);
    yield FadeGUITexture(guiObject, fadeTime, Fade.Out);
    Application.LoadLevel (LoadLevel);
}
     
function FadeGUITexture (guiObject : GUITexture, timer : float, fadeType : Fade) {
    var start = fadeType == Fade.In? 0.0 : 1.0;
    var end = fadeType == Fade.In? 1.0 : 0.0;
    var i = 0.0;
    var step = 1.0/timer;
       
    while (i < 1.0) {
        i += step * Time.deltaTime;
        guiObject.color.a = Mathf.Lerp(start, end, i)*.5;
        yield;
    }
}