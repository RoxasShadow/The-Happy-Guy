private var got = false;

var time = 2.0;
var text = "Buh!";
var y = -10;
var width = 200;
var height = 40;

function OnGUI() {
    if (!got && transform.position.y < y)
    {
        GUI.Box(new Rect(1, 1, width, height), text);
        StartCoroutine(Timer());
    }
}

function Timer() {
    yield WaitForSeconds(time);
    got = true;
}