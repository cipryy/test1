var images = [
  { "src": "img1.jpg", "alt": "1 Nature" },
  { "src": "img2.jpg", "alt": "2 Fjords" }, 
  { "src": "img3.jpg", "alt": "3 Mountains" },
  { "src": "img4.jpg", "alt": "4 Lights" }
];
var index = 0;

//.1) Realizati o bucla for care parcurge array-ul de imagini si printeaza in consola fiecare proprietate .alt si .src  a obiectelor.    
function loop(){
for (i = 0; i < images.length; i++){
  console.log(images[i].src);
  console.log(images[i].alt);
  }
}

//2) Functia completeaza continutul tagului "img" la fiecare apelare, conform indexului folosit ca parametru. Se poate verifica in consola.
function ShowImage(index){
  var slide = document.getElementById("slide");

  slide.src = images[index].src;
  slide.alt = images[index].alt;
 }

 //3) Se adauga un event intregului document care presupune incarcarea imaginii cu indexul 0, initializat mai sus. Eventul se declanseaza la incarcarea ferestrei.
 document.addEventListener(window.onload, ShowImage(index));

 //4) Functie care corespunde butonului "inainte" si care parcurge inainte imaginile din array-ul "images".
function next(){
   var slide = document.getElementById("slide");
   index++
   if (index >= images.length) {
     index = 0; //5) Indexul se intoarce la 0, odata ajuns la finalul listei.
     /*
     button = document.getElementById("inainte");
     button.parentNode.removeChild(button);
     */
    document.getElementById("inainte").hidden = true; //7) Odata ajuns la finalul listei, butonul "inainte" este ascuns.
    };
   ShowImage(index);
 }

 //6)Functie care corespunde butonului "inapoi" si care parcurge inapoi imaginile din array-ul "images".
 function previous(){
  var slide = document.getElementById("slide");
  index--
  if (index < 0) {
    index = images.length-1; //5) Indexul se intoarce la valoarea maxima, odata ajuns la inceputul listei.
    /*
    button = document.getElementById("inapoi");
    button.parentNode.removeChild(button);
    */
   document.getElementById("inapoi").hidden = true; //7) Odata ajuns la inceputul listei, butonul "inapoi" este ascuns.
  };
  ShowImage(index);
}

//9)Functia genereaza continului elementului cu id="meniu", in functie de numarul de imagini din array. 
function meniuBilute(){
  var meniu = document.getElementById("meniu");
  for (i = 0; i < images.length; i++){
    var newSpan = document.createElement("span");
    newSpan.setAttribute("class", "biluta");
    newSpan.setAttribute("id", "biluta" + (i+1));
    meniu.appendChild(newSpan);
  }
}

meniuBilute();
