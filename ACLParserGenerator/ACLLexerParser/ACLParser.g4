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
	: JAW INTEGER (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER) INTEGER?
	;

acc
	: ACC INTEGER
	;

speed
	: SPEED INTEGER
	;

home
	: HOME INTEGER*
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
	: SETPVC (IDENTIFIER | INTEGER) IDENTIFIER (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER)
	;

setpv
	: SETPV (IDENTIFIER | INTEGER) INTEGER (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER)
	;

shiftc
	: SHIFTC (IDENTIFIER | INTEGER) BY IDENTIFIER (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER)
	;

shift
	: SHIFT (IDENTIFIER | INTEGER) BY INTEGER (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER)
	;

setp
	: SETP IDENTIFIER EQUAL (IDENTIFIER | INTEGER)
	;

define 
	: (DEFINE | GLOBAL) IDENTIFIER+
	;

delvar
	: DELVAR IDENTIFIER
	;

condition
	: (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER) (COMPAREOPERATOR | EQUAL) (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER | BOOL)
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
	: SET IDENTIFIER EQUAL ((PVAL (IDENTIFIER | INTEGER) INTEGER) | (PVALC (IDENTIFIER | INTEGER) IDENTIFIER) | (PSTATUS (IDENTIFIER | INTEGER)))
	;

setvar
	: SET IDENTIFIER EQUAL (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER | BOOL | calculation)
	;

calculation
	: (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER) (SUMOPERATOR | FACTOROPERATOR | BOOLOPERATOR) (DECIMAL | SIGNEDINT | INTEGER | IDENTIFIER)
	;

