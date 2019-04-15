parser grammar ACLParser;

options { tokenVocab=ACLLexer; }


// Top Level Description

root
	: statement*
	;

statement
	: axisctrl NEWLINE
//	| progctrl NEWLINE
//	| posmanipulation NEWLINE
//	| variable NEWLINE
	| progflow
//	| config NEWLINE
//	| report NEWLINE
	;

// statements
axisctrl
	: move
	| OPEN
	| CLOSE
	| jaw
	| acc
	| speed
	| home
	;

progflow
	: if
	;

// commands
move
	: MOVE INTEGER
	;

jaw
	: JAW INTEGER
	;

acc
	: ACC INTEGER
	;

speed
	: SPEED INTEGER
	;

home
	: HOME INTEGER?
	;

condition
	: (INTEGER | IDENTIFIER) (COMPAREOPERATOR | EQUAL) (INTEGER | IDENTIFIER)
	;

and_or_if
	: (ANDIF | ORIF) condition NEWLINE
	;

if
	: IF condition NEWLINE and_or_if* statement+ else? ENDIF NEWLINE
	;

else
	: ELSE statement+ NEWLINE
	;


