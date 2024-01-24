let _allKeysMap = new Map();
let namesArr = new Array(
	'Esc','F1','F2','F3','F4','F5','F6','F7','F8','F9',
	'F10','F11','F12','PrtSc','ScrLk','Pause','VolumeMute','VolumeDown','VolumeUp','Calculator',

	'Oem3', 'D1', 'D2', 'D3', 'D4', 'D5', 'D6', 'D7', 'D8','D9',
	'D0', 'OemMinus', 'OemPlus', 'Back', 'Insert', 'Home', 'PageUp', 'NumLock', 'Divide','Multiply',
	'Subtract',

	'Tab','Q','W','E','R','T','Y','U','I','O',
	'P', 'OemOpenBrackets', 'Oem6', 'Oem5', 'Delete', 'End', 'Next', 'NumPad7', 'NumPad8','NumPad9',
	'Add',

	'Capital','A','S','D','F','G','H','J','K','L',
	'Oem1', 'OemQuotes', 'Enter', 'NumPad4', 'NumPad5','NumPad6',

	'LeftShift', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 'OemComma','OemPeriod',
	'OemQuestion', 'RightShift', 'Up', 'NumPad1', 'NumPad2','NumPad3','NumEnter',

	'LeftCtrl', 'LWin', 'LeftAlt', 'Space', 'RightAlt', 'RWin', 'FinalMode', 'RightCtrl', 'Left','Down',
	'Right', 'NumPad0','Decimal'
);
let leftsColorArr = new Array(
	'2%','10.5%','15%','19%','23.5%','30%','34.5%','39%','43%','49.5%',
	'54%','58.2%','62.6%','68%','72.5%','77%','82.2%','86.5%','91%','95.5%',

	'2%','6.2%','10.5%','15%','19%','23.5%','28%','32.2%','36.5%','41%',
	'45.2%','49.7%','54%','58.3%','68%','72.5%','76.7%','82.3%','87%','91%'
	,'95.5%',

	'2%','8.2%','12.5%','17%','21.2%','25.6%','30%','34.2%','38.6%','43%',
	'47.3%','51.7%','56%','62%','68%','72.5%','76.7%','82.3%','87%','91%',
	'95.5%',

	'2%','9.5%','13.8%','18.3%','22.5%','26.8%','31.3%','35.5%','39.9%','44.3%',
	'48.6%','53%','57.3%','82.3%','87%','91%',

	'2%','11.7%','16%','20.5%','24.7%','29%','33.5%','37.7%','42.1%','46.5%',
	'50.8%','55.2%','72.5%','82.3%','87%','91%','95.5%',

	'2.2%','7.5%','13%','27.3%','46.5%','51.7%','57%','62.5%','68%','72.5%',
	'77%','84.5%','91%'
);
let topsColorArr = new Array(
	'4%','4%','4%','4%','4%','4%','4%','4%','4%','4%',
	'4%','4%','4%','4%','4%','4%','4%','4%','4%','4%',

	'24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%',
	'24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%','24.3%',
	'24.3%',

	'39%','39%','39%','39%','39%','39%','39%','39%','39%','39%',
	'39%','39%','39%','39%','39%','39%','39%','39%','39%','39%',
	'39%',

	'54%','54%','54%','54%','54%','54%','54%','54%','54%','54%',
	'54%','54%','54%','54%','54%','54%',

	'68%','68%','68%','68%','68%','68%','68%','68%','68%','68%',
	'68%','68%','68%','68%','68%','68%','68%',

	'82%','82%','82%','82%','82%','82%','82%','82%','82%','82%',
	'82%','82%','82%'
);
let leftsBgArr = new Array(
	13,101,145,190,235,300,346,390,435,500,
	545.2,590,635,689,734,779,834,879,924,969,

	13,57,102,145,190,235,280,324,368,412,
	457,500,545,590,689,734,779,834,879,924,
	969,

	13,80,125,170,215,258,302,346,390,435,
	480,525,568,610,689,734,779,834,879,924,
	969,

	13,91,135,180,225,270,314,358,402,447,
 	490,535,580,834,879,924,

	13,113,158,203,248,292,336,380,425,468,
	513,556,734,834,879,924,969,

 	13,68,123,180,455,510,565,620,689,734,
 	779,834,924
);
let topsBgArr = new Array(
	11,11,11,11,11,11,11,11,11,11,
	11,11,11,11,11,11,11,11,11,11,

	76,76,76,76,76,76,76,76,76,76,
	76,76,76,76,76,76,76,76,76,76,
	76,

	120,120,120,120,120,120,120,120,120,120,
	120,120,120,120,120,120,120,120,120,120,
	120,

	165,165,165,165,165,165,165,165,165,165,
	165,165,165,165,165,165,

	210,210,210,210,210,210,210,210,210,210,
	210,210,210,210,210,210,210,

	255,255,255,255,255,255,255,255,255,255,
	255,255,255
);
function createKeys(){
	var content = document.getElementById('AllKeys');

	for(let i=0;i<namesArr.length;i++){
		var div1 = document.createElement('div');
		div1.id = namesArr[i]+'_Color';
		content.appendChild(div1);
	}

	var div2 = document.createElement('div');
	div2.id = 'imgBoard';
	content.appendChild(div2);

	for(let i=0;i<namesArr.length;i++){
		var div3 = document.createElement('div');
		div3.id = namesArr[i]+'_Bg';
		content.appendChild(div3);
	}

	resizeKeys(true);
}
function initKeys(scale,isInit=false){
	
	for(let i=0;i<namesArr.length;i++){

		let color = namesArr[i]+'_Color';
		let bg = namesArr[i]+'_Bg';

		let leftColor = scale*1024*parseFloat(leftsColorArr[i])/100+'px';
		let topColor = scale*311*parseFloat(topsColorArr[i])/100+'px';
		let widthColor = scale*30+'px';
		let heightColor = scale*40+'px';
		if(namesArr[i] == 'Back'){widthColor=scale*50+'px';}
		else if(namesArr[i] == 'Capital'){widthColor=scale*40+'px';}
		else if(namesArr[i] == 'Enter'){widthColor=scale*45+'px';}
		else if(namesArr[i] == 'LeftShift' || namesArr[i] == 'RightShift'){widthColor=scale*40+'px';}
		else if(namesArr[i] == 'NumEnter'){heightColor=scale*60+'px';}
		else if(namesArr[i] == 'Space'){widthColor=scale*70+'px';}

		document.getElementById(color).style.position='absolute';
		document.getElementById(color).style.backgroundColor='rgb(0, 0, 0)';
		document.getElementById(color).style.width=widthColor;
		document.getElementById(color).style.height=heightColor;
		document.getElementById(color).style.left=leftColor;
		document.getElementById(color).style.top=topColor;


		let leftBg = scale*leftsBgArr[i]+'px';
		let topBg = scale*topsBgArr[i]+'px';
		let widthBg = scale*41+'px';
		let heightBg = scale*41+'px';
		if(namesArr[i] == 'F5'){widthBg=scale*42+'px';}
		else if(namesArr[i] == 'Back'){widthBg=scale*85+'px';}
		else if(namesArr[i] == 'Tab'){widthBg=scale*63+'px';}
		else if(namesArr[i] == 'R' || namesArr[i] == 'T' || namesArr[i] == 'OemOpenBrackets'){widthBg=scale*40+'px';}
		else if(namesArr[i] == 'Oem5'){widthBg=scale*65+'px';}
		else if(namesArr[i] == 'Del' || namesArr[i] == 'End' || namesArr[i] == 'Next' || namesArr[i] == 'NumPad7' || namesArr[i] == 'NumPad8' || namesArr[i] == 'NumPad9'){heightBg=scale*42+'px';}
		else if(namesArr[i] == 'Add'){heightBg=scale*87+'px';}
		else if(namesArr[i] == 'Capital'){widthBg=scale*75+'px';}
		else if(namesArr[i] == 'G' || namesArr[i] == 'H' || namesArr[i] == 'J' || namesArr[i] == 'L'){widthBg=scale*40+'px';}
		else if(namesArr[i] == 'Enter'){widthBg=scale*95+'px';}
		else if(namesArr[i] == 'NumPad4' || namesArr[i] == 'NumPad5' || namesArr[i] == 'NumPad6'){heightBg=scale*42+'px';}
		else if(namesArr[i] == 'LeftShift'){widthBg=scale*97+'px';}
		else if(namesArr[i] == 'V' || namesArr[i] == 'B' || namesArr[i] == 'N' || namesArr[i] == 'OemComma' || namesArr[i] == 'OemQuestion'){widthBg=scale*40+'px';}
		else if(namesArr[i] == 'RightShift'){widthBg=scale*119+'px';}
		else if(namesArr[i] == 'Up'){heightBg=scale*42+'px';}
		else if(namesArr[i] == 'NumEnter'){heightBg=scale*86+'px';}
		else if(namesArr[i] == 'LeftCtrl' || namesArr[i] == 'LWin'){widthBg=scale*52+'px';}
		else if(namesArr[i] == 'LeftAlt' || namesArr[i] == 'RightAlt'){widthBg=scale*53+'px';}
		else if(namesArr[i] == 'RWin'){widthBg=scale*54+'px';}
		else if(namesArr[i] == 'FinalMode' || namesArr[i] == 'RightCtrl'){widthBg=scale*55+'px';}
		else if(namesArr[i] == 'Space'){widthBg=scale*273+'px';}
		else if(namesArr[i] == 'NumPad0'){widthBg=scale*86+'px';}


		document.getElementById(bg).style.width=widthBg;
		document.getElementById(bg).style.height=heightBg;
		document.getElementById(bg).style.left=leftBg;
		document.getElementById(bg).style.top=topBg;
		document.getElementById(bg).style.position='absolute';
		document.getElementById(bg).style.borderStyle='solid';
		document.getElementById(bg).style.borderWidth='2px';
		document.getElementById(bg).style.borderColor='white';
		document.getElementById(bg).style.opacity='0';
		document.getElementById(bg).onclick=function(){keyClick(this);};

		if(isInit){
			_allKeysMap.set(namesArr[i],document.getElementById(color));
		}
	}
}

let _singleKeysMap = new Map();
function keyClick(obj){
	let name = obj.id.split('_')[0];
	if(obj.style.opacity == 0){
		clearSingleKeys();
		obj.style.opacity = 1;
		_singleKeysMap.set(name,obj);
		if(name == 'Back'){Backspace_Click();}
	}else{
		obj.style.opacity = 0;
		if(_singleKeysMap.has(name)){_singleKeysMap.delete(name);}
	}
	
	window.cefSharpExample.testMethod(name);
}
function Backspace_Click(){
	//do somethings
}

createKeys();

//适配
const debounce = (fn, delay) => {
	let timer;
	return function() {
		if (timer) {
			clearTimeout(timer);
		}
		timer = setTimeout(() => {
			fn();
		}, delay);
	}
};
const cancalDebounce = debounce(resizeKeys, 1000);
window.addEventListener('resize', cancalDebounce);
function resizeKeys(isInit=false){
	var scale = window.innerWidth/1024;
	document.getElementById('imgBoard').style.height = scale*311+'px';
	initKeys(scale,isInit);
};

//外部调用
function setColor(r,g,b,a=1){
	if(_singleKeysMap.size == 0){
		for(var val of _allKeysMap.values()){
			val.style.backgroundColor = 'rgba('+r+','+g+','+b+','+a+')';
		}
	}else{
		for(var val of _singleKeysMap.values()){
			val.style.backgroundColor = 'rgba('+r+','+g+','+b+','+a+')';
		}
	}
}
function setSingleColor(name,r,g,b,a=1){
	if(_allKeysMap.has(name)){
		let val = _allKeysMap.get(name);
		val.style.backgroundColor = 'rgba('+r+','+g+','+b+','+a+')';
	}
}
function clearSingleKeys(){
	for(var val of _singleKeysMap.values()){
		val.style.opacity = '0';
	}
	_singleKeysMap.clear();
}