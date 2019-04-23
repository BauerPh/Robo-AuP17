lexer grammar ACLLexer;

// Axis Control Commands
MOVE:			'MOVE';	
OPEN:			'OPEN';
CLOSE:			'CLOSE';
JAW:			'JAW';
ACC:			'ACC';	
SPEED:			'SPEED';
HOME:			'HOME';
PARK:			'PARK';

// Program Control
DELAY:			'DELAY';
WAIT:			'WAIT';

// Position def & manipulation
DEFP:			'DEFP';
DELP:			'DELP';
UNDEF:			'UNDEF';
HERE:			'HERE';
TEACH:			'TEACH';
SETPVC:			'SETPVC';
SETPV:			'SETPV';
SHIFTC:			'SHIFTC';
SHIFT:			'SHIFT';
SETP:			'SETP';
BY:				'BY';
PVAL:			'PVAL';
PVALC:			'PVALC';
PSTATUS:		'PSTATUS';

// Variable Definition & manipulation
DEFINE:			'DEFINE';
GLOBAL:			'GLOBAL';
DELVAR:			'DELVAR';
SET:			'SET';

// Program Flow Commands
IF:				'IF';	
ANDIF:			'ANDIF';
ORIF:			'ORIF';		
ELSE:			'ELSE';		
ENDIF:			'ENDIF';	
FOR:			'FOR';
TO:				'TO';
ENDFOR:			'ENDFOR';
LABEL:			'LABEL';	
GOTO:			'GOTO';	

// Report
PRINT:			'PRINT';

// expressions
DECIMAL:			SIGNEDINT '.' [0-9]+;
INTEGER:			'0' | ( [1-9] [0-9]* );
SIGNEDINT:			( '+' | '-' )? INTEGER;
BOOL:				'TRUE' | 'FALSE';
SUMOPERATOR:		'+' | '-';
FACTOROPERATOR:		'*' | '/' | 'EXP' | 'MOD';
BOOLOPERATOR:		'AND' | 'OR';
NOT:				'NOT';
EQUAL:				'=';
COMPAREOPERATOR:	'<>' | '<=' | '>=' | '<' | '>';
OPENBRACKET:		'(';
CLOSEBRACKET:		')';
OPENCURLYBRACKET:	'{';
CLOSECURLYBRACKET:	'}';
COMMA:				',';
STRING:				'"' ~[\r\n\t\f"]* '"';
IDENTIFIER:			[a-zA-Z] [a-zA-Z0-9]*;

NEWLINE:			(COMMENT? ('\r'? '\n')+)+;

// Comment
COMMENT:			'//' ~[\r\n]*				-> skip;

// Skip Whitespaces
WS:					[ \t\f]+					-> skip;

// Unknown token
ERROR_RECONGNIGION: .;				//-> channel(HIDDEN);