package travel.flying;

import travel.DateAndTime;
import travel.Destination;

import static travel.Destination.ROME;
import static travel.DestinationUtils.getDestinationDuration;

public class Flight {
    protected String name;
    protected Destination destinationCity;
    protected int numberOfTravelers;
    protected DateAndTime flightDateAndTime;

    public Flight( String name, Destination destinationCity, int numberOfTravelers, DateAndTime flightDateAndTime ) {
        try {
            if ( numberOfTravelers >= 15 && numberOfTravelers <= 100 ) {
                this.numberOfTravelers = numberOfTravelers;
            } else {
                throw new IllegalArgumentException ( );
            }
        } catch ( IllegalArgumentException e ) {
            e.getMessage ();
        }
        this.name = name;
        this.destinationCity = destinationCity;
        this.flightDateAndTime = flightDateAndTime;
    }

    public Flight( ) {
        this ("AirBus", ROME, 83, new DateAndTime ( ));
    }


    public String getName( ) {
        return name;
    }

    public Destination getDestinationCity( ) {
        return destinationCity;
    }

    public int getNumberOfTravelers( ) {
        return numberOfTravelers;
    }

    public String getFlightDateAndTime( ) {
        return flightDateAndTime.toString ( );
    }

    public String getFlightDuration(Destination destinationCity){
        return getDestinationDuration(destinationCity);
    }


    @Override
    public String toString( ) {
        return "Flying " + name + " with " + numberOfTravelers + " passengers to " + destinationCity + "on " + flightDateAndTime ;
    }
}
