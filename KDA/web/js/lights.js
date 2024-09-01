var controls = function(){};
controls.maxRowNum = 6;
controls.maxColNum = 21;
controls.click=false;
controls.row=0;
controls.col=0;
controls.leds = 11;//style
controls.maxLedNum = 17;
controls.colorMode = 0;// 0=七彩,1=红,2=绿,3=蓝,4=黄,5=紫,6=青,7=白
controls.ledOver = true;//灯效运行是否完成
let ledRunState = new Uint8Array(controls.maxLedNum);
controls.logen = true;
// 1:静态常亮ok
controls.static = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let cnt = 0;
	let temp = 0;
	var color = [];
	let t11 = null;
	let ch = new Uint16Array(controls.maxColNum);
	let Colorful=[];
	for(var i = 0; i < 12; i++)
	{
		ch[i] = parseInt(3 * 100 * i / 12);
		color = ledChangeSmooth(ch[i]);
		Colorful[i]=[];
		Colorful[i][0]=color[0];
		Colorful[i][1]=color[1];
		Colorful[i][2]=color[2];
	}
	let brightness = 1;
	t11 = setInterval(function(){
		if(controls.leds != 1){
			clearInterval(t11);
			return;
		}
		if(0 == controls.colorMode)
		{
			let m = 0;
			for(cnt = 0;cnt<126;cnt++)
			{
				temp = parseInt(cnt / controls.maxRowNum);
				if(temp <= 8)
				{
					m = 11-temp;
					window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
				}
				else if(temp < 12)
				{
					if((temp == 9) || (temp == 11))
					{
						m=2;
						window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=1;
						window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=2;
						window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=3;
						window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					}
					else
					{
						m=2;
						window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=1;
						window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=0;
						window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=1;
						window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=2;
						window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=3;
						window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					}
				}
				else
				{
					m = temp-10;
					window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
				}
			}
		}
		else
		{
			color = window.getColor(controls.colorMode);
			window.setAllKeysColor(color[0], color[1], color[2],brightness);
		}
	}, 20);
}
// 2:动态呼吸ok
controls.breathe = function() {
	setLedType(2);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let cnt = 0;
	var color = [];
	let ch = new Uint16Array(controls.maxColNum);
	let Colorful=[];
	for(var i = 0; i < 12; i++)
	{
		ch[i] = parseInt(3 * 100 * i / 12);
		color = ledChangeSmooth(ch[i]);
		Colorful[i]=[];
		Colorful[i][0]=color[0];
		Colorful[i][1]=color[1];
		Colorful[i][2]=color[2];
	}
	let t11 = null;
	let brightness = 1;
	let darken = true;
	let brighten=false;
	let holden=false;
	let bricnt = 0;
	let darkcnt = 0;
	let holdcnt = 0;
	t11 = setInterval(function(){
		if(controls.leds != 2){
			clearInterval(t11);
			return;
		}
		if(darken)
		{
			darkcnt++;
			if(darkcnt > 1)
			{
				darkcnt=0;
				if(brightness>0)
				{
					brightness = (brightness - 0.1).toFixed(1);
					if(brightness==0.1)
					{
						darken=false;
						holden=true;
					}
				}
			}
		}
		else if(brighten)
		{
			bricnt++;
			if(bricnt > 1)
			{
				bricnt=0;
				if(brightness < 1)
				{
					brightness = (parseFloat)(brightness) + 0.1;
					brightness = brightness.toFixed(1);
					if(brightness==1)
					{
						brighten=false;
						holden=true;
					}
				}
			}
		}
		else
		{
			holdcnt++;
			if(holdcnt > 2)
			{
				holden=false;
				if(brightness==1)
				{
					darken=true;
				}
				else
				{
					brighten=true;
				}
				holdcnt = 0;
			}
		}
		if(0 == controls.colorMode)
		{
			let m = 0;
			for(cnt = 0;cnt<126;cnt++)
			{
				temp = parseInt(cnt / controls.maxRowNum);
				if(temp <= 8)
				{
					m = 11-temp;
					window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
				}
				else if(temp < 12)
				{
					if((temp == 9) || (temp == 11))
					{
						m=2;
						window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=1;
						window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=2;
						window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=3;
						window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					}
					else
					{
						m=2;
						window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=1;
						window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=0;
						window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=1;
						window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=2;
						window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
						m=3;
						window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					}
				}
				else
				{
					m = temp-10;
					window.setCSingleColor(0,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(1,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(2,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(3,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(4,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
					window.setCSingleColor(5,temp,Colorful[m][0],Colorful[m][1],Colorful[m][2],brightness);
				}
			}
		}
		else
		{
			color = window.getColor(controls.colorMode);
			window.setAllKeysColor(color[0], color[1], color[2], brightness);
		}
	}, 100);
}
// 3:梦幻彩虹ok
controls.dream_rainbow = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	var color = [];
	let ch = new Uint16Array(controls.maxColNum);
	ch[0] = 0;
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 3){
			clearInterval(t11);
			return;
		}
		color = ledChangeSmooth(ch[0]);
		setRowColor(0,color[0],color[1],color[2]);
		setRowColor(1,color[0],color[1],color[2]);
		setRowColor(2,color[0],color[1],color[2]);
		setRowColor(3,color[0],color[1],color[2]);
		setRowColor(4,color[0],color[1],color[2]);
		setRowColor(5,color[0],color[1],color[2]);
		ch[0]++;
		if(ch[0] >= (3 * 100))
		{
			ch[0] = 0;
		}
	}, 100);
}
// 4:一触即发[七彩ok纯色NG]
controls.touch = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let display=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let hold=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let turnoff=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let Colorful=[];
	let brightness=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let holdcnt=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let index=0;
	let spread=false;
	let layer=0;
	let layerNum=0;
	touch_init();
	let interval=40;
	let holdTime=1200;
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 4){
			clearInterval(t11);
			return;
		}

		touch_handle();
	}, interval);
	function touch_init()
	{
		let i=0;
		let j=0;

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				Colorful[i*controls.maxColNum+j]=[];
				Colorful[i*controls.maxColNum+j][0]=color[0];
				Colorful[i*controls.maxColNum+j][1]=color[1];
				Colorful[i*controls.maxColNum+j][2]=color[2];
			}
		}
	}
	function touch_handle()
	{
		let i=0;
		let j=0;

		if(controls.click)
		{
			controls.click=false;
			index=controls.row*controls.maxColNum+controls.col;
			holdcnt[index]=0;
			hold[index]=false;
			turnoff[index]= false;
			display[index]=true;
			spread=true;
			layer=0;
			var leftLayerNum=controls.col;
			var rightLayerNum=controls.maxColNum-1-controls.col;
			layerNum=(leftLayerNum>rightLayerNum)?leftLayerNum:rightLayerNum;
		}

		if(spread)
		{
			if(layer <= layerNum)
			{
				layer++;
				if(layer > layerNum)
				{
					spread=false;
				}
				else
				{
					for(j=-layer;j<=layer;j++)
					{
						if(((controls.col+j) >= 0) && ((controls.col+j) <= controls.maxColNum-1))
						{
							var r=controls.row;
							var c=controls.col+j;
							index=r*controls.maxColNum+c;
							holdcnt[index]=0;
							hold[index]=false;
							turnoff[index]= false;
							display[index]=true;
						}
					}
				}
			}
		}

		var isOff=false;
		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var temp=i*controls.maxColNum+j;
				if(display[temp])
				{
					// turn on itself
					brightness[temp] = 10;
					var bri=(brightness[temp]/10).toFixed(1);
					window.setCSingleColor(i, j, Colorful[temp][0], Colorful[temp][1], Colorful[temp][2],bri);
					turnoff[temp] = false;
					display[temp]=false;
					hold[temp]=true;
					holdcnt[temp]=0;
				}
				else if(hold[temp])
				{
					holdcnt[temp]++;
					if(holdcnt[temp] >= parseInt(holdTime/interval))
					{
						holdcnt[temp]=0;
						hold[temp]=false;
						turnoff[temp]=true;
					}
				}
				else if(turnoff[temp])
				{
					if(brightness[temp]>0)
					{
						brightness[temp]-=1;
						var bri=(brightness[temp]/10).toFixed(1);
						// window.setCSingleColor(i, j, Colorful[temp][0], Colorful[temp][1], Colorful[temp][2],bri);
						window.setCSingleBrightness(i,j,(brightness[i*controls.maxColNum+j]/10).toFixed(1));
						if(brightness[temp]==0)
						{
							turnoff[temp]=false;
						}
						else{
							isOff=true;
						}
					}
				}
			}
		}
	}
}
// 5:雨中散步[七彩ok纯色NG]
controls.walk_rain = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let Colorful=[];
	let col_offset = 3;
	let cur_col=0;
	let cur_row=0;
	let index=0;
	walkrain_init();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 5){
			clearInterval(t11);
			return;
		}

		walkrain_handle();
	}, 100);
	function walkrain_init()
	{
		let i=0;
		let j=0;

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				Colorful[i*controls.maxColNum+j]=[];
				Colorful[i*controls.maxColNum+j][0]=color[0];
				Colorful[i*controls.maxColNum+j][1]=color[1];
				Colorful[i*controls.maxColNum+j][2]=color[2];
			}
		}
	}
	function walkrain_handle()
	{
		let i=0;

		if(cur_row < controls.maxRowNum)
		{
			for(i=0;i<controls.maxColNum;i++)
			{
				if((i%col_offset)==cur_col)
				{
					index=parseInt(Math.random() * (1000-1)+1)%(controls.maxRowNum*controls.maxColNum-1);
					window.setCSingleColor(cur_row, i, Colorful[index][0],Colorful[index][1],Colorful[index][2],1);
					if(i+1<controls.maxColNum)
					{
						index=parseInt(Math.random() * (1000-1)+1)%(controls.maxRowNum*controls.maxColNum-1);
						window.setCSingleColor(cur_row, i+1, Colorful[index][0],Colorful[index][1],Colorful[index][2],1);
					}
				}
			}
			cur_row++;
		}
		else
		{
			cur_row=0;
			window.clearAllKeys();
			if(cur_col < col_offset)
			{
				cur_col++;
				if(cur_col >= col_offset)
				{
					cur_col=0;
				}
			}
	}
	}
}
// 6:彩虹轮盘[七彩ok纯色NG]
controls.roul_rainbow = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
		let roulrainbowMapBk1 =[];
		let roulrainbowMapBk2 =[];
		let roulrainbowMapBk3 =[];
		let roulrainbowMapBk4 =[];
		let roulrainbowMapBk5 =[];
		let roulrainbowMapBk6 =[];
		let roulrainbowMapBk7 =[];
		let roulrainbowMapBk8 =[];
		let roulrainbowMapBk9 =[];
		let roulrainbowMapBk10=[];
		let chBk1 =new Uint16Array(6);
		let chBk2 =new Uint16Array(14);
		let chBk3 =new Uint16Array(22);
		let chBk4 =new Uint16Array(12);
		let chBk5 =new Uint16Array(12);
		let chBk6 =new Uint16Array(12);
		let chBk7 =new Uint16Array(12);
		let chBk8 =new Uint16Array(12);
		let chBk9 =new Uint16Array(12);
		let chBk10=new Uint16Array(12);
		let roulrainbowMap1 =[];
		let roulrainbowMap2 =[];
		let roulrainbowMap3 =[];
		let roulrainbowMap4 =[];
		let roulrainbowMap5 =[];
		let roulrainbowMap6 =[];
		let roulrainbowMap7 =[];
		let roulrainbowMap8 =[];
		let roulrainbowMap9 =[];
		let roulrainbowMap10=[];
		
		let ch1 =new Uint16Array(6);
		let ch2 =new Uint16Array(14);
		let ch3 =new Uint16Array(22);
		let ch4 =new Uint16Array(12);
		let ch5 =new Uint16Array(12);
		let ch6 =new Uint16Array(12);
		let ch7 =new Uint16Array(12);
		let ch8 =new Uint16Array(12);
		let ch9 =new Uint16Array(12);
		let ch10=new Uint16Array(12);
		roulrainbowInit();
		let cnt = 0;
		let virtualCol = 0;
		let LED_STATUS=new Uint16Array(controls.maxRowNum*controls.maxColNum);
		let LED_CNT=new Uint8Array(controls.maxRowNum*controls.maxColNum);
		let STEP_MASK=0x0F00;
		let BEGIN_MASK_BREATHE=0x8000;
		let breathecnt = 0;
		let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 6){
			clearInterval(t11);
			return;
		}

		roulrainbow_mem_handle();

		roulrainbow_handle();

	}, 30);
	function roulrainbowInit()
	{
		var i = 0;
		let c1=new Uint16Array(6);
		let c2=new Uint16Array(14);
		let c3=new Uint16Array(22);
		let c4=new Uint16Array(30);
		let c5=new Uint16Array(38);
		let c6=new Uint16Array(46);
		let c7=new Uint16Array(54);
		let c8=new Uint16Array(62);
		let c9=new Uint16Array(70);
		let c10=new Uint16Array(78);
		let roulMap1 =[];
		let roulMap2 =[];
		let roulMap3 =[];
		let roulMap4 =[];
		let roulMap5 =[];
		let roulMap6 =[];
		let roulMap7 =[];
		let roulMap8 =[];
		let roulMap9 =[];
		let roulMap10=[];

		let temp=0;
		let color = [];
	
		for(i = 0; i < 6; i++)
		{
			c1[i] = parseInt(420 * (6 - i) / 6);
			color = ledChangeSmooth(c1[i], 140);
			roulMap1[i]=[];
			roulMap1[i][0] = color[0];
			roulMap1[i][1] = color[1];
			roulMap1[i][2] = color[2];
		}
		
		for(i = 0; i < 14; i++)
		{
			c2[i] = parseInt(420 * (14 - i) / 14);
			color = ledChangeSmooth(c2[i], 140);
			roulMap2[i]=[];
			roulMap2[i][0] = color[0];
			roulMap2[i][1] = color[1];
			roulMap2[i][2] = color[2];
		}
	
		for(i = 0; i < 22; i++)
		{
			c3[i] = 420 * (22 - i) / 22;
			c3[i] = parseInt(420 * (22 - i) / 22);
			color = ledChangeSmooth(c3[i], 140);
			roulMap3[i]=[];
			roulMap3[i][0] = color[0];
			roulMap3[i][1] = color[1];
			roulMap3[i][2] = color[2];
		}
		
		for(i = 0; i < 30; i++)
		{
			c4[i] = parseInt(420 * (30 - i) / 30);
			color = ledChangeSmooth(c4[i], 140);
			roulMap4[i]=[];
			roulMap4[i][0] = color[0];
			roulMap4[i][1] = color[1];
			roulMap4[i][2] = color[2];
		}
	
		for(i = 0; i < 38; i++)
		{
			c5[i] = parseInt(420 * (38 - i) / 38);
			color = ledChangeSmooth(c5[i], 140);
			roulMap5[i]=[];
			roulMap5[i][0] = color[0];
			roulMap5[i][1] = color[1];
			roulMap5[i][2] = color[2];
		}
		
		for(i = 0; i < 46; i++)
		{
			c6[i] = parseInt(420 * (46 - i) / 46);
			color = ledChangeSmooth(c6[i], 140);
			roulMap6[i]=[];
			roulMap6[i][0] = color[0];
			roulMap6[i][1] = color[1];
			roulMap6[i][2] = color[2];
		}
		
		for(i = 0; i < 54; i++)
		{
			c7[i] = parseInt(420 * (54 - i) / 54);
			color = ledChangeSmooth(c7[i], 140);
			roulMap7[i]=[];
			roulMap7[i][0] = color[0];
			roulMap7[i][1] = color[1];
			roulMap7[i][2] = color[2];
		}
	
		for(i = 0; i < 62; i++)
		{
			c8[i] = parseInt(420 * (62 - i) / 62);
			color = ledChangeSmooth(c8[i], 140);
			roulMap8[i]=[];
			roulMap8[i][0] = color[0];
			roulMap8[i][1] = color[1];
			roulMap8[i][2] = color[2];
		}
		
		for(i = 0; i < 70; i++)
		{
			c9[i] = parseInt(420 * (70 - i) / 70);
			color = ledChangeSmooth(c9[i], 140);
			roulMap9[i]=[];
			roulMap9[i][0] = color[0];
			roulMap9[i][1] = color[1];
			roulMap9[i][2] = color[2];
		}
		
		for(i = 0; i < 78; i++)
		{
			c10[i] = parseInt(420 * (78 - i) / 78);
			color = ledChangeSmooth(c10[i], 140);
			roulMap10[i]=[];
			roulMap10[i][0] = color[0];
			roulMap10[i][1] = color[1];
			roulMap10[i][2] = color[2];
		}

		for(let index = 0; index < controls.maxColNum; index++)
		{
			if(index <= 7)
			{
				temp = 6 + 8 * ((9 - index)) -  2 * (10 - index) + 3;
			}
			else if(index >= 13)
			{
				temp = 2 * (index - 10) - 3;
			}
			
			switch(index)
			{
			case 0:
				for(i = 0; i < 6; i++)
				{
					chBk10[i] = c10[temp - i];
					roulrainbowMapBk10[i] = [];
					roulrainbowMapBk10[i][0] = roulMap10[temp - i][0];
					roulrainbowMapBk10[i][1] = roulMap10[temp - i][1];
					roulrainbowMapBk10[i][2] = roulMap10[temp - i][2];
					ch10[i]=chBk10[i];
					roulrainbowMap10[i] = [];
					roulrainbowMap10[i][0] = roulrainbowMapBk10[i][0];
					roulrainbowMap10[i][1] = roulrainbowMapBk10[i][1];
					roulrainbowMap10[i][2] = roulrainbowMapBk10[i][2];
				}
				break;
			case 1:
				for(i = 0; i < 6; i++)
				{
					chBk9[i] = c9[temp - i];
					roulrainbowMapBk9[i] = [];
					roulrainbowMapBk9[i][0] = roulMap9[temp - i][0];
					roulrainbowMapBk9[i][1] = roulMap9[temp - i][1];
					roulrainbowMapBk9[i][2] = roulMap9[temp - i][2];

					ch9[i]=chBk9[i];
					roulrainbowMap9[i] = [];
					roulrainbowMap9[i][0] = roulrainbowMapBk9[i][0];
					roulrainbowMap9[i][1] = roulrainbowMapBk9[i][1];
					roulrainbowMap9[i][2] = roulrainbowMapBk9[i][2];
				}
				break;
			case 2:
				for(i = 0; i < 6; i++)
				{
					chBk8[i] = c8[temp - i];
					roulrainbowMapBk8[i] = [];
					roulrainbowMapBk8[i][0] = roulMap8[temp - i][0];
					roulrainbowMapBk8[i][1] = roulMap8[temp - i][1];
					roulrainbowMapBk8[i][2] = roulMap8[temp - i][2];

					ch8[i]=chBk8[i];
					roulrainbowMap8[i] = [];
					roulrainbowMap8[i][0] = roulrainbowMapBk8[i][0];
					roulrainbowMap8[i][1] = roulrainbowMapBk8[i][1];
					roulrainbowMap8[i][2] = roulrainbowMapBk8[i][2];
				}
				break;
			case 3:
				for(i = 0; i < 6; i++)
				{
					chBk7[i] = c7[temp - i];
					roulrainbowMapBk7[i] = [];
					roulrainbowMapBk7[i][0] = roulMap7[temp - i][0];
					roulrainbowMapBk7[i][1] = roulMap7[temp - i][1];
					roulrainbowMapBk7[i][2] = roulMap7[temp - i][2];

					ch7[i]=chBk7[i];
					roulrainbowMap7[i] = [];
					roulrainbowMap7[i][0] = roulrainbowMapBk7[i][0];
					roulrainbowMap7[i][1] = roulrainbowMapBk7[i][1];
					roulrainbowMap7[i][2] = roulrainbowMapBk7[i][2];
				}
				break;
			case 4:
				for(i = 0; i < 6; i++)
				{
					chBk6[i] = c6[temp - i];
					roulrainbowMapBk6[i] = [];
					roulrainbowMapBk6[i][0] = roulMap6[temp - i][0];
					roulrainbowMapBk6[i][1] = roulMap6[temp - i][1];
					roulrainbowMapBk6[i][2] = roulMap6[temp - i][2];

					ch6[i]=chBk6[i];
					roulrainbowMap6[i] = [];
					roulrainbowMap6[i][0] = roulrainbowMapBk6[i][0];
					roulrainbowMap6[i][1] = roulrainbowMapBk6[i][1];
					roulrainbowMap6[i][2] = roulrainbowMapBk6[i][2];
				}
				break;
			case 5:
				for(i = 0; i < 6; i++)
				{
					chBk5[i] = c5[temp - i];
					roulrainbowMapBk5[i] = [];
					roulrainbowMapBk5[i][0] = roulMap5[temp - i][0];
					roulrainbowMapBk5[i][1] = roulMap5[temp - i][1];
					roulrainbowMapBk5[i][2] = roulMap5[temp - i][2];

					ch5[i]=chBk5[i];
					roulrainbowMap5[i] = [];
					roulrainbowMap5[i][0] = roulrainbowMapBk5[i][0];
					roulrainbowMap5[i][1] = roulrainbowMapBk5[i][1];
					roulrainbowMap5[i][2] = roulrainbowMapBk5[i][2];
				}
				break;
			case 6:
				for(i = 0; i < 6; i++)
				{
					chBk4[i] = c4[temp - i];
					roulrainbowMapBk4[i] = [];
					roulrainbowMapBk4[i][0] = roulMap4[temp - i][0];
					roulrainbowMapBk4[i][1] = roulMap4[temp - i][1];
					roulrainbowMapBk4[i][2] = roulMap4[temp - i][2];

					ch4[i]=chBk4[i];
					roulrainbowMap4[i] = [];
					roulrainbowMap4[i][0] = roulrainbowMapBk4[i][0];
					roulrainbowMap4[i][1] = roulrainbowMapBk4[i][1];
					roulrainbowMap4[i][2] = roulrainbowMapBk4[i][2];
				}
				break;
			case 14:
				for(i = 0; i < 6; i++)
				{
					chBk4[i + 6] = c4[temp + i];
					roulrainbowMapBk4[i + 6] = [];
					roulrainbowMapBk4[i + 6][0] = roulMap4[temp + i][0];
					roulrainbowMapBk4[i + 6][1] = roulMap4[temp + i][1];
					roulrainbowMapBk4[i + 6][2] = roulMap4[temp + i][2];

					ch4[i + 6]=chBk4[i + 6];
					roulrainbowMap4[i + 6] = [];
					roulrainbowMap4[i + 6][0] = roulrainbowMapBk4[i + 6][0];
					roulrainbowMap4[i + 6][1] = roulrainbowMapBk4[i + 6][1];
					roulrainbowMap4[i + 6][2] = roulrainbowMapBk4[i + 6][2];
				}
				break;
			case 15:
				for(i = 0; i < 6; i++)
				{
					chBk5[i + 6] = c5[temp + i];
					roulrainbowMapBk5[i + 6] = [];
					roulrainbowMapBk5[i + 6][0] = roulMap5[temp + i][0];
					roulrainbowMapBk5[i + 6][1] = roulMap5[temp + i][1];
					roulrainbowMapBk5[i + 6][2] = roulMap5[temp + i][2];

					ch5[i + 6]=chBk5[i + 6];
					roulrainbowMap5[i + 6] = [];
					roulrainbowMap5[i + 6][0] = roulrainbowMapBk5[i + 6][0];
					roulrainbowMap5[i + 6][1] = roulrainbowMapBk5[i + 6][1];
					roulrainbowMap5[i + 6][2] = roulrainbowMapBk5[i + 6][2];
				}
				break;
			case 16:
				for(i = 0; i < 6; i++)
				{
					chBk6[i + 6] = c6[temp + i];
					roulrainbowMapBk6[i + 6] = [];
					roulrainbowMapBk6[i + 6][0] = roulMap6[temp + i][0];
					roulrainbowMapBk6[i + 6][1] = roulMap6[temp + i][1];
					roulrainbowMapBk6[i + 6][2] = roulMap6[temp + i][2];

					ch6[i + 6]=chBk6[i + 6];
					roulrainbowMap6[i + 6] = [];
					roulrainbowMap6[i + 6][0] = roulrainbowMapBk6[i + 6][0];
					roulrainbowMap6[i + 6][1] = roulrainbowMapBk6[i + 6][1];
					roulrainbowMap6[i + 6][2] = roulrainbowMapBk6[i + 6][2];
				}
				break;
			case 17:
				for(i = 0; i < 6; i++)
				{
					chBk7[i + 6] = c7[temp + i];
					roulrainbowMapBk7[i + 6] = [];
					roulrainbowMapBk7[i + 6][0] = roulMap7[temp + i][0];
					roulrainbowMapBk7[i + 6][1] = roulMap7[temp + i][1];
					roulrainbowMapBk7[i + 6][2] = roulMap7[temp + i][2];

					ch7[i + 6]=chBk7[i + 6];
					roulrainbowMap7[i + 6] = [];
					roulrainbowMap7[i + 6][0] = roulrainbowMapBk7[i + 6][0];
					roulrainbowMap7[i + 6][1] = roulrainbowMapBk7[i + 6][1];
					roulrainbowMap7[i + 6][2] = roulrainbowMapBk7[i + 6][2];
				}
				break;
			case 18:
				for(i = 0; i < 6; i++)
				{
					chBk8[i + 6] = c8[temp + i];
					roulrainbowMapBk8[i + 6] = [];
					roulrainbowMapBk8[i + 6][0] = roulMap8[temp + i][0];
					roulrainbowMapBk8[i + 6][1] = roulMap8[temp + i][1];
					roulrainbowMapBk8[i + 6][2] = roulMap8[temp + i][2];

					ch8[i + 6]=chBk8[i + 6];
					roulrainbowMap8[i + 6] = [];
					roulrainbowMap8[i + 6][0] = roulrainbowMapBk8[i + 6][0];
					roulrainbowMap8[i + 6][1] = roulrainbowMapBk8[i + 6][1];
					roulrainbowMap8[i + 6][2] = roulrainbowMapBk8[i + 6][2];
				}
				break;
			case 19:
				for(i = 0; i < 6; i++)
				{
					chBk9[i + 6] = c9[temp + i];
					roulrainbowMapBk9[i + 6] = [];
					roulrainbowMapBk9[i + 6][0] = roulMap9[temp + i][0];
					roulrainbowMapBk9[i + 6][1] = roulMap9[temp + i][1];
					roulrainbowMapBk9[i + 6][2] = roulMap9[temp + i][2];

					ch9[i + 6]=chBk9[i + 6];
					roulrainbowMap9[i + 6] = [];
					roulrainbowMap9[i + 6][0] = roulrainbowMapBk9[i + 6][0];
					roulrainbowMap9[i + 6][1] = roulrainbowMapBk9[i + 6][1];
					roulrainbowMap9[i + 6][2] = roulrainbowMapBk9[i + 6][2];
				}
				break;
			case 20:
				for(i = 0; i < 6; i++)
				{
					chBk10[i + 6] = c10[temp + i];
					roulrainbowMapBk10[i + 6] = [];
					roulrainbowMapBk10[i + 6][0] = roulMap10[temp + i][0];
					roulrainbowMapBk10[i + 6][1] = roulMap10[temp + i][1];
					roulrainbowMapBk10[i + 6][2] = roulMap10[temp + i][2];

					ch10[i + 6]=chBk10[i + 6];
					roulrainbowMap10[i + 6] = [];
					roulrainbowMap10[i + 6][0] = roulrainbowMapBk10[i + 6][0];
					roulrainbowMap10[i + 6][1] = roulrainbowMapBk10[i + 6][1];
					roulrainbowMap10[i + 6][2] = roulrainbowMapBk10[i + 6][2];
				}
				break;
			}
		}
		for(i=0;i<6;i++)
		{
			roulrainbowMapBk1[i]=[];
			roulrainbowMapBk1[i][0]=roulMap1[i][0];
			roulrainbowMapBk1[i][1]=roulMap1[i][1];
			roulrainbowMapBk1[i][2]=roulMap1[i][2];
			chBk1[i]=c1[i];
			roulrainbowMap1[i]=[];
			roulrainbowMap1[i][0]=roulrainbowMapBk1[i][0];
			roulrainbowMap1[i][1]=roulrainbowMapBk1[i][1];
			roulrainbowMap1[i][2]=roulrainbowMapBk1[i][2];
			ch1[i]=chBk1[i];
		}
		for(i=0;i<14;i++)
		{
			roulrainbowMapBk2[i]=[];
			roulrainbowMapBk2[i][0]=roulMap2[i][0];
			roulrainbowMapBk2[i][1]=roulMap2[i][1];
			roulrainbowMapBk2[i][2]=roulMap2[i][2];
			chBk2[i]=c2[i];
			roulrainbowMap2[i]=[];
			roulrainbowMap2[i][0]=roulrainbowMapBk2[i][0];
			roulrainbowMap2[i][1]=roulrainbowMapBk2[i][1];
			roulrainbowMap2[i][2]=roulrainbowMapBk2[i][2];
			ch2[i]=chBk2[i];
		}
		for(i=0;i<22;i++)
		{
			roulrainbowMapBk3[i]=[];
			roulrainbowMapBk3[i][0]=roulMap3[i][0];
			roulrainbowMapBk3[i][1]=roulMap3[i][1];
			roulrainbowMapBk3[i][2]=roulMap3[i][2];
			chBk3[i]=c3[i];
			roulrainbowMap3[i]=[];
			roulrainbowMap3[i][0]=roulrainbowMapBk3[i][0];
			roulrainbowMap3[i][1]=roulrainbowMapBk3[i][1];
			roulrainbowMap3[i][2]=roulrainbowMapBk3[i][2];
			ch3[i]=chBk3[i];
		}
	}
	function roulrainbow_mem_handle()
	{
		let color = [];
		if(controls.colorMode==0)
		{// 七彩
			for(i = 0; i < 6; i++)
			{
				color =ledChangeSmooth(ch1[i], 140);
				roulrainbowMap1[i]=[];
				roulrainbowMap1[i][0] = color[0];
				roulrainbowMap1[i][1] = color[1];
				roulrainbowMap1[i][2] = color[2];
				ch1[i]++;
				if(ch1[i] >= (420))
				{
					ch1[i] = 0;
				}
			}
			
			for(i = 0; i < 14; i++)
			{
				color =ledChangeSmooth(ch2[i], 140);
				roulrainbowMap2[i]=[];
				roulrainbowMap2[i][0] = color[0];
				roulrainbowMap2[i][1] = color[1];
				roulrainbowMap2[i][2] = color[2];
				ch2[i]++;
				if(ch2[i] >= (420))
				{
					ch2[i] = 0;
				}
			}
			
			for(i = 0; i < 22; i++)
			{
				color =ledChangeSmooth(ch3[i], 140);
				roulrainbowMap3[i]=[];
				roulrainbowMap3[i][0] = color[0];
				roulrainbowMap3[i][1] = color[1];
				roulrainbowMap3[i][2] = color[2];
				ch3[i]++;
				if(ch3[i] >= (420))
				{
					ch3[i] = 0;
				}
			}
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch4[i], 140);
				roulrainbowMap4[i]=[];
				roulrainbowMap4[i][0] = color[0];
				roulrainbowMap4[i][1] = color[1];
				roulrainbowMap4[i][2] = color[2];
				ch4[i]++;
				if(ch4[i] >= (420))
				{
					ch4[i] = 0;
				}
			}
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch5[i], 140);
				roulrainbowMap5[i]=[];
				roulrainbowMap5[i][0] = color[0];
				roulrainbowMap5[i][1] = color[1];
				roulrainbowMap5[i][2] = color[2];
				ch5[i]++;
				if(ch5[i] >= (420))
				{
					ch5[i] = 0;
				}
			}
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch6[i], 140);
				roulrainbowMap6[i]=[];
				roulrainbowMap6[i][0] = color[0];
				roulrainbowMap6[i][1] = color[1];
				roulrainbowMap6[i][2] = color[2];
				ch6[i]++;
				if(ch6[i] >= (420))
				{
					ch6[i] = 0;
				}
			}		
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch7[i], 140);
				roulrainbowMap7[i]=[];
				roulrainbowMap7[i][0] = color[0];
				roulrainbowMap7[i][1] = color[1];
				roulrainbowMap7[i][2] = color[2];
				ch7[i]++;
				if(ch7[i] >= (420))
				{
					ch7[i] = 0;
				}
			}
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch8[i], 140);
				roulrainbowMap8[i]=[];
				roulrainbowMap8[i][0] = color[0];
				roulrainbowMap8[i][1] = color[1];
				roulrainbowMap8[i][2] = color[2];
				ch8[i]++;
				if(ch8[i] >= (420))
				{
					ch8[i] = 0;
				}
			}
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch9[i], 140);
				roulrainbowMap9[i]=[];
				roulrainbowMap9[i][0] = color[0];
				roulrainbowMap9[i][1] = color[1];
				roulrainbowMap9[i][2] = color[2];
				ch9[i]++;
				if(ch9[i] >= (420))
				{
					ch9[i] = 0;
				}
			}
			
			for(i = 0; i < 12; i++)
			{
				color =ledChangeSmooth(ch10[i], 140);
				roulrainbowMap10[i]=[];
				roulrainbowMap10[i][0] = color[0];
				roulrainbowMap10[i][1] = color[1];
				roulrainbowMap10[i][2] = color[2];
				ch10[i]++;
				if(ch10[i] >= (420))
				{
					ch10[i] = 0;
				}
			}
		}
		else
		{
			if(breathecnt<5)
			{
				breathecnt++;
				return;
			}
			breathecnt=0;
			let i=0;
			if(virtualCol <= 20)
			{
				for(i = 0; i < 3; i++)
				{
					LED_STATUS[virtualCol * 6 + i] |= BEGIN_MASK_BREATHE;
					LED_STATUS[(20 - virtualCol) * 6 + i + 3] |= BEGIN_MASK_BREATHE;
				}
			}
			else
			{
				for(i = 0; i < 3; i++)
				{
					LED_STATUS[(41 - virtualCol) * 6 + i + 3] |= BEGIN_MASK_BREATHE;
					LED_STATUS[(virtualCol - 21) * 6 + i] |= BEGIN_MASK_BREATHE;
				}
			}
			virtualCol++;
			if(virtualCol >= 42)
			{
				virtualCol = 0;
			}
		}
	}
	function roulrainbow_handle()
	{
		let i=0;
		let j=0;
		let index = 0;
		let temp=0;
		let color=[];

		cnt = cnt + 6;
		if(cnt >= controls.maxColNum*controls.maxRowNum)
		{
			cnt=0;
		}
		index = parseInt(cnt / 6);
		if(controls.colorMode==0)
		{
			// for(j=0;j < controls.maxColNum*controls.maxRowNum;j++)
			{
				if(index <= 7)
				{
					temp = 6 + 8 * ((9 - index)) -  2 * (10 - index) + 3;
				}
				else if(index >= 13)
				{
					temp = 2 * (index - 10) - 3;
				}
				switch(index)
				{
				case 0:
					window.setCSingleColor(0,index,roulrainbowMap10[0][0],roulrainbowMap10[0][1],roulrainbowMap10[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap10[1][0],roulrainbowMap10[1][1],roulrainbowMap10[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap10[2][0],roulrainbowMap10[2][1],roulrainbowMap10[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap10[3][0],roulrainbowMap10[3][1],roulrainbowMap10[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap10[4][0],roulrainbowMap10[4][1],roulrainbowMap10[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap10[5][0],roulrainbowMap10[5][1],roulrainbowMap10[5][2]);
					break;
				case 1:
					window.setCSingleColor(0,index,roulrainbowMap9[0][0],roulrainbowMap9[0][1],roulrainbowMap9[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap9[1][0],roulrainbowMap9[1][1],roulrainbowMap9[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap9[2][0],roulrainbowMap9[2][1],roulrainbowMap9[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap9[3][0],roulrainbowMap9[3][1],roulrainbowMap9[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap9[4][0],roulrainbowMap9[4][1],roulrainbowMap9[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap9[5][0],roulrainbowMap9[5][1],roulrainbowMap9[5][2]);
					break;
				case 2:
					window.setCSingleColor(0,index,roulrainbowMap8[0][0],roulrainbowMap8[0][1],roulrainbowMap8[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap8[1][0],roulrainbowMap8[1][1],roulrainbowMap8[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap8[2][0],roulrainbowMap8[2][1],roulrainbowMap8[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap8[3][0],roulrainbowMap8[3][1],roulrainbowMap8[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap8[4][0],roulrainbowMap8[4][1],roulrainbowMap8[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap8[5][0],roulrainbowMap8[5][1],roulrainbowMap8[5][2]);
					break;
				case 3:
					window.setCSingleColor(0,index,roulrainbowMap7[0][0],roulrainbowMap7[0][1],roulrainbowMap7[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap7[1][0],roulrainbowMap7[1][1],roulrainbowMap7[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap7[2][0],roulrainbowMap7[2][1],roulrainbowMap7[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap7[3][0],roulrainbowMap7[3][1],roulrainbowMap7[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap7[4][0],roulrainbowMap7[4][1],roulrainbowMap7[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap7[5][0],roulrainbowMap7[5][1],roulrainbowMap7[5][2]);
					break;
				case 4:
					window.setCSingleColor(0,index,roulrainbowMap6[0][0],roulrainbowMap6[0][1],roulrainbowMap6[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap6[1][0],roulrainbowMap6[1][1],roulrainbowMap6[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap6[2][0],roulrainbowMap6[2][1],roulrainbowMap6[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap6[3][0],roulrainbowMap6[3][1],roulrainbowMap6[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap6[4][0],roulrainbowMap6[4][1],roulrainbowMap6[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap6[5][0],roulrainbowMap6[5][1],roulrainbowMap6[5][2]);
					break;
				case 5:
					window.setCSingleColor(0,index,roulrainbowMap5[0][0],roulrainbowMap5[0][1],roulrainbowMap5[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap5[1][0],roulrainbowMap5[1][1],roulrainbowMap5[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap5[2][0],roulrainbowMap5[2][1],roulrainbowMap5[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap5[3][0],roulrainbowMap5[3][1],roulrainbowMap5[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap5[4][0],roulrainbowMap5[4][1],roulrainbowMap5[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap5[5][0],roulrainbowMap5[5][1],roulrainbowMap5[5][2]);
					break;
				case 6:
					window.setCSingleColor(0,index,roulrainbowMap4[0][0],roulrainbowMap4[0][1],roulrainbowMap4[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap4[1][0],roulrainbowMap4[1][1],roulrainbowMap4[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap4[2][0],roulrainbowMap4[2][1],roulrainbowMap4[2][2]);
					window.setCSingleColor(3,index,roulrainbowMap4[3][0],roulrainbowMap4[3][1],roulrainbowMap4[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap4[4][0],roulrainbowMap4[4][1],roulrainbowMap4[4][2]);
					window.setCSingleColor(5,index,roulrainbowMap4[5][0],roulrainbowMap4[5][1],roulrainbowMap4[5][2]);
					break;
				case 7:
					window.setCSingleColor(0,index,roulrainbowMap3[temp - 0][0],roulrainbowMap3[temp - 0][1],roulrainbowMap3[temp - 0][2]);
					window.setCSingleColor(1,index,roulrainbowMap3[temp - 1][0],roulrainbowMap3[temp - 1][1],roulrainbowMap3[temp - 1][2]);
					window.setCSingleColor(2,index,roulrainbowMap3[temp - 2][0],roulrainbowMap3[temp - 2][1],roulrainbowMap3[temp - 2][2]);
					window.setCSingleColor(3,index,roulrainbowMap3[temp - 3][0],roulrainbowMap3[temp - 3][1],roulrainbowMap3[temp - 3][2]);
					window.setCSingleColor(4,index,roulrainbowMap3[temp - 4][0],roulrainbowMap3[temp - 4][1],roulrainbowMap3[temp - 4][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[temp - 5][0],roulrainbowMap3[temp - 5][1],roulrainbowMap3[temp - 5][2]);
					break;
				case 8:
					window.setCSingleColor(0,index,roulrainbowMap3[20][0],roulrainbowMap3[20][1],roulrainbowMap3[20][2]);
					window.setCSingleColor(1,index,roulrainbowMap3[12][0],roulrainbowMap3[12][1],roulrainbowMap3[12][2]);
					window.setCSingleColor(2,index,roulrainbowMap3[11][0],roulrainbowMap3[11][1],roulrainbowMap3[11][2]);
					window.setCSingleColor(3,index,roulrainbowMap3[10][0],roulrainbowMap3[10][1],roulrainbowMap3[10][2]);
					window.setCSingleColor(4,index,roulrainbowMap3[9][0],roulrainbowMap3[9][1],roulrainbowMap3[9][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[13][0],roulrainbowMap3[13][1],roulrainbowMap3[13][2]);
					break;
				case 9:
					window.setCSingleColor(0,index,roulrainbowMap3[21][0],roulrainbowMap3[21][1],roulrainbowMap3[21][2]);
					window.setCSingleColor(1,index,roulrainbowMap2[13][0],roulrainbowMap3[13][1],roulrainbowMap3[13][2]);
					window.setCSingleColor(2,index,roulrainbowMap1[5][0],roulrainbowMap3[5][1],roulrainbowMap3[5][2]);
					window.setCSingleColor(3,index,roulrainbowMap1[4][0],roulrainbowMap3[4][1],roulrainbowMap3[4][2]);
					window.setCSingleColor(4,index,roulrainbowMap2[8][0],roulrainbowMap3[8][1],roulrainbowMap3[8][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[12][0],roulrainbowMap3[12][1],roulrainbowMap3[12][2]);
					break;
				case 10:
					window.setCSingleColor(0,index,roulrainbowMap3[0][0],roulrainbowMap3[0][1],roulrainbowMap3[0][2]);
					window.setCSingleColor(1,index,roulrainbowMap2[0][0],roulrainbowMap3[0][1],roulrainbowMap3[0][2]);
					window.setCSingleColor(2,index,roulrainbowMap1[0][0],roulrainbowMap3[0][1],roulrainbowMap3[0][2]);
					window.setCSingleColor(3,index,roulrainbowMap1[3][0],roulrainbowMap3[3][1],roulrainbowMap3[3][2]);
					window.setCSingleColor(4,index,roulrainbowMap2[7][0],roulrainbowMap3[7][1],roulrainbowMap3[7][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[11][0],roulrainbowMap3[11][1],roulrainbowMap3[11][2]);
					break;
				case 11:
					window.setCSingleColor(0,index,roulrainbowMap3[1][0],roulrainbowMap3[1][1],roulrainbowMap3[1][2]);
					window.setCSingleColor(1,index,roulrainbowMap2[1][0],roulrainbowMap3[1][1],roulrainbowMap3[1][2]);
					window.setCSingleColor(2,index,roulrainbowMap1[1][0],roulrainbowMap3[1][1],roulrainbowMap3[1][2]);
					window.setCSingleColor(3,index,roulrainbowMap1[2][0],roulrainbowMap3[2][1],roulrainbowMap3[2][2]);
					window.setCSingleColor(4,index,roulrainbowMap2[6][0],roulrainbowMap3[6][1],roulrainbowMap3[6][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[10][0],roulrainbowMap3[10][1],roulrainbowMap3[10][2]);
					break;
				case 12:
					window.setCSingleColor(0,index,roulrainbowMap3[2][0],roulrainbowMap3[2][1],roulrainbowMap3[2][2]);
					window.setCSingleColor(1,index,roulrainbowMap2[2][0],roulrainbowMap3[2][1],roulrainbowMap3[2][2]);
					window.setCSingleColor(2,index,roulrainbowMap2[3][0],roulrainbowMap3[3][1],roulrainbowMap3[3][2]);
					window.setCSingleColor(3,index,roulrainbowMap2[4][0],roulrainbowMap3[4][1],roulrainbowMap3[4][2]);
					window.setCSingleColor(4,index,roulrainbowMap2[5][0],roulrainbowMap3[5][1],roulrainbowMap3[5][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[9][0],roulrainbowMap3[9][1],roulrainbowMap3[9][2]);
					break;
				case 13:
					window.setCSingleColor(0,index,roulrainbowMap3[temp+0][0],roulrainbowMap3[temp+0][1],roulrainbowMap3[temp+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap3[temp+1][0],roulrainbowMap3[temp+1][1],roulrainbowMap3[temp+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap3[temp+2][0],roulrainbowMap3[temp+2][1],roulrainbowMap3[temp+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap3[temp+3][0],roulrainbowMap3[temp+3][1],roulrainbowMap3[temp+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap3[temp+4][0],roulrainbowMap3[temp+4][1],roulrainbowMap3[temp+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap3[temp+5][0],roulrainbowMap3[temp+5][1],roulrainbowMap3[temp+5][2]);
					break;
				case 14:
					window.setCSingleColor(0,index,roulrainbowMap4[6+0][0],roulrainbowMap4[6+0][1],roulrainbowMap4[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap4[6+1][0],roulrainbowMap4[6+1][1],roulrainbowMap4[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap4[6+2][0],roulrainbowMap4[6+2][1],roulrainbowMap4[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap4[6+3][0],roulrainbowMap4[6+3][1],roulrainbowMap4[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap4[6+4][0],roulrainbowMap4[6+4][1],roulrainbowMap4[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap4[6+5][0],roulrainbowMap4[6+5][1],roulrainbowMap4[6+5][2]);
					break;
				case 15:
					window.setCSingleColor(0,index,roulrainbowMap5[6+0][0],roulrainbowMap5[6+0][1],roulrainbowMap5[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap5[6+1][0],roulrainbowMap5[6+1][1],roulrainbowMap5[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap5[6+2][0],roulrainbowMap5[6+2][1],roulrainbowMap5[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap5[6+3][0],roulrainbowMap5[6+3][1],roulrainbowMap5[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap5[6+4][0],roulrainbowMap5[6+4][1],roulrainbowMap5[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap5[6+5][0],roulrainbowMap5[6+5][1],roulrainbowMap5[6+5][2]);
					break;
				case 16:
					window.setCSingleColor(0,index,roulrainbowMap6[6+0][0],roulrainbowMap6[6+0][1],roulrainbowMap6[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap6[6+1][0],roulrainbowMap6[6+1][1],roulrainbowMap6[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap6[6+2][0],roulrainbowMap6[6+2][1],roulrainbowMap6[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap6[6+3][0],roulrainbowMap6[6+3][1],roulrainbowMap6[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap6[6+4][0],roulrainbowMap6[6+4][1],roulrainbowMap6[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap6[6+5][0],roulrainbowMap6[6+5][1],roulrainbowMap6[6+5][2]);
					break;
				case 17:
					window.setCSingleColor(0,index,roulrainbowMap7[6+0][0],roulrainbowMap7[6+0][1],roulrainbowMap7[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap7[6+1][0],roulrainbowMap7[6+1][1],roulrainbowMap7[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap7[6+2][0],roulrainbowMap7[6+2][1],roulrainbowMap7[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap7[6+3][0],roulrainbowMap7[6+3][1],roulrainbowMap7[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap7[6+4][0],roulrainbowMap7[6+4][1],roulrainbowMap7[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap7[6+5][0],roulrainbowMap7[6+5][1],roulrainbowMap7[6+5][2]);
					break;
				case 18:
					window.setCSingleColor(0,index,roulrainbowMap8[6+0][0],roulrainbowMap8[6+0][1],roulrainbowMap8[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap8[6+1][0],roulrainbowMap8[6+1][1],roulrainbowMap8[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap8[6+2][0],roulrainbowMap8[6+2][1],roulrainbowMap8[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap8[6+3][0],roulrainbowMap8[6+3][1],roulrainbowMap8[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap8[6+4][0],roulrainbowMap8[6+4][1],roulrainbowMap8[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap8[6+5][0],roulrainbowMap8[6+5][1],roulrainbowMap8[6+5][2]);
					break;
				case 19:
					window.setCSingleColor(0,index,roulrainbowMap9[6+0][0],roulrainbowMap9[6+0][1],roulrainbowMap9[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap9[6+1][0],roulrainbowMap9[6+1][1],roulrainbowMap9[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap9[6+2][0],roulrainbowMap9[6+2][1],roulrainbowMap9[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap9[6+3][0],roulrainbowMap9[6+3][1],roulrainbowMap9[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap9[6+4][0],roulrainbowMap9[6+4][1],roulrainbowMap9[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap9[6+5][0],roulrainbowMap9[6+5][1],roulrainbowMap9[6+5][2]);
					break;
				case 20:
					window.setCSingleColor(0,index,roulrainbowMap10[6+0][0],roulrainbowMap10[6+0][1],roulrainbowMap10[6+0][2]);
					window.setCSingleColor(1,index,roulrainbowMap10[6+1][0],roulrainbowMap10[6+1][1],roulrainbowMap10[6+1][2]);
					window.setCSingleColor(2,index,roulrainbowMap10[6+2][0],roulrainbowMap10[6+2][1],roulrainbowMap10[6+2][2]);
					window.setCSingleColor(3,index,roulrainbowMap10[6+3][0],roulrainbowMap10[6+3][1],roulrainbowMap10[6+3][2]);
					window.setCSingleColor(4,index,roulrainbowMap10[6+4][0],roulrainbowMap10[6+4][1],roulrainbowMap10[6+4][2]);
					window.setCSingleColor(5,index,roulrainbowMap10[6+5][0],roulrainbowMap10[6+5][1],roulrainbowMap10[6+5][2]);
					break;
				}				
			}
		}
		else// NG
		{
			for(i = 0; i < 6; i++)
			{
				if(LED_STATUS[cnt + i] & BEGIN_MASK_BREATHE)
				{
					LED_STATUS[cnt + i]++;
					if(((LED_STATUS[cnt + i] & 0xFF)) > 20)
					{
						LED_STATUS[cnt + i] &= ~0xFF;
						if((LED_CNT[cnt + i] & 0xF8) == 0)
						{
						   LED_STATUS[cnt + i] &= ~STEP_MASK;
						   LED_STATUS[cnt + i] |= 0x100;
						}
						else if(((LED_CNT[cnt + i] & 0xF8) == 0xF8) && ((LED_STATUS[cnt + i] & STEP_MASK) == 0x100))
						{
							// 0 --> off the light
							LED_STATUS[cnt + i] &= ~STEP_MASK;
						}
						else if(((LED_CNT[cnt + i] & 0xF8) == 0xF8) && ((LED_STATUS[cnt + i] & STEP_MASK) == 0))
						{
							// 2 --> brighten
							LED_STATUS[cnt + i] &= ~STEP_MASK;
							LED_STATUS[cnt + i] |= 0x200;
						}
						
						if((LED_STATUS[cnt + i] & STEP_MASK) == 0x100)
						{
							LED_CNT[cnt + i] += 0x08;
						}
						else if((LED_STATUS[cnt + i] & STEP_MASK) == 0x200)
						{
							LED_CNT[cnt + i] -= 0x08;
						}
					}
				}

				let brightness = 1;
				// if(LED_CNT[cnt + i])
				color=window.getColor(controls.colorMode);
				window.setCSingleColor(i,index,0,0,0,brightness);
				window.setCSingleColor(i,index,color[0],color[1],color[2],brightness);
			}
		}
	}
}
// 7:涟漪扩散[七彩ok纯色NG]
controls.dimple = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let display=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let hold=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let turnoff=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let Colorful=[];
	let brightness=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let holdcnt=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let index=0;
	let spread=false;
	let layer=0;
	let layerNum=0;
	dimple_init();
	let interval=40;
	let holdTime=1200;
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 7){
			clearInterval(t11);
			return;
		}

		dimple_handle();
	}, interval);
	function dimple_init()
	{
		let i=0;
		let j=0;

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				Colorful[i*controls.maxColNum+j]=[];
				Colorful[i*controls.maxColNum+j][0]=color[0];
				Colorful[i*controls.maxColNum+j][1]=color[1];
				Colorful[i*controls.maxColNum+j][2]=color[2];
			}
		}
	}
	function dimple_handle()
	{
		let i=0;
		let j=0;

		if(controls.click)
		{
			controls.click=false;
			index=controls.row*controls.maxColNum+controls.col;
			holdcnt[index]=0;
			hold[index]=false;
			turnoff[index]= false;
			display[index]=true;
			spread=true;
			layer=0;
			var upLayerNum=controls.row;
			var downLayerNum=controls.maxRowNum-1-controls.row;
			var leftLayerNum=controls.col;
			var rightLayerNum=controls.maxColNum-1-controls.col;
			var maxRowLayerNum=(upLayerNum>downLayerNum)?upLayerNum:downLayerNum;
			var maxColLayerNum=(leftLayerNum>rightLayerNum)?leftLayerNum:rightLayerNum;
			layerNum=maxRowLayerNum+maxColLayerNum;
		}

		if(spread)
		{
			if(layer <= layerNum)
			{
				layer++;
				if(layer > layerNum)
				{
					spread=false;
				}
				else
				{
					for(i=-layer;i<=layer;i++)
					{
						for(j=-layer;j<=layer;j++)
						{
							if(((controls.row+i) >= 0) && ((controls.row+i) <= controls.maxRowNum-1))
							{
								if(((controls.col+j) >= 0) && ((controls.col+j) <= controls.maxColNum-1))
								{
									if((Math.abs(i)+Math.abs(j))==layer)
									{
										var r=controls.row+i;
										var c=controls.col+j;
										index=r*controls.maxColNum+c;
										holdcnt[index]=0;
										hold[index]=false;
										turnoff[index]= false;
										display[index]=true;
									}
								}
							}
						}
					}
				}
			}
		}

		var isOff=false;
		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var temp=i*controls.maxColNum+j;
				if(display[temp])
				{
					// turn on itself
					brightness[temp] = 10;
					var bri=(brightness[temp]/10).toFixed(1);
					window.setCSingleColor(i, j, Colorful[temp][0], Colorful[temp][1], Colorful[temp][2],bri);
					turnoff[temp] = false;
					display[temp]=false;
					hold[temp]=true;
					holdcnt[temp]=0;
				}
				else if(hold[temp])
				{
					holdcnt[temp]++;
					if(holdcnt[temp] >= parseInt(holdTime/interval))
					{
						holdcnt[temp]=0;
						hold[temp]=false;
						turnoff[temp]=true;
					}
				}
				else if(turnoff[temp])
				{
					if(brightness[temp]>0)
					{
						brightness[temp]-=1;
						var bri=(brightness[temp]/10).toFixed(1);
						// window.setCSingleColor(i, j, Colorful[temp][0], Colorful[temp][1], Colorful[temp][2],bri);
						window.setCSingleBrightness(i,j,(brightness[i*controls.maxColNum+j]/10).toFixed(1));
						if(brightness[temp]==0)
						{
							turnoff[temp]=false;
						}
						else{
							isOff=true;
						}
					}
				}
			}
		}
	}
}
// 8:繁星点点[七彩ok纯色NG]
controls.star = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let Colorful=[];
	star_init();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 8){
			clearInterval(t11);
			return;
		}
		star_handle();
	}, 500);
	function star_init()
	{
		let i=0;
		let j=0;

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				Colorful[i*controls.maxColNum+j]=[];
				Colorful[i*controls.maxColNum+j][0]=color[0];
				Colorful[i*controls.maxColNum+j][1]=color[1];
				Colorful[i*controls.maxColNum+j][2]=color[2];
			}
		}
	}
	function star_handle()
	{
		let i=0;
		let j=0;
		let starArray = [];

		window.clearAllKeys();
		length=15;
		starArray[0]=[];
		starArray[0] = Array.from({ length }, () => (parseInt(Math.random()*100)%controls.maxColNum));
		starArray[1]=[];
		starArray[1] = Array.from({ length }, () => (parseInt(Math.random()*100)%controls.maxColNum));
		starArray[2]=[];
		starArray[2] = Array.from({ length }, () => (parseInt(Math.random()*100)%controls.maxColNum));
		starArray[3]=[];
		starArray[3] = Array.from({ length }, () => (parseInt(Math.random()*100)%controls.maxColNum));
		starArray[4]=[];
		starArray[4] = Array.from({ length }, () => (parseInt(Math.random()*100)%controls.maxColNum));
		starArray[5]=[];
		starArray[5] = Array.from({ length }, () => (parseInt(Math.random()*100)%controls.maxColNum));
		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				window.setCSingleColor(i,starArray[i][j], color[0],color[1],color[2],1);
			}
		}
	}
}
// 9:单熄灭/踏雪无痕[七彩ok纯色NG]
controls.single_off = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let display=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let hold=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let turnon=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let Colorful=[];
	let brightness=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let holdcnt=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	singleoff_init();
	let interval=20;
	let holdTime=1000;
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 9){
			clearInterval(t11);
			return;
		}
		singleoff_handle();
	}, interval);
	function singleoff_init()
	{
		let i=0;
		let j=0;

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				var temp=i*controls.maxColNum+j;
				brightness[temp]=10;
				Colorful[temp]=[];
				Colorful[temp][0]=color[0];
				Colorful[temp][1]=color[1];
				Colorful[temp][2]=color[2];
				window.setCSingleColor(i,j,color[0],color[1],color[2],(brightness[temp]/10).toFixed(1));
			}
		}
	}
	function singleoff_handle()
	{
		let i=0;
		let j=0;
		let index=0;

		if(controls.click)
		{
			controls.click=false;
			index=controls.row*controls.maxColNum+controls.col;
			holdcnt[index]=0;
			hold[index]=false;
			turnon[index]= false;
			display[index]=true;
		}

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var temp=i*controls.maxColNum+j;
				if(display[temp])
				{
					// turn off itself
					brightness[temp] = 0;
					var bri=(brightness[temp]/10).toFixed(1);
					window.setCSingleBrightness(i,j,(brightness[i*controls.maxColNum+j]/10).toFixed(1));
					// window.setCSingleColor(controls.row, controls.col, Colorful[temp][0], Colorful[temp][1], Colorful[temp][2],bri);
					turnon[temp] = false;
					display[temp]=false;
					hold[temp]=true;
					holdcnt[temp]=0;
				}
				else if(hold[temp])
				{
					holdcnt[temp]++;
					if(holdcnt[temp] >= parseInt(holdTime/interval))
					{
						holdcnt[temp]=0;
						hold[temp]=false;
						turnon[temp]=true;
					}
				}
				else if(turnon[temp])
				{
					if(brightness[temp]<10)
					{
						holdcnt[temp]++;
						if(holdcnt[temp] > 3)
						{
							holdcnt[temp]=0;
							brightness[temp]+=1;
							var bri=(brightness[temp]/10).toFixed(1);
							window.setCSingleBrightness(i,j,bri);
							if(brightness[temp]==10)
							{
								turnon[temp]=false;
							}
						}
					}
				}
			}
		}
	}
}
// 10:川流不息[七彩ok纯色NG]
controls.flow = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let led_table = new Uint8Array(10*controls.maxColNum);
	let bri_table = [0.0,0.2,0.4,0.6,0.8,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,1.0,];
	let cnt = 0;
	let start=true;
	let r_table = new Uint8Array(bri_table.length);
	let g_table = new Uint8Array(bri_table.length);
	let b_table = new Uint8Array(bri_table.length);
	let head=0;
	let tail=0;
	flow_init();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 10){
			clearInterval(t11);
			return;
		}

		flow_handle();
	}, 60);
	function flow_init()
	{
		let i=0;
		let j=0;
		for(i=0;i < 10;i++)
		{
			if(i<controls.maxRowNum)
			{
				if((i%2)==0)
				{
					for(j=0;j < controls.maxColNum;j++)
					{
						led_table[i*controls.maxColNum+j]=i*controls.maxColNum+j;
					}
				}
				else
				{
					for(j=0;j < controls.maxColNum;j++)
					{
						led_table[i*controls.maxColNum+j]=i*controls.maxColNum+(controls.maxColNum-j-1);
					}
				}
			}
			else
			{
				if((i%2)!=0)
				{
					for(j=0;j < controls.maxColNum;j++)
					{
						led_table[i*controls.maxColNum+j]=((10-i)*controls.maxColNum)+controls.maxColNum-j-1;
					}
				}
				else
				{
					for(j=0;j < controls.maxColNum;j++)
					{
						led_table[i*controls.maxColNum+j]=(10-i)*controls.maxColNum+j;
					}
				}
			}
		}
		for(i=0;i<bri_table.length;i++)
		{
			var color=window.getRandColor();
			r_table[i]=color[0];
			g_table[i]=color[1];
			b_table[i]=color[2];
		}
	}
	function flow_handle()
	{
		let i=0;
		let index=led_table[cnt];
		let row = parseInt(index/controls.maxColNum);
		let col = index%controls.maxColNum;
		let len=bri_table.length;

		// let bri = new

		if(tail >= head) len=tail-head+1;
		else len=tail+10*controls.maxColNum-head+1;

		// log('cnt='+cnt+'head='+head+',tail='+tail+',length='+len+',led_head'+led_table[head]+',led_tail'+led_table[tail]);
		// log('head='+head+',tail='+tail+',length='+len);
		if(start)
		{
			for(i=0;i<len;i++)
			{
				index=led_table[tail-i];
				row = parseInt(index/controls.maxColNum);
				col = index%controls.maxColNum;
				var r=r_table[bri_table.length-1-i];
				var g=g_table[bri_table.length-1-i];
				var b=b_table[bri_table.length-1-i];
				var bri=bri_table[bri_table.length-1-i];
				// log('i='+i+'head='+head+',tail='+tail+',length='+len+',index='+index+',row='+row+',col='+col+',r='+r+',g='+g+',b='+b+',bri='+bri);
				window.setCSingleColor(row,col,r,g,b,bri);
			}
			if(len >= bri_table.length)
			{
				start=false;
			}
		}
		else
		{
			index=led_table[tail-i];
			for(i=0;i<len;i++)
			{
				if(tail>=i) index=led_table[tail-i];
				else index=led_table[tail+10*controls.maxColNum-i];
				row = parseInt(index/controls.maxColNum);
				col = index%controls.maxColNum;
				var r=r_table[bri_table.length-1-i];
				var g=g_table[bri_table.length-1-i];
				var b=b_table[bri_table.length-1-i];
				var bri=bri_table[bri_table.length-1-i];
				// log('i='+i+'head='+head+',tail='+tail+',length='+len+',index='+index+',row='+row+',col='+col+',r='+r+',g='+g+',b='+b+',bri='+bri);
				window.setCSingleColor(row,col,r,g,b,bri);
			}
		}

		cnt++;
		if(cnt >= 10*controls.maxColNum)
		{
			cnt=0;
		}

		tail=cnt;
		if(len >= bri_table.length)
		{
			head=(tail + 10*controls.maxColNum-(bri_table.length-1))%(10*controls.maxColNum);
		}
	}
}
// 11:随波逐流[七彩ok纯色NG]
controls.piano = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let cnt = 0;
	let ch=new Uint16Array(21);
	let signal=new Uint8Array(21);
	let changeOrder=0;
	let waveCount=0;
	let LED_TWINKLE=[];
	wave_init();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 11){
			clearInterval(t11);
			return;
		}
		wave_mem_handle();

		wave_handle();
	}, 10);
	function led_strip_hsv2rgb(h, s, v)
	{
		let color = new Uint8Array(3);
		let rgb_max = parseInt(v * 255/100);
		let rgb_min = parseInt(rgb_max * (100 - s) / 100);
	
		h %= 480; // h -> [0,360]
		let i = parseInt(h / 80);
		let diff = h % 80;
	
		// RGB adjustment amount by hue
		let rgb_adj = parseInt((rgb_max - rgb_min) * diff / 80);
	
		switch (i) {
		case 0:
			color[0] = rgb_max;
			color[1] = rgb_min + rgb_adj;
			color[2] = rgb_min;
			break;
		case 1:
			color[0] = rgb_max - rgb_adj;
			color[1] = rgb_max;
			color[2] = rgb_min;
			break;
		case 2:
			color[0] = rgb_min;
			color[1] = rgb_max;
			color[2] = rgb_min + rgb_adj;
			break;
		case 3:
			color[0] = rgb_min;
			color[1] = rgb_max - rgb_adj;
			color[2] = rgb_max;
			break;
		case 4:
			color[0] = rgb_min + rgb_adj;
			color[1] = rgb_min;
			color[2] = rgb_max;
			break;
		default:
			color[0] = rgb_max;
			color[1] = rgb_min;
			color[2] = rgb_max - rgb_adj;
			break;
		}
		return color;
	}
	function wave_init()
	{
		let i=0;
        if(controls.colorMode==0)
        {
            for(i = 0; i < 21; i++)
            {
                ch[i] = parseInt(3 * 100 * i / 21);
                var color=ledChangeSmooth(ch[i], 100);
				LED_TWINKLE[i]=[];
				LED_TWINKLE[i][0]=color[0];
				LED_TWINKLE[i][1]=color[1];
				LED_TWINKLE[i][2]=color[2];
                signal[i] = 0;
            }
            signal[0] = 1;
        }
        else
        {
            for(i = 0; i < 126; i++)
            {
				var color=getColor(controls.colorMode)
				LED_TWINKLE[i]=[];
				LED_TWINKLE[i][0]=color[0];
				LED_TWINKLE[i][1]=color[1];
				LED_TWINKLE[i][2]=color[2];
                LED_TWINKLE[i] = u32SclkColorNum;
            }
        }
	}
	function wave_mem_handle()
	{
		// let tmp=0;
	
		// if(u32SclkColorNum > 0)
		// {
		// 	tmp = (waveCount & WAVEFORM_2_COUNT_MASK) >> 8;
	
		// 	for(uint8_t i = 0; i < 6; i++)
		// 	{
		// 		LED_STATUS[(waveCount & WAVEFORM_1_COUNT_MASK) * KEYB_Dev.ui8row + i] |= LED_STATUS_BREATHE_MASK;
		// 		LED_TWINKLE[(waveCount & WAVEFORM_1_COUNT_MASK) * KEYB_Dev.ui8row + i] = u32SclkColorNum;
		// 		if(waveCount > 10)
		// 		{
		// 			LED_STATUS[(tmp - 10) * KEYB_Dev.ui8row + i] |= LED_STATUS_BREATHE_MASK;
		// 			LED_TWINKLE[(tmp - 10) * KEYB_Dev.ui8row + i] = u32SclkColorNum;
		// 		}
		// 	}
	
		// 	if(tmp >= 10)
		// 	{
		// 		waveCount += 0x100;
		// 	}
	
		// 	waveCount++;
	
		// 	if((waveCount & WAVEFORM_1_COUNT_MASK) == 10)
		// 	{
		// 		waveCount &= ~WAVEFORM_2_COUNT_MASK;
		// 		waveCount |= 0xA00;
		// 	}
	
		// 	if((waveCount & WAVEFORM_1_COUNT_MASK) >= 21)
		// 	{
		// 		waveCount &= ~WAVEFORM_1_COUNT_MASK;
		// 	}
		// }
	}
	function wave_handle()
	{
		let i=0;
		let col=0;

		cnt += 6;
		if(cnt >= 6 * 21)
		{
			cnt = 0;
		}
	
		if(controls.colorMode==0)
		{
			for(i = 0; i < 21; i++)
			{
				if(signal[i] == 1)
				{
					var color=ledChangeSmooth(ch[i], 100);
					LED_TWINKLE[i]=[];
					LED_TWINKLE[i][0]=color[0];
					LED_TWINKLE[i][1]=color[1];
					LED_TWINKLE[i][2]=color[2];
					ch[i]++;
					if(ch[i] >= (3 * 100))
					{
						ch[i] = 0;
						if(i != 0)
						{
							signal[i] = 0;
						}
					}
				}
			}
			
			for(i = 0; i < 20; i++)
			{
				if(ch[i] >= (ch[i + 1] + 8))
				{
					signal[i + 1] = 1;
				}
			}

			col = parseInt(cnt / 6);
			for(i = 0; i < 6; i++)
			{
				window.setCSingleColor(i, col, LED_TWINKLE[col][0], LED_TWINKLE[col][1], LED_TWINKLE[col][2]);
			}
		}
		else
		{
			// pKeybDev->LED.ui32FrameTime++;
			// if(pKeybDev->LED.ui32FrameTime >= pKeybDev->LED.ui32FrameTimeMax)
			// {
			// 	pKeybDev->LED.ui32FrameTime = 0;
			// 	pKeybDev->LED.ui32FrameTimeMax = getUpdateOnPeriod2(pKeybDev->LED.ui8Shift32);
			// 	wave_mem_handle();
			// }
	
			// for(i = 0; i < 6; i++)
			// {
			// 	if(LED_STATUS[cnt + i] & LED_STATUS_BREATHE_MASK)
			// 	{
			// 		LED_STATUS[cnt + i]++;
			// 		if((LED_STATUS[cnt + i] & 0xFF) >= getUpdateOffPeriod1(pKeybDev->LED.ui8Shift32))
			// 		{
			// 			LED_STATUS[cnt + i] &= ~0xFF;
			// 			if((LED_TWINKLE[cnt + i] & 0xF8000000) != 0xF8000000)
			// 			{
			// 				LED_TWINKLE[cnt + i] += 0x8000000;
			// 			}
			// 			else
			// 			{
			// 				LED_STATUS[cnt + i] &= ~LED_STATUS_BREATHE_MASK;
			// 			}
			// 		}
			// 	}
			// 	LED_BASIC[i] = LED_TWINKLE[cnt + i];
			// 	LED_BASIC[i] &= ~COLOR_SETTING_MASK;
			// 	LED_BASIC[i] |= u32SclkColorNum;
			// }
		}
	}
}
// 12:单点亮/如影随形[七彩ok纯色NG]
controls.single_on = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let display=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let hold=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let turnoff=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let Colorful=[];
	let brightness=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	let holdcnt=new Uint8Array(controls.maxRowNum*controls.maxColNum);
	singleon_init();
	let interval=20;
	let holdTime=1000;
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 12){
			clearInterval(t11);
			return;
		}
		singleon_handle();
	}, interval);
	function singleon_init()
	{
		let i=0;
		let j=0;

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var color=window.getRandColor();
				Colorful[i*controls.maxColNum+j]=[];
				Colorful[i*controls.maxColNum+j][0]=color[0];
				Colorful[i*controls.maxColNum+j][1]=color[1];
				Colorful[i*controls.maxColNum+j][2]=color[2];
			}
		}
	}
	function singleon_handle()
	{
		let i=0;
		let j=0;
		let index=0;

		if(controls.click)
		{
			controls.click=false;
			index=controls.row*controls.maxColNum+controls.col;
			holdcnt[index]=0;
			hold[index]=false;
			turnoff[index]= false;
			display[index]=true;
		}

		for(i=0;i<controls.maxRowNum;i++)
		{
			for(j=0;j<controls.maxColNum;j++)
			{
				var temp=i*controls.maxColNum+j;
				if(display[temp])
				{
					// turn on itself
					// index=controls.row*controls.maxColNum+controls.col;
					brightness[temp] = 10;
					var bri=(brightness[temp]/10).toFixed(1);
					window.setCSingleColor(i, j, Colorful[temp][0], Colorful[temp][1], Colorful[temp][2],bri);
					turnoff[temp] = false;
					display[temp]=false;
					hold[temp]=true;
					holdcnt[temp]=0;
				}
				else if(hold[temp])
				{
					holdcnt[temp]++;
					if(holdcnt[temp] >= parseInt(holdTime/interval))
					{
						holdcnt[temp]=0;
						hold[temp]=false;
						turnoff[temp]=true;
					}
				}
				else if(turnoff[temp])
				{
					if(brightness[temp]>0)
					{
						holdcnt[temp]++;
						if(holdcnt[temp] > 3)
						{
							holdcnt[temp]=0;
							brightness[temp]-=1;
							var bri=(brightness[temp]/10).toFixed(1);
							window.setCSingleBrightness(i,j,(brightness[i*controls.maxColNum+j]/10).toFixed(1));
							if(brightness[temp]==0)
							{
								turnoff[temp]=false;
							}
						}
					}
				}
			}
		}
	}
}
// 13:正炫光波[七彩ok纯色NG]
controls.sinu_wave = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let cnt = 0;
	let LED_SINE_BUFF=new Uint8Array(126);
	let LED_STATUS=new Uint8Array(126);
	let mask=new Uint8Array(126);
	let delay=0;
	let LED_SINE= new Array(
		0, 0, 0, 0, 0, 1,
		0, 0, 0, 0, 1, 0,
		0, 0, 0, 1, 0, 0,
		0, 0, 1, 0, 0, 0,
		0, 1, 0, 0, 0, 0,
		1, 0, 0, 0, 0, 0,
		1, 0, 0, 0, 0, 0,
		1, 0, 0, 0, 0, 0,
		0, 1, 0, 0, 0, 0,
		0, 0, 1, 0, 0, 0,
		0, 0, 0, 1, 0, 0,
		0, 0, 0, 0, 1, 0,
		0, 0, 0, 0, 0, 1,
		0, 0, 0, 0, 0, 1,
		0, 0, 0, 0, 0, 1,
		0, 0, 0, 0, 1, 0,
		0, 0, 0, 1, 0, 0,
		0, 0, 1, 0, 0, 0,
		0, 1, 0, 0, 0, 0,
		1, 0, 0, 0, 0, 0,
		1, 0, 0, 0, 0, 0,
	);
	let LED_TWINKLE=[];
	sine_init();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 13){
			clearInterval(t11);
			return;
		}
		if(delay>=3)
		{
			delay=0;
			sine_process();
		}
		delay++;
		sine_handle();
	}, 10);
	function sine_init()
	{
		let i=0;
        if(controls.colorMode==0)
        {
            for(i = 0; i < 126; i++)
            {
                if(LED_SINE[i] != 1)
                {
					LED_TWINKLE[i]=[];
                    LED_TWINKLE[i][0] = 0;
                    LED_TWINKLE[i][1] = 0;
                    LED_TWINKLE[i][2] = 0;
                }
            }
            for(i = 0; i < 126; i++)
            {
				LED_TWINKLE[i]=[];
				LED_TWINKLE[i][0] = 0;
				LED_TWINKLE[i][1] = 0;
				LED_TWINKLE[i][2] = 0;
                if(LED_SINE[i] == 1)
                {
					var color=window.getRandColor();
					LED_TWINKLE[i][0] = color[0];
					LED_TWINKLE[i][1] = color[1];
					LED_TWINKLE[i][2] = color[2];
				}
            }
        }
        else
        {
            for(i = 0; i < 126; i++)
            {
				var color=window.getColor(controls.colorMode);
				LED_TWINKLE[i]=[];
				LED_TWINKLE[i][0] = 0;
				LED_TWINKLE[i][1] = 0;
				LED_TWINKLE[i][2] = 0;
                LED_STATUS[i] = 0;
            }
        }
        
        for(i = 0; i < 126; i++)
        {
            LED_SINE_BUFF[i] = LED_SINE[i];
        }
	}
	function sine_process()
	{
		let i=0;
		let j=0;
		let temp=new Uint8Array(6);
	
		for(i = 0; i < 6; i++)
		{
			temp[i] = LED_SINE_BUFF[i];
		}
	
		for(i = 0; i < 20; i++)
		{
			for(j = 0; j < 6; j++)
			{
				LED_SINE_BUFF[i * 6 + j] = LED_SINE_BUFF[(i + 1) * 6 + j];
			}
		}
	
		for(i = 0; i < 6; i++)
		{
			LED_SINE_BUFF[120 + i] = temp[5 - i];
		}
	
		if(controls.colorMode==0)
		{
			var index=parseInt(Math.random() * (100-1)+1)%8;
			for(i = 0; i < 126; i++)
			{
				if(LED_SINE_BUFF[i] == 1)
				{
					var color = getColor((index + i) % 8);
					LED_TWINKLE[i]=[];
					LED_TWINKLE[i][0]=color[0];
					LED_TWINKLE[i][1]=color[1];
					LED_TWINKLE[i][2]=color[2];
				}
			}
		}
		else
		{
			for(i = 0; i < 126; i++)
			{
				if(LED_SINE_BUFF[i] == 1)
				{
					var color = getColor(controls.colorMode);
					LED_TWINKLE[i]=[];
					LED_TWINKLE[i][0]=color[0];
					LED_TWINKLE[i][1]=color[1];
					LED_TWINKLE[i][2]=color[2];
				}
			}
		}
	}
	function sine_handle()
	{
		let i=0;
		cnt += 6;
		if(cnt == controls.maxRowNum*controls.maxColNum)
		{
			cnt = 0;
		}
		var col=parseInt(cnt/6);
		if(controls.colorMode==0)
		{
			for(i = 0; i < 6; i++)
			{
				if((LED_TWINKLE[cnt+i][0]!=0)||(LED_TWINKLE[cnt+i][0]!=0)||(LED_TWINKLE[cnt+i][0]!=0))
				{
					LED_STATUS[cnt + i]+=4;
					if(((LED_STATUS[cnt + i] & 0xFF)) > 50)
					{
						LED_STATUS[cnt + i] = ~0xFF;
						if((mask[cnt + i] & 0xF8000000) != 0xF8000000)
						{
							mask[cnt + i] += 0x8000000;
						}
						else
						{
							mask[cnt + i] = 0;
							LED_TWINKLE[cnt + i][0]=0;
							LED_TWINKLE[cnt + i][1]=0;
							LED_TWINKLE[cnt + i][2]=0;
						}
					}
				}
				
				window.setCSingleColor(i,col,LED_TWINKLE[cnt + i][0],LED_TWINKLE[cnt + i][1],LED_TWINKLE[cnt + i][2])
			}
		}
		else
		{
			// for(i = 0; i < 6; i++)
			// {
			// 	if(LED_TWINKLE[cnt + i] != 0)
			// 	{
			// 		LED_STATUS[cnt + i]++;
			// 		if(((LED_STATUS[cnt + i] & 0xFF)) > getUpdateOffPeriod2(pKeybDev->LED.ui8Shift32)) // use KBCU_LEDx bit0~bit7 to count
			// 		{
			// 			LED_STATUS[cnt + i] = ~0xFF;
			// 			if((LED_TWINKLE[cnt + i] & 0xF8000000) != 0xF8000000)
			// 			{
			// 				LED_TWINKLE[cnt + i] += 0x8000000;
			// 			}
			// 			else
			// 			{
			// 				LED_TWINKLE[cnt + i] = 0;
			// 			}
			// 		}
			// 	}
				
			// 	LED_BASIC[i] = LED_TWINKLE[cnt + i];
			// }
		}
	}
}
// 14:峰回路转/旋转风车ok
controls.vortex = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let signalBk=new Uint8Array(42);
	let signal=new Uint8Array(42);
	let chBk=new Uint16Array(42);
	let ch=new Uint16Array(42);
	let ledBk=[];
	let LED_TWINKLE=[];
	VortexInit();
	let cnt=0;
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 14){
			clearInterval(t11);
			return;
		}
		vortexProcess();

		cnt=cnt+6;
		if(cnt >=controls.maxRowNum*controls.maxColNum)
		{
			cnt=0;
		}
		var col=parseInt(cnt/6);
		window.setCSingleColor(0,col,LED_TWINKLE[col][0],LED_TWINKLE[col][1],LED_TWINKLE[col][2]);
		window.setCSingleColor(1,col,LED_TWINKLE[col][0],LED_TWINKLE[col][1],LED_TWINKLE[col][2]);
		window.setCSingleColor(2,col,LED_TWINKLE[col][0],LED_TWINKLE[col][1],LED_TWINKLE[col][2]);
		window.setCSingleColor(3,col,LED_TWINKLE[41-col][0],LED_TWINKLE[41-col][1],LED_TWINKLE[41-col][2]);
		window.setCSingleColor(4,col,LED_TWINKLE[41-col][0],LED_TWINKLE[41-col][1],LED_TWINKLE[41-col][2]);
		window.setCSingleColor(5,col,LED_TWINKLE[41-col][0],LED_TWINKLE[41-col][1],LED_TWINKLE[41-col][2]);
	}, 15);
	function VortexInit()
	{
		let j=0;
		let i=0;

		signalBk[0] = 1;
	
		for(j = 0; j < 420; j++)
		{
			for(i = 0; i < 41; i++)
			{
				if(chBk[i] >= (chBk[i + 1] + 10))
				{
					signalBk[i + 1] = 1;
				}
			}
	
			for(i = 0; i < 42; i++)
			{
				if(signalBk[i] == 1)
				{
					var color=ledChangeSmooth(chBk[i], 140);
					ledBk[i]=[];
					ledBk[i][0]=color[0];
					ledBk[i][1]=color[1];
					ledBk[i][2]=color[2];
					chBk[i]++;
					if(chBk[i] >= (420))
					{
						chBk[i] = 0;
					}
				}
			}
		}
		for(i = 0; i < 42; i++)
		{
			signal[i]=signalBk[i];
			ch[i]=chBk[i];
			LED_TWINKLE[i]=[];
			LED_TWINKLE[i][0]=ledBk[i][0];
			LED_TWINKLE[i][1]=ledBk[i][1];
			LED_TWINKLE[i][2]=ledBk[i][2];
		}
	}
	function vortexProcess()
	{
		let i=0;
		for(i = 0; i < 42; i++)
		{
			var color=ledChangeSmooth(ch[i], 140);
			LED_TWINKLE[i]=[];
			LED_TWINKLE[i][0]=color[0];
			LED_TWINKLE[i][1]=color[1];
			LED_TWINKLE[i][2]=color[2];
			ch[i]++;
			if(ch[i] >= (420))
			{
				ch[i] = 0;
			}
		}
	}
}
// 15:多彩纵横/七彩瀑布ok
controls.tide = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let LED_TWINKLE=[];
	let ch=new Uint16Array(21);
	let cnt=0;
	TideInit();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 15){
			clearInterval(t11);
			return;
		}
		TideProcess();
		tideHandle();
	}, 7);
	function TideInit()
	{
		let i=0;
		for(i = 0; i < 10; i++)
		{
			ch[i] = parseInt(3 * 100 * (10 - i)/ 10);
			var color=ledChangeSmooth(ch[i], 100);
			LED_TWINKLE[i]=[];
			LED_TWINKLE[i][0]=color[0];
			LED_TWINKLE[i][1]=color[1];
			LED_TWINKLE[i][2]=color[2];
		}
	}
	function TideProcess()
	{
		let i=0;
		for(i = 0; i < 10; i++)
		{
			var color=ledChangeSmooth(ch[i], 100);
			LED_TWINKLE[i]=[];
			LED_TWINKLE[i][0]=color[0];
			LED_TWINKLE[i][1]=color[1];
			LED_TWINKLE[i][2]=color[2];
			ch[i] += 2;
			if(ch[i] >= (3 * 100))
			{
				ch[i] = 0;
			}
		}
	}
	function tideHandle()
	{
		let i=0;
		let col=0;
		cnt += 6;
		if(cnt >= controls.maxRowNum * controls.maxColNum)
		{
			cnt = 0;
		}
		col=parseInt(cnt/6);
		for(i = 0; i < 6; i++)
		{
			window.setCSingleColor(i,col,LED_TWINKLE[i][0],LED_TWINKLE[i][1],LED_TWINKLE[i][2]);
		}
	}
}
// 16:花开富贵ok
controls.rich_honor = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	let cnt=0;
	let changeOrder=0;
	let ch=new Uint16Array(21);
	let signal=new Uint16Array(21);
	let LED_TWINKLE=[];
	RichInit();
	let t11 = null;
	t11 = setInterval(function(){
		if(controls.leds != 16){
			clearInterval(t11);
			return;
		}
		RichProcess();

		RichHandle();
	}, 6);
	function RichInit()
	{
		let i=0;
		for(i = 0; i < 12; i++)
		{
			ch[i] = parseInt(3 * 100 * i / 12);
			var color=ledChangeSmooth(ch[i], 100);
			LED_TWINKLE[i]=[];
			LED_TWINKLE[i][0] = color[0];
			LED_TWINKLE[i][1] = color[1];
			LED_TWINKLE[i][2] = color[2];
			signal[i] = 0;
		}
		signal[0] = 1;
	}
	function RichProcess()
	{
		let i=0;
		
		if(signal[changeOrder] == 1)
		{
			var color=ledChangeSmooth(ch[changeOrder], 100);
			LED_TWINKLE[changeOrder]=[];
			LED_TWINKLE[changeOrder][0] = color[0];
			LED_TWINKLE[changeOrder][1] = color[1];
			LED_TWINKLE[changeOrder][2] = color[2];
			ch[changeOrder] += 6;// 可以加减时间
			if(ch[changeOrder] >= (3 * 100))
			{
				ch[changeOrder] = 0;
			}
		}
		
		for(i = 0; i < 11; i++)
		{
			if(ch[i] >= (ch[i + 1] + 20))
			{
				signal[i + 1] = 1;
			}
		}
		
		changeOrder++;
		if(changeOrder >= 12)
		{
			changeOrder = 0;
		}
	}
	function RichHandle()
	{
		let i=0;
		let col=0;
		cnt += 6;
		if(cnt >= controls.maxRowNum * controls.maxColNum)
		{
			cnt = 0;
		}
		col=parseInt(cnt/6);
		if(col <= 8)
		{
			for(i = 0; i < 6; i++)
			{
				window.setCSingleColor(i,col,LED_TWINKLE[11 - col][0],LED_TWINKLE[11 - col][1],LED_TWINKLE[11 - col][2]);
			}
		}
		else if(col < 12)
		{
			if((col == 9) || (col == 11))
			{
				window.setCSingleColor(0,col,LED_TWINKLE[2][0],LED_TWINKLE[2][1],LED_TWINKLE[2][2]);
				window.setCSingleColor(1,col,LED_TWINKLE[1][0],LED_TWINKLE[1][1],LED_TWINKLE[1][2]);
				window.setCSingleColor(2,col,LED_TWINKLE[1][0],LED_TWINKLE[1][1],LED_TWINKLE[1][2]);
				window.setCSingleColor(3,col,LED_TWINKLE[1][0],LED_TWINKLE[1][1],LED_TWINKLE[1][2]);
				window.setCSingleColor(4,col,LED_TWINKLE[2][0],LED_TWINKLE[2][1],LED_TWINKLE[2][2]);
				window.setCSingleColor(5,col,LED_TWINKLE[3][0],LED_TWINKLE[3][1],LED_TWINKLE[3][2]);
			}
			else
			{
				window.setCSingleColor(0,col,LED_TWINKLE[2][0],LED_TWINKLE[2][1],LED_TWINKLE[2][2]);
				window.setCSingleColor(1,col,LED_TWINKLE[1][0],LED_TWINKLE[1][1],LED_TWINKLE[1][2]);
				window.setCSingleColor(2,col,LED_TWINKLE[0][0],LED_TWINKLE[0][1],LED_TWINKLE[0][2]);
				window.setCSingleColor(3,col,LED_TWINKLE[1][0],LED_TWINKLE[1][1],LED_TWINKLE[1][2]);
				window.setCSingleColor(4,col,LED_TWINKLE[2][0],LED_TWINKLE[2][1],LED_TWINKLE[2][2]);
				window.setCSingleColor(5,col,LED_TWINKLE[3][0],LED_TWINKLE[3][1],LED_TWINKLE[3][2]);
			}
		}
		else
		{
			for(i = 0; i < 6; i++)
			{
				window.setCSingleColor(i,col,LED_TWINKLE[col - 10][0],LED_TWINKLE[col - 10][1],LED_TWINKLE[col - 10][2]);
			}
		}
	}
}
// 17:游戏模式ok
controls.game_mode = function() {
	setLedType(controls.leds);
	if(ledRunState[controls.leds-1] == true){return;}//正在运行时，直接返回
	ledRunState[controls.leds-1]=true;
	window.clearAllKeys();
	window.setSingleColor("Esc", 0,0,255);
	window.setSingleColor("A", 0,0,255);
	window.setSingleColor("S", 0,0,255);
	window.setSingleColor("D", 0,0,255);
	window.setSingleColor("W", 0,0,255);
	window.setSingleColor("Up", 0,0,255);
	window.setSingleColor("Down", 0,0,255);
	window.setSingleColor("Left", 0,0,255);
	window.setSingleColor("Right", 0,0,255);
}

function SwitchLed(type)
{
	log('Switch led '+type);
	switch(type)
	{
	case 1:// 静态常亮
		controls.static();
		break;
	case 2:// 动态呼吸
		controls.breathe();
		break;
	case 3:// 梦幻彩虹
		controls.dream_rainbow();
		break;
	case 4:// 一触即发
		controls.touch();
		break;
	case 5:// 雨中散步
		controls.walk_rain();
		break;
	case 6:// 彩虹轮盘
		controls.roul_rainbow();
		break;
	case 7:// 涟漪扩散
		controls.dimple();
		break;
	case 8:// 繁星点点
		controls.star();
		break;
	case 9:// 单熄灭/踏雪无痕
		controls.single_off();
		break;
	case 10:// 川流不息
		controls.flow();
		break;
	case 11:// 随波逐流
		controls.piano();
		break;
	case 12:// 单点亮/如影随形
		controls.single_on();
		break;
	case 13:// 正炫光波
		controls.sinu_wave();
		break;
	case 14:// 峰回路转/旋转风车
		controls.vortex();
		break;
	case 15:// 多彩纵横/七彩瀑布
		controls.tide();
		break;
	case 16:// 花开富贵
		controls.rich_honor();
		break;
	case 17:// 游戏模式
		controls.game_mode();
		break;
	default:// 随波逐流
		controls.piano();
		break;
	}
}

{
	// let sat = 0;
	// let timer = 0;
	// let dir = true;
	// window.setCSingleColor(0,0,255,0,0,1);
	SwitchLed(controls.leds);
	// timer = 10;
	// setInterval(function() {
	// if(!dir)
	// {
	// 	if(timer > 0)
	// 	{
	// 		timer--;
			
	// 	}
	// 	dir= (0==timer);
	// }
	// else
	// {
	// 	if(timer < 10)
	// 	{
	// 		timer++;
			
	// 	}
	// 	dir= (10==timer)? false:true;
	// }
	// sat = (timer/10).toFixed(1);
	// window.setCSingleBrightness(0,0,sat);
	// }, 200);
}
function log(content)
{
	if(!controls.logen) return;
	console.log(content+'\n');
}
function setLedType(type)
{
	for(let i = 0; i < controls.maxLedNum;i++)
	{
		if((i+1) != type)
		{
			ledRunState[i] = false;
		}
	}
}
function setColumnColor(col, r,g,b,a=1)
{
	for(let row = 0;row<controls.maxRowNum;row++)
	{
		window.setCSingleColor(row, col, r,g,b,a);
	}
}
function setRowColor(row, r,g,b,a=1)
{
	for(let col = 0;col<controls.maxColNum;col++)
	{
		window.setCSingleColor(row, col, r,g,b,a);
	}
}
/* ledNum:灯效序号，取值1-17
 * color: 颜色序号，取值0-7
 * index: 按键序号，取值0-127，默认值0表示无按键按下，按下后一次有效
 * brightness: 亮度，取值1-5
 * speed: 速度，取值1-5
 */
function setUserData(ledNum,color,index,brightness,speed)
{
	var led=parseInt(ledNum) + 1;
	// alert('设置ledNum 为' + led)
	// if(led != controls.leds)
	{
		controls.leds=led;
		// controls.colorMode=color;
		SwitchLed(controls.leds);
	}
}
window.setClickKey=function(name)
{
	if((controls.leds==4)||(controls.leds==7)||(controls.leds==9)||(controls.leds==12))
	{
		controls.click=true;
		var obj=window.getRowNCol(name);
		controls.row=obj.row;
		controls.col=obj.col;
		// alert('leds='+controls.leds+',row=' + controls.row+',col='+controls.col)
	}
}