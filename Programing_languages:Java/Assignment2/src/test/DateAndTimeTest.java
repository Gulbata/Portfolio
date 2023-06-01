package test;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import travel.DateAndTime;

import static org.junit.jupiter.api.Assertions.*;

class DateAndTimeTest {

    DateAndTime date1;
    DateAndTime date2;

    @BeforeEach
    void setUp ( ) {
        date1 = new DateAndTime ();
        date2 = new DateAndTime ( 2022,2,24,5,20 );
    }

    @Test
    void getTimeConstructionWithNoArguments ( ) {
        assertEquals ( "4:30",date1.getTime() );
    }

    @Test
    void getYearConstructionWithNoArguments ( ) {
        assertEquals ( 2021,date1.getYear () );
    }

    @Test
    void getMonthConstructionWithNoArguments ( ) {
        assertEquals ( 12,date1.getMonth () );
    }

    @Test
    void getDayConstructionWithNoArguments ( ) {
        assertEquals ( 22,date1.getDay () );
    }

    @Test
    void getHourConstructionWithNoArguments ( ) {
        assertEquals ( 4,date1.getHour () );
    }

    @Test
    void getMinuteConstructionWithNoArguments ( ) {
        assertEquals ( 30,date1.getMinute () );
    }

    @Test
    void getTimeConstructionWithArguments ( ) {
        assertEquals ( "5:20",date2.getTime() );
    }

    @Test
    void getYearConstructionWithArguments ( ) {
        assertEquals ( 2022,date2.getYear () );
    }

    @Test
    void getMonthConstructionWithArguments ( ) {
        assertEquals ( 2,date2.getMonth () );
    }

    @Test
    void getDayConstructionWithArguments ( ) {
        assertEquals ( 24,date2.getDay () );
    }

    @Test
    void getHourConstructionWithArguments ( ) {
        assertEquals ( 5,date2.getHour () );
    }

    @Test
    void getMinuteConstructionWithArguments ( ) {
        assertEquals ( 20,date2.getMinute () );
    }



}