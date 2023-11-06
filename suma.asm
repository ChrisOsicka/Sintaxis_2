Autor: Guillermo Fernandez Romero
Fecha: 3-Mayo-2023 15:09
include 'emu8086.inc'
org 100h
MOV AX, 5
PUSH AX
MOV AX, 5
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
MOV AX, 10
PUSH AX
MOV AX, 4
PUSH AX
POP BX
POP AX
SUB AX, BX
PUSH AX
POP BX
POP AX
SUB AX, BX
PUSH AX
POP AX
; Asignacion k
MOV k, AX
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
