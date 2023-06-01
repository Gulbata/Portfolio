package travel.flying;

import travel.Destination;

public interface Flyable {
    int estimatedArrivalTime( Destination destination);

    double getPrice(double discountRate);

}
