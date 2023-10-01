#include<stdio.h>
#include<math.h>
#include<iostream>

char  a;
int   b,i;
float c;

void main() // Funcion principal
{
    c = 257.6;
    printf("C = ",c);
    a = (char)(c);
    printf("\na = ",a);

    //a = (char)((char)(c) + (float)(b));
/*
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

            for (i=0; i<3; i++)
            {
                printf("\nHola ", i);
            }

        }
    }
    else
    {
        printf("\nmundo\n");
    }
*/
}
