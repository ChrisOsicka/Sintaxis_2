using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

/*
    Requerimiento 1: Programar scanf 
    Requerimiento 2: Programar printf
    Requerimiento 3: Programar ++,--,+=,-=,*=,/=,%=
    Requerimiento 4: Programar else
    Requerimiento 5: Programar do para que genere una sola vez el codigo
    Requerimiento 6: Programar while para que genere una sola vez el codigo
    Requerimiento 7: Programar el for para que gerenre una sola vez el codigo
    Requerimiento 8: Programar el CAST   
*/

namespace Sintaxis_2
{
    public class Lenguaje : Sintaxis
    {
        List<Variable> lista;
        Stack<float> stack;
        int contIf, contElse, contFor, contDo, contWhile;

        Variable.TiposDatos tipoDatoExpresion;
        public Lenguaje()
        {
            lista = new List<Variable>();
            stack = new Stack<float>();
            tipoDatoExpresion = Variable.TiposDatos.Char;
            contIf = contFor = 1;
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            lista = new List<Variable>();
            stack = new Stack<float>();
            tipoDatoExpresion = Variable.TiposDatos.Char;
            contIf = contFor = 1;
        }

        //Programa  -> Librerias? Variables? Main
        public void Programa()
        {
            asm.WriteLine("include 'emu8086.inc'");
            asm.WriteLine("org 100h");
            if (getContenido() == "#")
            {
                Librerias();
            }
            if (getClasificacion() == Tipos.TipoDato)
            {
                Variables();
            }
            Main(true);
            asm.WriteLine("int 20h");
            asm.WriteLine("RET");
            asm.WriteLine("define_scan_num");
            asm.WriteLine("define_print_num");
            asm.WriteLine("define_print_num_uns");
            Imprime();
            asm.WriteLine("END");
        }

        private void Imprime()
        {
            log.WriteLine("-----------------");
            log.WriteLine("V a r i a b l e s");
            log.WriteLine("-----------------");
            asm.WriteLine("; V a r i a b l e s");
            foreach (Variable v in lista)
            {
                log.WriteLine(v.getNombre() + " " + v.getTipoDato() + " = " + v.getValor());
                asm.WriteLine(v.getNombre() + " dw 0h");
            }
            log.WriteLine("-----------------");
        }

        private bool Existe(string nombre)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    return true;
                }
            }
            return false;
        }
        private void Modifica(string nombre, float nuevoValor)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    v.setValor(nuevoValor);
                }
            }
        }
        private float getValor(string nombre)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    return v.getValor();
                }
            }
            return 0;
        }
        private Variable.TiposDatos getTipo(string nombre)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    return v.getTipoDato();
                }
            }
            return Variable.TiposDatos.Char;
        }
        private Variable.TiposDatos getTipo(float resultado)
        {
            if (resultado % 1 != 0)
            {
                return Variable.TiposDatos.Float;
            }
            else if (resultado < 256)
            {
                return Variable.TiposDatos.Char;
            }
            else if (resultado < 65536)
            {
                return Variable.TiposDatos.Int;
            }
            return Variable.TiposDatos.Float;
        }
        // Libreria -> #include<Identificador(.h)?>
        private void Libreria()
        {
            match("#");
            match("include");
            match("<");
            match(Tipos.Identificador);
            if (getContenido() == ".")
            {
                match(".");
                match("h");
            }
            match(">");
        }
        //Librerias -> Libreria Librerias?
        private void Librerias()
        {
            Libreria();
            if (getContenido() == "#")
            {
                Librerias();
            }
        }
        //Variables -> tipo_dato ListaIdentificadores; Variables?
        private void Variables()
        {
            Variable.TiposDatos tipo = Variable.TiposDatos.Char;
            switch (getContenido())
            {
                case "int": tipo = Variable.TiposDatos.Int; break;
                case "float": tipo = Variable.TiposDatos.Float; break;
            }
            match(Tipos.TipoDato);
            ListaIdentificadores(tipo);
            match(";");
            if (getClasificacion() == Tipos.TipoDato)
            {
                Variables();
            }
        }
        //ListaIdentificadores -> identificador (,ListaIdentificadores)?
        private void ListaIdentificadores(Variable.TiposDatos tipo)
        {
            if (!Existe(getContenido()))
            {
                lista.Add(new Variable(getContenido(), tipo));
            }
            else
            {
                Console.WriteLine("\n");
                throw new Error("de sintaxis, la variable <" + getContenido() + "> está duplicada", log, linea, columna);
            }
            match(Tipos.Identificador);
            if (getContenido() == ",")
            {
                match(",");
                ListaIdentificadores(tipo);
            }
        }
        //BloqueInstrucciones -> { ListaInstrucciones ? }
        private void BloqueInstrucciones(bool ejecuta, bool primeraVez)
        {
            match("{");
            if (getContenido() != "}")
            {
                ListaInstrucciones(ejecuta, primeraVez);
            }
            match("}");
        }

        //ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void ListaInstrucciones(bool ejecuta, bool primeraVez)
        {
            Instruccion(ejecuta, primeraVez);
            if (getContenido() != "}")
            {
                ListaInstrucciones(ejecuta, primeraVez);
            }
        }
        //Instruccion -> Printf | Scanf | If | While | Do | For | Asignacion
        private void Instruccion(bool ejecuta, bool primeraVez)
        {
            if (getContenido() == "printf")
            {
                Printf(ejecuta, primeraVez);
            }
            else if (getContenido() == "scanf")
            {
                Scanf(ejecuta);
            }
            else if (getContenido() == "if")
            {
                If(ejecuta, primeraVez);
            }
            else if (getContenido() == "while")
            {
                While(ejecuta,primeraVez);
            }
            else if (getContenido() == "do")
            {
                Do(ejecuta,primeraVez);
            }
            else if (getContenido() == "for")
            {
                For(ejecuta,primeraVez);
            }
            else
            {
                Asignacion(ejecuta, primeraVez);
            }
        }
        //Asignacion -> identificador = Expresion;
        private void Asignacion(bool ejecuta, bool primeraVez)
        {
            float resultado = 0;
            tipoDatoExpresion = Variable.TiposDatos.Char;
            if (!Existe(getContenido()))
            {
                Console.WriteLine("\n");
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            log.Write(getContenido() + " = ");
            string variable = getContenido();
            match(Tipos.Identificador);
            if (getContenido() == "=")
            {
                match("=");
                Expresion(primeraVez);
                resultado = stack.Pop();
                if(primeraVez)
                {
                    asm.WriteLine("POP AX");
                    asm.WriteLine("; Asignacion "+variable);
                    asm.WriteLine("MOV "+variable+", AX");
                }
                
            }
            else if (getClasificacion() == Tipos.IncrementoTermino)
            {
                if (getContenido() == "++")
                {
                    match("++");
                    // INC
                    resultado = getValor(variable) + 1;
                    if(primeraVez)
                    {
                        asm.WriteLine("INC " + variable);
                    }
                }
                else
                {
                    match("--");
                    // DEC
                    resultado = getValor(variable) - 1;
                    if(primeraVez)
                    {
                        asm.WriteLine("DEC " + variable);
                    }
                }
            }
            else if (getClasificacion() == Tipos.IncrementoFactor)
            {
                resultado = getValor(variable);
                if (getContenido() == "+=")
                {
                    match("+=");
                    Expresion(primeraVez);
                    resultado += stack.Pop();
                    if(primeraVez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX, " + variable);
                        asm.WriteLine("ADD BX, AX");
                        asm.WriteLine("MOV " + variable + ", BX");
                    }
                }
                else if (getContenido() == "-=")
                {
                    match("-=");
                    Expresion(primeraVez);
                    resultado -= stack.Pop();
                    if(primeraVez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX, " + variable);
                        asm.WriteLine("SUB BX, AX");
                        asm.WriteLine("MOV " + variable + ", BX");
                    }
                }
                else if (getContenido() == "*=")
                {
                    match("*=");
                    Expresion(primeraVez);
                    resultado *= stack.Pop();
                    if(primeraVez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX, " + variable);
                        asm.WriteLine("MUL BX");
                        asm.WriteLine("MOV " + variable + ", AX");
                    }
                }
                else if (getContenido() == "/=")
                {
                    match("/=");
                    Expresion(primeraVez);
                    resultado /= stack.Pop();
                    if(primeraVez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX, " + variable);
                        asm.WriteLine("DIV BX");
                        asm.WriteLine("MOV " + variable + ", AX");
                    }
                }
                else if (getContenido() == "%=")
                {
                    match("%=");
                    Expresion(primeraVez);
                    resultado %= stack.Pop();
                    if(primeraVez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX, " + variable);
                        asm.WriteLine("DIV BX");
                        asm.WriteLine("MOV AX, DX");
                        asm.WriteLine("MOV " + variable + ", AX");
                    }
                }
            }
            log.WriteLine(" = " + resultado);
            if (ejecuta)
            {
                Variable.TiposDatos tipoDatoVariable = getTipo(variable);
                Variable.TiposDatos tipoDatoResultado = getTipo(resultado);

                // Console.WriteLine(variable + " = "+tipoDatoVariable);
                // Console.WriteLine(resultado + " = "+tipoDatoResultado);
                // Console.WriteLine("expresion = "+tipoDatoExpresion);

                if (tipoDatoExpresion > tipoDatoResultado)
                {
                    tipoDatoResultado = tipoDatoExpresion;
                }
                if (tipoDatoVariable >= tipoDatoResultado)
                {
                    Modifica(variable, resultado);
                }
                else
                {
                    Console.WriteLine("\n");
                    throw new Error("de semantica, no se puede asignar un <" + tipoDatoResultado + "> a un <" + tipoDatoVariable + ">", log, linea, columna);
                }
            }
            primeraVez = false;
            match(";");
        }

        //While -> while(Condicion) BloqueInstrucciones | Instruccion
        private void While(bool ejecuta, bool primeraVez)
        {
            string etiquetaInicio = "InicioWhile" + contWhile;

            if(primeraVez)
            {
                asm.WriteLine("; WHILE: " + contWhile);
                asm.WriteLine(etiquetaInicio + ":");
            }

            string etiquetaFin = "FinWhile" + contWhile++;
            int inicia = caracter;
            int lineaInicio = linea;
            float resultado = 0;
            log.WriteLine("while: ");

            do
            {
                match("while");
                match("(");
                ejecuta = Condicion(etiquetaFin, primeraVez) && ejecuta;    
                string variable = getContenido();
                match(")");
                
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta, primeraVez);
                }
                else
                {
                    Instruccion(ejecuta, primeraVez);
                }
                if (ejecuta)
                {
                    Modifica(variable, resultado);
                    archivo.DiscardBufferedData();
                    caracter = inicia - 6;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
                if (primeraVez)
                {
                    asm.WriteLine("JMP " + etiquetaInicio);
                    asm.WriteLine(etiquetaFin + ":");
                }
                primeraVez = false;
            }
            while (ejecuta);
        }

        //Do -> do BloqueInstrucciones | Instruccion while(Condicion)
        private void Do(bool ejecuta, bool primeraVez)
        {
            string etiquetaInicio = "InicioDo" + contDo;

            if(primeraVez)
            {
                asm.WriteLine("; DO: " + contDo);
                asm.WriteLine(etiquetaInicio + ":");
            }

            string etiquetaFin = "FinDo" + contDo++;
            int inicia = caracter;
            int lineaInicio = linea;
            float resultado = 0;
            log.WriteLine("do: ");
            
            do
            {
                match("do");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta, primeraVez);
                }
                else
                {
                    Instruccion(ejecuta, primeraVez);
                }

                match("while");
                match("(");
                ejecuta = Condicion(etiquetaFin, primeraVez) && ejecuta;    
                string variable = getContenido();
                match(")");
                match(";");

                if (ejecuta)
                {
                    Modifica(variable, resultado);
                    archivo.DiscardBufferedData();
                    caracter = inicia - 5;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
                if (primeraVez)
                {
                    asm.WriteLine("JMP " + etiquetaInicio);
                    asm.WriteLine(etiquetaFin + ":");
                }
                primeraVez = false;
            }
            while(ejecuta);
        }

        //For -> for(Asignacion Condicion; Incremento) BloqueInstrucciones | Instruccion
        private void For(bool ejecuta, bool primeraVez)
        {
            
            match("for");
            match("(");
            Asignacion(ejecuta, primeraVez);
            string etiquetaInicio = "InicioFor" + contFor;

            if(primeraVez)
            {
                asm.WriteLine("; FOR: " + contFor);
                asm.WriteLine(etiquetaInicio + ":");
            }

            string etiquetaFin = "FinFor" + contFor++;
            int inicia = caracter;
            int lineaInicio = linea;
            float resultado = 0;
            string variable = getContenido();
            log.WriteLine("for: " + variable);
            
            do
            {
                ejecuta = Condicion(etiquetaFin, primeraVez) && ejecuta;
                match(";");
                resultado = Incremento(ejecuta, primeraVez);
                match(")");
                
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta,primeraVez);
                }
                else
                {
                    Instruccion(ejecuta, primeraVez);
                }

                if (getValor(variable) < resultado)
                {
                    if (primeraVez)
                    {
                        asm.WriteLine("INC " + variable);
                    }
                }
                else if (getValor(variable) > resultado)
                {
                    if (primeraVez)
                    {
                        asm.WriteLine("DEC " + variable);
                    }
                }
                if (ejecuta)
                {
                    Variable.TiposDatos tipoDatoVariable = getTipo(variable);
                    Variable.TiposDatos tipoDatoResultado = getTipo(resultado);
                    if (tipoDatoVariable >= tipoDatoResultado)
                    {
                        Modifica(variable, resultado);
                        archivo.DiscardBufferedData();
                        caracter = inicia - variable.Length - 1;
                        archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                        nextToken();
                        linea = lineaInicio;
                    }
                    else
                    {
                        Console.WriteLine("\n");
                        throw new Error("de semantica, no se puede asignar un <" + tipoDatoResultado + "> a un <" + tipoDatoVariable + ">", log, linea, columna);
                    }
                }
                if (primeraVez)
                {
                    asm.WriteLine("JMP " + etiquetaInicio);
                    asm.WriteLine(etiquetaFin + ":");
                }
                primeraVez = false;
            }
            while (ejecuta);
        }

        //Incremento -> Identificador ++ | --
        private float Incremento(bool ejecuta, bool primeraVez)
        {
            if (!Existe(getContenido()))
            {
                Console.WriteLine("\n");
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            string variable = getContenido();
            match(Tipos.Identificador);
            if (getContenido() == "++")
            {
                match("++");
                return getValor(variable) + 1;
            }
            else
            {
                match("--");
                return getValor(variable) - 1;
            }
        }
        //Condicion -> Expresion OperadorRelacional Expresion
        private bool Condicion(string etiquetaIF, bool primeraVez)
        {
            Expresion(primeraVez);
            string operador = getContenido();
            match(Tipos.OperadorRelacional);
            Expresion(primeraVez);
            float R1 = stack.Pop();  // Expresion 2
            float R2 = stack.Pop();  // Expresion 1

            if (primeraVez)
            {
                asm.WriteLine("POP BX"); // Expresion 2
                asm.WriteLine("POP AX"); // Expresion 1
                asm.WriteLine("CMP AX, BX");
            }
            switch (operador)
            {
                case "==":
                    if (primeraVez) asm.WriteLine("JNE "+etiquetaIF);
                    return R2 == R1;
                case ">": 
                    if (primeraVez) asm.WriteLine("JBE "+etiquetaIF);
                    return R2 > R1;
                case ">=": 
                    if (primeraVez) asm.WriteLine("JB "+etiquetaIF);
                    return R2 >= R1;
                case "<": 
                    if (primeraVez) asm.WriteLine("JAE "+etiquetaIF);
                    return R2 < R1;
                case "<=": 
                    if (primeraVez) asm.WriteLine("JA "+etiquetaIF);
                    return R2 <= R1;
                default: 
                    if (primeraVez) asm.WriteLine("JE "+etiquetaIF);
                    return R2 != R1;
            }
        }
        //If -> if (Condicion) BloqueInstrucciones | Instruccion (else BloqueInstrucciones | Instruccion)?
        private void If(bool ejecuta, bool primeraVez)
        {
            match("if");
            match("(");

            if (primeraVez)
            {
                asm.WriteLine("; if: " + contIf);
            }

            string etiqueta = "Eif" + contIf;

            if (primeraVez)
            {
                contIf++;
                contElse++;
            }
            string etiquetaE = "Eelse" + contElse;
            bool evaluacion = Condicion(etiqueta, primeraVez);

            match(")");
            if (getContenido() == "{")
            {
                BloqueInstrucciones(evaluacion && ejecuta, primeraVez);

            }
            else
            {
                Instruccion(evaluacion && ejecuta, primeraVez);

            }
            if (getContenido() == "else")
            {
                match("else");
                if (primeraVez)
                {
                    asm.WriteLine("; else: " + contElse);
                    asm.WriteLine("JMP " + etiquetaE);
                    asm.WriteLine(etiqueta + ":");
                }
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(!evaluacion && ejecuta, primeraVez);
                }
                else
                {
                    Instruccion(!evaluacion && ejecuta, primeraVez);
                }
                if (primeraVez)
                {
                    asm.WriteLine(etiquetaE + ":");
                }
                /*if(primeraVez)
                {
                    contElse++;
                }*/

            }
            primeraVez = false;

        }
        //Printf -> printf(cadena(,Identificador)?);
        private void Printf(bool ejecuta, bool primeraVez)
        {
            match("printf");
            match("(");

            if (ejecuta)
            {
                string cadena = getContenido().TrimStart('"');
                cadena = cadena.Remove(cadena.Length - 1);
                string cadena2 = cadena;
                cadena = cadena.Replace(@"\n", "\n");
                cadena2 = cadena2.Replace(@"\n", "");
                Console.Write(cadena);
                if(primeraVez)
                {
                    asm.WriteLine("printn " +'"' + '"');
                    asm.Write("printn ");
                    asm.WriteLine('"' + cadena2 + '"');
                }
            }
            match(Tipos.Cadena);
            if (getContenido() == ",")
            {
                match(",");
                if (!Existe(getContenido()))
                {
                    Console.WriteLine("\n");
                    throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
                }
                if (ejecuta)
                {
                    Console.Write(getValor(getContenido()));
                    if(primeraVez)
                    {
                        asm.WriteLine("MOV AX, " + getContenido());
                        asm.WriteLine("call print_num");
                        asm.WriteLine("XOR AX, AX");
                    }
                    
                }
                match(Tipos.Identificador);
            }
            match(")");
            match(";");
            primeraVez = false;
        }
        //Scanf -> scanf(cadena,&Identificador);
        private void Scanf(bool ejecuta)
        {
            match("scanf");
            match("(");
            match(Tipos.Cadena);
            match(",");
            match("&");
            if (!Existe(getContenido()))
            {
                Console.WriteLine("\n");
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            string variable = getContenido();
            match(Tipos.Identificador);
            if (ejecuta)
            {
                string captura = "" + Console.ReadLine();
                float resultado = float.Parse(captura);
                Modifica(variable, resultado);
                asm.WriteLine("call scan_num");
                asm.WriteLine("MOV " + variable + ", CX");
            }
            match(")");
            match(";");
        }
        //Main -> void main() BloqueInstrucciones
        private void Main(bool ejecuta)
        {
            match("void");
            match("main");
            match("(");
            match(")");
            BloqueInstrucciones(ejecuta, true);
        }
        //Expresion -> Termino MasTermino
        private void Expresion(bool primeraVez)
        {
            Termino(primeraVez);
            MasTermino(primeraVez);
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino(bool primeraVez)
        {
            if (getClasificacion() == Tipos.OperadorTermino)
            {
                string operador = getContenido();
                match(Tipos.OperadorTermino);
                Termino(primeraVez);
                log.Write(" " + operador);
                float R2 = stack.Pop();
                float R1 = stack.Pop();
                if (primeraVez)
                {
                    asm.WriteLine("POP BX");
                    asm.WriteLine("POP AX");
                }
                if (operador == "+")
                {
                    stack.Push(R1 + R2);
                    if (primeraVez)
                    {
                        asm.WriteLine("ADD AX, BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
                else
                {
                    stack.Push(R1 - R2);
                    if (primeraVez)
                    {
                        asm.WriteLine("SUB AX, BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
            }
            primeraVez = false;
        }
        //Termino -> Factor PorFactor
        private void Termino(bool primeraVez)
        {
            Factor(primeraVez);
            PorFactor(primeraVez);
        }
        //PorFactor -> (OperadorFactor Factor)?
        private void PorFactor(bool primeraVez)
        {
            if (getClasificacion() == Tipos.OperadorFactor)
            {
                string operador = getContenido();
                match(Tipos.OperadorFactor);
                Factor(primeraVez);
                log.Write(" " + operador);
                float R2 = stack.Pop();
                float R1 = stack.Pop();
                if (primeraVez)
                {
                    asm.WriteLine("POP BX");
                    asm.WriteLine("POP AX");
                }
                if (operador == "*")
                {
                    stack.Push(R1 * R2);
                    if (primeraVez)
                    {
                        asm.WriteLine("MUL  BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
                else if (operador == "/")
                {
                    stack.Push(R1 / R2);
                    if (primeraVez)
                    {
                        asm.WriteLine("DIV  BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
                else
                {
                    stack.Push(R1 % R2);
                    if (primeraVez)
                    {
                        asm.WriteLine("DIV  BX");
                        asm.WriteLine("PUSH DX");
                    }
                }
            }
            primeraVez = false;
        }
        //Factor -> numero | identificador | (Expresion)
        private void Factor(bool primeraVez)
        {
            if (getClasificacion() == Tipos.Numero)
            {
                log.Write(" " + getContenido());
                if (primeraVez)
                {
                    asm.WriteLine("MOV AX, "+getContenido());
                    asm.WriteLine("PUSH AX");
                }
                stack.Push(float.Parse(getContenido()));
                if (tipoDatoExpresion < getTipo(float.Parse(getContenido())))
                {
                    tipoDatoExpresion = getTipo(float.Parse(getContenido()));
                }
                match(Tipos.Numero);
            }
            else if (getClasificacion() == Tipos.Identificador)
            {
                if (!Existe(getContenido()))
                {
                    Console.WriteLine("\n");
                    throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
                }
                if (primeraVez)
                {
                    asm.WriteLine("MOV AX, "+getContenido());
                    asm.WriteLine("PUSH AX");
                }
                stack.Push(getValor(getContenido()));
                match(Tipos.Identificador);
                if (tipoDatoExpresion < getTipo(getContenido()))
                {
                    tipoDatoExpresion = getTipo(getContenido());
                }
            }
            else
            {
                bool huboCast = false;
                Variable.TiposDatos tipoDatoCast = Variable.TiposDatos.Char;
                match("(");
                if (getClasificacion() == Tipos.TipoDato)
                {
                    huboCast = true;
                    switch (getContenido())
                    {
                        case "int": tipoDatoCast = Variable.TiposDatos.Int; break;
                        case "float": tipoDatoCast = Variable.TiposDatos.Float; break;
                    }
                    match(Tipos.TipoDato);
                    match(")");
                    match("(");
                }
                Expresion(primeraVez);
                match(")");
                if (huboCast)
                {
                    tipoDatoExpresion = tipoDatoCast;
                    stack.Push(castea(stack.Pop(), tipoDatoCast));
                    /*
                    if (primeraVez)
                    {
                        asm.WriteLine("POP AX");
                    }
                    */
                }
            }
            primeraVez = false;
        }
        float castea(float resultado, Variable.TiposDatos tipoDato)
        {
            switch (tipoDato)
            {
                case Variable.TiposDatos.Char:
                asm.WriteLine("POP AX");
                asm.WriteLine("MOV BX, 256");
                asm.WriteLine("DIV BX");
                asm.WriteLine("PUSH DX");
                asm.WriteLine("XOR DX, DX");
                return MathF.Round(resultado) % 256;

                case Variable.TiposDatos.Int :
                return MathF.Round(resultado) % 65536;
            }
            return resultado;
        }
    }
}