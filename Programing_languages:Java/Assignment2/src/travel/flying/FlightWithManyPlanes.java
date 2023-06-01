package travel.flying;

import travel.DateAndTime;
import travel.Destination;

import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Scanner;

public class FlightWithManyPlanes extends Flight{

    ArrayList<Plane> planes = new ArrayList<> ();

    public FlightWithManyPlanes ( String name , Destination destinationCity , int numberOfTravelers , DateAndTime flightDateAndTime , ArrayList<Plane> planes ) {
        super ( name , destinationCity , numberOfTravelers , flightDateAndTime );
        this.planes = planes;
    }

    public void save(String filename)throws IOException{

        try(PrintWriter writer = new PrintWriter(new File(filename + ".txt")))
            {
                writer.println (name);
                writer.println (destinationCity.name () );
                writer.println (numberOfTravelers );
                writer.println (flightDateAndTime );
                for (Plane a:this.planes){
                    writer.println ( a );
                }
        }

    }

    public void load(String filename)throws IOException{
        try (Scanner scanner = new Scanner(new File("test.txt"))) {
            String nam = scanner.nextLine();
            Destination destinationCit= Destination.valueOf ( scanner.nextLine() );
            int numberOfTraveler= Integer.parseInt ( scanner.nextLine() );
            scanner.nextLine();
            this.planes = new ArrayList<> ();
            while (scanner.hasNext()) {
                this.planes.add ( Plane.make ( scanner.nextLine() ) );

            }
        }

    }

    public int getCheapestRide(double discountRateIncrease)throws IllegalStateException{
        if (planes.size ()==0 ){throw new IllegalStateException (  );}

        double  minPrice = planes.get ( 0 ).getTicketPrice ();
        int minIndex = 0;
        int indexCounter = 0;

        for (Plane a :planes){
            if (a.getTicketPrice () * (discountRateIncrease * minIndex) < minPrice) {
                minPrice =  a.getTicketPrice () * (discountRateIncrease * minIndex);
                minIndex = indexCounter;

            }
            indexCounter ++;
        }
        return minIndex;
    }

}
