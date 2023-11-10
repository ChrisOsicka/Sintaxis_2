#include<stdio.h>
#include<math.h>
#include<iostream>



int altura,i,j,k;

void main() // Funcion principal
{
    i = 300;
    k = 1 + (char)(i);
    //k = 1.5;
    //k = ((char)(1.5));
    
    /*
    printf("Ingrese un numero: ");
    scanf("%d", &k);
    */

   /*
    printf("El valor de de k es: ", k);
    printf("\nIntrduzca un valor para j: ");
    scanf("%d", &j);
    printf("El valor de de j es: ", j);
    */

    //k = (5+5) - (10-4);
    //printf("\nPrograma finalizado");

/*
    for(i=0;i<10;i++) 
    {
        printf("\nHola\n");

        for(j=0;j<3;j++) 
        {
            printf(".");
        }

        k=i;
    }

    for (i = 1; i <= altura; i++)
    {
        for (j = 250; j < 250+i; j++)
        {
            if (j%2==0)
                printf("-");
            else
                printf("+");
        }
        printf("\n");
    }
*/


/*
    printf("\nAltura: ");
    scanf("&i",&altura);

    printf("\nfor:\n");
    for (i = 1; i <= altura; i++)
    {
        for (j = 250; j < 250+i; j++)
        {
            if (j%2==0)
                printf("-");
            else
                printf("+");
        }
        printf("\n");
    }
   
    printf("\nwhile:\n");
    i = 1;
    while (i <= altura)
    {
        j = 250;
        while (j < 250+i)
        {
            if (j%2==0)
                printf("-");
            else
                printf("+");
            j++;
        }
        i++;
        printf("\n");
    }

    printf("\ndo:\n");
    i = 1;
    do
    {
        j = 250;
        do
        {
            if (j%2==0)
                printf("-");
            else
                printf("+");
            j++;
        } while (j < 250+i);
        i++;
        printf("\n");
    } while (i <= altura);
    
    */

}