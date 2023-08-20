var opacity = 1;
    var timeUntilDisappear = 500;
  
    var timer = setInterval(function() {
   timeUntilDisappear--;
   if(timeUntilDisappear < 1){
       opacity-= 0.005;
var element = document.getElementById("BROWSER-MESSAGE-BOX");
   element.style.opacity = ""+opacity;
   if(opacity < 0){
       element.remove();
       clearInterval(timer);
   }
   }

}, 1);
    