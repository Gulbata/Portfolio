package test.flying;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import travel.Destination;
import travel.flying.Plane;

import static org.junit.jupiter.api.Assertions.*;

class PlaneTest {
    Plane plane1;
    Plane plane2;
    Plane plane3;

    @BeforeEach
    void setUp ( ) {
        plane1 = new Plane ( "flyBus",89,100 );
        plane2 = new Plane ( "flyBus",89,100 );
        plane3 = new Plane ( "flyLines",45,78 );
    }

    @Test
    void testEstimatedArrivalTimeNoArgument ( ) {
        assertEquals ( 7,plane1.estimatedArrivalTime());
        assertEquals ( 7,plane3.estimatedArrivalTime());
    }

    @Test
    void testEstimatedArrivalTimeWithArgument ( ) {
        assertEquals ( 7,plane1.estimatedArrivalTime(Destination.PARIS));
        assertEquals ( 8,plane1.estimatedArrivalTime( Destination.HELSINKI));
    }

    @Test
    void testEquals ( ) {
        assertEquals ( true,plane1.equals ( plane2 ) );
        assertEquals ( false,plane1.equals ( plane3 ) );
    }

    @Test
    void testHashCode ( ) {
        assertEquals ( true,plane1.hashCode () == plane2.hashCode () );
        assertEquals ( false,plane1.hashCode () == plane3.hashCode () );
    }
}