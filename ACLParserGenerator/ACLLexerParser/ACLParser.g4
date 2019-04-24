parser grammar ACLParser;

options { tokenVocab=ACLLexer; }


// Top Level Description

root
	: NEWLINE? statement+ EOF
	;

statement
	: axisctrl NEWLINE
	| progctrl NEWLINE
	| posmanipulation NEWLINE
	| variable NEWLINE
	| progflow NEWLINE
	| report NEWLINE
	| setcommand NEWLINE	// pos manipulation OR variable
	;

// statements
axisctrl
	: move
	| openclose
	| jaw
	| acc
	| speed
	| home
	| park
	;

progctrl
	: delay
	| wait
	;

posmanipulation
	: defp
	| delp
	| undef
	| here
	| teach
	| setpvc
	| setpv
	| shiftc
	| shift
	| setp
	;

variable
	: define
	| delvar
	;

progflow
	: if
	| for
	| label
	| goto
	;

report
	: print
	;

setcommand
	: setpos
	| setvar
	;

// commands
move
	: MOVE (INTEGER | IDENTIFIER)
	;

openclose
	: OPEN INTEGER?
	| CLOSE INTEGER?
	;

jaw
	: JAW INTEGER INTEGER
	;

acc
	: ACC INTEGER
	;

speed
	: SPEED INTEGER
	;

home
	: HOME INTEGER+
	;

park
	: PARK INTEGER+
	;

delay
	: DELAY INTEGER
	;

wait
	: WAIT condition
	;

defp
	: DEFP IDENTIFIER
	;

delp
	: DELP IDENTIFIER
	;

undef
	: UNDEF IDENTIFIER
	;

here
	: HERE IDENTIFIER
	;

teach
	: TEACH IDENTIFIER
	;

setpvc
	: SETPVC (IDENTIFIER | INTEGER) IDENTIFIER (SIGNEDINT | INTEGER | IDENTIFIER)
	;

setpv
	: SETPV (IDENTIFIER | INTEGER) INTEGER (SIGNEDINT | INTEGER | IDENTIFIER)
	;

shiftc
	: SHIFTC (IDENTIFIER | INTEGER) BY IDENTIFIER (SIGNEDINT | INTEGER | IDENTIFIER)
	;

shift
	: SHIFT (IDENTIFIER | INTEGER) BY INTEGER (SIGNEDINT | INTEGER | IDENTIFIER)
	;

setp
	: SETP IDENTIFIER EQUAL IDENTIFIER
	;

define 
	: (DEFINE | GLOBAL) IDENTIFIER+
	;

delvar
	: DELVAR IDENTIFIER
	;

condition
	: (SIGNEDINT | INTEGER | IDENTIFIER) (COMPAREOPERATOR | EQUAL) (SIGNEDINT | INTEGER | IDENTIFIER | BOOL)
	;

and_or_if
	: (ANDIF | ORIF) condition NEWLINE
	;

if
	: IF condition NEWLINE and_or_if* statement+ else? ENDIF
	;

else
	: ELSE NEWLINE statement+
	;

for
	: FOR IDENTIFIER EQUAL (SIGNEDINT | INTEGER | IDENTIFIER) TO (SIGNEDINT | INTEGER | IDENTIFIER) NEWLINE statement+ ENDFOR
	;

label
	: LABEL IDENTIFIER
	;

goto
	: GOTO IDENTIFIER
	;

print
	: PRINT (STRING | IDENTIFIER)+
	;

setpos
	: SET IDENTIFIER EQUAL ((PVAL IDENTIFIER INTEGER) | (PVALC IDENTIFIER IDENTIFIER) | (PSTATUS IDENTIFIER))
	;

setvar
	: SET IDENTIFIER EQUAL (SIGNEDINT | INTEGER | IDENTIFIER | BOOL | calculation)
	;

calculation
	: (SIGNEDINT | INTEGER | IDENTIFIER) (SUMOPERATOR | FACTOROPERATOR | BOOLOPERATOR) (SIGNEDINT | INTEGER | IDENTIFIER)
	;

