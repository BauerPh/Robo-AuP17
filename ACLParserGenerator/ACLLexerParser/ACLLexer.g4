lexer grammar ACLLexer;

// Axis Control Commands
MOVE:			'MOVE';
OPEN:			'OPEN';
CLOSE:			'CLOSE';
JAW:			'JAW';
ACC:			'ACC';
SPEED:			'SPEED';
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
IF:				'IF';
ANDIF:			'ANDIF';
ORIF:			'ORIF';
ELSE:			'ELSE';
ENDIF:			'ENDIF';
FOR:			'FOR';
ENDFOR:			'ENDFOR';
LABEL:			'LABEL';
GOTO:			'GOTO';

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