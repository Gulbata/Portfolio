package travel.flying;

import travel.DateAndTime;
import travel.Destination;
import travel.DestinationUtils;

import java.util.Objects;

public class Plane extends Flight implements Flyable {


    private String name;//super class also has same field.
    private int id;
    private int ticketPrice;



    //from assignment description it was not clear which super constructor we had to use, so I created constructor for both.
    public Plane ( String name , Destination destinationCity , int numberOfTravelers , DateAndTime flightDateAndTime , String name1 , int id , int ticketPrice ) {
        super ( name , destinationCity , numberOfTravelers , flightDateAndTime );
        try {
            if ( name1 == null || ticketPrice < 10 ) {
                throw new IllegalArgumentException ( );
            } else {
                this.name = name1;
                this.ticketPrice = ticketPrice;
            }
        } catch ( IllegalArgumentException e ) {
            e.getMessage ();
        }
        this.id = id;
    }

    public Plane ( String name1 , int id , int ticketPrice ) {
        try {
            if ( name1 == null || ticketPrice < 10 ) {
                throw new IllegalArgumentException ( );
            } else {
                this.name = name1;
                this.ticketPrice = ticketPrice;
            }
        } catch ( IllegalArgumentException e ) {
            e.getMessage ();
        }
        this.id = id;
    }

    @Override
    public String getName( ) {
        return name;
    }

    public int getId( ) {
        return id;
    }

    public int getTicketPrice( ) {
        return ticketPrice;
    }

    @Override
    public int estimatedArrivalTime( Destination destination ) {
        int startHour = 0;
        if (this.flightDateAndTime.getMinute () < 30){
            startHour = this.flightDateAndTime.getHour ( );
        }else {
            startHour = this.flightDateAndTime.getHour ( ) + 1;
        }

        int duration = DestinationUtils.getRoundedHour(destination);

        return startHour + duration;
    }
    //I would rather make estimatedArrivalTime method without parameter. @overloading  of method.
    public int estimatedArrivalTime() {
        int startHour = 0;
        if (this.flightDateAndTime.getMinute () < 30){
            startHour = this.flightDateAndTime.getHour ( );
        }else {
            startHour = this.flightDateAndTime.getHour ( ) + 1;
        }
        //I would rather make this
        int duration = DestinationUtils.getRoundedHour(destinationCity);

        return startHour + duration;
    }

    @Override
    public double getPrice( double discountRate ) {
        return ticketPrice * (1.0 - discountRate);
    }

    static Plane make(String data){
        String[] elements = data.split(",", 3);
        String name = elements[0];
        int id = Integer.parseInt(elements[1]);
        int ticketPrice = Integer.parseInt(elements[2]);

        return new Plane (name,id,ticketPrice);
    }



    @Override
    public boolean equals ( Object other ) {
        if ( this == other ) return true;
        if(other instanceof Plane that){
            return this.id == that.id && this.ticketPrice == that.ticketPrice && Objects.equals ( this.name , that.name );
        }
        return false;
    }

    @Override
    public int hashCode ( ) {
        return Objects.hash ( name , id , ticketPrice );
    }

    @Override
    public String toString ( ) {
        return ""+ name + "," + id + "," + ticketPrice;
    }
}
