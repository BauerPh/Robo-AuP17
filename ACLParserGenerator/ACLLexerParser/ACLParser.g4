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
	| report NEWLINE
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
	| delay
	;

progflow
	: if
	| label
	| goto
	;

report
	: print
	| println
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

delay
	: DELAY INTEGER
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
	: ELSE NEWLINE statement+
	;

label
	: LABEL IDENTIFIER NEWLINE
	;

goto
	: GOTO IDENTIFIER NEWLINE
	;

println
	: PRINTLN (STRING | IDENTIFIER) NEWLINE
	;

print
	: PRINT (STRING | IDENTIFIER) NEWLINE
	;

