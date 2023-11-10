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
; For: 1
MOV AX, 0
PUSH AX
POP AX
; Asignacion i
MOV i, AX
; FOR: 1
InicioFor1:
MOV AX, i
PUSH AX
MOV AX, 10
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor1
printn "Hola"
; For: 2
MOV AX, 0
PUSH AX
POP AX
; Asignacion j
MOV j, AX
; FOR: 2
InicioFor2:
MOV AX, j
PUSH AX
MOV AX, 3
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor2
printn "."
INC j
JMP InicioFor2
FinFor2:
MOV AX, i
PUSH AX
POP AX
; Asignacion k
MOV k, AX
INC i
JMP InicioFor1
FinFor1:
; For: 3
