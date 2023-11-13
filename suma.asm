include 'emu8086.inc'
org 100h
MOV AX, 300
PUSH AX
POP AX
; Asignacion i
MOV i, AX
MOV AX, 1
PUSH AX
MOV AX, i
PUSH AX
POP AX
MOV BX, 256
DIV BX
PUSH DX
XOR DX, DX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP AX
; Asignacion k
MOV k, AX
MOV AX, 0
PUSH AX
POP AX
; Asignacion i
MOV i, AX
printn ""
printn "Altura: "
call scan_num
MOV altura, CX
printn ""
printn "for:"
MOV AX, 1
PUSH AX
POP AX
; Asignacion i
MOV i, AX
; FOR: 1
InicioFor1:
MOV AX, i
PUSH AX
MOV AX, altura
PUSH AX
POP BX
POP AX
CMP AX, BX
JA FinFor1
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
; FOR: 2
InicioFor2:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor2
; if: 1
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV  BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif1
printn ""
printn "-"
; else: 1
JMP Eelse1
Eif1:
Eelse1:
INC j
JMP InicioFor2
FinFor2:
printn ""
printn ""
INC i
JMP InicioFor1
FinFor1:
printn ""
printn "while:"
MOV AX, 1
PUSH AX
POP AX
; Asignacion i
MOV i, AX
; WHILE: 0
InicioWhile0:
MOV AX, i
PUSH AX
MOV AX, altura
PUSH AX
POP BX
POP AX
CMP AX, BX
JA FinWhile0
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
; WHILE: 1
InicioWhile1:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinWhile1
; if: 2
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV  BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif2
printn ""
printn "-"
; else: 2
JMP Eelse2
Eif2:
Eelse2:
INC j
JMP InicioWhile1
FinWhile1:
INC i
printn ""
printn ""
JMP InicioWhile0
FinWhile0:
printn ""
printn "do:"
MOV AX, 1
PUSH AX
POP AX
; Asignacion i
MOV i, AX
; DO: 0
InicioDo0:
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
; DO: 1
InicioDo1:
; if: 3
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV  BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif3
printn ""
printn "-"
; else: 3
JMP Eelse3
Eif3:
Eelse3:
INC j
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinDo1
JMP InicioDo1
FinDo1:
INC i
printn ""
printn ""
MOV AX, i
PUSH AX
MOV AX, altura
PUSH AX
POP BX
POP AX
CMP AX, BX
JA FinDo0
JMP InicioDo0
FinDo0:
int 20h
RET
define_scan_num
define_print_num
define_print_num_uns
; V a r i a b l e s
altura dw 0h
i dw 0h
j dw 0h
k dw 0h
END
