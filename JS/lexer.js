//	Simple lexer

var OPER	=	/^(?:&(?:&||=)|\|(?:\|||=)|!(?:=||==)|\+(?:\+||=)|-(?:-||=||>)|\*(?:\*||=)|\/(?:\/||=)|%(?:|=|)|\^(?:|=|)|\\|\~|<{1,2}=?|>{1,3}=?|={1,3}|\.{1,2}|\,|\?|\:{1,2}|\;)/;
var NUMB	=	/^(?:0b[01]+|0o[0-7]+|0x[\da-fA-F]+|\d*\.?\d*(?:e[+-]?\d+)?)/;
var SPEC	=	/^[\s\t\v\f\r\0\n\(\)\{\}\[\]]/;
var SYMB	=	/^[@#$_0-9a-zA-Z]+/;
var LINE	=	/^(?:'(?:[^']|\\')*'|"(?:[^"]|\\")*")/;
var tokenize=	function( reg , chunk )	{	var match = reg.exec( chunk );	return( match?match[0]:void(0) );	}
var Lexer = function( data )
	{
	var source=data||'', _pntr=0, _line=0, _char=0;
	this.next = function()
		{
		var chunk = source.slice( _pntr );
		if( chunk.length==0 )return(void(0));
		var	token	=	{};
		token['type']=	( tokenize(OPER,chunk) || tokenize(NUMB,chunk) || tokenize(SPEC,chunk) || tokenize(SYMB,chunk) || tokenize(LINE,chunk) );
		token['line']=	_line	;	//	For 'Warning' and 'Error' reports
		token['char']=	_char	;
		_pntr+=token['type'].length;
		_char+=token['type'].length;
		if( token['type'].indexOf('\n')>=0 )	{	_line++;	_char=0;	}
		return( token );
		}
	}
module.exports = Lexer;