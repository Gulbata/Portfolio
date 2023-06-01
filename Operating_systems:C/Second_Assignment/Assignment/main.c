#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <signal.h>
#include <sys/types.h>
#include <unistd.h>
#include <sys/wait.h>
#include <sys/msg.h>

#define PEOPLE_NEEDED_EACH_DAY 10
#define MAX_LINE_LENGTH 1000
#define MAX_APPLICANTS 20
#define READ 0
#define WRITE 1

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
  printf("         1 : Print all applicants \n");
  printf("         2 : Add new applicant \n");
  printf("         3 : Remove the applicant \n");
  printf("         4 : Modify the days for the applicants \n");
  printf("         5 : Update file \n");
}

int settingupday(app *applicants, app *applicantsforDay, char **applicantsforDay1, char **argv, int *weekdaysTametable, int *counter, char **dayOfWeek)
{

  cleanApplicants(applicants);
  cleanApplicants(applicantsforDay);

  readfile(*argv, weekdaysTametable, applicants, counter);

  int counting = 0;

  for (int i = 0; i < *counter; i++)
  {
    if (applicants[i].day[dayToIndex(*dayOfWeek)] == 1)
    {
      applicantsforDay[counting].index = counting;

      strcpy(applicantsforDay[counting].name, applicants[i].name);
      counting++;
    }
  }

  for (int i = 0; i < counting; i++)
  {
    printf("%s\n", applicantsforDay[i].name);
  }

  return counting;
}

void applicantsinfo(app *applicants, char **argv, int *weekdaysTametable, int *counter)
{

  int choice = -1;

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
      printApplicants(applicants, *counter);
      break;
    case 2:

      enterNewApplicant(weekdaysTametable, applicants, counter);
      break;
    case 3:

      printf("Choose the applicant you want to modify by index : ");
      int a;
      fflush(stdin);
      scanf("%d", &a);
      fflush(stdin);
      delete (a, weekdaysTametable, applicants, counter);
      break;
    case 4:

      printf("Choose the applicant you want to modify by index : ");
      int b;
      fflush(stdin);
      scanf("%d", &b);
      fflush(stdin);
      override(b, weekdaysTametable, applicants);
      break;
    case 5:

      updatefile(argv[1], applicants, *counter);
      break;

    default:
      printf("Wrong input! Entering integes from 0 to 5");
      break;
    }

  } while (choice != 0);
}

void sigusr_handler(int signum, siginfo_t *siginfo, void *context)
{

  if (signum == SIGUSR1)
  {
    printf("Bus 1 is ready to work \n");
  }
  else if (signum == SIGUSR2)
  {
    printf("Bus 2 is ready to work \n");
  }
}

struct messg
{
  long mtype; // this is a free value e.g for the address of the message
  int number; // this is the message itself
  int child;
};

// sendig a message
int send(int mqueue, int message, int childname)
{
  const struct messg m = {5, message, childname};
  int status = msgsnd(mqueue, &m, sizeof(struct messg) - sizeof(long), 0);

  if (status < 0)
  {
    perror("msgsnd error");
  }

  return 0;
}

// receiving a message.
int receive(int mqueue)
{
  struct messg m;
  int status = msgrcv(mqueue, &m, sizeof(struct messg) - sizeof(long), 5, 0);
  printf("%d workers have been brought to vineyeard by child%d \n", m.number, m.child);

  if (status < 0)
  {
    perror("msgrcv error");
  }

  return 0;
}

int main(int argc, char **argv)
{ // Checking argument count
  if (argc < 2)
  {
    printf("number of arguments shall be 2, first argument: file name  and second argument: week day(as integer)\n");
    return 0;
  }
  //_____________________________________________________________________________
  //                                              registering handler for signals
  struct sigaction sigact;
  sigact.sa_flags = SA_SIGINFO;
  sigact.sa_sigaction = sigusr_handler;
  sigemptyset(&sigact.sa_mask);
  sigact.sa_flags = 0;
  sigaction(SIGUSR1, &sigact, NULL);
  sigaction(SIGUSR2, &sigact, NULL);
  //_____________________________________________________________________________

  //                                creating pipes for parent and child processes
  int pipe_fd1[2];
  int pipe_fd2[2];

  if (pipe(pipe_fd1) == -1 || pipe(pipe_fd2) == -1)
  {
    perror("pipe");
    exit(1);
  }
  //_____________________________________________________________________________

  //                                             initialization for message queue
  // msgget needs a key, created by  ftok
  key_t key;
  key = ftok(argv[0], 1);
  int messg = msgget(key, 0600 | IPC_CREAT);

  if (messg < 0)
  {
    perror("msgget error");
    return 1;
  }

  //_____________________________________________________________________________

  //                                                                   First fork
  pid_t child = fork();
  if (child < 0)
  {
    perror("The fork calling was not succesful\n");
    exit(1);
  }
  //_____________________________________________________________________________

  if (child > 0)
  {
    //                                                                  Second fork
    pid_t child2 = fork();
    if (child2 < 0)
    {
      perror("The fork calling was not succesful\n");
      exit(1);
    }
    //_____________________________________________________________________________

    if (child2 > 0) //                                                                   Parent process
    {
      //                                                        close pipes READ side
      close(pipe_fd1[READ]);
      close(pipe_fd2[READ]);
      //_____________________________________________________________________________

      //                                                          waiting for signals
      sigset_t sigset;
      sigfillset(&sigset);
      sigdelset(&sigset, SIGUSR1);
      sigdelset(&sigset, SIGUSR2);
      sigsuspend(&sigset);
      sigsuspend(&sigset);
      //_____________________________________________________________________________

      //                                                           containers of info
      int counter = 0;
      int num;

      int weekdaysTametable[7] = {0, 0, 0, 0, 0, 0, 0};

      app applicants[MAX_APPLICANTS];
      app applicantsforDay[PEOPLE_NEEDED_EACH_DAY];
      char *applicantsforDay1[PEOPLE_NEEDED_EACH_DAY];

      //_____________________________________________________________________________

      //                                                               setting up day
      num = settingupday(applicants, applicantsforDay, applicantsforDay1, &argv[1], weekdaysTametable, &counter, &argv[2]);
      //_____________________________________________________________________________

      //                                            sending data to children with pipe
      for (int i = 0; i < num; i++)
      {
        if (i < 5)
        {
          int c;
          c = strlen(applicantsforDay[i].name) + 1;
          write(pipe_fd1[WRITE], &c, sizeof(c));
          write(pipe_fd1[WRITE], applicantsforDay[i].name, c);
        }
        else
        {
          int c;
          c = strlen(applicantsforDay[i].name) + 1;
          write(pipe_fd2[WRITE], &c, sizeof(c));
          write(pipe_fd2[WRITE], applicantsforDay[i].name, c);
        }
      }

      printf("Parent process finished sending the data\n");
      //_____________________________________________________________________________

      //                                                      closing pipes write side
      close(pipe_fd1[WRITE]);
      close(pipe_fd2[WRITE]);

      printf("Parent process closed the pipes\n");
      //_____________________________________________________________________________
      //                                                        receiving the message
      receive(messg);
      receive(messg);

      //_____________________________________________________________________________

      //                                    waiting for children to finihsh execution
      int status;
      waitpid(child, &status, 0);
      waitpid(child2, &status, 0);
      //_____________________________________________________________________________

      //                                                                modifing data
      applicantsinfo(applicants, argv, weekdaysTametable, &counter);

      //_____________________________________________________________________________ End Parent process

      printf("Parent process ended\n");
      return 0;
    }
    else //                                                                           Child2 process
    {
      //                                                     closing pipes Write side
      close(pipe_fd1[WRITE]);
      close(pipe_fd2[WRITE]);
      //_____________________________________________________________________________

      //                                                 Sending the signal to parent
      sleep(4);
      printf("Child2 wait 4 seconds, and sent a SIGUSR2 signal \n");
      kill(getppid(), SIGUSR2);
      printf("Child2 sent SIGUSR2 signal \n");
      //_____________________________________________________________________________

      //                                              receiving the message with pipe
      char sz[100];
      int l;
      int count = 0;
      while (read(pipe_fd2[READ], &l, sizeof(l)))
      {
        read(pipe_fd2[READ], sz, l);
        printf("printed from child 2: %s \n", sz);
        count++;
      }
      //_____________________________________________________________________________

      //                                                       closing pipe read side
      close(pipe_fd1[READ]);
      close(pipe_fd2[READ]);
      //_____________________________________________________________________________
      //                                                            sending a message

      send(messg, count, 2);
      sleep(2);
      // After terminating child process, the message queue is deleted.
      int status;
      status = msgctl(messg, IPC_RMID, NULL);
      if (status < 0)
        perror("msgctl error");
      //_____________________________________________________________________________
      //_____________________________________________________________________________ End Child2 process
      printf("child 2 finished printing:  \n");
      return 0;
    }
  }
  else //                                                                            Child1 process
  {
    //                                                     closing pipes Write side
    close(pipe_fd1[WRITE]);
    close(pipe_fd2[WRITE]);
    //_____________________________________________________________________________

    //                                                 Sending the signal to parent
    sleep(2);
    printf("Child1 waited 2 seconds, and sent a SIGUSR2 signal \n");
    kill(getppid(), SIGUSR1);
    printf("Child1 sent SIGUSR1 signal \n");
    //_____________________________________________________________________________

    //                                              receiving the message with pipe
    char sz[100];
    int l;
    int count = 0;
    while (read(pipe_fd1[READ], &l, sizeof(l)))
    {
      read(pipe_fd1[READ], sz, l);
      printf("printed from child 1:%s \n", sz);
      count++;
    }
    //_____________________________________________________________________________

    //                                                       closing pipe read side
    close(pipe_fd1[READ]);
    close(pipe_fd2[READ]);
    //                                                            sending a message
    send(messg, count, 1);
    //_____________________________________________________________________________
    //_____________________________________________________________________________ End child process
    printf("child 1 finished printing:  \n");
    return 0;
  }

  return 0;
}
