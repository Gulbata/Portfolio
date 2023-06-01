package test;
import schedule.Schedule;


import org.junit.jupiter.api.*;
import org.junit.jupiter.params.*;
import org.junit.jupiter.params.provider.*;
import static org.junit.jupiter.api.Assertions.*;


import java.io.FileNotFoundException;

class SchoolTest {
    //I used an array to use @ParameterizedTest.Otherwise I could not.
    //array of 8, bacause I wanted to make is simple and associate respective positions to classes 4,5,6,7.

    Schedule[] schedule = new Schedule[8];

    @BeforeEach
    public void beforeEach() throws FileNotFoundException {
        schedule[4] = new Schedule("schedule4.txt");
        schedule[5] = new Schedule("schedule5.txt");
        schedule[6] = new Schedule("schedule6.txt");
        schedule[7] = new Schedule("schedule7.txt");

    }

    @ParameterizedTest(name = "number of classes per day for {0} th class")
    @CsvSource({"4,4","5,5","6,6","7,7"})
    public void classesPerDay(int expected,int forClass) throws FileNotFoundException{
        assertEquals(expected,schedule[forClass].getClassesPerDay());
    }

    @ParameterizedTest(name = "Right subject type for {1}th grade" )
    @CsvSource({"HUMANITY,4,1,2","HUMANITY,5,3,4","SCIENCE,6,5,6","HUMANITY,7,1,1"})
    public void scheduledClassType(String subjType,int forClass,int weekDay,int classNum){
        assertEquals(subjType,schedule[forClass].get(weekDay,classNum).getSubjectTypes().toString());
        }
    

    @ParameterizedTest(name = "If {0} th schedule is suitable for {1} th grade student" )
    @CsvSource({"4,4","5,5","6,7","7,7"})
    public void suitable(int forClass, int year) {
        assertTrue(schedule[forClass].isSuitableForYear(year));
    }

    @ParameterizedTest(name = "that {0} th schedule is not suitable for {1} th grade student" )
    @CsvSource({"5,4","6,5","7,6"})
    public void notSuitable(int forClass, int year) {
        assertFalse(schedule[forClass].isSuitableForYear(year));
    }

    @ParameterizedTest(name = "Checking thrown exception")
    @CsvSource({"4"})
    public void invalidName(int forClass){
        assertThrows(IllegalArgumentException.class, 
        () ->{schedule[forClass] = new Schedule("abcdefg.txt");});
    }

  
    @ParameterizedTest (name = "Checking thrown exception")
    @CsvSource({"4"})
    public void shortContent(int forClass) {
        assertThrows(IllegalStateException.class, 
        () ->{schedule[forClass] = new Schedule("scheduleWithShortContent.txt");});
    }

}