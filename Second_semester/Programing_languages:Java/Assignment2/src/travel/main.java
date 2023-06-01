package travel;

import travel.flying.Flight;
import travel.flying.FlightWithManyPlanes;
import travel.flying.Plane;

import java.util.ArrayList;

public class main {
    public static void main(String[]args){
        FlightWithManyPlanes flight = new FlightWithManyPlanes ("bus",Destination.HELSINKI,76,new DateAndTime ( ), new ArrayList<>());
    }

}
