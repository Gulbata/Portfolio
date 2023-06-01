#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#define PEOPLE_NEEDED_EACH_DAY 10
#define MAX_LINE_LENGTH 1000
#define MAX_APPLICANTS 20

typedef struct applicant
{
  int index;
  char name[20];
  bool day[7];
} app;

char *indexToDay(int i)
{
  switch (i)
  {
  case 0:
    return "Monday";
    break;

  case 1:
    return "Tuesday";
    break;

  case 2:
    return "Wednesday";
    break;

  case 3:
    return "Thursday";
    break;

  case 4:
    return "Friday";
    break;

  case 5:
    return "Saturday";
    break;

  default:
    return "Sunday";
    break;
  }
}

int dayToIndex(char *string)
{

  if (strcmp(string, "Monday") == 0)
  {
    return 0;
  }
  else if (strcmp(string, "Tuesday") == 0)
  {
    return 1;
  }
  else if (
      strcmp(string, "Wednesday") == 0)
  {
    return 2;
  }
  else if (strcmp(string, "Thursday") == 0)
  {
    return 3;
  }
  else if (strcmp(string, "Friday") == 0)
  {
    return 4;
  }
  else if (strcmp(string, "Saturday") == 0)
  {
    return 5;
  }
  else if (strcmp(string, "Sunday") == 0)
  {
    return 6;
  }
  else
  {
    return -1;
  }
}

bool dayexists(char *string)
{

  if (strcmp(string, "Monday") == 0 ||
      strcmp(string, "Tuesday") == 0 ||
      strcmp(string, "Wednesday") == 0 ||
      strcmp(string, "Thursday") == 0 ||
      strcmp(string, "Friday") == 0 ||
      strcmp(string, "Saturday") == 0 ||
      strcmp(string, "Sunday") == 0)
  {
    return true;
  }

  return false;
}

void cleanApplicantsWorkdays(app *applicant)
{
  for (int j = 0; j < 7; j++)
  {
    applicant->day[j] = 0;
  }
}

void cleanApplicants(app *applicants)
{
  for (int i = 0; i < MAX_APPLICANTS; i++)
  {
    cleanApplicantsWorkdays(&applicants[i]);
  }
}

int updateDays(int *weekdaysTametable, char *day, app *applicant)
{

  if (!dayexists(day))
  {
    printf("Format of day was wrong!\n");
    return 0;
  }

  if (weekdaysTametable[dayToIndex(day)] >= PEOPLE_NEEDED_EACH_DAY)
  {
    printf("We do not need more workers on \"%s\". Try later!\n", day);
    return 0;
  }

  printf("%s was added \n", day);
  weekdaysTametable[dayToIndex(day)]++;
  applicant->day[dayToIndex(day)] = 1;
  return 1;
}

void readfile(char *filen, int *weekdaysTametable, app *applicants, int *counter)
{
  FILE *file;

  file = fopen(filen, "r");

  if (file == NULL)
  {
    printf("reading file was not succesfull \n");
  }

  while (!feof(file) && !ferror(file))
  {
    if (*counter < MAX_APPLICANTS)
    {
      char line[MAX_LINE_LENGTH];

      fgets(line, MAX_LINE_LENGTH, file);
      line[strcspn(line, "\r\n")] = 0;

      atoi(strtok(line, " "));

      char *name = strtok(NULL, " ");

      char *day = strtok(NULL, " ");

      int scounter = 0;
      if (day != NULL)
      {
        do
        {
          int a = updateDays(weekdaysTametable, day, &applicants[*counter]);
          scounter = scounter + a;

          day = strtok(NULL, " ");

        } while (day != NULL);
      }

      if (scounter > 0)
      {
        applicants[*counter].index = *counter;
        strcpy(applicants[*counter].name, name);
        *counter = *counter + 1;
      }
    }
  }
  fclose(file);
}

void updatefile(char *filen, app *applicants, int counter)
{
  FILE *file;

  file = fopen(filen, "w");

  for (int i = 0; i < counter; i++)
  {
    char *string = (char *)malloc(1000 * sizeof(char));
    strcpy(string, "");

    char buffer[20] = {0};
    sprintf(buffer, "%i", applicants[i].index);

    strcat(string, buffer);
    strcat(string, " ");
    strcat(string, applicants[i].name);

    for (unsigned long j = 0; j < 7; j++)
    {

      if (applicants[i].day[j] == 1)
      {
        strcat(string, " ");
        strcat(string, indexToDay(j));
      }
    }
    strcat(string, "\n");
    fprintf(file, "%s", string);
  }
  fclose(file);
}

void enterNewApplicant(int *weekdaysTametable, app *applicants, int *counter)
{

  if (*counter < MAX_APPLICANTS)
  {

    char line[MAX_LINE_LENGTH];
    printf("Please Enter new applicants name and Tametable:\n");
    fflush(stdin);
    fgets(line, MAX_LINE_LENGTH, stdin);
    fgets(line, MAX_LINE_LENGTH, stdin);
    fflush(stdin);
    line[strcspn(line, "\r\n")] = 0;

    char *name = strtok(line, " ");

    char *day = strtok(NULL, " ");

    int scounter = 0;
    if (day != NULL)
    {
      do
      {

        int a = updateDays(weekdaysTametable, day, &applicants[*counter]);
        scounter = scounter + a;

        day = strtok(NULL, " ");
      } while (day != NULL);
    }

    //

    if (scounter > 0)
    {
      applicants[*counter].index = *counter;
      strcpy(applicants[*counter].name, name);
      *counter = *counter + 1;
    }
  }
  else
  {
    printf("maximum number of applicants %i, has been reached!\n", MAX_APPLICANTS);
    return;
  }
}

void printApplicants(app *applicants, int counter)
{
  for (int i = 0; i < counter; i++)
  {
    printf("Index: %i ", applicants[i].index);
    printf("%s ", applicants[i].name);
    for (int j = 0; j < 7; j++)
    {
      if (applicants[i].day[j] == 1)
      {
        printf("%s ", indexToDay(j));
      }
    }
    printf("\n ");
  }
}

void delete(int index, int *weekdaysTametable, app *applicants, int *counter)
{
  if (index == *counter - 1)
  {

    for (int i = 0; i < 7; i++)
    {
      if (applicants[index].day[i] == 1)
      {
        weekdaysTametable[i] = weekdaysTametable[i] - 1;
      }
    }
    cleanApplicantsWorkdays(&applicants[index]);
    *counter = *counter - 1;

    return;
  }

  if (index >= 0 && index < *counter - 1)
  {
    strcpy(applicants[index].name, applicants[*counter - 1].name);

    for (int i = 0; i < 7; i++)
    {
      if (applicants[index].day[i] == 1)
      {
        weekdaysTametable[i] = weekdaysTametable[i] - 1;
      }
    }
    cleanApplicantsWorkdays(&applicants[index]);

    for (int i = 0; i < 7; i++)
    {
      if (applicants[*counter - 1].day[i] == 1)
      {
        applicants[index].day[i] = 1;
        weekdaysTametable[i] = weekdaysTametable[i] + 1;
      }
    }
    cleanApplicantsWorkdays(&applicants[*counter - 1]);

    *counter = *counter - 1;
  }
  else
  {
    printf("Wrong index");
  }
}

void override(int index, int *weekdaysTametable, app *applicants)
{

  for (int i = 0; i < 7; i++)
  {
    if (applicants[index].day[i] == 1)
    {
      weekdaysTametable[i] = weekdaysTametable[i] - 1;
    }
  }

  cleanApplicantsWorkdays(&applicants[index]);

  int scounter = 0;
  do
  {
    char line[MAX_LINE_LENGTH];
    printf("Enter new Week days: ");
    fflush(stdin);

    fgets(line, MAX_LINE_LENGTH, stdin);
    fflush(stdin);
    line[strcspn(line, "\r\n")] = 0;

    char *day = strtok(line, " ");

    if (day != NULL)
    {
      do
      {

        int a = updateDays(weekdaysTametable, day, &applicants[index]);
        scounter = scounter + a;

        day = strtok(NULL, " ");
      } while (day != NULL);

    } // Some error here

    if (scounter == 0)
    {
      printf("Override was not succesfull! try again!\n");
    }
  } while (scounter == 0);
}

void callMenu()
{

  printf("Choose the opperation: \n");

  printf("         0 : Quit \n");
  printf("         1 : Input data from file \n");
  printf("         2 : Print all applicants \n");
  printf("         3 : Add new applicant \n");
  printf("         4 : Remove the applicant \n");
  printf("         5 : Modify the days for the applicants \n");
  printf("         6 : Update file \n");
}

int main(int argc, char **argv)
{

  int counter = 0;

  int weekdaysTametable[7] = {0, 0, 0, 0, 0, 0, 0};

  app applicants[MAX_APPLICANTS];

  cleanApplicants(applicants);

  int choice = -1;
  bool filewasread = false;
  do
  {
    fflush(stdin);
    fflush(stdin);
    callMenu();
    scanf("%d", &choice);
    fflush(stdin);
    switch (choice)
    {
    case 0:
      printf("Bye!");
      break;

    case 1:
      if (!filewasread)
      {
        readfile(argv[1], weekdaysTametable, applicants, &counter);
        filewasread = true;
      }
      else
      {
        printf("You can not read the file more than once!");
      }
      break;

    case 2:
      printApplicants(applicants, counter);
      break;
    case 3:

      enterNewApplicant(weekdaysTametable, applicants, &counter);
      break;
    case 4:

      printf("Choose the applicant you want to modify by index : ");
      int a;
      fflush(stdin);
      scanf("%d", &a);
      fflush(stdin);
      delete (a, weekdaysTametable, applicants, &counter);
      break;
    case 5:

      printf("Choose the applicant you want to modify by index : ");
      int b;
      fflush(stdin);
      scanf("%d", &b);
      fflush(stdin);
      override(b, weekdaysTametable, applicants);
      break;
    case 6:

      updatefile(argv[1], applicants, counter);
      break;

    default:
      printf("Wrong input! Entering integes from 0 to 5");
      break;
    }

  } while (choice != 0);

  return 0;
}
