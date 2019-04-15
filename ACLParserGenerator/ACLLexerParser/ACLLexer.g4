lexer grammar ACLLexer;

// Axis Control Commands
MOVE:			'MOVE';		//okay
OPEN:			'OPEN';
CLOSE:			'CLOSE';
JAW:			'JAW';
ACC:			'ACC';		//okay
SPEED:			'SPEED';	//okay
HOME:			'HOME';

// Program Control
DELAY:			'DELAY';
WAIT:			'WAIT';

// Position def & manipulation
HERER:			'HERER';
HERE:			'HERE';
TEACHR:			'TEACHR';
TEACH:			'TEACH';
SETPVC:			'SETPVC';
SETPV:			'SETPV';
SHIFTC:			'SHIFTC';
SHIFT:			'SHIFT';
SETP:			'SETP';
BY:				'BY';

// Variable Definition & manipulation
DEFINE:			'DEFINE';
DELVAR:			'DELVAR';
SET:			'SET';

// Program Flow Commands
IF:				'IF';		//okay
ANDIF:			'ANDIF';	//okay
ORIF:			'ORIF';		//okay
ELSE:			'ELSE';		//okay
ENDIF:			'ENDIF';	//okay
FOR:			'FOR';
ENDFOR:			'ENDFOR';
LABEL:			'LABEL';	//okay
GOTO:			'GOTO';		//okay

// Config
INIT:			'INIT';

// Report
SHOW:			'SHOW';
PRINTLN:		'PRINTLN';
PRINT:			'PRINT';

// expressions
IDENTIFIER:			[a-zA-Z][a-zA-Z0-9]*;
SIGNEDINT:			( '+' | '-' )+ INTEGER;
INTEGER:			[0-9] [0-9]*;
DECIMAL:			( '+' | '-' )? ( '0' | ( [1-9] [0-9]* ) ) ( '.' [0-9]+ )?;
SUMOPERATOR:		'+' | '-';
FACTOROPERATOR:		'*' | '/';
EQUAL:				'=';
COMPAREOPERATOR:	'<>' | '<=' | '>=' | '<' | '>';
OPENBRACKET:		'(';
CLOSEBRACKET:		')';
OPENCURLYBRACKET:	'{';
CLOSECURLYBRACKET:	'}';
STRING:				'"' ~[\r\n\t\f"]* '"';
NEWLINE:			'\n' | '\r\n' | '\r' | EOF;


// Skip Whitespaces
WS: [ \t\f]+ -> skip;

// Unknown token
ERROR_RECONGNIGION: . -> channel(HIDDEN);