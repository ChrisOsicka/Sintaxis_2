Autor: Guillermo Fernandez Romero
Fecha: 3-Mayo-2023 15:09
include 'emu8086.inc'
org 100h
MOV AX, 65666
PUSH AX
POP AX
MOV BX, 65536
DIV BX
PUSH DX
POP AX
; Asignacion k
MOV k, AX
printn "El valor de de k es: "
print "130"
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
