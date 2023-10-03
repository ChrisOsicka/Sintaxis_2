#include<stdio.h>
#include<math.h>
#include<iostream>

char  a;
int   b,i;
float c;

void main() // Funcion principal
{
    c = 259.5;
    printf("C = ",c);
    a = (char)(c);
    printf("\na = ",a);

    a = (char)((char)(c) + (float)(b));

    if (1 == 1)
    {
        printf("\nHola");
        if (1==2)
        {
            printf(" a todos");
        }
        else
        {
            printf(" a nadie\n");

            for (i=0; i<5; i++)
            {
                printf("\nHola ", i);
                printf("\n");
            }
            printf("\n");

        }
    }
    else
    {
        printf("\nmundo\n");
    }
 
    i=0;


    while( i<5 )
    {
        printf("\nAdios\n");
        i++;
    }



}
