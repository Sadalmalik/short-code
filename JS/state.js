//	Async State Machine!

var AsyncStateMachine = function()
	{
	var states={};
	var executing=false;
	var next, data;
	//====================================================================================================//
	this.setState	=	function( name , act )	{	if( act )	states[name]=act;	};
	this.delState	=	function( name )		{	if( !(/#init|#error|#exit/.exec(name)) )	delete( states[name] );	};
	//== Technical functions =============================================================================//
	var eval = function(  )
		{
		try	{
			if( !states[next] )	throw new Error("Unknown state '"+next+"' !");
			states[next]( data , (next=='#exit') ? (exit) : (back) );
			}
		catch( errr )	{	states['#error']( errr , back );	}
		}
	var back = function( name )	{	next=name;	if( executing )	setImmediate( eval );	}
	var exit = function( name )	{	console.log( 'State Machine stoped!' );	}
	//== Main cycle ======================================================================================//
	this.start	=	function( input )	{	if(!executing)with(this){data=input;executing=true;}	back( '#init' );	};
	this.stop	=	function()			{	if( executing)executing=false;	}
	//== Default states ==================================================================================//
	this.setState( '#init'	,	function( data , call ){console.log('#init	'+data);call('#exit');} );
	this.setState( '#error'	,	function( errr , call ){console.log('#error	'+errr);call('#exit');} );
	this.setState( '#exit'	,	function( data , call ){console.log('#exit	'+data);call('#exit');} );
	}
module.exports = AsyncStateMachine;