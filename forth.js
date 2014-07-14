//	FORTH MACHINE

var STK=[];
var DIT={};
var DEF=null;
var E=function( I )
	{
	var token;
	var state	=	'execute';
	var tokens	=	I.split(/[\s\n]+/);
	var create = function(content){return function(){	tokens = content.concat(tokens);	return 'execute';	}}
	while (typeof (token = tokens.shift()) !== 'undefined')
		{	//	And i think we can destroy this cycle too...
		console.log( '	'+ token +'	'+ state +'	'+ require('util').inspect(STK) );
		if( state=='definition' )
			{
			if(!DEF )	{	DEF={name:token,content:[]};	}
			else{	if (token==';')	{	DIT[DEF.name] = create(DEF.content);	DEF=null;	state='execute';	}
					else			{	DEF.content.push(token);	}	}
			}
		if( state=='execute' )
			{
			if (isNaN(token) && !(token in DIT))
								{	console.error('Error: '+token+' is undefined.');	return;	}
			if (isNaN(token))	{	state	=	DIT[token]();	}
			else				{	STK.push(Number(token));	}
			}
		}
	}
var U = function(arity)	{	return (STK.length < arity) && console.log('Error: stack underflow!');	}
//	PRIMARY
DIT[':']=function(){	return	'definition';	}
DIT[';']=function(){	return	'execute'	;	}
DIT['.']=function(){	if (!U(1))	{	console.log( STK.pop() );	}	return	'execute'	;	}
//	MATH
DIT['+']	=function(){	if (!U(2))	{	STK.push(STK.pop() + STK.pop());	}	return 'execute'	;	}
DIT['-']	=function(){	if (!U(2))	{	STK.push(STK.pop() - STK.pop());	}	return 'execute'	;	}
DIT['*']	=function(){	if (!U(2))	{	STK.push(STK.pop() * STK.pop());	}	return 'execute'	;	}
DIT['/']	=function(){	if (!U(2))	{	STK.push(STK.pop() / STK.pop());	}	return 'execute'	;	}
DIT['MOD']	=function(){	if (!U(2))	{	STK.push(STK.pop() % STK.pop());	}	return 'execute'	;	}
DIT['/MOD']	=function(){	if (!U(2))	{	a=STK.pop();b=STK.pop();STK.push(a%b);STK.push(a/b);	}	return 'execute'	;	}
DIT['SIN']	=function(){	if (!U(1))	{	STK.push(Math.sin( Math.PI*STK.pop()/180 ));	}	return 'execute'	;	}
DIT['COS']	=function(){	if (!U(1))	{	STK.push(Math.cos( Math.PI*STK.pop()/180 ));	}	return 'execute'	;	}
DIT['TAN']	=function(){	if (!U(1))	{	STK.push(Math.tan( Math.PI*STK.pop()/180 ));	}	return 'execute'	;	}
//	STACK
DIT['DUP']	=function(){	if (!U(1))	{	a=STK.pop();				STK.push(a);	STK.push(a);	}	return 'execute'	;	}
DIT['SWAP']	=function(){	if (!U(1))	{	a=STK.pop();b=STK.pop();	STK.push(a);	STK.push(b);	}	return 'execute'	;	}
DIT['DROP']	=function(){	if (!U(1))	{	STK.pop();											}	return 'execute'	;	}

module.exports = { execute : E , dictionary : DIT };