package travel;

public class DestinationUtils {

    public static Destination getDestination(String arg1){
        switch (arg1){
            case "01:34":
                return Destination.BERLIN;
            case "01:35":
                return Destination.ROME;
            case "02:05":
                return Destination.AMSTERDAM;
            case "02:20":
                return Destination.PARIS;
            case "02:43":
                return Destination.HELSINKI;
            default:
                return null;
        }
    }
    public static String getDestinationDuration(Destination dest){
        switch (dest){
            case HELSINKI:
                return "02:43";
            case PARIS:
                return "02:20";
            case AMSTERDAM:
                return "02:05";
            case ROME:
                return "01:45";
            case BERLIN:
                return "01:34";
            default:
                return null;
        }
    }

    public static int getRoundedHour ( Destination dest ){
        String str = getDestinationDuration(dest);
        String[] elements = str.split(":", 2);
        int hour = Integer.parseInt(elements[0]);
        int min = Integer.parseInt(elements[1]);

        if (min >= 30){
            return hour + 1;
        }else {return   hour;}
    }

}
