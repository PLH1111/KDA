let _allKeysMap = new Map();
//C语言二维数组映射
//[0][0]=Esc  [0][1]=Null
//[1][0]=Oem3 [1][1]=D1
let CMapArr2 = [
	['Esc'      ,'Null','F1'     ,'F2'  ,'F3'  ,'F4'   ,'F5'  ,'F6'  ,'F7'      ,'F8'       ,'F9'         ,'F10'            ,'F11'    ,'F12'       ,'PrtSc' ,'ScrLk','Pause' ,'VolumeMute','VolumeDown','VolumeUp','Calculator'],
	['Oem3'     ,'D1'  ,'D2'     ,'D3'  ,'D4'  ,'D5'   ,'D6'  ,'D7'  ,'D8'      ,'D9'       ,'D0'         ,'OemMinus'       ,'OemPlus','Back'      ,'Insert','Home' ,'PageUp','NumLock','Divide' ,'Multiply','Subtract'],
	['Tab'      ,'Q'   ,'W'      ,'E'   ,'R'   ,'T'    ,'Y'   ,'U'   ,'I'       ,'O'        ,'P'          ,'OemOpenBrackets','Oem6'   ,'Oem5'      ,'Delete','End'  ,'Next'  ,'NumPad7','NumPad8','NumPad9' ,'Add'     ],
	['Capital'  ,'A'   ,'S'      ,'D'   ,'F'   ,'G'    ,'H'   ,'J'   ,'K'       ,'L'        ,'Oem1'       ,'OemQuotes'      ,'Null'   ,'Enter'     ,'Null'  ,'Null' ,'Null'  ,'NumPad4','NumPad5','NumPad6' ,'Null'    ],
	['LeftShift','Z'   ,'X'      ,'C'   ,'V'   ,'B'    ,'N'   ,'M'   ,'OemComma','OemPeriod','OemQuestion','Null'           ,'Null'   ,'RightShift','Null'  ,'Up'   ,'Null'  ,'NumPad1','NumPad2','NumPad3' ,'NumEnter'],
	['LeftCtrl' ,'LWin','LeftAlt','Null','Null','Space','Null','Null','Null'    ,'RightAlt' ,'RWin'       ,'FinalMode'      ,'Null'   ,'RightCtrl' ,'Left'  ,'Down' ,'Right' ,'Null'   ,'NumPad0','Decimal' ,'Null'    ]
];
//键盘布局名称
let namesArr = new Array(
	'Esc','D1','D2','D3','D4','D5','D6','D7','D8','D9','D0','OemMinus','OemPlus','Back','Oem3',

	'Tab','Q','W','E','R','T','Y','U','I','O','P','OemOpenBrackets','Oem6','Oem5','Delete',

	'Capital','A','S','D','F','G','H','J','K','L','Oem1','OemQuotes','Enter','PageUp',

	'LeftShift','Z','X','C','V','B','N','M','OemComma','OemPeriod','OemQuestion','RightShift','Up','Next',

	'LeftCtrl','LWin','LeftAlt','Space','RightAlt','FinalMode','RightCtrl','Left','Down','Right'
);
//键盘单个按键X坐标
let leftsColorArr = new Array(
	'2.4%','8.5%','14.8%','20.7%','27%','33.2%','39.4%','45.5%','51.5%','58%','64%','70%','76.2%','84.7%','94.7%',

	'3%','11.5%','17.8%','24%','30%','36.3%','42.5%','48.5%','54.8%','61%','67%','73.2%','79.3%','87%','94.7%',

	'3%','13.2%','19.2%','25.5%','31.6%','37.8%','44%','50.2%','56.2%','62.3%','68.5%','74.7%','83.5%','94.7%',

	'5%','16.4%','22.5%','28.5%','34.7%','41%','47%','53.2%','59.2%','65.5%','71.6%','79.5%','88.7%','94.7%',

	'3.2%','10.8%','18.8%','39.5%','64%','70.2%','76.5%','82.5%','88.7%','94.7%'
);
//键盘单个按键Y坐标
let topsColorArr = new Array(
	'4%','4%','4%','4%','4%','4%','4%','4%','4%','4%','4%','4%','4%','4%','4%',

	'23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%','23.3%',

	'42%','42%','42%','42%','42%','42%','42%','42%','42%','42%','42%','42%','42%','42%',

	'61%','61%','61%','61%','61%','61%','61%','61%','61%','61%','61%','61%','61%','61%',

	'80%','80%','80%','80%','80%','80%','80%','80%','80%','80%'
);
//键盘单个按键点击框X坐标
let leftsBgArr = new Array(
	16,78,135,195,255,315,373,433,490,550,610,670,730,790,906,

	16,108,168,225,283,342,404,462,522,580,640,698,760,820,906,

	16,120,180,240,300,358,417,475,537,595,656,713,772,906,

	16,150,210,268,328,387,447,508,564,625,683,743,848,906,

	16,92,165,240,610,670,730,787,848,906
);
//键盘单个按键点击框Y坐标
let topsBgArr = new Array(
	11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,

	77,77,77,77,77,77,77,77,77,77,77,77,77,77,77,

	145,145,145,145,145,145,145,145,145,145,145,145,145,145,

	210,210,210,210,210,210,210,210,210,210,210,210,210,210,

	280,280,280,280,280,280,280,280,280,280
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

		let leftColor = scale*963*parseFloat(leftsColorArr[i])/100+'px';
		let topColor = scale*352*parseFloat(topsColorArr[i])/100+'px';
		let widthColor = scale*30+'px';
		let heightColor = scale*40+'px';
		if(namesArr[i] == 'Back'){widthColor=scale*50+'px';}
		else if(namesArr[i] == 'Tab'){widthColor=scale*50+'px';}
		else if(namesArr[i] == 'Capital'){widthColor=scale*65+'px';}
		else if(namesArr[i] == 'Enter'){widthColor=scale*55+'px';}
		else if(namesArr[i] == 'LeftShift'){widthColor=scale*55+'px';}
		else if(namesArr[i] == 'RightShift'){widthColor=scale*40+'px';}
		else if(namesArr[i] == 'NumEnter'){heightColor=scale*60+'px';}
		else if(namesArr[i] == 'Space'){widthColor=scale*70+'px';}

		document.getElementById(color).style.position='absolute';
		document.getElementById(color).style.backgroundColor='rgb(255, 255, 255)';
		document.getElementById(color).style.width=widthColor;
		document.getElementById(color).style.height=heightColor;
		document.getElementById(color).style.left=leftColor;
		document.getElementById(color).style.top=topColor;


		let leftBg = scale*leftsBgArr[i]+'px';
		let topBg = scale*topsBgArr[i]+'px';
		let widthBg = scale*38+'px';
		let heightBg = scale*47+'px';
		if(namesArr[i] == 'F5'){widthBg=scale*42+'px';}
		else if(namesArr[i] == 'Back'){widthBg=scale*95+'px';}
		else if(namesArr[i] == 'Tab'){widthBg=scale*68+'px';}
		else if(namesArr[i] == 'R' || namesArr[i] == 'T' || namesArr[i] == 'OemOpenBrackets'){widthBg=scale*40+'px';}
		else if(namesArr[i] == 'Oem5'){widthBg=scale*65+'px';}
		else if(namesArr[i] == 'Del' || namesArr[i] == 'End' || namesArr[i] == 'NumPad7' || namesArr[i] == 'NumPad8' || namesArr[i] == 'NumPad9'){heightBg=scale*42+'px';}
		else if(namesArr[i] == 'Add'){heightBg=scale*87+'px';}
		else if(namesArr[i] == 'Capital'){widthBg=scale*85+'px';}
		else if(namesArr[i] == 'G' || namesArr[i] == 'H' || namesArr[i] == 'J' || namesArr[i] == 'L'){widthBg=scale*40+'px';}
		else if(namesArr[i] == 'Enter'){widthBg=scale*110+'px';}
		else if(namesArr[i] == 'NumPad4' || namesArr[i] == 'NumPad5' || namesArr[i] == 'NumPad6'){heightBg=scale*42+'px';}
		else if(namesArr[i] == 'LeftShift'){widthBg=scale*111+'px';}
		else if(namesArr[i] == 'V' || namesArr[i] == 'B' || namesArr[i] == 'N' || namesArr[i] == 'OemComma' || namesArr[i] == 'OemQuestion'){widthBg=scale*40+'px';}
		else if(namesArr[i] == 'RightShift'){widthBg=scale*83+'px';}
		else if(namesArr[i] == 'NumEnter'){heightBg=scale*86+'px';}
		else if(namesArr[i] == 'LeftCtrl' || namesArr[i] == 'LWin'){widthBg=scale*52+'px';}
		else if(namesArr[i] == 'LeftAlt'){widthBg=scale*53+'px';}
		else if(namesArr[i] == 'RWin'){widthBg=scale*54+'px';}
		else if(namesArr[i] == 'Space'){widthBg=scale*350+'px';}
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
		document.getElementById(bg).onclick=function(){keyClick(this);};//这是系统级别的按键捕获函数

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
	//在这里，按键的点击事件是js获取的，如过上位机传给你，性能损失很大，你可以在
	//name就是按键名字

	window.setClickKey(name);
	//调用lights的function   --------1
	// window.cefSharpExample.testMethod(name);
}
function clearSingleKeys(){
	_singleKeysMap.forEach((val,key)=>{
		val.style.opacity = 0;
	});
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
	var scale = window.innerWidth/963;
	document.getElementById('imgBoard').style.height = scale*352+'px';
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
//将HEX格式转换为RGB数组
function hexToRgb(hex) {
  let result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
  return result ? [
    parseInt(result[1], 16),
    parseInt(result[2], 16),
    parseInt(result[3], 16)
  ] : null;
}
//将颜色转换为HSL对象
function colorToHsl(color) {
  let rgb;
  if (color.startsWith('#')) {
    // 转换HEX格式到RGB
    rgb = hexToRgb(color);
  } else if (color.startsWith('rgb(')) {
    // 提取RGB值
    rgb = color.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
    rgb.shift(); // 去除匹配信息
  } else {
    throw new Error('Unsupported color format');
  }
  
  // 转换RGB到HSL
  let r = rgb[0] / 255,
      g = rgb[1] / 255,
      b = rgb[2] / 255,
      max = Math.max(r, g, b),
      min = Math.min(r, g, b),
      delta = max - min,
      h,
      s,
      l;
 
  if (max === min) {
    h = 0; // 无色
  } else if (r === max) {
    h = (g - b) / delta % 6 * 60; // 红色为主
  } else if (g === max) {
    h = (b - r) / delta + 120; // 绿色为主
  } else {
    h = (r - g) / delta + 240; // 蓝色为主
  }
 
  l = (max + min) / 2;
  s = delta === 0 ? 0 : delta / (1 - Math.abs(2 * l - 1));
 
  return {
    h: h,
    s: s,
    l: l
  };
}
//将HSL对象转换回颜色字符串
function hslToColorString({h, s, l}) {
  return `hsl(${h}, ${(s * 100).toFixed(0)}%, ${(l * 100).toFixed(0)}%)`;
}
//参数(行号，列号，颜色，饱和度)
//颜色可写方式：#00ff00 & rgb(0,255,0)
window.setSaturation = function(row,column,color,saturation) {
  // 将颜色转换为HSL格式
  let hsl = colorToHsl(color);
  // 设置饱和度
  hsl.s = saturation;
  // 将HSL格式转换回字符串
  let hslValue = hslToColorString(hsl);
  let name = CMapArr[row][column];
  if(name == 'Null'){return;}
  if(_allKeysMap.has(name)){
		let val = _allKeysMap.get(name);
		val.style.backgroundColor = hslValue;
	}
}
window.setSingleColor = function(name,r,g,b,a=1){
	if(_allKeysMap.has(name)){
		let val = _allKeysMap.get(name);
		val.style.backgroundColor = 'rgba('+r+','+g+','+b+','+a+')';
		val.style.opacity = a;
	}
}
window.setCSingleColor = function(row,column,r,g,b,a=1){
	let name = CMapArr[row][column];
	if(name == 'Null'){return;}
	window.setSingleColor(name,r,g,b,a);
}
window.setAllKeysColor = function(r,g,b,a=1){
	for(let i=0;i<namesArr.length;i++){
		window.setSingleColor(namesArr[i],r,g,b,a);
	}
}
window.setSingleBrightness = function(name,a=1){
	if(_allKeysMap.has(name)){
		let val = _allKeysMap.get(name);
		val.style.opacity = a;
	}
}
window.setCSingleBrightness = function(row,column,a=1){
	let name = CMapArr[row][column];
	if(name == 'Null'){return;}
	window.setSingleBrightness(name,a);
}
window.setAllKeysBrightness = function(a=1){
	for(let i=0;i<namesArr.length;i++){
		window.setSingleBrightness(namesArr[i],a);
	}
}
window.clearAllKeys = function(){
	for(let i=0;i<namesArr.length;i++){
		window.setSingleColor(namesArr[i],255,255,255);
	}
}
window.getColor=function(colorMode)
{
	var arr =[];
	switch(colorMode)
	{
	case 0:// 七彩
		arr[0] = 255;
		arr[1] = 0;
		arr[2] = 0;
		break;
	case 1:// 红
		arr[0] = 255;
		arr[1] = 0;
		arr[2] = 0;
		break;
	case 2:// 绿
		arr[0] = 0;
		arr[1] = 255;
		arr[2] = 0;
		break;
	case 3:// 蓝
		arr[0] = 0;
		arr[1] = 0;
		arr[2] = 255;
		break;
	case 4:// 黄
		arr[0] = 255;
		arr[1] = 255;
		arr[2] = 0;
		break;
	case 5:// 紫
		arr[0] = 255;
		arr[1] = 0;
		arr[2] = 255;
		break;
	case 6:// 青
		arr[0] = 0;
		arr[1] = 0;
		arr[2] = 255;
		break;
	case 7:// 白
		arr[0] = 255;
		arr[1] = 255;
		arr[2] = 255;
		break;
	default:
		arr[0] = 255;
		arr[1] = 0;
		arr[2] = 0;
		break;
	}
	return arr;
}
window.getRandColor=function()
{
	var index=parseInt(Math.random() * (100-1)+1)%7;
	return getColor(index);
}
window.ledChangeSmooth=function(color, rate=100){
	let i = 0;
	let step = 0;
	let temp = new Int16Array(3);
	let rgb = new Uint8Array(3);

	i = parseInt(color / rate);

    step = parseInt((color % rate) * 255 / rate);

    switch(i)
    {
    case 0:
        temp[0] = 255 - step;
        temp[1] = 0 + step;
        temp[2] = 0;
        break;
    case 1:
        temp[0] = 0;
        temp[1] = 255 - step;
        temp[2] = 0 + step;
        break;
        case 2:
        temp[0] = 0 + step;
        temp[1] = 0;
        temp[2] = 255 - step;
        break;
        case 3:
        temp[0] = 255;
        temp[1] = 255 - step;
        temp[2] = 255 - step;
        break;
    default:
		temp[0] = 255 - step;
        temp[1] = 0 + step;
        temp[2] = 0;
       break;
    }

    for(i = 0; i < 3; i++)
    {
        if(temp[i] < 0)
        {
            temp[i] = 0;
        }
        if(temp[i] > 255)
        {
            temp[i] = 255;
        }
		rgb[i] = temp[i];
    }

    return rgb;
}
//使用样例:
// let obj = window.getRowNCol('Esc');
// console.log(obj.row);
// console.log(obj.col);
window.getRowNCol=function(name)
{
	if(name == 'Null'){return null;}
	for(let i=0;i<6;i++)
	{
		for(let j=0;j<21;j++)
		{
			if(CMapArr[i][j] == name)
			{
				return{
					row:i,
					col:j
				}
			}
		}
	}
	return null;
}
