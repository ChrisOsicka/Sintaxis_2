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

    scanf("%c", &a);
    printf("a = ", a);





    while(i<5)
    {
        printf("\nAdios\n");
        i++;
    }


}


/*
  k = (int)(10);
    k = 1.5;
    k = (int)((char)(1.5));

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

}
*/