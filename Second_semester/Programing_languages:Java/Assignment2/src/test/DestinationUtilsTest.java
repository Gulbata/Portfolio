package test;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import travel.Destination;
import travel.DestinationUtils;

import static org.junit.jupiter.api.Assertions.assertEquals;

class DestinationUtilsTest {


    @Test
    void getDestination ( ) {
        assertEquals ( Destination.BERLIN, DestinationUtils.getDestination ( "01:34"));
    }

    @Test
    void getDestinationDuration ( ) {
        assertEquals ( "02:05", DestinationUtils.getDestinationDuration ( Destination.AMSTERDAM));
    }

    @Test
    void getRoundedHour ( ) {
        assertEquals ( 2, DestinationUtils.getRoundedHour ( Destination.ROME));
    }
}