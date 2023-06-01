package schedule;

import subject.Subject;

import java.io.*;
import java.util.NoSuchElementException;
import java.util.Scanner;


public class Schedule {
    
    private  int numberOfClassesPerDay;
    private  Subject[][] schedule = new Subject[5][10];

    int numberOfSubjectLines = 0;


    public Schedule(String input) throws IllegalArgumentException,NoSuchElementException{
        File file;

       
        if (!(input.startsWith("schedule"))){
            throw new IllegalArgumentException("File name does not Start with \"schedule\"");
        }else {file = new File(input);}


        try (Scanner sc = new Scanner(file)) {
            this.numberOfClassesPerDay = Integer.parseInt(sc.nextLine());
            String buffer;
            for(int i = 0; i < 5; i++) {
                for (int j = 0; j < numberOfClassesPerDay & sc.hasNextLine();j++ ) {
                    try {
                        while (sc.hasNextLine() & (buffer = sc.nextLine()).isEmpty()) {
                        }
                    }catch (IllegalStateException g){
                        throw new IllegalStateException("there aren't enough lines to fill each day with classes,");
                    }
                    numberOfSubjectLines++;
                    schedule[i][j] = Subject.valueOf(buffer);

                }
            }
        }catch(FileNotFoundException e){
            System.out.println("File was not found");;
        }

        if (numberOfSubjectLines < numberOfClassesPerDay * 5) {
            throw new IllegalStateException();
        }
    }

    public int  getClassesPerDay(){
        return numberOfClassesPerDay;
    }

    public Subject get(int dayN,int classN){

        if(dayN >5 || dayN  <1 || classN >numberOfClassesPerDay || classN <1) {
            throw new IllegalArgumentException("arguments for get method are not correct");
        }else{
            return (schedule[dayN-1][classN-1]);}
    }

    public boolean isSuitableForYear(int grade){
        if (grade <4 || grade >7){
            throw new IllegalArgumentException("grade shall be between 4(inclusive) and 7(inclusive)");
        }
        boolean answer = true;
        int i= 0;
        while(answer && i < 5){
            for(int j=0;j<numberOfClassesPerDay;j++){
                if (schedule[i][j].getStartingYear() > grade){
                    answer = false;
                }
            }
            i++;
        }

        return answer;
    }

}



